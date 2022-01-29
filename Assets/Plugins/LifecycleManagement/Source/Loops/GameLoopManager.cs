using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;


namespace Lifecycle
{
	public class GameLoopManager : MonoBehaviour, IGameLoopManager
	{
		private readonly List<UpdateLoop> updateLoops = new List<UpdateLoop>();
		private readonly List<UpdateLoop> fixedUpdateLoops = new List<UpdateLoop>();
		private readonly SortedDictionary<string, UpdateLoop> nameToLoop = new SortedDictionary<string, UpdateLoop>();

		private readonly Dictionary<string, List<TickDelegate>> waitingTickDelegates =
			new Dictionary<string, List<TickDelegate>>();
		
		public void RegisterUpdateLoop(UpdateLoop updateLoop, bool fixedUpdate)
		{
			Assert.IsNotNull(updateLoop);
			Assert.IsFalse(nameToLoop.ContainsKey(updateLoop.Name));
			if (fixedUpdate)
			{
				fixedUpdateLoops.Add(updateLoop);
			}
			else
			{
				updateLoops.Add(updateLoop);
			}

			updateLoop.IsEnabled = true;
			nameToLoop[updateLoop.Name] = updateLoop;

			if (waitingTickDelegates.TryGetValue(updateLoop.Name, out var ticksWaitingForThisLoop))
			{
				foreach (TickDelegate tickDelegate in ticksWaitingForThisLoop)
				{
					updateLoop.RegisterTickMethod(tickDelegate);
				}
			}
		}

		public void RemoveUpdateLoop(UpdateLoop updateLoop)
		{
			Assert.IsNotNull(updateLoop);
			updateLoops.Remove(updateLoop);
			fixedUpdateLoops.Remove(updateLoop);
			nameToLoop.Remove(updateLoop.Name);
		}

		public void HandleObjectSpawn(GameObject spawnedObject)
		{
			ITickable[] tickables = spawnedObject.GetComponentsInChildren<ITickable>();
			foreach (var tickable in tickables)
			{
				foreach (var (loopName, tickDelegate) in tickable.TickDelegates)
				{
					if (nameToLoop.TryGetValue(loopName, out var updateLoop))
					{
						updateLoop.RegisterTickMethod(tickDelegate);
					}
					else
					{
						if (!waitingTickDelegates.TryGetValue(loopName, out var ticksWaitingToRegister))
						{
							ticksWaitingToRegister = new List<TickDelegate>();
							waitingTickDelegates.Add(loopName, ticksWaitingToRegister);
						}
						ticksWaitingToRegister.Add(tickDelegate);
					}
				}
			}
		}

		public void HandleObjectDestruction(GameObject destroyedObject)
		{
			ITickable[] tickables = destroyedObject.GetComponentsInChildren<ITickable>();
			foreach (var tickable in tickables)
			{
				foreach (var (loopName, tickDelegate) in tickable.TickDelegates)
				{
					if (nameToLoop.TryGetValue(loopName, out var updateLoop))
					{
						updateLoop.RemoveTickMethod(tickDelegate);
					}
				}
			}
		}

		public void SetUpdateLoopActive(string loopName, bool active)
		{
			if (nameToLoop.TryGetValue(loopName, out var updateLoop))
			{
				updateLoop.IsEnabled = active;
			}
			else
			{
				Debug.LogError($"Trying to access a non-existent update loop {loopName}. " +
				               $"Available loops: {BuildLoopNameString()}");
			}
		}

		private void Update()
		{
			foreach (var updateLoop in updateLoops)
			{
				if (!updateLoop.IsEnabled)
					continue;
				updateLoop.Tick(Time.deltaTime);
			}
		}
		
		private void FixedUpdate()
		{
			foreach (var updateLoop in fixedUpdateLoops)
			{
				if (!updateLoop.IsEnabled)
					continue;
				updateLoop.Tick(Time.fixedDeltaTime);
			}
		}

		private string BuildLoopNameString()
		{
			StringBuilder loopNameListBuilder = new StringBuilder();
			loopNameListBuilder.Append("[");
			for (var index = 0; index < updateLoops.Count; index++)
			{
				var existingLoop = updateLoops[index];
				loopNameListBuilder.Append(existingLoop.Name);

				if (index <= updateLoops.Count - 1)
				{
					loopNameListBuilder.Append(", ");
				}
			}

			loopNameListBuilder.Append("]");
			return loopNameListBuilder.ToString();
		}
	}
}