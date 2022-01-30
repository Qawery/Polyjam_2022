namespace Polyjam_2022
{
    public interface IDayNightCycle
    {
        public event System.Action<DayNightCycle> OnCycleChanged;
        public bool IsDay { get; }
        public bool IsNight { get; }
    }
}