using System.IO;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// ������-������ ������, �������� �� ���������� ������������.
    /// </summary>
    public abstract class DeviceReadWriteData : ReadWriteData
    {
        public DeviceReadWriteData(string dataFolderName)
        {
            dataFolderDirectoryPath = $"{Application.persistentDataPath}/{dataFolderName}";

            if (!Directory.Exists(dataFolderDirectoryPath))
                Directory.CreateDirectory(dataFolderDirectoryPath);
        }

        protected string fileExtension;

        /// <summary>
        /// ���� � ����� c ������� ������.
        /// </summary>
        protected readonly string dataFolderDirectoryPath;

        public override bool HasWrittenData()
        {
            // ��������� ������� ����� � ����������� �������.
            if (!Directory.Exists(dataFolderDirectoryPath))
                return false;

            // ��������� ������� ������ � �����.
            if (Directory.GetFiles(dataFolderDirectoryPath).Length == 0)
                return false;
            else
                return true;
        }

        public override bool DeleteData(string id)
        {
            var filePath = GetFullFilePath(id);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else
            {
                Debug.LogError($"������� ������� �������������� ����.\n����������� ���� - {filePath}.");
                return false;
            }
        }

        protected string GetFullFilePath(string fileName)
        {
            return $"{dataFolderDirectoryPath}/{fileName}{fileExtension}";
        }
    }
}