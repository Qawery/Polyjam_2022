using UnityEngine;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public class ResourceSource : MonoBehaviour, IResourceHolder
    {
        [SerializeField, Range(0, 1000)] private int maxCapacity = 10;
        [SerializeField, Range(0, 1000)] private List<(ResourceType type, int amount)> startingAmmount;

        public ResourceManager Resources { get; private set; }

        private void Awake()
        {
            Resources = new ResourceManager(maxCapacity, startingAmmount);
        }
    }
}