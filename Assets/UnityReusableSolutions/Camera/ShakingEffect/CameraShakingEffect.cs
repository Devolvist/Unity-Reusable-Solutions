using Devolvist.UnityReusableSolutions.Events;
using System.Collections;
using UnityEngine;
using UnityCamera = UnityEngine.Camera;

namespace Devolvist.UnityReusableSolutions.Camera
{
    /// <summary>
    /// Эффект тряски камеры.
    /// </summary>
    public class CameraShakingEffect : MonoBehaviour
    {
        [Tooltip("Внешний триггер для запуска воспроизведения.")]
        [SerializeField] private ScriptableEvent _playingTrigger;

        [Header("Конфигурация")]
        [SerializeField, Range(1, 10)] private float _force = 5;
        [SerializeField, Range(0.1f, 1.0f)] private float _durationInSeconds = 0.2f;

        private Vector3 _initialPosition;

        public bool IsPlaying { get; private set; }

        private void OnEnable()
        {
            if (_playingTrigger != null)
                _playingTrigger.Subscribe(OnPlayingTriggered);
        }

        private void OnDisable()
        {
            if (_playingTrigger != null)
                _playingTrigger.Unsubscribe(OnPlayingTriggered);

            CancelInvoke();
        }

        private void OnPlayingTriggered()
        {
            if (IsPlaying)
                return;

            IsPlaying = true;
            _initialPosition = UnityCamera.main.transform.position;
        
            Invoke(nameof(Stop), _durationInSeconds);
            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {         
            var waitForEndOfFrame = new WaitForEndOfFrame();

            while (IsPlaying)
            {
                UnityCamera.main.transform.position = _initialPosition + (_force * Time.deltaTime * Random.insideUnitSphere);
                yield return waitForEndOfFrame;
            }           
        }

        private void Stop()
        {         
            UnityCamera.main.transform.position = _initialPosition;
            IsPlaying = false;
        }
    }
}