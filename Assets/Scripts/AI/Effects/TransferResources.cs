using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class TransferResources : Effect
    {
        private readonly Resources source;
        private readonly Resources destination;
        private readonly float amount;

        public TransferResources(IResourceHolder source, IResourceHolder destination, float amount = float.MaxValue)
        {
            Assert.IsFalse(source == destination);
            Assert.IsNotNull(source);
            Assert.IsNotNull(source.ResourcesHeld);
            this.source = source.ResourcesHeld;

            Assert.IsNotNull(destination);
            Assert.IsNotNull(destination.ResourcesHeld);
            this.destination = destination.ResourcesHeld;

            Assert.IsTrue(amount > 0.0f);
            this.amount = amount;
        }

        public override void TakeEffect(float deltaTime)
        {
            float amountToTransfer = Mathf.Min(source.CurrentAmount, destination.CapacityLeft, amount);
            source.CurrentAmount -= amountToTransfer;
            destination.CurrentAmount += amountToTransfer;
        }
    }
}