using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Action
    {
        private readonly List<Precondition> preconditions = new List<Precondition>();
        private readonly List<Effect> effects = new List<Effect>();

        public Action(IEnumerable<Precondition> preconditions, IEnumerable<Effect> effects)
        {
            this.preconditions.AddRange(preconditions);
            this.effects.AddRange(effects);
            Assert.IsTrue(this.effects.Count > 0);
        }

        public bool IsValid()
        {
            if (preconditions.Count == 0)
            {
                return true;
            }
            foreach (var precondition in preconditions)
            {
                if (!precondition.IsSatisified())
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryExecute(float delatTime)
        {
            if (IsValid())
            {
                foreach (var effect in effects)
                {
                    effect.TakeEffect(delatTime);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class ActionFactory
    {
        public static bool TryMakeTakeAnyResources(ref Action action, GameObject source, GameObject target)
        {
            if (source.TryGetComponent<IResourceHolder>(out var sourceResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var sourcePosition) &&
                source.TryGetComponent<IResourceHolder>(out var targetResourceHolder) &&
                source.TryGetComponent<IPositionProvider>(out var targetPosition))
            {
                action = new Action(new Precondition[]{ new HasAnyResources(sourceResourceHolder), new HasCapacityForResources(targetResourceHolder) }, 
                                    new Effect[]{  });
                return true;
            }
            return false;
        }
    }
}