using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Action
    {
        private readonly List<Precondition> preconditions = new List<Precondition>();
        private readonly List<Effect> effects = new List<Effect>();

        public Action(IEnumerable<Precondition> preconditions, IEnumerable<Effect> effects)
        {
            if (preconditions != null)
            {
                this.preconditions.AddRange(preconditions);
            }

            this.effects.AddRange(effects);
            Assert.IsTrue(this.effects.Count > 0);
        }

        public bool IsValid()
        {
            foreach (var precondition in preconditions)
            {
                if (!precondition.IsSatisified())
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryExecute(float deltaTime)
        {
            if (IsValid())
            {
                foreach (var effect in effects)
                {
                    effect.TakeEffect(deltaTime);
                }
                return true;
            }
            return false;
        }
    }
}