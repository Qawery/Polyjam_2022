using System.Linq;
using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class ConstructionSite : MonoBehaviour, IResourceHolder
    {
        [Inject] private IWorld world = null;
        [Inject] private IBuildingPrefabCollection buildingPrefabCollection;
        private BuildingData buildingData;
        
        public BuildingData BuildingData
        {
            set
            {
                Resources = new ResourceManager(int.MaxValue,
                    value.ConstructionResourceRequirements.Select(requirement => requirement.ResourceType));
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

                    world.Instantiate(buildingPrefabCollection.GetBuildingPrefabData(buildingData.BuildingId).BuildingPrefab, transform.position, transform.rotation).BuildingData = buildingData;
                    world.Destroy(gameObject);
                };
                buildingData = value;
            }
        }

        public ResourceManager Resources { get; private set; } 
    }
}