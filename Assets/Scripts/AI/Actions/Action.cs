using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class Action
    {
        public bool IsFinished { get; private set; } = false;
        public readonly List<Condition> Preconditions = new List<Condition>();
        public readonly List<Effect> Effects = new List<Effect>();
        public readonly List<Condition> PostConditions = new List<Condition>();

        public Action(IEnumerable<Condition> preconditions, IEnumerable<Effect> effects, IEnumerable<Condition> postConditions)
        {
            if (preconditions != null)
            {
                this.Preconditions.AddRange(preconditions);
            }

            this.Effects.AddRange(effects);
            Assert.IsTrue(this.Effects.Count > 0);

            if (postConditions != null)
            {
                this.PostConditions.AddRange(postConditions);
            }
        }

        public bool IsValid()
        {
            if (IsFinished)
            {
                return false;
            }
            foreach (var precondition in Preconditions)
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
                foreach (var effect in Effects)
                {
                    effect.TakeEffect(deltaTime);
                }
                IsFinished = CheckIfFinished();
                return true;
            }
            return false;
        }

        private bool CheckIfFinished()
        {
            if (PostConditions.Count == 0)
            {
                return true;
            }
            foreach (var postCondition in PostConditions)
            {
                if (!postCondition.IsSatisified())
                {
                    return false;
                }
            }
            return true;
        }
    }
}