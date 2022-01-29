using UnityEngine;

namespace Lifecycle
{
	public interface IGameLoopManager
	{
		void RegisterUpdateLoop(UpdateLoop updateLoop, bool fixedUpdate);
		void RemoveUpdateLoop(UpdateLoop updateLoop);
		void HandleObjectSpawn(GameObject spawnedObject);
		void HandleObjectDestruction(GameObject destroyedObject);
	}
}