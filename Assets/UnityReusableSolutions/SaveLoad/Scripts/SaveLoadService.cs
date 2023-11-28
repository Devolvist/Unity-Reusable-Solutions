using Devolvist.UnityReusableSolutions.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    // Высокоуровневый сервис, предоставляющий единый интерфейс для работы с сохранениями.
    // Инкапсулирует логику работы с локальными сохранениями.

    public class SaveLoadService : MonoSingleton<SaveLoadService>
    {
        [SerializeField] private SaveLoadConfig _config;
        private DeviceReadWriteData _deviceReadWriteData;

        private static List<ISavable> _registeredSavableObjects;

        protected override void InitializeOnAwake()
        {
            _deviceReadWriteData = new BinaryDataReadWrite(_config.SavesDataFolderName);
        }

        /// <returns>
        /// True - данные есть.
        /// False - данных нет.
        /// </returns>
        public bool CheckForSavedData()
        {
            return _deviceReadWriteData.HasWrittenData();
        }

        public void SaveData<T>(string id, T data)
        {
            _deviceReadWriteData.WriteData(id, data);
        }

        public T LoadData<T>(string id)
        {
            return _deviceReadWriteData.ReadData<T>(id);
        }

        public bool DeleteSavedData(string id)
        {
            return _deviceReadWriteData.DeleteData(id);
        }

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

        // Есть подозрение что в сборке данные портятся при сохранении при выходе.
        //private void OnApplicationQuit()
        //{
        //    SaveRegisteredSavableObjects();
        //}
    }
}