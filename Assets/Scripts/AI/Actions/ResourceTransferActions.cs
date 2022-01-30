using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class ResourceTransferActions
    {
        public static Action GetResourcesOfAllTypesFromSource(IResourceManipulator resourceManipulator,
                                                    IResourceHolder sourceResourceHolder,
                                                    IPositionProvider sourcePosition)
        {
            return GetResourcesOfTypeFromSource(resourceManipulator, sourceResourceHolder, sourcePosition, ResourceHelpers.GetAllTypes());
        }

        public static Action GetResourcesOfTypeFromSource(IResourceManipulator resourceManipulator,
                                                    IResourceHolder sourceResourceHolder,
                                                    IPositionProvider sourcePosition,
                                                    IEnumerable<ResourceType> resources)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(sourceResourceHolder);
            Assert.IsNotNull(sourcePosition);
            Assert.IsNotNull(resources);
            return new Action(new List<Condition>{ new CloserThan(resourceManipulator, sourcePosition, resourceManipulator.Range) },
                              new List<Effect> { new TransferTypesOfResources(sourceResourceHolder, resourceManipulator,
                              ResourceHelpers.GetCommonResources(resourceManipulator.Resources.SupportedTypes, ResourceHelpers.GetCommonResources(resources, sourceResourceHolder.Resources.SupportedTypes))) }, null);
        }

        public static Action GetResourcesAmountFromSource(IResourceManipulator resourceManipulator,
                                                    IResourceHolder sourceResourceHolder,
                                                    IPositionProvider sourcePosition,
                                                    IEnumerable<(ResourceType type, int amount)> resources, 
                                                    bool exactAmmount = true)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(sourceResourceHolder);
            Assert.IsNotNull(sourcePosition);
            Assert.IsNotNull(resources);
            var preconditions = new List<Condition>{ new CloserThan(resourceManipulator, sourcePosition, resourceManipulator.Range) };
            if (exactAmmount)
            {
                preconditions.Add(new HasAmountOfResources(sourceResourceHolder, resources));
                preconditions.Add(new HasCapacityForResources(resourceManipulator, resources));
            }
            return new Action( preconditions, new List<Effect> { new TransferAmountOfResources(sourceResourceHolder, resourceManipulator, resources) }, null);
        }

        public static Action GiveResourcesOfAllTypesToDestination(IResourceManipulator resourceManipulator,
                                                        IResourceHolder destinationResourceHolder,
                                                        IPositionProvider destinationPosition)
        {
            return GiveResourcesOfTypeToDestination(resourceManipulator, destinationResourceHolder, destinationPosition, ResourceHelpers.GetAllTypes());
        }

        public static Action GiveResourcesOfTypeToDestination(IResourceManipulator resourceManipulator,
                                                        IResourceHolder destinationResourceHolder,
                                                        IPositionProvider destinationPosition,
                                                        IEnumerable<ResourceType> resources)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(destinationResourceHolder);
            Assert.IsNotNull(destinationPosition);
            Assert.IsNotNull(resources);
            return new Action(new List<Condition> { new CloserThan(resourceManipulator, destinationPosition, resourceManipulator.Range) },
                              new List<Effect> { new TransferTypesOfResources(resourceManipulator, destinationResourceHolder, 
                                ResourceHelpers.GetCommonResources(resourceManipulator.Resources.SupportedTypes, 
                                    ResourceHelpers.GetCommonResources(resources, destinationResourceHolder.Resources.SupportedTypes))) }, null);
        }

        public static Action GiveResourcesAmountToDestination(IResourceManipulator resourceManipulator,
                                                        IResourceHolder destinationResourceHolder,
                                                        IPositionProvider destinationPosition,
                                                        IEnumerable<(ResourceType type, int amount)> resources,
                                                        bool exactAmmount = true)
        {
            Assert.IsNotNull(resourceManipulator);
            Assert.IsNotNull(destinationResourceHolder);
            Assert.IsNotNull(destinationPosition);
            Assert.IsNotNull(resources);
            var preconditions = new List<Condition> { new CloserThan(resourceManipulator, destinationPosition, resourceManipulator.Range) };
            if (exactAmmount)
            {
                preconditions.Add(new HasAmountOfResources(resourceManipulator, resources));
                preconditions.Add(new HasCapacityForResources(destinationResourceHolder, resources));
            }
            return new Action(preconditions, new List<Effect>{ new TransferAmountOfResources(resourceManipulator, destinationResourceHolder, resources) }, null);
        }
    }
}