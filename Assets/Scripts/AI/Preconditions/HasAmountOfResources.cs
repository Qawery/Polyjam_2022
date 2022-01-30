using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public class HasAmountOfResources : Precondition
    {
        public readonly ResourceManager resourceManager;
        public readonly List<(ResourceType type, int amount)> resources = new List<(ResourceType type, int requiredAmount)>();

        public HasAmountOfResources(IResourceHolder resourceHolder, IEnumerable<(ResourceType type, int requiredAmount)> resources)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.Resources);
            this.resourceManager = resourceHolder.Resources;
            this.resources.AddRange(resources);
            Assert.IsTrue(this.resources.Count > 0);
            foreach (var resource in this.resources)
            {
                Assert.IsTrue(resource.amount > 0);
            }
        }

        public override bool IsSatisified()
        {
            if (resourceManager == null)
            {
                return false;
            }
            int currentAmount = 0;
            foreach (var resource in this.resources)
            {
                if (!resourceManager.TryGetCurrentAmount(resource.type, out currentAmount) || 
                    currentAmount < resource.amount)
                {
                    return false;
                }
            }
            return true;
        }
    }
}