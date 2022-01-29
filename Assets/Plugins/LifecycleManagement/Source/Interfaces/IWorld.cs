using UnityEngine;

namespace Lifecycle
{
	public interface IWorld
	{
		void Initialize();
		GameObject Instantiate(GameObject prefab, Transform parent = null);

		GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
		
		ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : MonoBehaviour;
		ComponentType Instantiate<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation, Transform parent = null) where ComponentType : MonoBehaviour;
		
		Class CreateInstance<Class>();
		void Destroy(GameObject gameObject);
	}
}