using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public class TransferTypesOfResources : Effect
    {
        private readonly ResourceManager source;
        private readonly ResourceManager destination;
        private readonly List<ResourceType> resources = new List<ResourceType>();

        public TransferTypesOfResources(IResourceHolder source, IResourceHolder destination, IEnumerable<ResourceType> resources)
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
        }

        public override void TakeEffect(float deltaTime)
        {
            foreach (var resource in resources)
            {
                int sourceAmount = 0;
                if (source.TryGetCurrentAmount(resource, out sourceAmount))
                {
                    int amountTotransfer = Mathf.Min(sourceAmount, destination.CapacityLeft);
                    source.TakeResource(resource, amountTotransfer);
                    destination.InsertResource(resource, amountTotransfer);
                }
                if (destination.CapacityLeft == 0)
                {
                    return;
                }
            }
        }
    }
}