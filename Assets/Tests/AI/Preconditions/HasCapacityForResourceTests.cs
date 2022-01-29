using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasCapacityForResourceTests
    {
        [Test]
        public void HasCapacityForResourceTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 0) });
            var hasCapacityForResource = new HasCapacityForResource(resources, ResourceType.Gold, 10);
            Assert.IsTrue(hasCapacityForResource.IsSatisified());
            resources.InsertResource(ResourceType.Gold, 10);
            Assert.IsFalse(hasCapacityForResource.IsSatisified());
        }
    }
}