using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class EnabledDuringDayNightPhase : MonoBehaviour
    {
        [SerializeField] private bool enabledDuringDayPhase = true;

        [Inject]
        private void Init(IDayNightCycle dayNightCycle)
        {
            dayNightCycle.OnCycleChanged += cycle =>
            {
                gameObject.SetActive(cycle.IsDay == enabledDuringDayPhase);

            };
        }
    }
}