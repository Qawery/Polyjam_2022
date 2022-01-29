using System.Collections.Generic;
using UnityEngine;

namespace Polyjam_2022
{
    [CreateAssetMenu(menuName = "Building Data Collection", fileName = "BuildingDataCollection")]
    public class BuildingDataCollection : ScriptableObject, IBuildingDataCollection
    {
        [SerializeField] private List<BuildingData> buildingData = new List<BuildingData>();

        public List<BuildingData> BuildingData => buildingData;
    }
}