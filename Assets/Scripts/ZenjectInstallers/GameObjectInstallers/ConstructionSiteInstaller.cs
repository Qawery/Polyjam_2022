using Zenject;

namespace Polyjam_2022
{
    public class ConstructionSiteInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBoundsProvider>().To<ColliderBoundsProvider>().FromComponentSibling();
        }
    }
}