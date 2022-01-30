using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public class TransferAmountOfResources : Effect
    {
        private readonly ResourceManager source;
        private readonly ResourceManager destination;
        private readonly List<(ResourceType type, int amount)> resources = new List<(ResourceType type, int amount)>();

        public TransferAmountOfResources(IResourceHolder source, IResourceHolder destination, IEnumerable<(ResourceType type, int amount)> resources)
        {
            Assert.IsFalse(source == destination);
            Assert.IsNotNull(source);
            Assert.IsNotNull(source.Resources);
            this.source = source.Resources;

            Assert.IsNotNull(destination);
            Assert.IsNotNull(destination.Resources);
            this.destination = destination.Resources;

            this.resources.AddRange(resources);
            Assert.IsTrue(this.resources.Count > 0);
            foreach (var resource in this.resources)
            {
                Assert.IsTrue(resource.amount > 0);
            }
        }

        public override void TakeEffect(float deltaTime)
        {
            foreach (var resource in resources)
            {
                int sourceAmount = 0;
                if (source.TryGetCurrentAmount(resource.type, out sourceAmount))
                {
                    int amountTotransfer = Mathf.Min(sourceAmount, destination.CapacityLeft, resource.amount);
                    source.TakeResource(resource.type, amountTotransfer);
                    destination.InsertResource(resource.type, amountTotransfer);
                }
                if (destination.CapacityLeft == 0)
                {
                    return;
                }
            }
        }
    }
}