using UnityEngine;

namespace Polyjam_2022
{
    public class PositionFromTransform : MonoBehaviour, IPositionProvider
    {
        public Vector3 Position => transform.position;
    }
}