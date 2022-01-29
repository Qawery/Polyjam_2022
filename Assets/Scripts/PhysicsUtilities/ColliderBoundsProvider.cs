using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class ColliderBoundsProvider : MonoBehaviour, IBoundsProvider
	{
		[Inject] private Collider collider = null;
		
		public Bounds Bounds => collider.bounds;
	}
}