using FS;
using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class CommonInstaller : Installer
	{
		private ILifecycleManager lifecycleManager;

		public CommonInstaller(ILifecycleManager lifecycleManager)
		{
			this.lifecycleManager = lifecycleManager;
		}
		
		public override void InstallBindings()
		{
			Container.Bind<ILifecycleManager>().FromInstance(lifecycleManager).AsSingle();
			Container.Bind<IGameLoopManager>().To<GameLoopManager>().FromNewComponentOnNewGameObject().AsSingle();
		}
	}

	public class LocalInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind<IWorld>().To<LocalWorld>().FromNewComponentOnNewGameObject().AsSingle();
		}
	}
}