using System.IO;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// ������-������ ������ �� ���������� ������������.
    /// </summary>
    public abstract class LocalDataReadWrite : ReadWriteData
    {
        public LocalDataReadWrite(string dataFolderName)
        {
            DataFolderDirectoryPath = $"{Application.persistentDataPath}/{dataFolderName}";

            if (!Directory.Exists(DataFolderDirectoryPath))
                Directory.CreateDirectory(DataFolderDirectoryPath);
        }

        protected string FileExtension;

        /// <summary>
        /// ���� � ����� c ������� ������.
        /// </summary>
        protected readonly string DataFolderDirectoryPath;

        public override bool HasWrittenData()
        {
            // ��������� ������� ����� � ����������� �������.
            if (!Directory.Exists(DataFolderDirectoryPath))
                return false;

            // ��������� ������� ������ � �����.
            if (Directory.GetFiles(DataFolderDirectoryPath).Length == 0)
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
            return $"{DataFolderDirectoryPath}/{fileName}{FileExtension}";
        }
    }
}