using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasAmountOfResources : Precondition
    {
        public readonly Resources resources;
        public readonly float amount;

        public HasAmountOfResources(IResourceHolder resourceHolder, float amount = float.Epsilon)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.ResourcesHeld);
            this.resources = resourceHolder.ResourcesHeld;

            Assert.IsTrue(amount >= 0.0f);
            this.amount = amount;
        }

        public override bool IsSatisified()
        {
            return resources != null && resources.CurrentAmount >= amount;
        }
    }
}