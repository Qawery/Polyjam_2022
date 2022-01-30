using TMPro;
using Zenject;

namespace Polyjam_2022
{
    public class BasicIndicatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>().FromComponentSibling();
        }
    }
}