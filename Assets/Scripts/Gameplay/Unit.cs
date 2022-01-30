using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Unit : MonoBehaviour, IResourceManipulator, IMovableAgent
    {
        [SerializeField, Range(1, 1000)] private readonly int startingMaxCapacity = 10;
        [SerializeField, Range(0, 1)] private readonly int playerId = 0;
        private NavMeshAgent navMeshAgent;

        public int PlayerId => playerId;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public ResourceManager Resources { get; private set; }
        public float Range => 2.0f;
        public Vector3 Position => transform.position;

        public DamageManager DamageManager { get; private set; } = new DamageManager(1, 2, 1.0f);
        public HealthPoints HealthPoints { get; private set; } = new HealthPoints(5);

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navMeshAgent);
            var allResourceTypes = 
            Resources = new ResourceManager(startingMaxCapacity, ResourceHelpers.GetAllTypes());
        }

        private void Update()
        {
            DamageManager.Update(Time.deltaTime);
        }
    }
}