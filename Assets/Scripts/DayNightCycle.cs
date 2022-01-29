using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Assets.Scripts
{
    public class DayNightCycle
    {
        private const float StartingTimeProgress = 0;
        private float TimeProgress { get; set; } = StartingTimeProgress;
        public event System.Action<DayNightCycle> OnCycleChanged;

        public bool IsDay()
        {
            return TimeProgress < 0.5f;
        }

        public bool IsNight()
        {
            return !IsDay();
        }

        public (int,int) GetHourMinute()
        {
            float totalMinutes = TimeProgress * 1440;
            int hours = (int)(totalMinutes / 60);
            return (hours, (int)(totalMinutes - hours * 60));
        }

        public void UpdateTimeProgress(float deltaTime)
        {
            Assert.IsTrue(deltaTime >= 0 && deltaTime < 1);
            bool previousDay = IsDay();
            TimeProgress += deltaTime;
            if (TimeProgress > 1f)
            {
                TimeProgress -= 1f;
            }
            if(IsDay() != previousDay)
            {
                OnCycleChanged?.Invoke(this);
            }
        }
    }
}
