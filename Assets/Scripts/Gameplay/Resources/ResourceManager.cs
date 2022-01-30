using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam_2022
{
    public class ResourceManager : IResourceHolder
    {
        private readonly int maxCapacity;
        private int currentTotalAmount;
        private readonly Dictionary<ResourceType, int> currentAmountPerType = new Dictionary<ResourceType, int>();

        public ResourceManager Resources => this;
        public List<ResourceType> SupportedTypes => currentAmountPerType.Keys.ToList();
        public int CapacityLeft => maxCapacity - currentTotalAmount;
        public int MaxCapacity => maxCapacity;
        public int CurrentTotalAmount
        {
            get => currentTotalAmount;
            private set
            {
                Assert.IsTrue(value >= 0.0f);
                Assert.IsTrue(value <= maxCapacity);
                currentTotalAmount = value;
            }
        }

        public event System.Action<int> OnTotalAmountChanged;

        public ResourceManager(int maxCapacity, ResourceType supportedType) : this(maxCapacity, new ResourceType[] { supportedType })
        {
        }

        public ResourceManager(int maxCapacity, IEnumerable<ResourceType> supportedTypes)
        {
            Assert.IsTrue(maxCapacity > 0.0f);
            this.maxCapacity = maxCapacity;
            CurrentTotalAmount = 0;
            foreach (var type in supportedTypes)
            {
                currentAmountPerType[type] = 0;
            }

            if (currentAmountPerType.Keys.Count == 0)
            {
                Debug.LogWarning("Created resource manager with no supported resource types.");
            }
        }

        public ResourceManager(int maxCapacity, IEnumerable<(ResourceType type, int amount)> startingResources) : this(maxCapacity, 
                            startingResources.Select(startingResource => startingResource.type).ToList())
        {
            foreach (var startingResource in startingResources)
            {
                if (startingResource.amount == 0)
                {
                    currentAmountPerType[startingResource.type] = 0;
                }
                else
                {
                    InsertResource(startingResource.type, startingResource.amount);
                }
            }
        }

        public static List<ResourceType> GetAllTypes()
        {
            var result = new List<ResourceType>();
            foreach (var resourceType in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            {
                result.Add(resourceType);
            }
            return result;
        }

        public bool SupportsType(ResourceType type)
        {
            return currentAmountPerType.ContainsKey(type);
        }

        public bool SupportsTypes(IEnumerable<ResourceType> types)
        {
            foreach (var type in types)
            {
                if (!currentAmountPerType.ContainsKey(type))
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryGetCurrentAmount(ResourceType type, out int currentAmount)
        {
            if (SupportsType(type))
            {
                currentAmount = currentAmountPerType[type];
                return true;
            }

            currentAmount = 0;
            return false;
        }

        public bool HasResource(ResourceType type, int amount)
        {
            return SupportsType(type) && currentAmountPerType[type] >= amount;
        }

        public bool HasResource(IEnumerable<(ResourceType type, int amount)> resources)
        {
            foreach (var resource in resources)
            {
                if (!HasResource(resource.type, resource.amount))
                {
                    return false;
                }
            }
            return true;
        }

        public void InsertResource(ResourceType type, int amount)
        {
            Assert.IsTrue(SupportsType(type));
            CurrentTotalAmount += amount;
            currentAmountPerType[type] += amount;
            OnTotalAmountChanged?.Invoke(currentTotalAmount);
        }

        public void InsertResource(IEnumerable<(ResourceType type, int amount)> resources)
        {
            foreach (var resource in resources)
            {
                InsertResource(resource.type, resource.amount);
            }
        }

        public void TakeResource(ResourceType type, int amount)
        {
            Assert.IsTrue(SupportsType(type));
            Assert.IsTrue(currentAmountPerType[type] >= amount);
            CurrentTotalAmount -= amount;
            currentAmountPerType[type] -= amount;
            OnTotalAmountChanged?.Invoke(currentTotalAmount);
        }

        public void TakeResource(IEnumerable<(ResourceType type, int amount)> resources)
        {
            foreach (var resource in resources)
            {
                TakeResource(resource.type, resource.amount);
            }
        }
    }
}