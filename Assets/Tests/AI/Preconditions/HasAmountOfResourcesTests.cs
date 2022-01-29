using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class HasAmountOfResourcesTests
    {
        [Test]
        public void HasAmountOfResourcesTest()
        {
            var resourceHolder = Resources.CreateFullResourceHolder(1.0f);
            var hasResources = new HasAmountOfResources(resourceHolder, 1.0f);
            Assert.IsTrue(hasResources.IsSatisified());
            resourceHolder.CurrentAmount = 0.0f;
            Assert.IsFalse(hasResources.IsSatisified());
        }
    }
}