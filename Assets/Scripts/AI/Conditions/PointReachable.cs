using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class PointReachable : Condition
    {
        public PointReachable(Vector3 destinationPoint)
        {
        }

        public override bool IsSatisified()
        {
            //TODO: Check if destination is reachable via Unity pathfinding.
            return true;
        }
    }
}