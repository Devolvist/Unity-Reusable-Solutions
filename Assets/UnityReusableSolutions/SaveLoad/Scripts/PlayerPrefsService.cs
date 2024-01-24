using System;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    public class PlayerPrefsService : ReadWriteData
    {
        private readonly string _hasDataKey = "HasData";

        public override bool DeleteData(string id)
        {
            if (PlayerPrefs.HasKey(id))
            {
                PlayerPrefs.DeleteKey(id);
                return true;
            }

            return false;
        }

        public override bool HasWrittenData() => PlayerPrefs.HasKey(_hasDataKey);

        public override T ReadData<T>(string id)
        {
            T returnedData = default;

            if (!IsValidType(typeof(T)))
            {
                Debug.LogError($"Невозможно прочитать данные типа {typeof(T).Name} из PlayerPrefs");
                return returnedData;
            }

            if (!PlayerPrefs.HasKey(id))
            {
                Debug.LogError($"Невозможно прочитать данные типа {typeof(T).Name} из PlayerPrefs");
                return returnedData;
            }

            string loadedData = PlayerPrefs.GetString(id);
            Type returnedType = typeof(T);

            try
            {
                if (returnedType == typeof(string))
                {
                    returnedData = (T)(object)loadedData;
                }
                else if (returnedType == typeof(bool))
                {
                    returnedData = (T)(object)bool.Parse(loadedData);
                }
                else if (returnedType == typeof(int))
                {
                    returnedData = (T)(object)int.Parse(loadedData);
                }
                else if (returnedType == typeof(float))
                {
                    returnedData = (T)(object)float.Parse(loadedData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при преобразовании данных типа {typeof(T).Name}: {e.Message}");
            }

            return returnedData;
        }

        public override void WriteData<T>(string id, T data)
        {
            if (!IsValidType(typeof(T)))
            {
                Debug.LogError($"Невозможно сохранить данные типа {typeof(T).Name} в PlayerPrefs");
                return;
            }    
            
            string savableDataInString = Convert.ToString(data);
            PlayerPrefs.SetString(id, savableDataInString);

            PlayerPrefs.SetString(nameof(_hasDataKey), _hasDataKey);
        }

        private bool IsValidType(Type validatingType) => 
            validatingType == typeof(string) |
            validatingType == typeof(bool) | 
            validatingType == typeof(int) |
            validatingType == typeof(float);
    }
}