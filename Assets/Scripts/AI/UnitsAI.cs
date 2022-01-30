using Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
    public class UnitsAI : MonoBehaviour
    {
        [Inject] private IWorld world;

        List<GameObject> units = new List<GameObject>();

        [Inject]
        private void Init(IWorld world)
        {
            this.world = world;
            world.OnObjectSpawned += World_OnObjectSpawned;
        }

        private void World_OnObjectSpawned(GameObject gameObject)
        {
            if (gameObject.GetComponent<Unit>() != null)
            {
                units.Add(gameObject);
            }
        }

        private void Update()
        {
            
        }
    }
}
