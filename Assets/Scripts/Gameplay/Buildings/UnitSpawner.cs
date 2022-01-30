using UnityEngine;

namespace Polyjam_2022
{
    public class UnitSpawner : MonoBehaviour, IUnitSpawner
    {
        public void QueueUnitSpawn()
        {
            Debug.Log("Unit spawn queued");
        }
    }
}