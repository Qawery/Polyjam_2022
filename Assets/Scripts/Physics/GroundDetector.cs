using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class GroundDetector : MonoBehaviour, IGroundDetector
    {
        [Inject] private ILayerManager layerManager = null;
        
        public bool IsMouseOnGround { get; private set; }
        public Vector3 MousePositionOnGround { get; private set; }

        private void Update()
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, float.MaxValue,
                    layerManager.GroundLayerMask))
            {
                IsMouseOnGround = false;
                return;
            }
            
            IsMouseOnGround = true;
            MousePositionOnGround = hitInfo.point;
        }
    }
}