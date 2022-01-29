using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class DistancePrecondition : Precondition
    {
        private readonly IPositionProvider source;
        private readonly IPositionProvider target;
        private readonly float distance;

        public DistancePrecondition(IPositionProvider source, IPositionProvider target, float distance)
        {
            Assert.IsNotNull(source);
            this.source = source;

            Assert.IsNotNull(target);
            this.target = target;

            Assert.IsTrue(distance >= 0.0f);
            this.distance = distance;
        }

        public override bool IsSatisified()
        {
            return source != null && target != null && Vector3.Distance(source.Position, target.Position) <= distance;
        }
    }
}