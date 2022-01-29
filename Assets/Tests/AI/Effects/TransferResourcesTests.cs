using NUnit.Framework;
using UnityEngine;

namespace Polyjam_2022.Tests
{
    public class TransferResourcesTests
    {
        [Test]
        public void TransferAllResourcesTest()
        {
            var source = Resources.CreateFullResourceHolder(1.0f);
            var destination = Resources.CreateEmptyResourceHolder(1.0f);
            var transferResources = new TransferResources(source, destination, 1.0f);
            transferResources.TakeEffect(0.0f);
            Assert.IsTrue(source.CurrentAmount == 0.0f);
            Assert.IsTrue(destination.CurrentAmount == 1.0f);
        }

        [Test]
        public void TransferAllAvailableResourcesTest()
        {
            var source = new Resources(1.0f, 0.5f);
            var destination = Resources.CreateEmptyResourceHolder(1.0f);
            var transferResources = new TransferResources(source, destination, 1.0f);
            transferResources.TakeEffect(0.0f);
            Assert.IsTrue(source.CurrentAmount == 0.0f);
            Assert.IsTrue(destination.CurrentAmount == 0.5f);
        }
    }
}