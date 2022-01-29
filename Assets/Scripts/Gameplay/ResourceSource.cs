using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class ResourceSource : MonoBehaviour, IResourceHolder
    {
        [SerializeField, Range(0.0f, 1000.0f)] private float maxCapacity = 0.0f;
        [SerializeField, Range(0.0f, 1000.0f)] private float startingAmmount = 0.0f;
        private ResourceHolder resourceHolder;

        public float MaxCapacity => resourceHolder.MaxCapacity;
        public float CurrentAmmount
        {
            get => resourceHolder.CurrentAmmount;
            set
            {
                resourceHolder.CurrentAmmount = value;
            }
        }

        private void Awake()
        {
            Assert.IsTrue(startingAmmount <= maxCapacity);
            resourceHolder = new ResourceHolder(maxCapacity, startingAmmount);
        }
    }
}