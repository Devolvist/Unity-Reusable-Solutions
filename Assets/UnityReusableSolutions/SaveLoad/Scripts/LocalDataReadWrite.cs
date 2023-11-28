using System.IO;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// Чтение-запись данных на устройстве пользователя.
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
        /// Путь к папке c файлами данных.
        /// </summary>
        protected readonly string DataFolderDirectoryPath;

        public override bool HasWrittenData()
        {
            // Проверить наличие папки с записанными данными.
            if (!Directory.Exists(DataFolderDirectoryPath))
                return false;

            // Проверить наличие файлов в папке.
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
                Debug.LogError($"Попытка удалить несуществующий файл.\nЗапрошенный путь - {filePath}.");
                return false;
            }
        }

        protected string GetFullFilePath(string fileName)
        {
            return $"{DataFolderDirectoryPath}/{fileName}{FileExtension}";
        }
    }
}