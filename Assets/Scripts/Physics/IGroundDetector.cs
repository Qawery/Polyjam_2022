using UnityEngine;

namespace Polyjam_2022
{
    public interface IGroundDetector
    {
        bool IsMouseOnGround { get; }
        Vector3 MousePositionOnGround { get; }
    }
}