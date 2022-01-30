using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Polyjam_2022
{
    public class ResourceSource : MonoBehaviour, IResourceLocation
    {
        [SerializeField, Range(0, 1000)] private int maxCapacity = 10;
        [SerializeField, Range(0, 1000)] private List<(ResourceType type, int amount)> startingAmount;

        public ResourceManager Resources { get; private set; }
        public Vector3 Position => transform.position;

        public Dictionary<ResourceType, int> BlockedAmountPerType { get; set; } = new Dictionary<ResourceType, int>();

        private void Awake()
        {
            Resources = new ResourceManager(maxCapacity, startingAmount);
            foreach (var resourceType in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            {
                BlockedAmountPerType.Add(resourceType, 0);
            }
        }
    }
}