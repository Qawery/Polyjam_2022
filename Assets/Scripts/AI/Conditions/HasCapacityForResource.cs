using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasCapacityForResource : Condition
    {
        public readonly ResourceManager resources;
        public readonly ResourceType type;
        public readonly int requiredCapacity;

        public HasCapacityForResource(IResourceHolder resourceHolder, ResourceType type, int requiredCapacity = 1)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.Resources);
            this.resources = resourceHolder.Resources;
            this.type = type;
            Assert.IsTrue(requiredCapacity >= 1);
            this.requiredCapacity = requiredCapacity;
        }

        public override bool IsSatisified()
        {
            return resources != null && resources.SupportsType(type) && resources.CapacityLeft >= requiredCapacity;
        }
    }
}