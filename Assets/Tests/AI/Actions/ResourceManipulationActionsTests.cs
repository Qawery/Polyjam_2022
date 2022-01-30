using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Polyjam_2022.Tests
{
    public class ResourceManipulationActionsTests
    {
        [Test]
        public void GetAndGiveResourcesTest()
        {
            var mockResourceManipulator = new MockResourceManipulator();
            var resourceHolder = new ResourceManager(20, new List<ResourceType> { ResourceType.Gold, ResourceType.Stone });
            var holderPosition = new MockPositionProvider();
            var resourceRequest = new List<(ResourceType type, int amount)> { (ResourceType.Gold, 5), (ResourceType.Stone, 4) };

            var action = ResourceTransferActions.GetResourcesAmountFromSource(mockResourceManipulator, resourceHolder, holderPosition, resourceRequest);
            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            int amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);

            resourceHolder.InsertResource(resourceRequest);
            resourceHolder.InsertResource(resourceRequest);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 4);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 4);

            action = ResourceTransferActions.GiveResourcesAmountToDestination(mockResourceManipulator, resourceHolder, holderPosition, resourceRequest);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 8);

            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 8);

            action = ResourceTransferActions.GetResourcesOfAllTypesFromSource(mockResourceManipulator, resourceHolder, holderPosition);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 8);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);

            action = ResourceTransferActions.GiveResourcesOfAllTypesToDestination(mockResourceManipulator, resourceHolder, holderPosition);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ResourceType.Stone, out amount));
            Assert.IsTrue(amount == 8);
        }
    }

    public class MockResourceManipulator : IResourceManipulator
    {
        public MockPositionProvider MockPositionProvider { get; private set; } = new MockPositionProvider();
        public ResourceManager Resources { get; private set; } = new ResourceManager(50, ResourceHelpers.GetAllTypes());
        public Vector3 Position => MockPositionProvider.Position;
        public float Range => 2.0f;
    }
}