using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class TransferResources : Effect
    {
        private readonly Resources source;
        private readonly Resources destination;
        private readonly float amount;

        public TransferResources(Resources source, Resources destination, float amount = float.MaxValue)
        {
            Assert.IsFalse(source == destination);
            this.source = source;
            Assert.IsNotNull(source);
            this.destination = destination;
            Assert.IsNotNull(destination);
            this.amount = amount;
            Assert.IsTrue(amount > 0.0f);
        }

        public override void TakeEffect(float deltaTime)
        {
            float amountToTransfer = Mathf.Min(source.CurrentAmount, destination.CapacityLeft, amount);
            source.CurrentAmount -= amountToTransfer;
            destination.CurrentAmount += amountToTransfer;
        }
    }
}