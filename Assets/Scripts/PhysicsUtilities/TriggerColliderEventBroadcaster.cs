using System;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class TriggerColliderEventBroadcaster : MonoBehaviour
    {
        [Inject] public Collider Collider { get; }
        
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<Collider> OnTriggerExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }
    }
}