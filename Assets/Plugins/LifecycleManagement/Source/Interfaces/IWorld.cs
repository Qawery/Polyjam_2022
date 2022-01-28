using UnityEngine;

namespace Lifecycle
{
	public interface IWorld
	{
		void Initialize();
		void Instantiate(GameObject prefab, Transform parent = null, System.Action<GameObject> spawnCallback = null);

		void Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null,
			System.Action<GameObject> spawnCallback = null);
		Class CreateInstance<Class>();
		void Destroy(GameObject gameObject);
	}
}