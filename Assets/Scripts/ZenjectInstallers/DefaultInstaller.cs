using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class DefaultInstaller : MonoInstaller
	{
		[SerializeField] private LifecycleManager lifecycleManager;

		public DefaultInstaller(LifecycleManager lifecycleManager)
		{
			this.lifecycleManager = lifecycleManager;
		}
		
		public override void InstallBindings()
		{
			Container.Bind<ILifecycleManager>().FromInstance(lifecycleManager).AsSingle();
			Container.Bind<IGameLoopManager>().To<GameLoopManager>().FromNewComponentOnNewGameObject().AsSingle();
			Container.Bind<IWorld>().To<LocalWorld>().FromNewComponentOnNewGameObject().AsSingle();
		}
	}
}