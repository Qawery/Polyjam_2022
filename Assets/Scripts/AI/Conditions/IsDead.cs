using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class IsDead : Condition
    {
        private readonly IDestructible target;

        public IsDead(IDestructible target)
        {
            Assert.IsNotNull(target);
            this.target = target;
        }

        public override bool IsSatisified()
        {
            return target != null && target.HealthPoints.CurrentValue <= 0;
        }
    }
}