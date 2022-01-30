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
        private float timeSinceLastUpdate = 0;
        [SerializeField] private float updateInterval = 0.1f;
        [Inject] private IWorld world;
        private ResourceGatheringAI gatheringAI;
       
        HashSet<Unit> availableUnits = new HashSet<Unit>();
        HashSet<Unit> busyUnits = new HashSet<Unit>();

        List<Task> activeTasks = new List<Task>();

        [Inject]
        private void Init(IWorld world)
        {
            this.world = world;
            this.gatheringAI = new ResourceGatheringAI();
            world.OnObjectSpawned += World_OnObjectSpawned;
        }

        private void World_OnObjectSpawned(GameObject gameObject)
        {
            var unit = gameObject.GetComponent<Unit>();
            if (unit  != null)
            {
                availableUnits.Add(unit);
            }
            else if(gameObject.GetComponent<ConstructionSite>() != null)
            {
                OnConstructionSiteSpawn(gameObject);
            }
            else if (gameObject.GetComponent<ResourceSource>() != null)
            {
                gatheringAI.AddResourceSource(gameObject.GetComponent<ResourceSource>());
     
            }
        }

        private void OnConstructionSiteSpawn(GameObject gameObject)
        {
            var constructionSite = gameObject.GetComponent<ConstructionSite>();

            foreach(var requirement in constructionSite.BuildingData.ConstructionResourceRequirements)
            {
                gatheringAI.AddRequest(new ResourceRequest()
                {
                    Amount = requirement.RequiredAmount,
                    ResourceType = requirement.ResourceType,
                    Destination = constructionSite,
                    Priority = 0
                });
            }
        }

        private void Update()
        {
            foreach(var currentTask in activeTasks)
            {
                currentTask.Execute(Time.deltaTime);
            }
            timeSinceLastUpdate += Time.deltaTime;
            if (timeSinceLastUpdate < updateInterval)
                return;

            timeSinceLastUpdate = 0;

            if (availableUnits.Count == 0)
                return;

            var task = gatheringAI.GetNextGatheringTask(availableUnits);
            if (task.Item1 == null)
                return;

            availableUnits.Remove(task.Item2);
            busyUnits.Add(task.Item2);

            activeTasks.Add(task.Item1);
        }
    }
}
