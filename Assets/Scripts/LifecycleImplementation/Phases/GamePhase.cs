using Lifecycle;
using Zenject;

namespace Polyjam_2022
{
	public class GamePhase : LifecyclePhase
	{
		[Inject] private IGameLoopManager gameLoopManager = null;
		
		public override bool IsManuallyFinished => true;

		public override void StartPhase()
		{
			base.StartPhase();
			gameLoopManager.RegisterUpdateLoop(new UpdateLoop(UpdateLoopIds.MAIN_LOOP_NAME), false);
			gameLoopManager.RegisterUpdateLoop(new UpdateLoop(UpdateLoopIds.MAIN_PHYSICS_LOOP_NAME), true);
		}
	}
}