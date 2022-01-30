using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Polyjam_2022
{
    public class BuildingButtonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Button>().FromComponentInChildren();
            Container.Bind<TextMeshProUGUI>().FromComponentInChildren();
            Container.Bind<Image>().FromComponentSibling();
        }
    }
}