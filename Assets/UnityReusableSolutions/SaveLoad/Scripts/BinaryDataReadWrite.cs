using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    public class BinaryDataReadWrite : LocalDataReadWrite
    {
        private BinaryFormatter _formatter;

        public BinaryDataReadWrite(string dataFolderName) : base(dataFolderName)
        {
            FileExtension = ".save";
            _formatter = new BinaryFormatter();
        }

        public override void WriteData<T>(string id, T data)
        {
            FileStream fileStream = File.Create(GetFullFilePath(id));
            _formatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public override T ReadData<T>(string id)
        {
            var filePath = GetFullFilePath(id);

            T data = default;

            if (File.Exists(filePath))
            {
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                data = (T)_formatter.Deserialize(fileStream);
                fileStream.Close();
            }
            else
            {
                Debug.LogWarning($"Файл не найден. Путь - {filePath}.");
            }

            return data;
        }
    }
}