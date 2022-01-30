using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public abstract class PlacedObject : MonoBehaviour
    {
        private bool init = false;
        private float offset;
        [Inject] private readonly IBoundsProvider boundsProvider = null;

        public void PlaceBaseAtPosition(Vector3 position)
        {
            if (!init)
            {
                init = true;
                offset = transform.position.y - boundsProvider.Bounds.min.y;
            }
            
            transform.position = position + offset * Vector3.up;
        }
    }
}