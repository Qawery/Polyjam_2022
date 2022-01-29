using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class BuildingPhantom : MonoBehaviour
	{
		private IBoundsProvider boundsProvider;
		private TriggerOverlapDetector buildingPhantomOverlapDetector;
		
		public bool CanBePlaced => buildingPhantomOverlapDetector.OverlappingColliderCount == 0;
		public float OffsetFromCenterToBase => transform.position.y - boundsProvider.Bounds.min.y;

		public event System.Action<bool> OnPlacementPossibilityChanged;
			
		[Inject]
		private void Init(ITriggerEventBroadcaster triggerEventBroadcaster, IBoundsProvider boundsProvider)
		{
			this.boundsProvider = boundsProvider;
			buildingPhantomOverlapDetector = new TriggerOverlapDetector(triggerEventBroadcaster);

			triggerEventBroadcaster.OnTriggerEnterEvent += col =>
			{
				OnPlacementPossibilityChanged?.Invoke(false);
			};
			
			triggerEventBroadcaster.OnTriggerExitEvent += col =>
			{
				if (CanBePlaced)
				{
					OnPlacementPossibilityChanged?.Invoke(true);
				}
			};
		}
	}
}