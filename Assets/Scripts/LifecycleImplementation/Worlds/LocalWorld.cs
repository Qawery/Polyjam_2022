using Lifecycle;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Polyjam_2022
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