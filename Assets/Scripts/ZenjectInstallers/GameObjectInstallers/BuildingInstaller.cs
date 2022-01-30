using Zenject;

namespace Polyjam_2022
{
	public class BuildingInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<IUnitSpawner>().To<UnitSpawner>().FromComponentSibling();
			Container.Bind<IBoundsProvider>().To<ColliderBoundsProvider>().FromComponentSibling();
			Container.Bind<ITriggerEventBroadcaster>().To<TriggerEventBroadcaster>().FromComponentSibling();
			Container.Bind<SpawnPoint>().FromComponentsInChildren();
		}
	}
}