using System.Collections.Generic;
using Lifecycle;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using ITickable = Lifecycle.ITickable;

namespace Polyjam_2022
{
    public class BuildingPlacementCursor : MonoBehaviour, ITickable
    {
        private bool isActive = false;
        private IBuildingPlacer placer;
        private IGroundDetector groundDetector;
        
        public IEnumerable<(string updateLoopName, TickDelegate tickDelegate)> TickDelegates =>
            new List<(string updateLoopName, TickDelegate tickDelegate)>()
            {
                (UpdateLoopIds.MAIN_LOOP_NAME, Tick)
            };

        [Inject]
        private void Init(IBuildingPlacer buildingPlacer, IGroundDetector detector)
        {
            buildingPlacer.OnBuildingSelectionChanged += data =>
            {
                isActive = data != null;
            };
            placer = buildingPlacer;
            groundDetector = detector;
        }

        private void Tick(float deltaTime)
        {
            if (!isActive || !groundDetector.IsMouseOnGround)
            {
                return;
            }

            placer.MovePhantomToPosition(groundDetector.MousePositionOnGround);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                placer.TryPlaceBuildingAtCurrentPosition();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                placer.Release();
            }
        }
    }
}