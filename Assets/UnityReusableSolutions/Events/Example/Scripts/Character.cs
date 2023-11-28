using Devolvist.UnityReusableSolutions.Events;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.EventsExample
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [Tooltip("Внешний запрос на выполнение прыжка.")]
        [SerializeField] private ScriptableEvent _jumpRequest;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _jumpRequest.Subscribe(Jump);
        }

        private void OnDisable()
        {
            _jumpRequest.Unsubscribe(Jump);
        }

        private void Jump()
        {
            Debug.Log("Выполняется прыжок.");

            float force = 5;

            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}