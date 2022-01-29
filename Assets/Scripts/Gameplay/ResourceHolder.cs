using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class ResourceHolder : IResourceHolder
    {
        private readonly float maxCapacity;
        private float currentAmmount;

        public float MaxCapacity => maxCapacity;
        public float CurrentAmmount
        {
            get => currentAmmount;
            set
            {
                currentAmmount = value;
                Assert.IsTrue(currentAmmount >= 0.0f);
                Assert.IsTrue(currentAmmount <= maxCapacity);
            }
        }

        public ResourceHolder(float maxCapacity, float currentAmmount)
        {
            this.maxCapacity = maxCapacity;
            this.currentAmmount = currentAmmount;
        }

        public static ResourceHolder CreateFullResourceHolder(float maxCapacity)
        {
            return new ResourceHolder(maxCapacity, maxCapacity);
        }

        public static ResourceHolder CreateEmptyResourceHolder(float maxCapacity)
        {
            return new ResourceHolder(maxCapacity, 0.0f);
        }
    }
}