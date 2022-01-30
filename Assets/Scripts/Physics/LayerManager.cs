using UnityEngine;

namespace Polyjam_2022
{
    [CreateAssetMenu(fileName = "LayerManager", menuName = "Layer Manager")]
    public class LayerManager : ScriptableObject, ILayerManager
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private LayerMask unitLayerMask;
        [SerializeField] private LayerMask buildingLayerMask;

        public LayerMask GroundLayerMask => groundLayerMask;
        public LayerMask UnitLayerMask => unitLayerMask;
        public LayerMask BuildingLayerMask => buildingLayerMask;
    }
}