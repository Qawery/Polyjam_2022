using NUnit.Framework;
using UnityEngine;

namespace Polyjam_2022.Tests
{
    public class DistancePreconditionTests
    {
        [Test]
        public void DistancePreconditionTest()
        {
            var source = new GameObject();
            var target = new GameObject();
            var distancePrecondition = new DistancePrecondition(source.transform, target.transform, 1.0f);

            target.transform.position = new Vector3(0.0f, 0.0f, 2.0f);
            Assert.IsFalse(distancePrecondition.IsSatisified());
            target.transform.position = Vector3.zero;
            Assert.IsTrue(distancePrecondition.IsSatisified());
            GameObject.DestroyImmediate(target);
            Assert.IsFalse(distancePrecondition.IsSatisified());
        }
    }
}