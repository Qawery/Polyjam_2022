using UnityEngine.Assertions;
using System.Linq;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public class HasCapacityForResources : Condition
    {
        public readonly ResourceManager resourceManager;
        public readonly int requiredCapacity;
        public readonly List<ResourceType> requiredTypes = new List<ResourceType>();

        public HasCapacityForResources(IResourceHolder resourceHolder, IEnumerable<(ResourceType type, int amount)> resources)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.Resources);
            this.resourceManager = resourceHolder.Resources;

            Assert.IsNotNull(resources);
            foreach (var resource in resources)
            {
                Assert.IsFalse(this.requiredTypes.Contains(resource.type));
                this.requiredTypes.Add(resource.type);
                requiredCapacity += resource.amount;
            }
            Assert.IsTrue(this.requiredTypes.Count > 0);
            Assert.IsTrue(requiredCapacity > 0);
        }

        public override bool IsSatisified()
        {
            return resourceManager != null && resourceManager.CapacityLeft >= requiredCapacity && resourceManager.SupportsTypes(requiredTypes);
        }
    }
}