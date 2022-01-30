using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class TransferResourcesTests
    {
        [Test]
        public void TransferResourcesTest()
        {
            var source = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 10) });
            var destination = new ResourceManager(10, ResourceType.Gold);
            var transferResources = new TransferAmountOfResources(source, destination, new List<(ResourceType type, int amount)>{ (ResourceType.Gold, 10) });
            transferResources.TakeEffect(0.0f);

            Assert.IsTrue(source.CurrentTotalAmount == 0);
            int amount = -1;
            Assert.IsTrue(source.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);

            Assert.IsTrue(destination.CurrentTotalAmount == 10);
            amount = -1;
            Assert.IsTrue(destination.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 10);
        }
    }
}