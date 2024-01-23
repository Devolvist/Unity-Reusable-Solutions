using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    [CreateAssetMenu(menuName = "Reusable Solutions/Save-Load/Local Save-Load Config")]
    public class LocalSaveLoadConfig : ScriptableObject
    {
        public const string DEFAULT_SAVES_FOLDER_NAME = "Saves";

        private static string _globalDevelopmentSavesFolderName = string.Empty;
        private static string _globalReleaseSavesFolderName = string.Empty;
        private static DataReadWriteType _globalDataReadWriteType = DataReadWriteType.Json;

        [Tooltip("�������� ����� � ���������� ������������ ��� ����� ��� ����������.")]
        [SerializeField, Delayed] private string _developmentSavesFolderName = $"{DEFAULT_SAVES_FOLDER_NAME}_Test";

        [Tooltip("�������� ����� � ���������� ������������ � �������� ������.")]
        [SerializeField, Delayed] private string _releaseSavesFolderName = DEFAULT_SAVES_FOLDER_NAME;

        [Tooltip("��� ��������� ��������� ����������� ������.")]
        [SerializeField] private DataReadWriteType _dataReadWriteType = DataReadWriteType.Json;

        /// <summary>
        /// �������� ����� � ���������� ������������.
        /// </summary>
        public static string SavableDataFolderName
        {
            get
            {
#if UNITY_EDITOR
                return _globalDevelopmentSavesFolderName != string.Empty ?
                    _globalDevelopmentSavesFolderName : $"{DEFAULT_SAVES_FOLDER_NAME}_Test";
#endif
#pragma warning disable CS0162 // ��������� ������������ ���
                return _globalReleaseSavesFolderName != string.Empty ?
                    _globalReleaseSavesFolderName : DEFAULT_SAVES_FOLDER_NAME;
            }
        }

        public static DataReadWriteType DataReadWriteType
        {
            get 
            {
                return _globalDataReadWriteType;
            }
        }

        private void Awake()
        {
            SyncGlobals();
        }

        private void OnValidate()
        {
            SyncGlobals();
        }

        private void SyncGlobals()
        {
            _globalDevelopmentSavesFolderName = _developmentSavesFolderName;
            _globalReleaseSavesFolderName = _releaseSavesFolderName;
            _globalDataReadWriteType = _dataReadWriteType;
        }
    }

    public enum DataReadWriteType
    {
        Binary,
        Json
    }
}