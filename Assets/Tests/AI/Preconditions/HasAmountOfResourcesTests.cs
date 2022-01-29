using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasAmountOfResourcesTests
    {
        [Test]
        public void HasAmountOfResourcesTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 5), (ResourceType.Stone, 5) });
            var hasAmountOfResources = new HasAmountOfResources(resources, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 5), (ResourceType.Stone, 5) });

            Assert.IsTrue(hasAmountOfResources.IsSatisified());
            resources.TakeResource(ResourceType.Gold, 5);
            Assert.IsFalse(hasAmountOfResources.IsSatisified());
        }
    }
}