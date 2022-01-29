using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Resources : IResourceHolder
    {
        private readonly float maxCapacity;
        private float currentAmount;

        public Resources Resources => this;
        public float CapacityLeft => maxCapacity - currentAmount;
        public float MaxCapacity => maxCapacity;
        public float CurrentAmount
        {
            get => currentAmount;
            set
            {
                Assert.IsTrue(value >= 0.0f);
                Assert.IsTrue(value <= maxCapacity);
                currentAmount = value;
            }
        }

        public Resources(float maxCapacity, float currentAmount)
        {
            Assert.IsTrue(maxCapacity > 0.0f);
            this.maxCapacity = maxCapacity;

            CurrentAmount = currentAmount;
        }

        public static Resources CreateFullResourceHolder(float maxCapacity)
        {
            return new Resources(maxCapacity, maxCapacity);
        }

        public static Resources CreateEmptyResourceHolder(float maxCapacity)
        {
            return new Resources(maxCapacity, 0.0f);
        }
    }
}