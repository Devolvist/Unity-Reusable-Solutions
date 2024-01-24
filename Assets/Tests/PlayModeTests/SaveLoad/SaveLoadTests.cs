using System;
using System.Collections;
using Devolvist.UnityReusableSolutions.SaveLoad;
using Devolvist.UnityReusableSolutions.Singleton;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Devolvist.UnityReusableSolutions.PlayModeTests
{
    [Category("SaveLoad")]
    public class SaveLoadTests
    {
        /// <summary>
        /// ������� false ��� ������� �� �������� ������� ���������� ��� �������� ���������� ���������� ���� Binary.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Is_False_When_Unavailable_BinarySaves()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.Binary);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            SaveLoadService.DeleteLocalSaves();
            bool savedDataAvailable = SaveLoadService.Instance.CheckForSavedData();

            Assert.IsFalse(savedDataAvailable);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }     

        /// <summary>
        /// ������� false ��� ������� �� �������� ������� ���������� ��� �������� ���������� ���������� ���� PlayerPrefs.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Is_False_When_Unavailable_PlayerPrefsSaves()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.PlayerPrefs);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            SaveLoadService.DeleteLocalSaves();
            bool savedDataAvailable = SaveLoadService.Instance.CheckForSavedData();

            Assert.IsFalse(savedDataAvailable);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// ������� true ��� ������� �� �������� ������� ���������� ��� �������� ������� ���������� ���� Binary.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Saves_Is_True_When_Available_BinarySaves()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.Binary);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithStringProperty savableClient = new MockSavableClientWithStringProperty();
            savableClient.Save();
            savableClient.Load();
            bool savedDataLoadedSuccessfully = savableClient.Message == MockSavableClientWithStringProperty.EXPECTED_MESSAGE;

            Assert.IsTrue(savedDataLoadedSuccessfully);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// ������� true ��� ������� �� �������� ������� ���������� ��� �������� ������� ���������� ���� PlayerPrefs.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Saves_Is_True_When_Available_PlayerPrefsSaves()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.PlayerPrefs);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithStringProperty savableClient = new MockSavableClientWithStringProperty();
            savableClient.Save();
            savableClient.Load();
            bool savedDataLoadedSuccessfully = savableClient.Message == MockSavableClientWithStringProperty.EXPECTED_MESSAGE;

            Assert.IsTrue(savedDataLoadedSuccessfully);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// �������� �������� ����� ���������� ������ ����� Binary.
        /// </summary>
        [UnityTest]
        public IEnumerator Successfully_Load_Afer_Saving_String_In_Binary()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.Binary);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithGenericProperty<string> savableClient = new MockSavableClientWithGenericProperty<string>();
            savableClient.Property = "Hello";
            savableClient.Save();
            savableClient.Property = null;
            savableClient.Load();

            Assert.IsTrue(savableClient.Property == "Hello");
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// �������� �������� ����� ���������� ������ ����� PlayerPrefs.
        /// </summary>
        [UnityTest]
        public IEnumerator Successfully_Load_Afer_Saving_String_In_PlayerPrefs()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.PlayerPrefs);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithGenericProperty<string> savableClient = new MockSavableClientWithGenericProperty<string>();
            savableClient.Property = "Hello";
            savableClient.Save();
            savableClient.Property = null;
            savableClient.Load();

            Assert.IsTrue(savableClient.Property == "Hello");
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// �������� �������� ����� ����������� ������ ����� ����� Binary.
        /// </summary>
        [UnityTest]
        public IEnumerator Successfully_Load_Afer_Saving_Int_In_Binary()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.Binary);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithGenericProperty<int> savableClient = new MockSavableClientWithGenericProperty<int>();
            savableClient.Property = 7;
            savableClient.Save();
            savableClient.Property = 0;
            savableClient.Load();

            Assert.IsTrue(savableClient.Property == 7);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        /// <summary>
        /// �������� �������� ����� ����������� ������ ����� ����� PlayerPrefs.
        /// </summary>
        [UnityTest]
        public IEnumerator Successfully_Load_Afer_Saving_Int_In_PlayerPrefs()
        {
            DataReadWriteType lastReadWriteType = LocalSaveLoadConfig.DataReadWriteType;
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(DataReadWriteType.PlayerPrefs);

            MonoSingleton<SaveLoadService>.ResetInstance();
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            MockSavableClientWithGenericProperty<int> savableClient = new MockSavableClientWithGenericProperty<int>();
            savableClient.Property = 7;
            savableClient.Save();
            savableClient.Property = 0;
            savableClient.Load();

            Assert.IsTrue(savableClient.Property == 7);
            SaveLoadService.DeleteLocalSaves();
            LocalSaveLoadConfig.SetGlobalDataReadWriteType(lastReadWriteType);
        }

        #region Mocks
        /// <summary>
        /// �������� ������� � ������������ �������.
        /// </summary>
        private class MockSavableClientWithStringProperty : ISavable
        {
            private const string SAVE_ID = "Mock_Save";
            public const string EXPECTED_MESSAGE = "Hello";

            public string Message { get; private set; } = null;

            public void DeleteSaves()
            {
                SaveLoadService.Instance.DeleteSavedData(SAVE_ID);
            }

            public void Load()
            {
                Message = SaveLoadService.Instance.LoadData<string>(SAVE_ID);
            }

            public void Save()
            {
                SaveLoadService.Instance.SaveData(SAVE_ID, EXPECTED_MESSAGE);
            }

            public void ResetToDefault()
            {
                Message = null;
            }
        }

        private class MockSavableClientWithGenericProperty<T> : ISavable
        {
            private const string SAVE_ID = "Mock_Save_1";

            public T Property { get; set; } = default;

            public void DeleteSaves()
            {
                SaveLoadService.Instance.DeleteSavedData(SAVE_ID);
            }

            public void Load()
            {
                Property = SaveLoadService.Instance.LoadData<T>(SAVE_ID);
            }

            public void Save()
            {
                SaveLoadService.Instance.SaveData(SAVE_ID, Property);
            }

            public void ResetToDefault()
            {
               Property = default;
            }
        }
        #endregion
    }
}