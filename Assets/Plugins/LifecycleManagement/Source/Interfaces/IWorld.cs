using UnityEngine;

namespace Lifecycle
{
	public interface IWorld
	{
		event System.Action<GameObject> OnObjectSpawned;
		void Initialize();
		GameObject Instantiate(GameObject prefab, Transform parent = null);
		GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
		ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : MonoBehaviour;
		ComponentType Instantiate<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation, Transform parent = null) where ComponentType : MonoBehaviour;
		void Destroy(GameObject gameObject);
	}
}