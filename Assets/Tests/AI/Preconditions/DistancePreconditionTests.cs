using NUnit.Framework;
using UnityEngine;

namespace Polyjam_2022.Tests
{
    public class DistancePreconditionTests
    {
        [Test]
        public void DistancePreconditionTest()
        {
            var sourcePosition = new MockPositionProvider();
            var targetPosition = new MockPositionProvider();
            var distancePrecondition = new DistancePrecondition(sourcePosition, targetPosition, 1.0f);

            Assert.IsTrue(distancePrecondition.IsSatisified());
            targetPosition.Position = new Vector3(0.0f, 0.0f, 5.0f);
            Assert.IsFalse(distancePrecondition.IsSatisified());
        }
    }

    public class MockPositionProvider : IPositionProvider
    {
        public Vector3 Position { get; set; } = Vector3.zero;
    }
}