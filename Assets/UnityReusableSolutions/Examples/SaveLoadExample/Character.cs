using Devolvist.UnityReusableSolutions.SaveLoad;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoadExample
{
    public class Character : MonoBehaviour, ISavable
    {
        public const int MIN_HEALTH = 10;
        public const int MAX_HEALTH = 100;

        private int _health = MAX_HEALTH;
        private readonly string _saveId = "ExampleSave_CharacterHealth";

        public int Health => _health;

        public void ReceiveDamage()
        {
            _health = Mathf.Clamp(_health - 10, MIN_HEALTH, MAX_HEALTH);
        }

        public void ReplenishHealth()
        {
            _health = Mathf.Clamp(_health + 10, MIN_HEALTH, MAX_HEALTH);
        }
    
        public void Load()
        {
            _health = SaveLoadService.Instance.LoadData<int>(_saveId);
        }

        public void Save()
        {
            SaveLoadService.Instance.SaveData(_saveId, _health);
        }

        public void DeleteSaves()
        {
            SaveLoadService.Instance.DeleteSavedData(_saveId);

            RestoreHealth();
        }

        private void RestoreHealth()
        {
            _health = MAX_HEALTH;
        }

        private void OnApplicationQuit()
        {
            DeleteSaves();
        }
    }
}