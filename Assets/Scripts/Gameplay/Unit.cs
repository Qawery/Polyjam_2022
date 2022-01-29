using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Unit : MonoBehaviour, IResourceManipulator, IPositionProvider
    {
        [SerializeField, Range(0.0f, 1000.0f)] private readonly float startingMaxCapacity = 10.0f;
        private NavMeshAgent navMeshAgent;

        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public Resources Resources { get; private set; }
        public float Range => 2.0f;
        public Vector3 Position => transform.position;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navMeshAgent);
            Resources = Resources.CreateEmptyResourceHolder(startingMaxCapacity);
        }
    }
}