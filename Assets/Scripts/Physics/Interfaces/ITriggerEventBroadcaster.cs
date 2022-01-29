using System;
using UnityEngine;

namespace Polyjam_2022
{
	public interface ITriggerEventBroadcaster
	{
		event Action<Collider> OnTriggerEnterEvent;
		event Action<Collider> OnTriggerExitEvent;
	}
}