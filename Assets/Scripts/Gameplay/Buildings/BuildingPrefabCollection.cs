using System.Collections.Generic;
using UnityEngine;

namespace Polyjam_2022
{
    [CreateAssetMenu(fileName = "BuildingPrefabCollection", menuName = "Building Prefab Collection")]
    public class BuildingPrefabCollection : ScriptableObject, IBuildingPrefabCollection
    {
        [SerializeField] private List<BuildingPrefabData> buildingData = new List<BuildingPrefabData>();
        
        public BuildingPrefabData GetBuildingPrefabData(string buildingId)
        {
            return buildingData.Find(data => data.BuildingId == buildingId);
        }
    }
}