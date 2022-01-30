using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Unit : MonoBehaviour, IResourceManipulator, IMovableAgent
    {
        [SerializeField, Range(1, 1000)] private readonly int startingMaxCapacity = 10;
        private NavMeshAgent navMeshAgent;

        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public ResourceManager Resources { get; private set; }
        public float Range => 2.0f;
        public Vector3 Position => transform.position;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navMeshAgent);
            var allResourceTypes = 
            Resources = new ResourceManager(startingMaxCapacity, ResourceHelpers.GetAllTypes());
        }
    }
}