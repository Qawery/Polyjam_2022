using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasAmountOfResources : Precondition
    {
        public readonly Resources resourceHolder;
        public readonly float amount;

        public HasAmountOfResources(IResourceHolder resourceHolder) : this(resourceHolder.Resources, float.Epsilon)
        {
        }

        public HasAmountOfResources(IResourceHolder resourceHolder, float amount) : this(resourceHolder.Resources, amount)
        {
        }

        public HasAmountOfResources(Resources resourceHolder) : this(resourceHolder, float.Epsilon)
        {
        }

        public HasAmountOfResources(Resources resourceHolder, float amount)
        {
            Assert.IsNotNull(resourceHolder);
            this.resourceHolder = resourceHolder;
            Assert.IsTrue(amount >= 0.0f);
            this.amount = amount;
        }

        public override bool IsSatisified()
        {
            return resourceHolder != null && resourceHolder.CurrentAmount >= amount;
        }
    }
}