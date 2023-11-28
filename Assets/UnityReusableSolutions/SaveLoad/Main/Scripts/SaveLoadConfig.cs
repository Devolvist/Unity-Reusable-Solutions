using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    [CreateAssetMenu(menuName = "Reusable Solutions/SaveLoad/Config")]
    public class SaveLoadConfig : ScriptableObject
    {
        [Tooltip("�������� ����� � ���������� ������������ ��� ����� ��� ����������.")]
        [SerializeField] private string _developmentSavesDataFolderName = "Test_Saves";

        [Tooltip("�������� ����� � ���������� ������������ � �������� ������.")]
        [SerializeField] private string _releaseSavesDataFolderName = "Release_Saves";

        /// <summary>
        /// �������� ����� � ���������� ������������.
        /// </summary>
        public string SavesDataFolderName
        {
            get
            {
#if UNITY_EDITOR
                return _developmentSavesDataFolderName;
#endif
#pragma warning disable CS0162 // ��������� ������������ ���
                return _releaseSavesDataFolderName;
            }
        }
    }
}