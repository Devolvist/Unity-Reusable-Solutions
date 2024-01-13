using System;
using System.Collections;
using Devolvist.UnityReusableSolutions.SaveLoad;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Devolvist.UnityReusableSolutions.PlayModeTests
{
    [Category("SaveLoad")]
    public class SaveLoadTests
    {
        //[Test]
        //public void SaveLoadTestsSimplePasses()
        //{           
        //}

        /// <summary>
        /// Возврат false при запросе на проверку наличия сохранений при реальном отсутствии сохранений.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Is_False_When_Unavailable_Saves()
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            bool savedDataAvailable = SaveLoadService.Instance.CheckForSavedData();

            Assert.IsFalse(savedDataAvailable);
        }

        /// <summary>
        /// Возврат true при запросе на проверку наличия сохранений при реальном наличии сохранений.
        /// </summary>
        [UnityTest]
        public IEnumerator Check_Saves_Is_True_When_Available_Saves()
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SaveLoadService>();

            while (SaveLoadService.Instance == null)
                yield return null;

            ISavable savableClient = new MockSavableClient();
            savableClient.Save();
            bool savedDataAvailable = SaveLoadService.Instance.CheckForSavedData();

            Assert.IsTrue(savedDataAvailable);

            savableClient.DeleteSaves();
        }

        #region Mocks
        /// <summary>
        /// Имитация объекта с сохраняемыми данными.
        /// </summary>
        private class MockSavableClient : ISavable
        {
            public const string DEFAULT_MESSAGE = "Hello";

            public string Message { get; private set; } 

            public void DeleteSaves()
            {
                SaveLoadService.Instance.DeleteSavedData("Mock_Save");
            }         

            public void Load()
            {
                Message = SaveLoadService.Instance.LoadData<string>("Mock_Save");
            }

            public void Save()
            {
                SaveLoadService.Instance.SaveData("Mock_Save", DEFAULT_MESSAGE);
            }
        }
        #endregion
    }
}