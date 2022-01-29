using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class DefaultGlobalsInstaller : MonoInstaller
	{
		[SerializeField] private LifecycleManager lifecycleManager = null;
		[SerializeField] private BuildingDataCollection buildingDataCollection = null;
		[SerializeField] private BuildingPrefabCollection buildingPrefabCollection = null;
		[SerializeField] private LayerManager layerManager = null;

		public DefaultGlobalsInstaller(LifecycleManager lifecycleManager)
		{
			this.lifecycleManager = lifecycleManager;
		}
		
		public override void InstallBindings()
		{
			Container.Bind<ILifecycleManager>().FromInstance(lifecycleManager);
			Container.Bind<ILayerManager>().FromInstance(layerManager);
			Container.Bind<IBuildingDataCollection>().FromInstance(buildingDataCollection);
			Container.Bind<IBuildingPrefabCollection>().FromInstance(buildingPrefabCollection);
			Container.Bind<IGameLoopManager>().To<GameLoopManager>().FromNewComponentOnNewGameObject().AsSingle();
			Container.Bind<IWorld>().To<LocalWorld>().FromNewComponentOnNewGameObject().AsSingle();

			Container.BindInterfacesTo<BuildingPlacer>().AsSingle();
			Container.Bind<IGroundDetector>().To<GroundDetector>().FromNewComponentOnNewGameObject()
				.WithGameObjectName("GroundDetector").AsSingle();
		}
	}
}