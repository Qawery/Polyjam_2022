using System;
using System.Collections.Generic;

namespace Polyjam_2022
{
    [Serializable]
    public class ResourceRequirement
    {
        public ResourceType ResourceType;
        public int RequiredAmount;
    }
    
    [Serializable]
    public class BuildingData
    {
        public string BuildingId = "invalid_id";
        public string BuildingName = "unnamed";
        public List<ResourceRequirement> ResourceRequirements = new List<ResourceRequirement>();
    }
}