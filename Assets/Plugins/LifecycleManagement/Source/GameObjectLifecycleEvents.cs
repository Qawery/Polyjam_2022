using UnityEngine;

namespace Lifecycle
{
	public class GameObjectLifecycleEvents : MonoBehaviour
	{
		public event System.Action EnableEvent;
		public event System.Action DisableEvent;
		public event System.Action DestructionEvent;

		private void OnEnable()
		{
			EnableEvent?.Invoke();
		}

		private void OnDisable()
		{
			DisableEvent?.Invoke();
		}

		private void OnDestroy()
		{
			DestructionEvent?.Invoke();
		}
	}
}