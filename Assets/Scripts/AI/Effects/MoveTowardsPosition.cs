using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class MoveTowardsPosition : Effect
    {
        private readonly NavMeshAgent navMeshAgent;
        private readonly IPositionProvider position;

        public MoveTowardsPosition(INavMeshAgentProvider navMeshAgentProvider, IPositionProvider position)
        {
            Assert.IsNotNull(navMeshAgentProvider);
            Assert.IsNotNull(navMeshAgentProvider.NavMeshAgent);
            this.navMeshAgent = navMeshAgentProvider.NavMeshAgent;
            Assert.IsNotNull(position);
            this.position = position;
        }

        public override void TakeEffect(float deltaTime)
        {
            navMeshAgent.SetDestination(position.Position);
        }
    }
}