using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class GatheringResources : Task
    {
        //TODO: After gathering half capacity prefer to return to drop site than to go long way with a lot of resources.
        //public const float CAPACITY_RATIO_RETURN_THRESHOLD = 0.5f;
        //public const float DISTANCE_TO_DROP_OFF_POINT_MODIFIER = 0.3f;
        
        //Task definition parameters
        private readonly IMobileResourceManipulator resourceManipulator;
        private readonly HashSet<ResourceType> wantedResources = new HashSet<ResourceType>();
        private readonly IResourceLocation resourceDestinationLocation;

        //Execution variables
        private Action goToResourceAction;
        private Action takeResourcesFromSourceAction;
        private Action unloadToBaseAction;
        private Action returnToBaseAction;
        private CloserThan areWeAtBase;

        private ResourceSource currentTargetResourceSource = null;

        public GatheringResources(IMobileResourceManipulator resourceManipulator, IResourceLocation resourceDestinationLocation,
                                    IEnumerable<ResourceType> wantedResources, ResourceSource source = null)
        {
            Assert.IsNotNull(resourceManipulator);
            this.resourceManipulator = resourceManipulator;
            Assert.IsNotNull(wantedResources);
            foreach (var wantedResource in wantedResources)
            {
                Assert.IsTrue(this.wantedResources.Add(wantedResource));
            }
            if (this.wantedResources.Count == 0)
            {
                Debug.LogWarning("Created GatheringResources task with no wanted resource types.");
            }
            Assert.IsNotNull(resourceDestinationLocation);
            this.resourceDestinationLocation = resourceDestinationLocation;
            this.currentTargetResourceSource = source;

            returnToBaseAction = MovementActions.MoveToDestination(resourceManipulator, resourceDestinationLocation, resourceManipulator.Range);
            areWeAtBase = returnToBaseAction.PostConditions[0] as CloserThan;
            Assert.IsNotNull(areWeAtBase);
            unloadToBaseAction = ResourceTransferActions.GiveResourcesOfTypeToDestination(resourceManipulator, resourceDestinationLocation, resourceDestinationLocation, wantedResources);
        }

        public override void Execute(float deltaTime)
        {
            if (currentAction?.TryExecute(deltaTime) ?? false)
            {
                return;
            }

            if (resourceManipulator.Resources.CapacityLeft > 0)
            {
                if (currentTargetResourceSource == null)
                {
                    FindNewTargetResourceSource();
                }
                if (currentTargetResourceSource != null)
                {
                    if (takeResourcesFromSourceAction.IsValid())
                    {
                        currentAction = takeResourcesFromSourceAction;
                    }
                    else
                    {
                        currentAction = goToResourceAction;
                    }
                }
                else
                {
                    if (!areWeAtBase.IsSatisified())
                    {
                        currentAction = returnToBaseAction;
                    }
                    else
                    {
                        currentAction = null;
                    }
                }
            }
            else
            {
                if (areWeAtBase.IsSatisified())
                {
                    currentAction = unloadToBaseAction;
                }
                else
                {
                    currentAction = returnToBaseAction;
                }
            }
            currentAction?.TryExecute(deltaTime);
        }

        private void FindNewTargetResourceSource()
        {
            var potentialSources = GameObject.FindObjectsOfType<ResourceSource>();
            foreach (var potentialSource in potentialSources)
            {
                if (ResourceHelpers.HasCommonResources(wantedResources, potentialSource.Resources.SupportedTypes) && 
                    (currentTargetResourceSource == null ||
                        Vector3.Distance(resourceManipulator.Position, potentialSource.Position) <
                        Vector3.Distance(resourceManipulator.Position, currentTargetResourceSource.Position)))
                {
                    currentTargetResourceSource = potentialSource;
                }
            }
            if (currentTargetResourceSource != null)
            {
                goToResourceAction = MovementActions.MoveToDestination(resourceManipulator, currentTargetResourceSource, resourceManipulator.Range);
                takeResourcesFromSourceAction = ResourceTransferActions.GetResourcesOfTypeFromSource(resourceManipulator, currentTargetResourceSource, currentTargetResourceSource, wantedResources);
            }
        }
    }
}