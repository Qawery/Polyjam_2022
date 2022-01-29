using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class DistancePrecondition : Precondition
    {
        private readonly Transform source;
        private readonly Transform target;
        private readonly float distance;

        public DistancePrecondition(Transform source, Transform target, float distance)
        {
            Assert.IsNotNull(source);
            Assert.IsNotNull(target);
            Assert.IsTrue(distance >= 0.0f);
            this.source = source;
            this.target = target;
            this.distance = distance;
        }

        public override bool IsSatisified()
        {
            return (source != null && target != null) ? Vector3.Distance(source.transform.position, target.transform.position) <= distance : false;
        }
    }
}