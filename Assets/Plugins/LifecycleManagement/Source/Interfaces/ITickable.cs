using System.Collections.Generic;

namespace Lifecycle
{
	public delegate void TickDelegate(float deltaTime);

	public interface ITickable
	{
		IEnumerable<(string updateLoopName, TickDelegate tickDelegate)> TickDelegates { get; }
	}
}