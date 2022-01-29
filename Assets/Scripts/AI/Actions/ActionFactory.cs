using UnityEngine;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class ActionFactory
    {
        /*
        public static bool TryMake_TakeAnyResourcesAction(Action action, GameObject source, GameObject target)
        {
            if (source.TryGetComponent<IResourceHolder>(out var sourceResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var sourcePosition) &&
                source.TryGetComponent<IResourceManipulator>(out var targetResourceManipulator) &&
                source.TryGetComponent<IPositionProvider>(out var targetPosition))
            {
                var preconditions = new List<Precondition>();
                preconditions.Add(new DistancePrecondition(sourcePosition, targetPosition, targetResourceManipulator.Range));
                preconditions.Add(new HasAmountOfResourceType(sourceResourceHolder));
                preconditions.Add(new HasCapacityForResourceType(targetResourceManipulator));

                action = new Action(preconditions,
                                    new Effect[] { new TransferResources(sourceResourceHolder, targetResourceManipulator) });
                return true;
            }
            return false;
        }

        public static bool TryMake_GiveAllResourcesAction(Action action, GameObject source, GameObject target)
        {
            if (source.TryGetComponent<IResourceManipulator>(out var sourceResourceManipulator) &&
                source.TryGetComponent<IPositionProvider>(out var sourcePosition) &&
                source.TryGetComponent<IResourceHolder>(out var targetResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var targetPosition))
            {
                action = new Action(new Precondition[]{ new DistancePrecondition(sourcePosition, targetPosition, sourceResourceManipulator.Range),
                                                        new HasAmountOfResource(sourceResourceManipulator),
                                                        new HasCapacityForResources(targetResourceHolder)},
                                    new Effect[] { new TransferResources(sourceResourceManipulator, targetResourceHolder) });
                return true;
            }
            return false;
        }
        */
    }
}