using System;
using Lifecycle;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace FS
{
	public class LocalWorld : MonoBehaviour, IWorld
	{
		[Inject] private IGameLoopManager gameLoopManager = null;
		[Inject] private ILifecycleManager lifecycleManager = null;
		[Inject] private DiContainer container = null;

		public event System.Action<GameObject> OnObjectSpawned;
		
		public void Initialize()
		{
			var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			foreach (var rootGameObject in rootGameObjects)
			{
				ProcessSpawnedObject(rootGameObject);
			}
		}

		public void Instantiate(GameObject prefab, Transform parent = null, Action<GameObject> spawnCallback = null)
		{
			Instantiate(prefab, Vector3.zero, Quaternion.identity, parent, spawnCallback);
		}

		public void Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null,
			Action<GameObject> spawnCallback = null)
		{
			var spawnedObject = Object.Instantiate(prefab, position, rotation, parent);
			ProcessSpawnedObject(spawnedObject);
			spawnCallback?.Invoke(spawnedObject);
			OnObjectSpawned?.Invoke(spawnedObject);
		}
		
		public void Destroy(GameObject destroyedObject)
		{
			gameLoopManager.HandleObjectDestruction(destroyedObject);
			lifecycleManager.HandleObjectDestruction(destroyedObject);
			Object.Destroy(destroyedObject);
		}
		
		public Class CreateInstance<Class>()
		{
			return container.Instantiate<Class>();
		}
		
		private void ProcessSpawnedObject(GameObject spawnedObject)
		{
			container.InjectGameObject(spawnedObject);
			lifecycleManager.HandleObjectSpawn(spawnedObject);
			gameLoopManager.HandleObjectSpawn(spawnedObject);
		}
	}
}