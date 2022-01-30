using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasCapacityForResourcesTests
    {
        [Test]
        public void HasCapacityForResourcesTest()
        {
            var resources = new ResourceManager(10, new List<ResourceType> { ResourceType.Gold, ResourceType.Stone });
            var hasCapacityForResource = new HasCapacityForResources(resources, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 5), (ResourceType.Stone, 5) });
            Assert.IsTrue(hasCapacityForResource.IsSatisified());
            resources.InsertResource(ResourceType.Gold, 10);
            Assert.IsFalse(hasCapacityForResource.IsSatisified());
        }
    }
}