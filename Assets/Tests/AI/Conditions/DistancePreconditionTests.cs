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
            var furtherThanCondition = new FurtherThan(sourcePosition, targetPosition, 1.0f);
            var closerThanCondition = new CloserThan(sourcePosition, targetPosition, 1.0f);

            Assert.IsFalse(furtherThanCondition.IsSatisified());
            Assert.IsTrue(closerThanCondition.IsSatisified());
            targetPosition.Position = new Vector3(0.0f, 0.0f, 5.0f);
            Assert.IsTrue(furtherThanCondition.IsSatisified());
            Assert.IsFalse(closerThanCondition.IsSatisified());
        }
    }

    public class MockPositionProvider : IPositionProvider
    {
        public Vector3 Position { get; set; } = Vector3.zero;
    }
}