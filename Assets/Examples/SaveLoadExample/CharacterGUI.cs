using Devolvist.UnityReusableSolutions.SaveLoad;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace Devolvist.UnityReusableSolutions.SaveLoadExample
{
    public class CharacterGUI : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Text _healthText;
        [SerializeField] private Button _damageButton;
        [SerializeField] private Button _replenishHealthButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _deleteHealthSaveButton;
        [SerializeField] private Button _openSavesFolderButton;

        private void Start()
        {
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        public void OpenSavesFolder()
        {
            string saveFolderPath = $"{Application.persistentDataPath}/Saves_Test";

            if (!Directory.Exists(saveFolderPath))
            {
                return;
            }

            Process.Start(saveFolderPath);
        }

        public void OnHealthReplenished()
        {
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        public void OnDamageReceived()
        {          
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        public void OnHealthLoaded()
        {
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        public void OnHealthSaved()
        {
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        public void OnHealthSaveDeleted()
        {
            DisplayCurrentHealth();
            UpdateButtonsInteractivity();
        }

        private void DisplayCurrentHealth()
        {
            _healthText.text = $"Текущее здоровье: {_character.Health}";
        }

        private void UpdateButtonsInteractivity()
        {
            bool savesAvailable = SaveLoadService.Instance.CheckForSavedData();

            _loadButton.interactable = savesAvailable;
            _deleteHealthSaveButton.interactable = savesAvailable;
            _openSavesFolderButton.interactable = savesAvailable;
            _damageButton.interactable = _character.Health > Character.MIN_HEALTH;
            _replenishHealthButton.interactable = _character.Health < Character.MAX_HEALTH;
        }
    }
}