using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasCapacityForResourceTypeTests
    {
        [Test]
        public void HasCapacityForResourceTypeTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 0) });
            var hasCapacityForResourceType = new HasCapacityForResourceType(resources, ResourceType.Gold, 10);
            Assert.IsTrue(hasCapacityForResourceType.IsSatisified());
            resources.InsertResource(ResourceType.Gold, 10);
            Assert.IsFalse(hasCapacityForResourceType.IsSatisified());
        }
    }
}