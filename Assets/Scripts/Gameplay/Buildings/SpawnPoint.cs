using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class SpawnPoint : MonoBehaviour
    {
        [Inject] private TriggerOverlapDetector triggerOverlapDetector = null;

        public bool IsTaken => triggerOverlapDetector.OverlappingColliderCount == 0;
    }
}