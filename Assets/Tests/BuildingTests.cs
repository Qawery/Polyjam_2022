using System;
using System.Collections;
using System.Collections.Generic;
using Lifecycle;
using NUnit.Framework;
using Polyjam_2022;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Random = UnityEngine.Random;

public class BuildingTests
{
    class MockTriggerEventBroadcaster : ITriggerEventBroadcaster
    {
        private readonly GameObject mockGameObject = new GameObject("Mock");
        private readonly Stack<Collider> colliders = new Stack<Collider>();

        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<Collider> OnTriggerExitEvent;

        public void TriggerEnter()
        {
            var collider = mockGameObject.AddComponent<BoxCollider>();
            colliders.Push(collider);
            OnTriggerEnterEvent?.Invoke(collider);
        }

        public void TriggerExit()
        {
            if (colliders.Count > 0)
            {
                OnTriggerExitEvent?.Invoke(colliders.Pop());
            }
        }
    }

    class MockBoundsProvider : IBoundsProvider
    {
        private readonly GameObject trackedObject;
        private readonly float boundsHeight;

        public Bounds Bounds => new Bounds(trackedObject.transform.position, new Vector3(0, boundsHeight, 0));

        public MockBoundsProvider(GameObject trackedObject, float boundsHeight)
        {
            this.trackedObject = trackedObject;
            this.boundsHeight = boundsHeight;
        }
    }

    class MockWorld : IWorld
    {
        public void Initialize()
        {
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            return prefab;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return prefab;
        }

        public ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : MonoBehaviour
        {
            return prefab;
        }

        public ComponentType Instantiate<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation,
            Transform parent = null) where ComponentType : MonoBehaviour
        {
            return prefab;
        }

        public Class CreateInstance<Class>()
        {
            return default;
        }

        public void Destroy(GameObject gameObject)
        {
        }
    }
    
    [Test]
    public void BuildingPhantomTriggerTest()
    {
        var container = new DiContainer();
        GameObject buildingPhantomGO = new GameObject("Building phantom");
        var mockTriggerEventBroadcaster = new MockTriggerEventBroadcaster();
        var mockBoundsProvider = new MockBoundsProvider(buildingPhantomGO, 0);
        container.Bind<ITriggerEventBroadcaster>().FromInstance(mockTriggerEventBroadcaster);
        container.Bind<IBoundsProvider>().FromInstance(mockBoundsProvider);

        var buildingPhantom = buildingPhantomGO.AddComponent<BuildingPhantom>();
        container.InjectGameObject(buildingPhantomGO);

        bool callbackCalled = false;
        buildingPhantom.OnPlacementPossibilityChanged += b =>
        {
            callbackCalled = true;
        };
        
        mockTriggerEventBroadcaster.TriggerEnter();
        Assert.IsFalse(buildingPhantom.CanBePlaced);
        Assert.IsTrue(callbackCalled);

        callbackCalled = false;
        mockTriggerEventBroadcaster.TriggerExit();
        Assert.IsTrue(buildingPhantom.CanBePlaced);
        Assert.IsTrue(callbackCalled);

        int triggerCount = Random.Range(1, 100);
        for (int i = 0; i < triggerCount; ++i)
        {
            mockTriggerEventBroadcaster.TriggerEnter();
        }
        Assert.IsFalse(buildingPhantom.CanBePlaced);
        
        for (int i = 0; i < triggerCount - 1; ++i)
        {
            mockTriggerEventBroadcaster.TriggerExit();
        }
        Assert.IsFalse(buildingPhantom.CanBePlaced);
        
        mockTriggerEventBroadcaster.TriggerExit();
        Assert.IsTrue(buildingPhantom.CanBePlaced);
    }
    
    [UnityTest]
    public IEnumerator BuildingPhantomMovementTest()
    {
        const int groundY = -40;
        const int boundsHeight = 15;
        
        var container = new DiContainer();
        GameObject buildingPhantomGO = new GameObject("Building phantom");
        var mockTriggerEventBroadcaster = new MockTriggerEventBroadcaster();
        var mockBoundsProvider = new MockBoundsProvider(buildingPhantomGO, boundsHeight);
        container.Bind<ITriggerEventBroadcaster>().FromInstance(mockTriggerEventBroadcaster);
        container.Bind<IBoundsProvider>().FromInstance(mockBoundsProvider);
        var groundGO = new GameObject("Ground");
        var groundCollider = groundGO.AddComponent<BoxCollider>();
        groundCollider.size = new Vector3(1000, 0, 1000);
        groundGO.transform.position = new Vector3(0, groundY, 0);
        
        var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
        container.InjectGameObject(buildingPhantomGO);

        yield return null;

        var buildingPlacementHelper = new BuildingPlacer(new MockWorld(), LayerMask.NameToLayer("Default"));
        var targetPosition = Random.onUnitSphere * Random.Range(0, 0.5f * groundY);
        buildingPlacementHelper.SetBuildingData(new BuildingData()
        {
            BuildingPhantomPrefab = buildingPhantomPrefab
        });

        buildingPlacementHelper.MovePhantomToPosition(targetPosition);
        
        Assert.IsTrue(Math.Abs(groundY + boundsHeight - buildingPlacementHelper.BuildingPhantom.transform.position.y) < 0.001f);
    }
    
    [Test]
    public void BuildingPlacementTest()
    {

    }
}
