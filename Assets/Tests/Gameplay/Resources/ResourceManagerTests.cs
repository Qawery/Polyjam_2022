using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class ResourceManagerTests
    {
        [Test]
        public void SingleResourcesTypeTest()
        {
            var resources = new ResourceManager(10, ResourceType.Gold);
            Assert.IsTrue(resources.MaxCapacity == 10);
            Assert.IsTrue(resources.SupportsType(ResourceType.Gold));
            Assert.IsFalse(resources.SupportsType(ResourceType.Stone));

            Assert.IsTrue(resources.CapacityLeft == 10);
            Assert.IsTrue(resources.CurrentTotalAmount == 0);            
            int amount = -1;
            Assert.IsFalse(resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == -1);
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 0));
            Assert.IsFalse(resources.HasResource(ResourceType.Gold, 5));
            Assert.IsFalse(resources.HasResource(ResourceType.Stone, 0));

            resources.InsertResource(ResourceType.Gold, 5);
            Assert.IsTrue(resources.CapacityLeft == 5);
            Assert.IsTrue(resources.CurrentTotalAmount == 5);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 0));
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 5));

            resources.TakeResource(ResourceType.Gold, 5);
            Assert.IsTrue(resources.CapacityLeft == 10);
            Assert.IsTrue(resources.CurrentTotalAmount == 0);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 0));
            Assert.IsFalse(resources.HasResource(ResourceType.Gold, 5));
        }
        
        [Test]
        public void MultiResourcesTypeTest()
        {
            var resources = new ResourceManager(8, new List<ResourceType> { ResourceType.Gold, ResourceType.Stone });
            Assert.IsTrue(resources.MaxCapacity == 8);
            Assert.IsTrue(resources.SupportsType(ResourceType.Gold));
            Assert.IsTrue(resources.SupportsType(ResourceType.Stone));

            Assert.IsTrue(resources.CapacityLeft == 8);
            Assert.IsTrue(resources.CurrentTotalAmount == 0);
            int amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);
        }

        [Test]
        public void StartingResourcesTypeTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 5), (ResourceType.Potatoes, 1) });
            Assert.IsTrue(resources.MaxCapacity == 10);
            Assert.IsTrue(resources.SupportsType(ResourceType.Gold));
            Assert.IsTrue(resources.SupportsType(ResourceType.Potatoes));

            Assert.IsTrue(resources.CapacityLeft == 4);
            Assert.IsTrue(resources.CurrentTotalAmount == 6);
            int amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 5));
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Potatoes));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Potatoes, 1));
            Assert.IsTrue(resources.HasResource(new List<(ResourceType type, int amount)>{ (ResourceType.Gold, 5), (ResourceType.Potatoes, 1) }));
            Assert.IsFalse(resources.HasResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 10), (ResourceType.Potatoes, 10) }));

            resources.InsertResource(ResourceType.Gold, 1);
            Assert.IsTrue(resources.CapacityLeft == 3);
            Assert.IsTrue(resources.CurrentTotalAmount == 7);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 6);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 6));
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Potatoes));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Potatoes, 1));
            Assert.IsTrue(resources.HasResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 6), (ResourceType.Potatoes, 1) }));

            resources.TakeResource(ResourceType.Gold, 5);
            Assert.IsTrue(resources.CapacityLeft == 8);
            Assert.IsTrue(resources.CurrentTotalAmount == 2);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 1));
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Potatoes));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Potatoes, 1));
            Assert.IsTrue(resources.HasResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 1), (ResourceType.Potatoes, 1) }));

            resources.InsertResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 1), (ResourceType.Potatoes, 1) });
            Assert.IsTrue(resources.CapacityLeft == 6);
            Assert.IsTrue(resources.CurrentTotalAmount == 4);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 2);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 2));
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Potatoes));
            Assert.IsTrue(amount == 2);
            Assert.IsTrue(resources.HasResource(ResourceType.Potatoes, 2));
            Assert.IsTrue(resources.HasResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 2), (ResourceType.Potatoes, 2) }));

            resources.TakeResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 1), (ResourceType.Potatoes, 1) });
            Assert.IsTrue(resources.CapacityLeft == 8);
            Assert.IsTrue(resources.CurrentTotalAmount == 2);
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Gold, 1));
            amount = -1;
            Assert.IsTrue(resources.TryGetCurrentAmount(ref amount, ResourceType.Potatoes));
            Assert.IsTrue(amount == 1);
            Assert.IsTrue(resources.HasResource(ResourceType.Potatoes, 1));
            Assert.IsTrue(resources.HasResource(new List<(ResourceType type, int amount)> { (ResourceType.Gold, 1), (ResourceType.Potatoes, 1) }));
        }
    }
}