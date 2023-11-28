using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    [CreateAssetMenu(menuName = "Reusable Solutions/SaveLoad/Config")]
    public class SaveLoadConfig : ScriptableObject
    {
        [Tooltip("Название папки с локальными сохранениями для теста при разработке.")]
        [SerializeField] private string _developmentSavesDataFolderName = "Test_Saves";

        [Tooltip("Название папки с локальными сохранениями в релизной версии.")]
        [SerializeField] private string _releaseSavesDataFolderName = "Release_Saves";

        /// <summary>
        /// Название папки с локальными сохранениями.
        /// </summary>
        public string SavesDataFolderName
        {
            get
            {
#if UNITY_EDITOR
                return _developmentSavesDataFolderName;
#endif
#pragma warning disable CS0162 // Обнаружен недостижимый код
                return _releaseSavesDataFolderName;
            }
        }
    }
}