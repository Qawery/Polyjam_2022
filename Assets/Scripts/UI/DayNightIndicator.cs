using Polyjam_2022;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class DayNightIndicator : MonoBehaviour
    {
        [Inject]
        private void Init(IDayNightCycle dayNightCycle, TextMeshProUGUI text)
        {
            dayNightCycle.OnCycleChanged += cycle =>
            {
                //TODO: remove this
                if (cycle.IsDay)
                {
                    text.text = "Day";
                    FindObjectOfType<Light>().color = Color.white;
                }
                else
                {
                    text.text = "Night";
                    FindObjectOfType<Light>().color = Color.black;
                }
            };
        }
    }
}