using UnityEngine;

namespace Lifecycle
{
	public abstract class ObjectLifecycleInterfaceProvider : ScriptableObject
	{
		public abstract void RegisterGameObjectLifecycleInterfaces(IGameObjectLifecycleRegister lifecycleRegister);
	}
}