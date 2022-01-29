using NUnit.Framework;

namespace Polyjam_2022.Tests
{
    public class ResourceHolderTests
    {
        [Test]
        public void ResourceHolderTest()
        {
            var resourceHolder = new Resources(10.0f, 6.0f);
            Assert.IsTrue(resourceHolder.MaxCapacity == 10.0f);
            Assert.IsTrue(resourceHolder.CurrentAmount == 6.0f);
            Assert.IsTrue(resourceHolder.CapacityLeft == 4.0f);

            resourceHolder = Resources.CreateFullResourceHolder(10.0f);
            Assert.IsTrue(resourceHolder.MaxCapacity == 10.0f);
            Assert.IsTrue(resourceHolder.CurrentAmount == 10.0f);
            Assert.IsTrue(resourceHolder.CapacityLeft == 0.0f);

            resourceHolder = Resources.CreateEmptyResourceHolder(10.0f);
            Assert.IsTrue(resourceHolder.MaxCapacity == 10.0f);
            Assert.IsTrue(resourceHolder.CurrentAmount == 0.0f);
            Assert.IsTrue(resourceHolder.CapacityLeft == 10.0f);
        }
    }
}