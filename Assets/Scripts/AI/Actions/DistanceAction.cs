using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public abstract class DistanceAction : Action
    {
        private readonly float distance;

        public float Distance => distance;

        public DistanceAction(float distance)
        {
            Assert.IsTrue(distance >= 0.0f);
            this.distance = distance;
        }
    }
}