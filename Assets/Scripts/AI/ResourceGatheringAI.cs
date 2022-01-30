using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Polyjam_2022
{
    public class ResourceGatheringAI
    {
        List<ResourceRequest> requests = new List<ResourceRequest>();
        Dictionary<ResourceType, List<ResourceSource>> sources = new Dictionary<ResourceType, List<ResourceSource>>();
        HashSet<Unit> availableUnits = new HashSet<Unit>();
        public event System.Action<ResourceRequest, Unit, ResourceSource> OnRequestAssigned;


        public ResourceGatheringAI()
        {
            foreach(var resourceType in ResourceManager.GetAllTypes())
            {
                sources.Add(resourceType, new List<ResourceSource>());
            }

        }
        public void AddResourceSource(ResourceSource source)
        {
            foreach (var type in source.Resources.SupportedTypes)
            {
                if (sources.TryGetValue(type, out var list))
                { 
                    list.Add(source);
                }
            }
        }

        public void RemoveResourceSource(ResourceSource source)
        {
            foreach (var type in source.Resources.SupportedTypes)
            {
                if (sources.TryGetValue(type, out var list))
                {
                    list.Remove(source);
                }
            }
        }

        public void AddRequest(ResourceRequest request)
        {
            requests.Add(request);
            requests.Sort((x, y) => x.Priority > y.Priority ? 1 : 0);
        }

        public void RemoveRequest(ResourceRequest request)
        {
            requests.Remove(request);
        }

        public void AddAvailableUnit(Unit unit)
        {
            availableUnits.Add(unit);
        }

        public void RemoveAvailableUnit(Unit unit)
        {
            availableUnits.Remove(unit);
        }

        public void Run()
        {
            foreach (var request in requests)
            {
                AssignSource(sources[request.ResourceType], request);
            }
        }

        private void AssignSource(List<ResourceSource> sources, ResourceRequest request)
        {
            float bestDistance = float.MaxValue;
            Unit bestUnit = null;
            ResourceSource bestSource = null;
            foreach (var source in sources)
            {
                if (source.Resources.CurrentTotalAmount > float.Epsilon)
                {
                    foreach (var unit in availableUnits)
                    {
                        float requestSatisfaction = 1.0f;
                        float amountToTake = Mathf.Min(source.Resources.CurrentTotalAmount - source.BlockedAmountPerType[request.ResourceType], unit.Resources.CapacityLeft);
                        if (amountToTake < request.Amount)
                        {
                            requestSatisfaction = amountToTake / request.Amount;
                        }
                        var distance = (2 - requestSatisfaction) * Vector3.Distance(source.transform.position, unit.transform.position);
                        if (distance < bestDistance)
                        {
                            bestUnit = unit;
                            bestSource = source;
                        }
                    }
                }
            }
            if (bestUnit == null)
                return;

            RemoveAvailableUnit(bestUnit);
            OnRequestAssigned?.Invoke(request, bestUnit, bestSource);
        }
    }
}
