using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class HasCapacityForResourcesTests
    {
        [Test]
        public void HasCapacityForResourcesTest()
        {
            var resources = Resources.CreateEmptyResourceHolder(1.0f);
            var hasCapacityForResources = new HasCapacityForResources(resources, 1.0f);
            Assert.IsTrue(hasCapacityForResources.IsSatisified());
            resources.CurrentAmount = 1.0f;
            Assert.IsFalse(hasCapacityForResources.IsSatisified());
        }
    }
}