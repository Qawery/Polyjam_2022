using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class ApplyDamage : Effect
    {
        private readonly DamageManager damageDealer;
        private readonly IDestructible damageReciever;

        public ApplyDamage(DamageManager damageDealer, IDestructible damageReciever)
        {
            Assert.IsNotNull(damageDealer);
            this.damageDealer = damageDealer;
            Assert.IsNotNull(damageReciever);
            this.damageReciever = damageReciever;
        }

        public override void TakeEffect(float deltaTime)
        {
            damageDealer.DealDamage(damageReciever);
        }
    }
}