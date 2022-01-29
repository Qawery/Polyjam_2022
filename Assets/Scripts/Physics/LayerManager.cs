using UnityEngine;

namespace Polyjam_2022
{
    [CreateAssetMenu(fileName = "LayerManager", menuName = "Layer Manager")]
    public class LayerManager : ScriptableObject, ILayerManager
    {
        [SerializeField] private LayerMask groundLayerMask;

        public LayerMask GroundLayerMask => groundLayerMask;
    }
}