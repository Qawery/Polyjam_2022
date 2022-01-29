using Zenject;

namespace Polyjam_2022
{
	public class BuildingPhantomInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<ITriggerEventBroadcaster>().FromComponentInChildren();
			Container.Bind<IBoundsProvider>().FromComponentInChildren();
		}
	}
}