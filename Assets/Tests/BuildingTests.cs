using System;
using System.Collections;
using System.Collections.Generic;
using Lifecycle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Random = UnityEngine.Random;

namespace Polyjam_2022.Tests
{
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
            public event System.Action<GameObject> OnObjectSpawned;

            public MockWorld()
            {
                Debug.Log("Mock world created");
            }

            public void Initialize()
            {
            }

            public GameObject Instantiate(GameObject prefab, Transform parent = null)
            {
                return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            }

            public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation,
                Transform parent = null)
            {
                OnObjectSpawned?.Invoke(prefab);
                return prefab;
            }

            public ComponentType Instantiate<ComponentType>(ComponentType prefab, Transform parent = null)
                where ComponentType : MonoBehaviour
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

        class MockLayerManager : ILayerManager
        {
            public LayerMask GroundLayerMask => LayerMask.GetMask("Default");
            public LayerMask UnitLayerMask { get; }
            public LayerMask BuildingLayerMask { get; }
        }

        class MockPrefabCollection : IBuildingPrefabCollection
        {
            private readonly Building buildingPrefab;
            private readonly BuildingPhantom buildingPhantom;
            private readonly ConstructionSite constructionSite;

            public MockPrefabCollection(BuildingPhantom buildingPhantom, Building buildingPrefab, ConstructionSite site)
            {
                this.buildingPhantom = buildingPhantom;
                this.constructionSite = site;
                this.buildingPrefab = buildingPrefab;
            }

            public BuildingPrefabData GetBuildingPrefabData(string buildingId)
            {
                return new BuildingPrefabData()
                {
                    BuildingPrefab = buildingPrefab,
                    ConstructionSitePrefab = constructionSite,
                    BuildingPhantomPrefab = buildingPhantom
                };
            }
        }

        class MockUnitSpawner : IUnitSpawner
        {
            public int SpawnsQueued { get; private set; }
            
            public void QueueUnitSpawn()
            {
                ++SpawnsQueued;
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
            buildingPhantom.OnPlacementPossibilityChanged += b => { callbackCalled = true; };

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
            groundCollider.size = new Vector3(1000, 0.0f, 1000);
            groundCollider.isTrigger = false;
            groundGO.transform.position = new Vector3(0, groundY, 0);
            groundGO.layer = LayerMask.NameToLayer("Default");

            var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
            container.InjectGameObject(buildingPhantomGO);
            
            var buildingPlacementHelper = new BuildingPlacer(new MockWorld(), new MockLayerManager(), new MockPrefabCollection(buildingPhantomPrefab, null, null));
            var targetPosition = Random.onUnitSphere * Random.Range(0, 0.5f * groundY);
            buildingPlacementHelper.SetBuildingData(new BuildingData());

            yield return null;
            buildingPlacementHelper.MovePhantomToPosition(targetPosition);
            Assert.IsTrue(Math.Abs(groundY + 0.5f * boundsHeight -
                                   buildingPlacementHelper.BuildingPhantom.transform.position.y) < 0.001f);
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
            container.Bind<IUnitSpawner>().To<MockUnitSpawner>().AsSingle();

            var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
            var buildingPrefabGO = new GameObject("Building");
            var constructionSite = buildingPrefabGO.AddComponent<ConstructionSite>();
            var buildingPrefabCollection = new MockPrefabCollection(buildingPhantomPrefab, null, constructionSite);
            container.Bind<IBuildingPrefabCollection>().FromInstance(buildingPrefabCollection);
            container.InjectGameObject(buildingPhantomGO);
            container.InjectGameObject(buildingPrefabGO);

            var buildingPlacementHelper = new BuildingPlacer(mockWorld, new MockLayerManager(), buildingPrefabCollection);
            var targetPosition = Random.onUnitSphere * Random.Range(0, 0.5f * groundY);
            buildingPlacementHelper.SetBuildingData(new BuildingData());

            bool spawnCallbackCalled = false;
            mockWorld.OnObjectSpawned += (GameObject x) => { spawnCallbackCalled = true; };

            buildingPlacementHelper.MovePhantomToPosition(targetPosition);
            mockTriggerEventBroadcaster.TriggerEnter();
            Assert.IsFalse(buildingPlacementHelper.TryPlaceBuildingAtCurrentPosition());
            Assert.IsFalse(spawnCallbackCalled);

            mockTriggerEventBroadcaster.TriggerExit();
            Assert.IsTrue(buildingPlacementHelper.TryPlaceBuildingAtCurrentPosition());
            Assert.IsTrue(spawnCallbackCalled);
        }

        [Test]
        public void ConstructionTest()
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
            container.Bind<IUnitSpawner>().To<MockUnitSpawner>().AsSingle();

            var buildingPhantomPrefab = buildingPhantomGO.AddComponent<BuildingPhantom>();
            var constructionSitePrefab = new GameObject("ConstructionSite");
            var constructionSite = constructionSitePrefab.AddComponent<ConstructionSite>();
            var buildingGO = new GameObject("Building");
            var buildingPrefab = buildingGO.AddComponent<Building>();
            var buildingPrefabCollection = new MockPrefabCollection(buildingPhantomPrefab, buildingPrefab, constructionSite);
            container.Bind<IBuildingPrefabCollection>().FromInstance(buildingPrefabCollection);
            container.InjectGameObject(buildingPhantomGO);
            container.InjectGameObject(constructionSitePrefab);
            container.InjectGameObject(buildingGO);

            var buildingPlacementHelper = new BuildingPlacer(mockWorld, new MockLayerManager(), buildingPrefabCollection);
            var targetPosition = Random.onUnitSphere * Random.Range(0, 0.5f * groundY);
            buildingPlacementHelper.SetBuildingData(new BuildingData()
            {
                ConstructionResourceRequirements = new List<ResourceRequirement>()
                {
                    new ResourceRequirement() {ResourceType = ResourceType.Gold, RequiredAmount = 10},
                    new ResourceRequirement() {ResourceType = ResourceType.Potatoes, RequiredAmount = 10},
                    new ResourceRequirement() {ResourceType = ResourceType.Stone, RequiredAmount = 10}
                }
            });
            
            buildingPlacementHelper.MovePhantomToPosition(targetPosition);
            buildingPlacementHelper.TryPlaceBuildingAtCurrentPosition();
            
            bool spawnCallbackCalled = false;
            mockWorld.OnObjectSpawned += (GameObject x) => { spawnCallbackCalled = true; };

            constructionSite.Resources.InsertResource(ResourceType.Gold, 10);
            Assert.IsNotNull(constructionSite);
            Assert.IsFalse(spawnCallbackCalled);
            
            constructionSite.Resources.InsertResource(ResourceType.Stone, 5);
            Assert.IsNotNull(constructionSite);
            Assert.IsFalse(spawnCallbackCalled);
            
            constructionSite.Resources.InsertResource(ResourceType.Potatoes, 5);
            Assert.IsNotNull(constructionSite);
            Assert.IsFalse(spawnCallbackCalled);
            
            constructionSite.Resources.InsertResource(ResourceType.Stone, 5);
            constructionSite.Resources.InsertResource(ResourceType.Potatoes, 5);
            Assert.IsTrue(spawnCallbackCalled);
        }

        [UnityTest]
        public IEnumerator ProductionTest()
        {
            const float recipeCooldown = 1.55f;

            var buildingData = new BuildingData()
            {
                BuildingId = "test",
                BuildingName = "Test",
                ResourceCapacity = 50,
                Recipes = new List<Recipe>()
                {
                    new Recipe()
                    {
                        ProductionCooldown = recipeCooldown,
                        ConsumptionData = new List<ResourceRequirement>() { new ResourceRequirement() { ResourceType = ResourceType.Gold, RequiredAmount = 2 } },
                        ProductionData = new List<ProductionData>()
                        {
                            new ProductionData() { ProducedAmount = 1, ProductionType = ProductionType.Unit },
                            new ProductionData() { ProducedAmount = 5, ProductionType = ProductionType.Resource, ProducedResource = ResourceType.Potatoes }
                        }
                    }
                }
            };
            
            var unitSpawner = new MockUnitSpawner();
            var container = new DiContainer();
            container.Bind<IUnitSpawner>().FromInstance(unitSpawner);
            container.Bind<IBoundsProvider>().FromInstance(new MockBoundsProvider(new GameObject("test"), 1));

            var building = (new GameObject("Building")).AddComponent<Building>();
            container.InjectGameObject(building.gameObject);
            building.BuildingData = buildingData;
            
            building.Resources.InsertResource(ResourceType.Gold, 1);
            yield return new WaitForSeconds(recipeCooldown + 0.1f);
            Assert.AreEqual(0, unitSpawner.SpawnsQueued);
            
            building.Resources.InsertResource(ResourceType.Gold, 1);
            yield return null;
            Assert.AreEqual(1, unitSpawner.SpawnsQueued);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Potatoes, out int amount) && amount == 5);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount) && amount == 0);
            
            building.Resources.InsertResource(ResourceType.Gold, 2);
            yield return null;
            Assert.AreEqual(1, unitSpawner.SpawnsQueued);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Potatoes, out amount) && amount == 5);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount) && amount == 2);

            yield return new WaitForSeconds(recipeCooldown + 0.1f);
            Assert.AreEqual(2, unitSpawner.SpawnsQueued);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Potatoes, out amount) && amount == 10);
            Assert.IsTrue(building.Resources.TryGetCurrentAmount(ResourceType.Gold, out amount) && amount == 0);
        }
    }
}