using Devolvist.UnityReusableSolutions.Events;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.CameraShakingExample
{
    public class GroundCollisionTrigger : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _collisionEvent;

        private void OnCollisionEnter(Collision collision)
        {
            if (_collisionEvent != null)
                _collisionEvent.Publish();
        }
    }
}