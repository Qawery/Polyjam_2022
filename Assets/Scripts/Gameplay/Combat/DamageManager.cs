using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class DamageManager
    {
        public readonly int Damage;
        public readonly float Range;
        public readonly float Cooldown;
        private float cooldownTimer = 0.0f;

        public bool IsReady => cooldownTimer <= 0.0f;

        public DamageManager(int damage, float range, float fireRate)
        {
            Assert.IsTrue(damage > 0);
            Damage = damage;
            Assert.IsTrue(range > 0.0f);
            Range = range;
            Assert.IsTrue(fireRate > 0.0f);
            Cooldown = 1.0f / fireRate;
        }

        public void Update(float deltaTime)
        {
            if (cooldownTimer > 0.0f)
            {
                cooldownTimer = Mathf.Max(0.0f, cooldownTimer - deltaTime);
            }
        }

        public void DealDamage(IDestructible target)
        {
            if (IsReady)
            {
                target.HealthPoints.ReceiveDamage(Damage);
                cooldownTimer = Cooldown;
            }
        }
    }
}