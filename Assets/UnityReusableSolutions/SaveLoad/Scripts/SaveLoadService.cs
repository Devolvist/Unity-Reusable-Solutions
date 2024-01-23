using Devolvist.UnityReusableSolutions.Singleton;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    // Высокоуровневый сервис, предоставляющий единый интерфейс для работы с сохранениями.
    // Инкапсулирует логику работы с локальными сохранениями.

    public class SaveLoadService : MonoSingleton<SaveLoadService>
    {
        private LocalDataReadWrite _localReadWriteData;

        protected override void InitializeOnAwake()
        {
            InitLocalDataHandler();
        }

        private void InitLocalDataHandler()
        {
            switch (LocalSaveLoadConfig.DataReadWriteType)
            {
                case DataReadWriteType.Binary:
                    _localReadWriteData = new BinaryDataReadWrite(LocalSaveLoadConfig.SavableDataFolderName);
                    break;
                case DataReadWriteType.Json:
                    _localReadWriteData = new LocalJsonDataReadWrite(LocalSaveLoadConfig.SavableDataFolderName);
                    break;
            }
        }

        /// <returns>
        /// True - данные есть.
        /// False - данных нет.
        /// </returns>
        public bool CheckForSavedData()
        {
            return _localReadWriteData.HasWrittenData();
        }

        public void SaveData<T>(string id, T data)
        {
            _localReadWriteData.WriteData(id, data);
        }

        public T LoadData<T>(string id)
        {
            return _localReadWriteData.ReadData<T>(id);
        }

        public bool DeleteSavedData(string id)
        {
            return _localReadWriteData.DeleteData(id);
        }

        #region STATIC_OPERATIONS_WITH_REGISTERED_CLIENTS
        /// <summary>
        /// Регистрация сохраняемого объекта для автоматического вызова методов его интерфейса.
        /// Повторная регистрация не засчитывается.
        /// </summary>
        public static void Register(ISavable savable)
        {
            RegisteredSavableObjectsStorage.Objects ??= new List<ISavable>();

            if (RegisteredSavableObjectsStorage.Objects.Contains(savable))
                return;

            RegisteredSavableObjectsStorage.Objects.Add(savable);
        }

        public static void SaveRegisteredSavableObjects()
        {
            if (RegisteredSavableObjectsStorage.Objects == null)
                return;

            if (RegisteredSavableObjectsStorage.Objects.Count == 0)
                return;

            foreach (ISavable savable in RegisteredSavableObjectsStorage.Objects)
            {
                savable.Save();
            }
        }

        public static void DeleteRegisteredSavableObjectsData()
        {
            if (RegisteredSavableObjectsStorage.Objects == null)
                return;

            if (RegisteredSavableObjectsStorage.Objects.Count == 0)
                return;

            foreach (ISavable savable in RegisteredSavableObjectsStorage.Objects)
            {
                savable.DeleteSaves();
            }

            ResetRegisteredSavableObjectsToDefault();
        }

        public static void ResetRegisteredSavableObjectsToDefault()
        {
            if (RegisteredSavableObjectsStorage.Objects == null)
                return;

            if (RegisteredSavableObjectsStorage.Objects.Count == 0)
                return;

            foreach (ISavable savable in RegisteredSavableObjectsStorage.Objects)
            {
                savable.ResetToDefault();
            }
        }
        #endregion

        #region EDITOR_MENU_ITEMS
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Local Saves/Open Folder")]
        private static void OpenLocalSavesFolder()
        {
            string savesFolderPath = $"{Application.persistentDataPath}/{LocalSaveLoadConfig.SavableDataFolderName}";

            if (!Directory.Exists(savesFolderPath))
            {
                UnityEngine.Debug.LogWarning($"Папка с локальными сохранениями отсутствует.\nЗапрошенный путь: {savesFolderPath}");
                return;
            }

            Process.Start($"{Application.persistentDataPath}/{LocalSaveLoadConfig.SavableDataFolderName}");
        }

        [MenuItem("Local Saves/Delete")]
        private static void DeleteLocalSaves()
        {
            string savedFilesFolderPath = $"{Application.persistentDataPath}/{LocalSaveLoadConfig.SavableDataFolderName}";

            if (!Directory.Exists(savedFilesFolderPath))
            {
                UnityEngine.Debug.LogWarning($"Папка с локальными сохранениями отсутствует.\nЗапрошенный путь: {savedFilesFolderPath}");
                return;
            }

            var files = Directory.GetFiles(savedFilesFolderPath);

            foreach (var file in files)
                File.Delete(file);

            Directory.Delete(savedFilesFolderPath);

            ResetRegisteredSavableObjectsToDefault();

            UnityEngine.Debug.Log("Локальные сохранения удалены.");
        }
#endif
        #endregion
    }
}