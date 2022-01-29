using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Lifecycle
{
	public class LifecycleManagerTests : MonoBehaviour
	{
		[UnityTest]
		public IEnumerator Test()
		{
			yield return null;
		}
	}
}
