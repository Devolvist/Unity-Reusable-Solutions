using System.IO;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// Чтение-запись данных, хранимых на устройстве пользователя.
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
        /// Путь к папке c файлами данных.
        /// </summary>
        protected readonly string dataFolderDirectoryPath;

        public override bool HasWrittenData()
        {
            // Проверить наличие папки с записанными данными.
            if (!Directory.Exists(dataFolderDirectoryPath))
                return false;

            // Проверить наличие файлов в папке.
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
                Debug.LogError($"Попытка удалить несуществующий файл.\nЗапрошенный путь - {filePath}.");
                return false;
            }
        }

        protected string GetFullFilePath(string fileName)
        {
            return $"{dataFolderDirectoryPath}/{fileName}{fileExtension}";
        }
    }
}