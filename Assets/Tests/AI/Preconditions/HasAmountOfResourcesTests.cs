using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class HasAmountOfResourcesTests
    {
        [Test]
        public void HasAmountOfResourcesTest()
        {
            var resources = Resources.CreateFullResourceHolder(1.0f);
            var hasResources = new HasAmountOfResources(resources, 1.0f);
            Assert.IsTrue(hasResources.IsSatisified());
            resources.CurrentAmount = 0.0f;
            Assert.IsFalse(hasResources.IsSatisified());
        }
    }
}