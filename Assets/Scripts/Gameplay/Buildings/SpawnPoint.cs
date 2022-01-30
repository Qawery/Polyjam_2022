using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class SpawnPoint : MonoBehaviour
    {
        private TriggerOverlapDetector triggerOverlapDetector = null;

        public bool IsTaken => triggerOverlapDetector.OverlappingColliderCount > 0;

        [Inject]
        private void Init(ITriggerEventBroadcaster broadcaster)
        {
            triggerOverlapDetector = new TriggerOverlapDetector(broadcaster);
        }
    }
}