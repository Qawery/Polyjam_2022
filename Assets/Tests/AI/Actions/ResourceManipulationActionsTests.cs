using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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

            var action = ActionTemplates.GetResourcesFromSource(mockResourceManipulator, resourceHolder, holderPosition, resourceRequest);
            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            int amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);

            resourceHolder.InsertResource(resourceRequest);
            resourceHolder.InsertResource(resourceRequest);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 4);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 5);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 4);

            action = ActionTemplates.GiveResourcesToDestination(mockResourceManipulator, resourceHolder, holderPosition, resourceRequest);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 8);

            Assert.IsFalse(action.IsValid());
            Assert.IsFalse(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 8);

            action = ActionTemplates.GetAllResourcesFromSource(mockResourceManipulator, resourceHolder, holderPosition);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 8);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);

            action = ActionTemplates.GiveAllResourcesToDestination(mockResourceManipulator, resourceHolder, holderPosition);
            Assert.IsTrue(action.IsValid());
            Assert.IsTrue(action.TryExecute(0.0f));
            amount = -1;
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(mockResourceManipulator.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 0);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Gold));
            Assert.IsTrue(amount == 10);
            Assert.IsTrue(resourceHolder.Resources.TryGetCurrentAmount(ref amount, ResourceType.Stone));
            Assert.IsTrue(amount == 8);
        }
    }

    public class MockResourceManipulator : IResourceManipulator
    {
        public MockPositionProvider MockPositionProvider { get; private set; } = new MockPositionProvider();
        public ResourceManager Resources { get; private set; } = new ResourceManager(50, ResourceManager.GetAllTypes());
        public Vector3 Position => MockPositionProvider.Position;
        public float Range => 2.0f;
    }
}