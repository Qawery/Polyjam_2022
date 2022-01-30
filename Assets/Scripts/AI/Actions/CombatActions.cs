using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class CombatActions
    {
        public static Action DealDamageToTarget(IMobileAttacker damageDealer, IAttackTarget target)
        {
            Assert.IsNotNull(damageDealer);
            Assert.IsNotNull(target);
            return new Action(new List<Condition> { new CloserThan(damageDealer, target, damageDealer.DamageManager.Range),
                                                    new IsAlive(target)},
                              new List<Effect> { new ApplyDamage(damageDealer.DamageManager, target) },
                              new List<Condition> { new IsDead(target) });
        }
    }
}