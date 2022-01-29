using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Resources
    {
        private readonly float maxCapacity;
        private float currentAmount;

        public float CapacityLeft => maxCapacity - currentAmount;
        public float MaxCapacity => maxCapacity;
        public float CurrentAmount
        {
            get => currentAmount;
            set
            {
                currentAmount = value;
                Assert.IsTrue(currentAmount >= 0.0f);
                Assert.IsTrue(currentAmount <= maxCapacity);
            }
        }

        public Resources(float maxCapacity, float currentAmount)
        {
            this.maxCapacity = maxCapacity;
            this.currentAmount = currentAmount;
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