using UnityEngine.AI;

namespace Polyjam_2022
{
    public interface INavMeshAgentProvider
    {
        public NavMeshAgent NavMeshAgent { get; }
    }
}