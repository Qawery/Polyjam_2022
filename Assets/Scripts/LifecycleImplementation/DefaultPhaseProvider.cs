using System.Collections.Generic;
using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	[CreateAssetMenu(menuName = "DefaultPhaseProvider", fileName = "DefaultPhaseProvider")]
	public class DefaultPhaseProvider : LifecyclePhaseProvider
	{
		public override IEnumerable<LifecyclePhase> GetLifecyclePhases(DiContainer diContainer) =>
			new List<LifecyclePhase>()
			{
				diContainer.Instantiate<WorldInitializationPhase>(),
				diContainer.Instantiate<GamePhase>(),
				diContainer.Instantiate<SummaryPhase>(),
				diContainer.Instantiate<ReleaseScene>()
			};
	}
}