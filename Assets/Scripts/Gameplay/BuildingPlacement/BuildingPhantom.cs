using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class BuildingPhantom : PlacedObject
	{
		private TriggerOverlapDetector buildingPhantomOverlapDetector;
		
		public bool CanBePlaced => buildingPhantomOverlapDetector.OverlappingColliderCount == 0;
		
		public event System.Action<bool> OnPlacementPossibilityChanged;
			
		[Inject]
		private void Init(ITriggerEventBroadcaster triggerEventBroadcaster)
		{
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