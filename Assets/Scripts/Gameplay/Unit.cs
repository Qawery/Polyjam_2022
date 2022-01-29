using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Unit : MonoBehaviour, IResourceHolder
    {
        [SerializeField, Range(0.0f, 1000.0f)] private readonly float startingMaxCapacity = 10.0f;
        private ResourceHolder resourceHolder;
        private NavMeshAgent navMeshAgent;

        public NavMeshAgent NavMeshAgent => navMeshAgent;
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
            navMeshAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navMeshAgent);
            resourceHolder = ResourceHolder.CreateEmptyResourceHolder(startingMaxCapacity);
        }
    }
}