using UnityEngine;

namespace Polyjam_2022
{
    public interface ILayerManager
    {
        LayerMask GroundLayerMask { get; }
        LayerMask UnitLayerMask { get; }
        LayerMask BuildingLayerMask{ get; }
    }
}

