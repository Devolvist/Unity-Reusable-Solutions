using Devolvist.UnityReusableSolutions.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    // Высокоуровневый сервис, предоставляющий единый интерфейс для работы с сохранениями.
    // Инкапсулирует логику работы с локальными сохранениями.

    public class SaveLoadService : MonoSingleton<SaveLoadService>
    {
        [SerializeField] private LocalSaveLoadConfig _localSaveLoadConfig;

        private LocalDataReadWrite _localReadWriteData;

        private static List<ISavable> _registeredSavableObjects;

        protected override void InitializeOnAwake()
        {
            string localFolderName =
                _localSaveLoadConfig != null ?
                _localSaveLoadConfig.SavesDataFolderName :
                LocalSaveLoadConfig.DEFAULT_SAVES_FOLDER_NAME;

            _localReadWriteData = new BinaryDataReadWrite(localFolderName);
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

        #region Operations_with_registered_clients
        /// <summary>
        /// Регистрация сохраняемого объекта для автоматического вызова методов его интерфейса.
        /// Повторная регистрация не засчитывается.
        /// </summary>
        public static void Register(ISavable savable)
        {
            _registeredSavableObjects ??= new List<ISavable>();

            if (_registeredSavableObjects.Contains(savable))
                return;

            _registeredSavableObjects.Add(savable);
        }

        public void DeleteRegisteredSavableObjectsData()
        {
            if (_registeredSavableObjects == null)
                return;

            if (_registeredSavableObjects.Count == 0)
                return;

            foreach (ISavable savable in _registeredSavableObjects)
            {
                savable.DeleteSaves();
            }
        }

        public void SaveRegisteredSavableObjects()
        {
            if (_registeredSavableObjects == null)
                return;

            if (_registeredSavableObjects.Count == 0)
                return;

            foreach (ISavable savable in _registeredSavableObjects)
            {
                savable.Save();
            }
        }
        #endregion

        // Есть подозрение что в сборке данные портятся при сохранении при выходе.
        //private void OnApplicationQuit()
        //{
        //    SaveRegisteredSavableObjects();
        //}
    }
}