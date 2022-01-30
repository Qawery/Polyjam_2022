using System;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public enum ProductionType
    {
        Unit,
        Resource
    }
    
    [Serializable]
    public class ResourceRequirement
    {
        public ResourceType ResourceType = ResourceType.Gold;
        public int RequiredAmount = 1;
    }
    
    [Serializable]
    public class ProductionData
    {
        public ProductionType ProductionType = ProductionType.Resource;
        public ResourceType ProducedResource = ResourceType.Potatoes;
        public int ProducedAmount = 1;
    }
    
    [Serializable]
    public class Recipe
    {
        public List<ResourceRequirement> ConsumptionData;
        public List<ProductionData> ProductionData;
        public float ProductionCooldown = 30;

        public bool CheckResources(ResourceManager resourceManager)
        {
            foreach (var requirement in ConsumptionData)
            {
                if (!resourceManager.TryGetCurrentAmount(requirement.ResourceType, out int amount) ||
                    amount < requirement.RequiredAmount)
                {
                    return false;
                }
            }

            return true;
        }

        public void ConsumeResources(ResourceManager resourceManager)
        {
            foreach (var requirement in ConsumptionData)
            {
                resourceManager.TakeResource(requirement.ResourceType, requirement.RequiredAmount);
            }
        }
    }
    
    [Serializable]
    public class BuildingData
    {
        public string BuildingId = "invalid_id";
        public string BuildingName = "unnamed";
        public List<ResourceRequirement> ConstructionResourceRequirements = new List<ResourceRequirement>();
        public List<Recipe> Recipes = new List<Recipe>();
        public int ResourceCapacity = 50;
    }
}