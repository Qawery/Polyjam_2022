using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class DayNightCycle : MonoBehaviour, IDayNightCycle
    {
        [SerializeField, Range(0.0f, 1.0f)] private float startingTimeProgress = 0;
        [SerializeField, Range(0.01f, 0.99f)] private float transitionThreshold = 0.5f;
        [SerializeField, Range(1.0f, 5000.0f)] private float cycleSeconds = 5.0f;

        private float TimeProgress { get; set; } = 0;
        
        public bool IsDay => TimeProgress < transitionThreshold;
        public bool IsNight => !IsDay;
        
        public event System.Action<DayNightCycle> OnCycleChanged;

        private void Awake()
        {
            TimeProgress = startingTimeProgress;
        }

        private void Update()
        {
            UpdateTimeProgress(Time.deltaTime / cycleSeconds);
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
            bool previousDay = IsDay;
            TimeProgress += deltaTime;
            if (TimeProgress > 1f)
            {
                TimeProgress -= 1f;
            }
            if(IsDay != previousDay)
            {
                OnCycleChanged?.Invoke(this);
            }
        }
    }
}
