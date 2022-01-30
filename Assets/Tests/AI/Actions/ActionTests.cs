using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class ActionTests
    {
        [Test]
        public void NullPreconditionActionTest()
        {
            var effect = new MockEffect();
            var action = new Action(null, new Effect[] { effect }, null);

            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            Assert.IsTrue(effect.takenEffect);
        }

        [Test]
        public void FalsePreconditionActionTest()
        {
            var precondition = new MockFalsePrecondition();
            var effect = new MockEffect();
            var action = new Action(new Condition[]{ precondition }, new Effect[]{ effect }, null);

            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            Assert.IsFalse(effect.takenEffect);
        }

        [Test]
        public void TruePreconditionActionTest()
        {
            var precondition = new MockTruePrecondition();
            var effect = new MockEffect();
            var action = new Action(new Condition[] { precondition }, new Effect[] { effect }, null);

            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            Assert.IsTrue(effect.takenEffect);
        }
    }

    public class MockTruePrecondition : Condition
    {
        public override bool IsSatisified()
        {
            return true;
        }
    }

    public class MockFalsePrecondition : Condition
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