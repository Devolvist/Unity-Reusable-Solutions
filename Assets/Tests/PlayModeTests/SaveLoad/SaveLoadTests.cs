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
        [UnityTest]
        [Description("������� false ��� ������� �� �������� ������� ���������� ��� �������� ���������� ���������� ���� Binary.")]
        public IEnumerator CheckIsFalseWhenUnavailableBinarySaves()
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

        [UnityTest]
        [Description("������� false ��� ������� �� �������� ������� ���������� ��� �������� ���������� ���������� ���� PlayerPrefs.")]
        public IEnumerator CheckIsFalseWhenUnavailablePlayerPrefsSaves()
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

        [UnityTest]
        [Description("������� true ��� ������� �� �������� ������� ���������� ��� �������� ������� ���������� ���� Binary.")]
        public IEnumerator CheckSavesIsTrueWhenAvailableBinarySaves()
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

        [UnityTest]
        [Description("������� true ��� ������� �� �������� ������� ���������� ��� �������� ������� ���������� ���� PlayerPrefs.")]
        public IEnumerator CheckSavesIsTrueWhenAvailablePlayerPrefsSaves()
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

        [UnityTest]
        [Description("�������� �������� ����� ���������� ������ ����� Binary.")]
        public IEnumerator SuccessfullyLoadingAferSavingStringInBinary()
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

        [UnityTest]
        [Description("�������� �������� ����� ���������� ������ ����� PlayerPrefs.")]
        public IEnumerator SuccessfullyLoadingAfterSavingStringInPlayerPrefs()
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

        [UnityTest]
        [Description("�������� �������� ����� ����������� ������ ����� ����� Binary.")]
        public IEnumerator SuccessfullyLoadingAferSavingIntInBinary()
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

        [UnityTest]
        [Description("�������� �������� ����� ����������� ������ ����� ����� PlayerPrefs.")]
        public IEnumerator SuccessfullyLoadingAferSavingIntInPlayerPrefs()
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