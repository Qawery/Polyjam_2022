using Lifecycle;
using Zenject;

namespace Polyjam_2022
{
	public class WorldInitializationPhase : LifecyclePhase
	{
		[Inject] private IWorld world = null;
		[Inject] private ResourceRandomizer resourceRandomizer = null;
		public override bool IsManuallyFinished => false;

		public override void StartPhase()
		{
			base.StartPhase();
			world.Initialize();
			resourceRandomizer.Randomize();
		}
	}
}