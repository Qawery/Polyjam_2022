using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class AttackReady : Condition
    {
        private readonly DamageManager damageDealer;

        public AttackReady(DamageManager damageDealer)
        {
            Assert.IsNotNull(damageDealer);
        }

        public override bool IsSatisified()
        {
            return damageDealer != null && damageDealer.IsReady;
        }
    }
}