using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public abstract class DistanceCondition : Condition
    {
        public readonly IPositionProvider source;
        public readonly IPositionProvider target;
        public readonly float distance;

        public DistanceCondition(IPositionProvider source, IPositionProvider target, float distance)
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
            return source != null && target != null;
        }
    }

    public class CloserThan : DistanceCondition
    {
        public CloserThan(IPositionProvider source, IPositionProvider target, float distance) : base(source, target, distance)
        {
        }

        public override bool IsSatisified()
        {
            return base.IsSatisified() && Vector3.Distance(source.Position, target.Position) < distance;
        }
    }

    public class FurtherThan : DistanceCondition
    {
        public FurtherThan(IPositionProvider source, IPositionProvider target, float distance) : base(source, target, distance)
        {
        }

        public override bool IsSatisified()
        {
            return base.IsSatisified() && Vector3.Distance(source.Position, target.Position) > distance;
        }
    }
}