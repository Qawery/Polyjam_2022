using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class HasAmountOfResourceType : Precondition
    {
        public readonly ResourceManager resources;
        public readonly ResourceType type;
        public readonly int requiredAmount;

        public HasAmountOfResourceType(IResourceHolder resourceHolder, ResourceType type, int requiredAmount = 1)
        {
            Assert.IsNotNull(resourceHolder);
            Assert.IsNotNull(resourceHolder.Resources);
            this.resources = resourceHolder.Resources;
            this.type = type;
            Assert.IsTrue(requiredAmount >= 1);
            this.requiredAmount = requiredAmount;
        }

        public override bool IsSatisified()
        {
            int currentAmount = 0;
            return resources != null && resources.TryGetCurrentAmount(type, out currentAmount) && currentAmount >= requiredAmount;
        }
    }
}