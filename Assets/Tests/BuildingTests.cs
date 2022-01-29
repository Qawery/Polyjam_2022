using System;
using System.Collections;
using System.Collections.Generic;
using Lifecycle;
using NUnit.Framework;
using Polyjam_2022;
using UnityEngine;
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
        public event System.Action OnObjectSpawned;
        
        public void Initialize()
        {
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            OnObjectSpawned?.Invoke();
            return prefab;
        }

        public ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : MonoBehaviour
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public ComponentType Instantiate<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation,
            Transform parent = null) where ComponentType : MonoBehaviour
        {
            return Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity, parent)
                .GetComponent<ComponentType>();
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
    
    [Test]
    public void BuildingPhantomMovementTest()
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
        groundCollider.size = new Vector3(1000, 0.1f, 1000);
        groundCollider.isTrigger = false;
        groundGO.transform.position = new Vector3(0, groundY, 0);
        groundGO.layer = LayerMask.NameToLayer("Default");
        
        var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
        container.InjectGameObject(buildingPhantomGO);

        var buildingPlacementHelper = new BuildingPlacer(new MockWorld(), LayerMask.GetMask("Default"));
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
        const int groundY = -40;
        const int boundsHeight = 15;
        
        GameObject buildingPhantomGO = new GameObject("Building phantom");
        var mockTriggerEventBroadcaster = new MockTriggerEventBroadcaster();
        var mockBoundsProvider = new MockBoundsProvider(buildingPhantomGO, boundsHeight);
        var mockWorld = new MockWorld();
        
        var container = new DiContainer();
        container.Bind<ITriggerEventBroadcaster>().FromInstance(mockTriggerEventBroadcaster);
        container.Bind<IBoundsProvider>().FromInstance(mockBoundsProvider);
        container.Bind<IWorld>().FromInstance(mockWorld);
        
        var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
        var buildingPrefabGO = new GameObject("Building");
        var buildingPrefab = (buildingPrefabGO).AddComponent<Building>();
        container.InjectGameObject(buildingPhantomGO);
        container.InjectGameObject(buildingPrefabGO);

        var buildingPlacementHelper = new BuildingPlacer(new MockWorld(), LayerMask.GetMask("Default"));
        var targetPosition = Random.onUnitSphere * Random.Range(0, 0.5f * groundY);
        buildingPlacementHelper.SetBuildingData(new BuildingData()
        {
            BuildingPhantomPrefab = buildingPhantomPrefab,
            BuildingPrefab = buildingPrefab
        });
        
        bool spawnCallbackCalled = false;
        mockWorld.OnObjectSpawned += () => { spawnCallbackCalled = true; };
        
        buildingPlacementHelper.MovePhantomToPosition(targetPosition);
        mockTriggerEventBroadcaster.TriggerEnter();
        Assert.IsFalse(buildingPlacementHelper.TryPlaceBuildingAtCurrentPosition());
        Assert.IsFalse(spawnCallbackCalled);
        
        mockTriggerEventBroadcaster.TriggerExit();
        Assert.IsTrue(buildingPlacementHelper.TryPlaceBuildingAtCurrentPosition());
        Assert.IsTrue(spawnCallbackCalled);
    }
}
