using UnityEngine;

namespace Lifecycle
{
	public interface ILifecycleManager
	{
		void HandleObjectSpawn(GameObject spawnedObject);
		void HandleObjectDestruction(GameObject spawnedObject);
		void ExecuteNextPhase();
	}
}