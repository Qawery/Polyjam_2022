using NUnit.Framework;
using UnityEngine;

namespace Polyjam_2022.Tests
{
    public class PositionFromTransformTests
    {
        [Test]
        public void PositionFromTransformTest()
        {
            var testGameObject = new GameObject();
            var position = testGameObject.AddComponent<PositionFromTransform>();
            testGameObject.transform.position = new Vector3(0.0f, 0.0f, 2.0f);
            Assert.IsTrue(testGameObject.transform.position == position.Position);
        }
    }
}