using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Zenject;

namespace Polyjam_2022
{
    public class BuildingButton : MonoBehaviour
    {
        [Inject] private TextMeshProUGUI nameText = null;

        public BuildingData BuildingData { private get; set; }
        public TextMeshProUGUI NameText => nameText;

        [Inject]
        private void Init(IBuildingDataReceiver buildingDataReceiver, Button button, TextMeshProUGUI nameText)
        {
            this.nameText = nameText;
            button.onClick.AddListener(() =>
            {
                Assert.IsNotNull(BuildingData);
                buildingDataReceiver.SetBuildingData(BuildingData);
            });
        }
    }
}