﻿using Lifecycle;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Polyjam_2022
{
	public class LocalWorld : MonoBehaviour, IWorld
	{
		private DiContainer mainContainer = null;
		private IGameLoopManager gameLoopManager = null;
		private ILifecycleManager lifecycleManager = null;

		public event System.Action<GameObject> OnObjectSpawned;
		
		public void Initialize()
		{
			foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
			{
				ProcessSpawnedObject(rootGameObject);
			}
		}

		public GameObject Instantiate(GameObject prefab, Transform parent = null)
		{
			return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			var spawnedObject = Object.Instantiate(prefab, position, rotation, parent);
			ProcessSpawnedObject(spawnedObject);
			OnObjectSpawned?.Invoke(spawnedObject);
			return spawnedObject;
		}

		public new ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : MonoBehaviour
		{
			return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public new ComponentType Instantiate<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation,
			Transform parent = null) where ComponentType : MonoBehaviour
		{
			return Instantiate(prefab.gameObject, position, rotation, parent).GetComponent<ComponentType>();
		}

		public void Destroy(GameObject destroyedObject)
		{
			gameLoopManager.HandleObjectDestruction(destroyedObject);
			lifecycleManager.HandleObjectDestruction(destroyedObject);
			Object.Destroy(destroyedObject);
		}

		[Inject]
		private void Init(IGameLoopManager gameLoopManager, ILifecycleManager lifecycleManager, DiContainer container)
		{
			this.mainContainer = container;
			this.gameLoopManager = gameLoopManager;
			this.lifecycleManager = lifecycleManager;
		}
		
		private void ProcessSpawnedObject(GameObject spawnedObject)
		{
			var gameObjectContexts = spawnedObject.GetComponentsInChildren<GameObjectContext>();
			if (gameObjectContexts.Length == 0)
			{
				mainContainer.InjectGameObject(spawnedObject);
			}
			else
			{
				foreach (var context in gameObjectContexts)
				{
					context.Construct(mainContainer);
					context.Run();
				}
			}
			lifecycleManager.HandleObjectSpawn(spawnedObject);
			gameLoopManager.HandleObjectSpawn(spawnedObject);
		}
	}
}