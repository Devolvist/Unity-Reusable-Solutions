using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    [CreateAssetMenu(menuName = "Reusable Solutions/Save-Load/Local Save-Load Config")]
    public class LocalSaveLoadConfig : ScriptableObject
    {
        public const string DEFAULT_SAVES_FOLDER_NAME = "Saves";

        [Tooltip("Название папки с локальными сохранениями для теста при разработке.")]
        [SerializeField] private string _developmentSavesFolderName = $"{DEFAULT_SAVES_FOLDER_NAME}_Test";

        [Tooltip("Название папки с локальными сохранениями в релизной версии.")]
        [SerializeField] private string _releaseSavesFolderName = DEFAULT_SAVES_FOLDER_NAME;

        /// <summary>
        /// Название папки с локальными сохранениями.
        /// </summary>
        public string SavesDataFolderName
        {
            get
            {
#if UNITY_EDITOR
                return _developmentSavesFolderName != string.Empty ?
                    _developmentSavesFolderName : DEFAULT_SAVES_FOLDER_NAME;
#endif
#pragma warning disable CS0162 // Обнаружен недостижимый код
                return _releaseSavesFolderName != string.Empty ?
                    _releaseSavesFolderName : DEFAULT_SAVES_FOLDER_NAME;
            }
        }
    }
}