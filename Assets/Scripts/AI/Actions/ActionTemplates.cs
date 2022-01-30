using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class ActionTemplates
    {
        public static Action GetAllResourcesFromSource(IResourceManipulator resourceManipulator,
                                                    IResourceHolder sourceResourceHolder,
                                                    IPositionProvider sourcePosition)
        {
            return GetResourcesFromSource(resourceManipulator, sourceResourceHolder, sourcePosition, sourceResourceHolder.Resources.GetAllCurrentAmountsPerType());
        }

        public static Action GetResourcesFromSource(IResourceManipulator resourceManipulator,
                                                    IResourceHolder sourceResourceHolder,
                                                    IPositionProvider sourcePosition,
                                                    IEnumerable<(ResourceType type, int amount)> resources)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(sourceResourceHolder);
            Assert.IsNotNull(sourcePosition);
            Assert.IsNotNull(resources);
            return new Action(new List<Precondition>{ new DistancePrecondition(resourceManipulator, sourcePosition, resourceManipulator.Range),
                                                    new HasAmountOfResources(sourceResourceHolder, resources),
                                                    new HasCapacityForResources(resourceManipulator, resources) },
                              new List<Effect> { new TransferResources(sourceResourceHolder, resourceManipulator, resources) });
        }

        public static Action GiveAllResourcesToDestination(IResourceManipulator resourceManipulator,
                                                        IResourceHolder destinationResourceHolder,
                                                        IPositionProvider destinationPosition)
        {
            return GiveResourcesToDestination(resourceManipulator, destinationResourceHolder, destinationPosition, resourceManipulator.Resources.GetAllCurrentAmountsPerType());
        }

        public static Action GiveResourcesToDestination(IResourceManipulator resourceManipulator,
                                                        IResourceHolder destinationResourceHolder,
                                                        IPositionProvider destinationPosition,
                                                        IEnumerable<(ResourceType type, int amount)> resources)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(destinationResourceHolder);
            Assert.IsNotNull(destinationPosition);
            Assert.IsNotNull(resources);
            return new Action(new List<Precondition>{ new DistancePrecondition(resourceManipulator, destinationPosition, resourceManipulator.Range),
                                                    new HasAmountOfResources(resourceManipulator, resources),
                                                    new HasCapacityForResources(destinationResourceHolder, resources) },
                              new List<Effect>{ new TransferResources(resourceManipulator, destinationResourceHolder, resources) });
        }
    }
}