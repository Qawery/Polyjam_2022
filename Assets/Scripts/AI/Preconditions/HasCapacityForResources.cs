using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasCapacityForResources : Precondition
    {
        public readonly Resources resources;
        public readonly float capacity;

        public HasCapacityForResources(IResourceHolder resourceHolder) : this(resourceHolder.Resources, float.Epsilon)
        {
        }

        public HasCapacityForResources(IResourceHolder resourceHolder, float amount) : this(resourceHolder.Resources, amount)
        {
        }

        public HasCapacityForResources(Resources resources) : this(resources, float.Epsilon)
        {
        }

        public HasCapacityForResources(Resources resources, float capacity)
        {
            Assert.IsNotNull(resources);
            this.resources = resources;
            Assert.IsTrue(capacity >= 0.0f);
            this.capacity = capacity;
        }

        public override bool IsSatisified()
        {
            return resources != null && resources.CapacityLeft >= capacity;
        }
    }
}