using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	[CreateAssetMenu(menuName = "DefaultDiInitializer", fileName = "DefaultDiInitializer")]
	public class DefaultDiInitializer : DiInitializer
	{
		public override DiContainer InitializeDi(ILifecycleManager lifecycleManager)
		{
			var sceneContext = FindObjectOfType<SceneContext>();
			if (sceneContext == null)
			{
				Debug.Log("No scene context found. Spawning a new one");
				sceneContext = RunnableContext
					.CreateComponent<SceneContext>(new GameObject("SceneDiContext"));
			}

			sceneContext.Install();
			return sceneContext.Container;
		}
	}
}