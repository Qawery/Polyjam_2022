using UnityEngine;

namespace Lifecycle
{
	public static class ComponentHelpers
	{
		public static void InvokeOnComponentsInChildren<ComponentType>(GameObject rootObject,
			System.Action<ComponentType> action)
		{
			var components = rootObject.GetComponentsInChildren<ComponentType>();
			foreach (var component in components)
			{
				action(component);
			}
		}
	}
}