using UnityEngine;

namespace Polyjam_2022
{
    public interface IPositionProvider
    {
        public Vector3 Position { get; }
    }
}