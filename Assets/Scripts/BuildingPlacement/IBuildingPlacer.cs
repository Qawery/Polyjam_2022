using UnityEngine;

namespace Polyjam_2022
{
    public interface IBuildingPlacer
    {
        event System.Action<BuildingData> OnBuildingSelectionChanged;
        void MovePhantomToPosition(Vector3 position);
        bool TryPlaceBuildingAtCurrentPosition();
        void Release();
    }
}