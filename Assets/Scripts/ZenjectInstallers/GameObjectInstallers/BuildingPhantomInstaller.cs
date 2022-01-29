using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class BuildingPhantomInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<ITriggerEventBroadcaster>().To<TriggerEventBroadcaster>().FromComponentOnRoot();
			Container.Bind<IBoundsProvider>().To<ColliderBoundsProvider>().FromComponentOnRoot();
			Container.Bind<Collider>().FromComponentsInChildren();
			Container.Bind<BuildingPhantom>().FromComponentOnRoot();
			Container.Bind<MeshRenderer>().FromComponentsInChildren();
		}
	}
}