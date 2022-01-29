using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasAmountOfResourceTypeTests
    {
        [Test]
        public void HasAmountOfResourceTypeTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 10) });
            var hasResources = new HasAmountOfResourceType(resources, ResourceType.Gold, 10);
            Assert.IsTrue(hasResources.IsSatisified());
            resources.TakeResource(ResourceType.Gold, 10);
            Assert.IsFalse(hasResources.IsSatisified());
        }
    }
}