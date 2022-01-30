using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class InitialUnitPlacer : MonoBehaviour
    {
        [SerializeField] private int unitsToSpawn = 2;
        [SerializeField] private Unit unitPrefab;
        [Inject] private IWorld world = null;
        [Inject] private ILayerManager layerManager = null;
        
        private void Update()
        {
            if (unitsToSpawn > 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                unitsToSpawn--;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue, layerManager.GroundLayerMask, QueryTriggerInteraction.Ignore))
                {
                    world.Instantiate(unitPrefab, hit.point, Quaternion.identity);
                }
            }
        }
    }
}