using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class BuildingPhantom : MonoBehaviour
	{
		private TriggerColliderEventBroadcaster triggerColliderEventBroadcaster;
		private TriggerOverlapDetector buildingPhantomOverlapDetector;
		
		public Collider Collider => triggerColliderEventBroadcaster.Collider;
		public bool CanBePlaced => buildingPhantomOverlapDetector.OverlappingColliderCount == 0;

		public event System.Action<bool> OnPlacementPossibilityChanged;
			
		[Inject]
		private void Init(TriggerColliderEventBroadcaster triggerColliderEventBroadcaster)
		{
			this.triggerColliderEventBroadcaster = triggerColliderEventBroadcaster;
			buildingPhantomOverlapDetector = new TriggerOverlapDetector(triggerColliderEventBroadcaster);

			triggerColliderEventBroadcaster.OnTriggerEnterEvent += collider =>
			{
				OnPlacementPossibilityChanged?.Invoke(false);
			};
			
			triggerColliderEventBroadcaster.OnTriggerExitEvent += collider =>
			{
				if (CanBePlaced)
				{
					OnPlacementPossibilityChanged?.Invoke(true);
				}
			};
		}
	}
}