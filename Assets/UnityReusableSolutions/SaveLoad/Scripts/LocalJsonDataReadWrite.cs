using System.IO;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    public class LocalJsonDataReadWrite : LocalDataReadWrite
    {
        public LocalJsonDataReadWrite(string dataFolderName) : base(dataFolderName)
        {
            FileExtension = "json";
        }

        public override T ReadData<T>(string id)
        {
            string filePath = GetFullFilePath(id);
            T data = default;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(json);
            }
            else
            {
                Debug.LogWarning($"Файл не найден. Путь - {filePath}.");
            }

            return data;
        }

        public override void WriteData<T>(string id, T data)
        {
            string filePath = GetFullFilePath(id);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
        }
    }
}