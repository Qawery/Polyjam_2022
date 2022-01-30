using UnityEngine;

namespace Polyjam_2022
{
    public class HealthPoints
    {
        private int currentValue;

        public int CurrentValue
        {
            get => currentValue;
            private set
            {
                currentValue = Mathf.Min(value, MaxValue);
                OnValueChanged?.Invoke(currentValue);
                if (currentValue <= 0)
                {
                    OnDeath?.Invoke();
                }
            }
        }
        
        public int MaxValue { get; }

        public event System.Action<int> OnValueChanged;
        public event System.Action OnDeath;

        public HealthPoints(int maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;
        }
        
        public void ReceiveDamage(int damage)
        {
            CurrentValue -= damage;
        }
    }
}