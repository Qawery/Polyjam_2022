using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasCapacityForResources : Precondition
    {
        public readonly Resources resources;
        public readonly float capacity;
                
        public HasCapacityForResources(IResourceHolder resourceHolder, float capacity = float.Epsilon)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.ResourcesHeld);
            this.resources = resourceHolder.ResourcesHeld;

            Assert.IsTrue(capacity >= 0.0f);
            this.capacity = capacity;
        }

        public override bool IsSatisified()
        {
            return resources != null && resources.CapacityLeft >= capacity;
        }
    }
}