using System;
using Lifecycle;
using UnityEngine;

namespace Polyjam_2022
{
    public class BuildingPlacer : IBuildingDataReceiver, IBuildingPlacer
    {
        private BuildingPhantom buildingPhantom;
        private BuildingPrefabData buildingPrefabData;
        private BuildingData buildingData;
        private readonly LayerMask groundLayerMask;
        private readonly IWorld world;
        private readonly IBuildingPrefabCollection buildingPrefabCollection;

        public BuildingPhantom BuildingPhantom => buildingPhantom;
        
        public event Action<BuildingData> OnBuildingSelectionChanged;

        public BuildingPlacer(IWorld world, ILayerManager layerManager, IBuildingPrefabCollection buildingPrefabCollection)
        {
            this.groundLayerMask = layerManager.GroundLayerMask;
            this.world = world;
            this.buildingPrefabCollection = buildingPrefabCollection;
            Debug.Log("constructed placer");
        }

        public void SetBuildingData(BuildingData buildingData)
        {
            if (buildingPhantom != null)
            {
                world.Destroy(buildingPhantom.gameObject);
            }

            if (buildingData != null)
            {
                this.buildingPrefabData = buildingPrefabCollection.GetBuildingPrefabData(buildingData.BuildingId);
                buildingPhantom = world.Instantiate(buildingPrefabData.BuildingPhantomPrefab);
            }

            this.buildingData = buildingData;
            OnBuildingSelectionChanged?.Invoke(buildingData);
        }
        
        public void MovePhantomToPosition(Vector3 position)
        {
            Vector3 groundPointPosition;
            if (Physics.Raycast(new Ray(position, Vector3.down), out RaycastHit hitInfo, 
                    float.MaxValue, groundLayerMask, QueryTriggerInteraction.Ignore) ||
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

            world.Instantiate(buildingPrefabData.ConstructionSitePrefab, buildingPhantom.transform.position, 
                buildingPhantom.transform.rotation).BuildingData = buildingData;
            return true;
        }

        public void Release()
        {
            SetBuildingData(null);
        }
    }
}