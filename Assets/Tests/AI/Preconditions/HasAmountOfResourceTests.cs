using NUnit.Framework;
using System.Collections.Generic;

namespace Polyjam_2022.Tests
{
    public class HasAmountOfResourceTests
    {
        [Test]
        public void HasAmountOfResourceTest()
        {
            var resources = new ResourceManager(10, new List<(ResourceType type, int amount)> { (ResourceType.Gold, 10) });
            var hasAmountOfResource = new HasAmountOfResource(resources, ResourceType.Gold, 10);
            Assert.IsTrue(hasAmountOfResource.IsSatisified());
            resources.TakeResource(ResourceType.Gold, 10);
            Assert.IsFalse(hasAmountOfResource.IsSatisified());
        }
    }
}