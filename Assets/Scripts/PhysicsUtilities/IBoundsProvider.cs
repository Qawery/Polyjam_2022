using UnityEngine;

namespace Polyjam_2022
{
	public interface IBoundsProvider
	{
		Bounds Bounds { get; }
	}
}