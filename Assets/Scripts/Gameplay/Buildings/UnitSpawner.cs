using System.Collections.Generic;
using Lifecycle;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class UnitSpawner : MonoBehaviour, IUnitSpawner
    {
        [SerializeField] private Unit unitPrefab = null;
        [Inject] private List<SpawnPoint> spawnPoints = null;
        [Inject] private IWorld world = null;
        private int spawnsQueued = 0;
        
        public void QueueUnitSpawn()
        {
            spawnsQueued++;
        }

        private void Update()
        {
            if (spawnsQueued == 0)
            {
                return;
            }
            
            foreach (var spawnPoint in spawnPoints)
            {
                if (!spawnPoint.IsTaken)
                {
                    world.Instantiate(unitPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    --spawnsQueued;
                    if (spawnsQueued == 0)
                    {
                        return;
                    }
                }
            }
        }
    }
}