using Devolvist.UnityReusableSolutions.Internet;
using UnityEngine;
using UnityEngine.UI;

namespace Devolvist.UnityReusableSolutions.InternetConnectionCheckingExample
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _buttonText;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _button.onClick.RemoveAllListeners();

            _buttonText.text = "Проверка соединения...";

            InternetConnection.Instance.IsAvailable(PrintConnectionMessage);
        }

        private void PrintConnectionMessage(bool status)
        {
            if (status == true)
            {
                _buttonText.text = "Есть соединение";
                _buttonText.color = Color.green;
            }
            else
            {
                _buttonText.text = "Нет соединения";
                _buttonText.color = Color.red;
            }
        }
    }
}