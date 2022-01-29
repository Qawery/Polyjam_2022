using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public static class ActionFactory
    {
        public static bool TryMake_TakeAnyResourcesAction(ref Action action, GameObject source, GameObject target)
        {
            if (source.TryGetComponent<IResourceHolder>(out var sourceResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var sourcePosition) &&
                source.TryGetComponent<IResourceManipulator>(out var targetResourceManipulator) &&
                source.TryGetComponent<IPositionProvider>(out var targetPosition))
            {
                action = new Action(new Precondition[]{ new DistancePrecondition(sourcePosition, targetPosition, targetResourceManipulator.Range),
                                                        new HasAmountOfResources(sourceResourceHolder),
                                                        new HasCapacityForResources(targetResourceManipulator)},
                                    new Effect[] { new TransferResources(sourceResourceHolder, targetResourceManipulator) });
                return true;
            }
            return false;
        }

        public static bool TryMake_GiveAllResourcesAction(ref Action action, GameObject source, GameObject target)
        {
            if (source.TryGetComponent<IResourceManipulator>(out var sourceResourceManipulator) &&
                source.TryGetComponent<IPositionProvider>(out var sourcePosition) &&
                source.TryGetComponent<IResourceHolder>(out var targetResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var targetPosition))
            {
                action = new Action(new Precondition[]{ new DistancePrecondition(sourcePosition, targetPosition, sourceResourceManipulator.Range),
                                                        new HasAmountOfResources(sourceResourceManipulator),
                                                        new HasCapacityForResources(targetResourceHolder)},
                                    new Effect[] { new TransferResources(sourceResourceManipulator, targetResourceHolder) });
                return true;
            }
            return false;
        }
    }
}