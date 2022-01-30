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

        public BuildingPlacer(IWorld world, ILayerManager layerManager, IBuildingPrefabCollection buildingPrefabCollection, IDayNightCycle dayNightCycle = null)
        {
            this.groundLayerMask = layerManager.GroundLayerMask;
            this.world = world;
            this.buildingPrefabCollection = buildingPrefabCollection;

            if (dayNightCycle != null)
            {
                dayNightCycle.OnCycleChanged += cycle =>
                {
                    if (cycle.IsNight)
                    {
                        Release();
                    }
                };
            }
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
                buildingPhantom = world.Instantiate(buildingPrefabData.BuildingPhantomPrefab, Vector3.one * 1000, Quaternion.identity);
            }

            this.buildingData = buildingData;
            OnBuildingSelectionChanged?.Invoke(buildingData);
        }
        
        public void MovePhantomToPosition(Vector3 position)
        {
            buildingPhantom.PlaceBaseAtPosition(GetGroundPosition(position));
        }

        public void RotatePhantom(float angle)
        {
            buildingPhantom.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
        }

        public bool TryPlaceBuildingAtCurrentPosition()
        {
            if (!buildingPhantom.CanBePlaced)
            {
                return false;
            }

            Vector3 placementPosition = GetGroundPosition(buildingPhantom.transform.position);
            world.Instantiate(buildingPrefabData.ConstructionSitePrefab, placementPosition,
                buildingPhantom.transform.rotation, site =>
                {
                    site.PlaceBaseAtPosition(placementPosition);
                    site.BuildingData = buildingData;
                    site.PlacementPosition = buildingPhantom.transform.position;
                });
            
            return true;
        }

        public void Release()
        {
            SetBuildingData(null);
        }

        private Vector3 GetGroundPosition(Vector3 position)
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

            return groundPointPosition;
        }
    }
}