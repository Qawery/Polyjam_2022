using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] private BuildingButton buildingButtonPrefab = null;
        [SerializeField] private Transform buttonParentTransform = null;

        [Inject]
        private void Init(IWorld world, IBuildingDataCollection buildingDataCollection, 
            IBuildingPrefabCollection buildingPrefabCollection)
        {
            foreach (var buildingData in buildingDataCollection.BuildingData)
            {
                var button = world.Instantiate(buildingButtonPrefab, buttonParentTransform);
                button.NameText.text = buildingData.BuildingName;
                button.BuildingData = buildingData;
            }
        }
    }
}