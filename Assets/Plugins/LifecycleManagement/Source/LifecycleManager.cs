using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Lifecycle
{
	public class LifecycleManager : MonoBehaviour, ILifecycleManager, IGameObjectLifecycleRegister
	{
		[SerializeField] private bool runOnStart = false;
		[SerializeField] private DiInitializer diInitializer = null;
		[SerializeField] private LifecyclePhaseProvider phaseProvider = null;
		[SerializeField] private ObjectLifecycleInterfaceProvider objectLifecycleInterfaceProvider = null;
		
		private Queue<LifecyclePhase> lifecyclePhases = null;
		private LifecyclePhase currentPhase = null;

		private readonly List<System.Action<GameObject>> gameObjectInitActions = new List<Action<GameObject>>();
		private readonly List<System.Action<GameObject>> gameObjectDestroyActions = new List<Action<GameObject>>();
		private bool isInitialized = false;
		
		public void Initialize()
		{
			if (isInitialized) return;
			Assert.IsNotNull(diInitializer);
			Assert.IsNotNull(phaseProvider);
			var container = diInitializer.InitializeDi(this);
			SetupLifecyclePhases(container);
			objectLifecycleInterfaceProvider.RegisterGameObjectLifecycleInterfaces(this);
			ExecuteNextPhase();
			isInitialized = true;
		}
		
		public void HandleObjectSpawn(GameObject spawnedObject)
		{
			foreach (var initAction in gameObjectInitActions)
			{
				initAction(spawnedObject);
			}
		}

		public void HandleObjectDestruction(GameObject destroyedObject)
		{
			foreach (var destroyAction in gameObjectDestroyActions)
			{
				destroyAction(destroyedObject);
			}
		}

		public void ExecuteNextPhase()
		{
			if (lifecyclePhases.Count == 0)
				return;
			
			currentPhase?.FinishPhase();
			currentPhase = lifecyclePhases.Dequeue();
			currentPhase.StartPhase();
			if (!currentPhase.IsManuallyFinished)
			{
				ExecuteNextPhase();
			}
		}
		
		public void RegisterInitializationInterface<InterfaceType>(Action<InterfaceType> initMethod)
		{
			gameObjectInitActions.Add(initializedObject =>
			{
				ComponentHelpers.InvokeOnComponentsInChildren(initializedObject, initMethod);
			});
		}

		public void RegisterDestructionInterface<InterfaceType>(Action<InterfaceType> destroyMethod)
		{
			gameObjectDestroyActions.Add(destroyedObject =>
			{
				ComponentHelpers.InvokeOnComponentsInChildren(destroyedObject, destroyMethod);
			});
		}
		
		private void SetupLifecyclePhases(DiContainer diContainer)
		{
			lifecyclePhases = new Queue<LifecyclePhase>();
			var phaseList = phaseProvider.GetLifecyclePhases(diContainer);
			foreach (var phase in phaseList)
			{
				lifecyclePhases.Enqueue(phase);
			}
		}

		private void Start()
		{
			if (runOnStart)
			{
				Initialize();
			}
		}
	}
}