using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class MoveTowardsPoint : Effect
    {
        private readonly NavMeshAgent navMeshAgent;
        private readonly Vector3 destinationPoint;

        public MoveTowardsPoint(INavMeshAgentProvider navMeshAgentProvider, Vector3 destinationPoint)
        {
            Assert.IsNotNull(navMeshAgentProvider);
            Assert.IsNotNull(navMeshAgentProvider.NavMeshAgent);
            this.navMeshAgent = navMeshAgentProvider.NavMeshAgent;
            this.destinationPoint = destinationPoint;
        }

        public override void TakeEffect(float deltaTime)
        {
            navMeshAgent.SetDestination(destinationPoint);
        }
    }
}