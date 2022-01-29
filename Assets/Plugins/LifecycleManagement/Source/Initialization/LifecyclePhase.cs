namespace Lifecycle
{
	public abstract class LifecyclePhase
	{
		public abstract bool IsManuallyFinished { get; }
		public event System.Action OnPhaseStarted;
		public event System.Action OnPhaseFinished;

		public virtual void StartPhase()
		{
			OnPhaseStarted?.Invoke();
		}

		public virtual void FinishPhase()
		{
			OnPhaseFinished?.Invoke();
		}
	}
}