using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class ColliderBoundsProvider : MonoBehaviour, IBoundsProvider
	{
		[Inject] private List<Collider> colliders = null;
		
		public Bounds Bounds
		{
			get
			{
				var bounds = new Bounds (transform.position , Vector3.zero);
				foreach (Collider col in colliders)
				{
					bounds.Encapsulate(col.bounds);
				}

				return bounds;
			}
		}
	}
}