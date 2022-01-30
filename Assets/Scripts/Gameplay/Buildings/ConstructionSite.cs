using System.Linq;
using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class ConstructionSite : PlacedObject, IResourceHolder, IResourceLocation
    {
        [Inject] private IWorld world = null;
        [Inject] private IBuildingPrefabCollection buildingPrefabCollection;
        private BuildingData buildingData;
        
        public BuildingData BuildingData
        {
            get => buildingData;
            set
            {
                Resources = new ResourceManager(1000, value.ConstructionResourceRequirements.Select(requirement => requirement.ResourceType));
                Resources.OnTotalAmountChanged += newValue =>
                {
                    foreach (ResourceRequirement requirement in buildingData.ConstructionResourceRequirements)
                    {
                        if (!Resources.TryGetCurrentAmount(requirement.ResourceType, out int amount))
                        {
                            return;
                        }

                        if (amount < requirement.RequiredAmount)
                        {
                            return;
                        }
                    }
                    
                    CompleteConstruction();
                };
                buildingData = value;
            }
        }
        
        public Vector3 PlacementPosition { private get; set; }

        public Vector3 Position => transform.position;
        public ResourceManager Resources { get; private set; }

        public void CompleteConstruction()
        {
            var building = world.Instantiate(buildingPrefabCollection.GetBuildingPrefabData(buildingData.BuildingId).BuildingPrefab, PlacementPosition, transform.rotation);
            building.BuildingData = buildingData;
            building.transform.position = PlacementPosition;
            world.Destroy(gameObject);
        }
    }
}