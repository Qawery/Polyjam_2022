using System.Collections.Generic;
using UnityEngine;

namespace Polyjam_2022
{
    public class ResourceGatheringAI
    {
        List<ResourceRequest> requests = new List<ResourceRequest>();
        Dictionary<ResourceType, List<ResourceSource>> sources = new Dictionary<ResourceType, List<ResourceSource>>();
        public event System.Action<ResourceRequest, Unit, ResourceSource> OnRequestAssigned;

        public ResourceGatheringAI()
        {
            foreach (var resourceType in ResourceHelpers.GetAllTypes())
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

        public (GatheringResources, Unit) GetNextGatheringTask(IEnumerable<Unit> availableUnits)
        {
            foreach (var request in requests)
            {
                return GenerateTask(sources[request.ResourceType], request, availableUnits);
            }
            return (null, null);
        }

        private (GatheringResources, Unit) GenerateTask(List<ResourceSource> sources, ResourceRequest request, IEnumerable<Unit> availableUnits)
        {
            float lowestPenaltyScore = float.MaxValue;
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

                        var distance = Vector3.Distance(source.transform.position, unit.transform.position) + Vector3.Distance(source.transform.position, request.Destination.Position);
                        var score = (2 - requestSatisfaction) * distance;
                        if (score < lowestPenaltyScore)
                        {
                            bestUnit = unit;
                            bestSource = source;
                        }
                    }
                }
            }
            if (bestUnit == null)
                return (null, null);

            OnRequestAssigned?.Invoke(request, bestUnit, bestSource);

            return (new GatheringResources(bestUnit,
                                           request.Destination,
                                           new List<ResourceType>() { request.ResourceType },
                                            bestSource),
                    bestUnit);
        }
    }
}
