using Lifecycle;

namespace Polyjam_2022
{
    public class SummaryPhase : LifecyclePhase
    {
        public override bool IsManuallyFinished => true;

        public override void StartPhase()
        {
            base.StartPhase();
        }
    }
}