using Lifecycle;
using UnityEngine;

namespace Polyjam_2022
{
	[CreateAssetMenu(menuName = "LifecycleInterfaceProvider", fileName = "LifecycleInterfaceProvider")]
	public class DefaultObjectLifecycleInterfaceProvider : ObjectLifecycleInterfaceProvider
	{
		public override void RegisterGameObjectLifecycleInterfaces(IGameObjectLifecycleRegister lifecycleRegister)
		{
			lifecycleRegister.RegisterInitializationInterface<IInitializeComponent>(init => init.InitializeComponent());
			lifecycleRegister.RegisterInitializationInterface<ISpawnHandler>(handler => handler.OnSpawned());
		}
	}
}
