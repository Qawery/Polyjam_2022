using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class TriggerOverlapDetector
	{
		private readonly HashSet<Collider> overlappingColliders = new HashSet<Collider>();

		public int OverlappingColliderCount => overlappingColliders.Count;
		public IEnumerable<Collider> OverlappingColliders => overlappingColliders;

		public TriggerOverlapDetector(ITriggerEventBroadcaster triggerEventBroadcaster)
		{
			triggerEventBroadcaster.OnTriggerEnterEvent += other =>
			{
				overlappingColliders.Add(other);
			};
			
			triggerEventBroadcaster.OnTriggerExitEvent += other =>
			{
				overlappingColliders.Remove(other);
			};
		}
	}
}