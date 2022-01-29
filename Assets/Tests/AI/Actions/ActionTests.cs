using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class ActionTests
    {
        [Test]
        public void FalsePreconditionActionTest()
        {
            var precondition = new MockFalsePrecondition();
            var effect = new MockEffect();
            var action = new Action(new Precondition[]{ precondition }, new Effect[]{ effect });

            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            Assert.IsFalse(effect.takenEffect);
        }

        [Test]
        public void TruePreconditionActionTest()
        {
            var precondition = new MockTruePrecondition();
            var effect = new MockEffect();
            var action = new Action(new Precondition[] { precondition }, new Effect[] { effect });

            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            Assert.IsTrue(effect.takenEffect);
        }
    }

    public class MockTruePrecondition : Precondition
    {
        public override bool IsSatisified()
        {
            return true;
        }
    }

    public class MockFalsePrecondition : Precondition
    {
        public override bool IsSatisified()
        {
            return false;
        }
    }

    public class MockEffect : Effect
    {
        public bool takenEffect = false;

        public override void TakeEffect(float deltaTime)
        {
            takenEffect = true;
        }
    }
}