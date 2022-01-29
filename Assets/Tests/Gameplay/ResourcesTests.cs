using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class ResourcesTests
    {
        [Test]
        public void ResourcesTest()
        {
            var resources = new Resources(10.0f, 6.0f);
            Assert.IsTrue(resources.MaxCapacity == 10.0f);
            Assert.IsTrue(resources.CurrentAmount == 6.0f);
            Assert.IsTrue(resources.CapacityLeft == 4.0f);

            resources = Resources.CreateFullResourceHolder(10.0f);
            Assert.IsTrue(resources.MaxCapacity == 10.0f);
            Assert.IsTrue(resources.CurrentAmount == 10.0f);
            Assert.IsTrue(resources.CapacityLeft == 0.0f);

            resources = Resources.CreateEmptyResourceHolder(10.0f);
            Assert.IsTrue(resources.MaxCapacity == 10.0f);
            Assert.IsTrue(resources.CurrentAmount == 0.0f);
            Assert.IsTrue(resources.CapacityLeft == 10.0f);
        }
    }
}