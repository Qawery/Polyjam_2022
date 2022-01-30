using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Polyjam_2022
{
    public class ResourceSource : MonoBehaviour, IResourceHolder
    {
        [SerializeField, Range(0, 1000)] private int maxCapacity = 10;
        [SerializeField] private List<ResourceRequirement> startingAmount;

        public ResourceManager Resources { get; private set; }

        public Dictionary<ResourceType, int> BlockedAmountPerType { get; set; } = new Dictionary<ResourceType, int>();
        private void Awake()
        {
            Resources = new ResourceManager(maxCapacity, startingAmount.Select(data => (data.ResourceType, data.RequiredAmount)));
            foreach (var resourceType in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            {
                BlockedAmountPerType.Add(resourceType, 0);
            }
        }
    }
}