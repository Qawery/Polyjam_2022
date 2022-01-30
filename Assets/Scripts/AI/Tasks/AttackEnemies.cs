using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class AttackEnemies : Task
    {
        //Task definition parameters
        private readonly IMobileAttacker mobileAttacker;
        private IAttackTarget attackTarget;

        //Execution variables
        private Action goToTarget;
        private Action dealDamage;

        public AttackEnemies(IMobileAttacker mobileAttacker)
        {
            Assert.IsNotNull(mobileAttacker);
            this.mobileAttacker = mobileAttacker;
        }

        public override void Execute(float deltaTime)
        {
            if (attackTarget == null)
            {
                FindNewTarget();
            }
            if (!dealDamage?.TryExecute(deltaTime) ?? false)
            {
                goToTarget?.TryExecute(deltaTime);
            }
        }

        private void FindNewTarget()
        {
            var potentialTargets = GameObject.FindObjectsOfType<Unit>();
            foreach (var potentialTarget in potentialTargets)
            {
                if (potentialTarget.PlayerId != mobileAttacker.PlayerId && 
                    (attackTarget == null ||
                        Vector3.Distance(mobileAttacker.Position, potentialTarget.Position) <
                        Vector3.Distance(mobileAttacker.Position, attackTarget.Position)))
                {
                    attackTarget = potentialTarget;
                }
            }
            if (attackTarget != null)
            {
                goToTarget = MovementActions.MoveToDestination(mobileAttacker, attackTarget, mobileAttacker.DamageManager.Range);
                dealDamage = CombatActions.DealDamageToTarget(mobileAttacker, attackTarget);
            }
        }
    }
}