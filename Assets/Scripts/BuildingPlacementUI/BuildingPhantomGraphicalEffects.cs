using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class BuildingPhantomGraphicalEffects : MonoBehaviour
    {
        [SerializeField] private Material normalMaterial = null;
        [SerializeField] private Material blockedMaterial = null;
        
        [Inject]
        private void Init(BuildingPhantom buildingPhantom, List<MeshRenderer> renderers)
        {
            buildingPhantom.OnPlacementPossibilityChanged += canBePlaced =>
            {
                foreach (var renderer in renderers)
                {
                    renderer.material = canBePlaced ? normalMaterial : blockedMaterial;
                }
            };

            foreach (var renderer in renderers)
            {
                renderer.material = normalMaterial;
            }
        }
    }
}