using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class HasCapacityForResourcesTests
    {
        [Test]
        public void HasCapacityForResourcesTest()
        {
            var resourceHolder = Resources.CreateEmptyResourceHolder(1.0f);
            var hasCapacityForResources = new HasCapacityForResources(resourceHolder, 1.0f);
            Assert.IsTrue(hasCapacityForResources.IsSatisified());
            resourceHolder.CurrentAmount = 1.0f;
            Assert.IsFalse(hasCapacityForResources.IsSatisified());
        }
    }
}