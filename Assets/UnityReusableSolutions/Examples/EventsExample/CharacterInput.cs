using Devolvist.UnityReusableSolutions.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Devolvist.UnityReusableSolutions.EventsExample
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _jumpRequest;
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Text _jumpText;

        private KeyCode _jumpKey = KeyCode.E;

        private void OnEnable()
        {
            _jumpButton.onClick.AddListener(RequestJump);

            DisplayJumpText();
        }

        private void OnDisable()
        {
            _jumpButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                RequestJump();
            }
        }

        private void DisplayJumpText()
        {
            _jumpText.text = $"Чтобы выполнить прыжок, нажмите кнопку на экране либо клавишу {_jumpKey}";
        }

        private void RequestJump()
        {
            _jumpRequest.Publish();
        }
    }
}