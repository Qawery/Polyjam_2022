using UnityEngine;
using UnityEngine.EventSystems;

namespace Polyjam_2022
{
    public class QueueSpawnOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponent<IUnitSpawner>().QueueUnitSpawn();
        }
    }
}