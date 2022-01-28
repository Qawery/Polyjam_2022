using Lifecycle;
using UnityEngine;

namespace Polyjam_2022
{
    public class BuildingPlacer
    {
        private BuildingPhantom buildingPhantom;
        private BuildingData buildingData;
        private readonly LayerMask groundLayerMask;
        private readonly IWorld world;

        public BuildingPhantom BuildingPhantom => buildingPhantom;
        
        public BuildingPlacer(IWorld world, LayerMask groundLayerMask)
        {
            this.groundLayerMask = groundLayerMask;
            this.world = world;
        }

        public void SetBuildingData(BuildingData buildingData)
        {
            if (buildingPhantom != null)
            {
                world.Destroy(buildingPhantom.gameObject);
            }

            this.buildingData = buildingData;

            if (buildingData != null)
            {
                buildingPhantom = world.Instantiate(buildingData.BuildingPhantomPrefab);
            }
        }
        
        public void MovePhantomToPosition(Vector3 position)
        {
            Vector3 groundPointPosition;
            if (Physics.Raycast(new Ray(position, Vector3.down), out RaycastHit hitInfo, 
                    float.MaxValue, groundLayerMask, QueryTriggerInteraction.Ignore) &&
                Physics.Raycast(new Ray(position, Vector3.up), out hitInfo, 
                    float.MaxValue, groundLayerMask, QueryTriggerInteraction.Ignore))
            {
                groundPointPosition = hitInfo.point;
            }
            else
            {
                //if the queries failed, the position has to be right on the ground
                groundPointPosition = position;
            }

            buildingPhantom.transform.position = groundPointPosition + buildingPhantom.OffsetFromCenterToBase * Vector3.up;
        }

        public bool TryPlaceBuildingAtCurrentPosition()
        {
            if (!buildingPhantom.CanBePlaced)
            {
                return false;
            }

            world.Instantiate(buildingData.BuildingPrefab, buildingPhantom.transform.position, 
                buildingPhantom.transform.rotation);
            return true;
        }

        public void Release()
        {
            SetBuildingData(null);
        }
    }
}