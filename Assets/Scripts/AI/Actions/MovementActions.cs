using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class MovementActions
    {
        public static Action MoveToDestination(IMovableAgent movableAgent, IPositionProvider destination, float range)
        {
            Assert.IsNotNull(movableAgent);
            Assert.IsNotNull(destination);
            return new Action(new List<Condition> { new FurtherThan(movableAgent, destination, range) },
                                                    new List<Effect> { new MoveTowardsPoint(movableAgent, destination.Position) },
                                                    new List<Condition> { new CloserThan(movableAgent, destination, range) });
        }
    }
}