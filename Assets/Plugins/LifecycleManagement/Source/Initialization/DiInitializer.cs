using UnityEngine;
using Zenject;

namespace Lifecycle
{
	public abstract class DiInitializer : ScriptableObject
	{
		public abstract DiContainer InitializeDi(ILifecycleManager lifecycleManager);
	}
}