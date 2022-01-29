using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Lifecycle
{
	public class UpdateLoop
	{
		private float tickTimer = 0;
		private readonly List<TickDelegate> tickDelegates = new List<TickDelegate>();

		public bool IsEnabled { get; set; }
		public float TickTime { get; set; }
		public string Name { get; }

		public UpdateLoop(string name)
		{
			Name = name;
		}

		public void Tick(float deltaTime)
		{
			tickTimer += deltaTime;
			if (tickTimer > TickTime)
			{
				foreach (var tickDelegate in tickDelegates)
				{
					tickDelegate.Invoke(tickTimer);
				}

				tickTimer = 0;
			}
		}

		public void RegisterTickMethod(TickDelegate tickDelegate)
		{
			Assert.IsNotNull(tickDelegate);
			tickDelegates.Add(tickDelegate);
		}

		public void RemoveTickMethod(TickDelegate tickDelegate)
		{
			Assert.IsNotNull(tickDelegate);
			tickDelegates.Remove(tickDelegate);
		}
	}
}