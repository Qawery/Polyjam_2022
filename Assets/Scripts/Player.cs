using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Polyjam_2022
{
    public class Player : MonoBehaviour
    {
        private Dictionary<ResourceType, int> resourcesAvailable = new Dictionary<ResourceType, int>();

        public Player()
        {
            foreach (var resourceType in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            {
                resourcesAvailable.Add(resourceType, 0);
            }
        }
        public int GetResourceValue(ResourceType resourceType) => resourcesAvailable[resourceType];
        public void SetResourceValue(ResourceType resourceType, int newValue)
        {
            resourcesAvailable[resourceType] = newValue;
        }
    }
}
