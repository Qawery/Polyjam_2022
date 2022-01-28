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

		public TriggerOverlapDetector(TriggerColliderEventBroadcaster triggerColliderEventBroadcaster)
		{
			triggerColliderEventBroadcaster.OnTriggerEnterEvent += other =>
			{
				overlappingColliders.Add(other);
			};
			
			triggerColliderEventBroadcaster.OnTriggerExitEvent += other =>
			{
				overlappingColliders.Add(other);
			};
		}
	}
}