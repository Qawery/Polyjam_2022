using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lifecycle
{
	public abstract class LifecyclePhaseProvider : ScriptableObject
	{
		public abstract IEnumerable<LifecyclePhase> GetLifecyclePhases(DiContainer diContainer);
	}
}