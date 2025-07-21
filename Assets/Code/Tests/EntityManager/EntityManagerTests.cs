using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
    public struct TestComponent : IComponent
    {
        public int Value;
    }
    
    public class EntityManagerTests
    {
        private EntityManager _em;

        [SetUp]
        public void SetUp()
        {
            TypeManager.Initialize();
            _em = EntityManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _em.Dispose();
        }

        [Test]
        public void CreateEntity()
        {
            var entity = _em.CreateEntity();

            Assert.AreEqual(1, entity.Index);
            Assert.AreEqual(0, entity.Version);
        }

        [Test]
        public void IndexGoesUp()
        {
            var baseIdx = 1;

            for (int i = 0; i < 100_000; i++)
            {
                var entity = _em.CreateEntity();
                Assert.AreEqual(baseIdx + i, entity.Index);
                Assert.AreEqual(0, entity.Version);
            }
        }

        [Test]
        public void VersionGoesUp()
        {
            var baseVer = 1;
            var entity = _em.CreateEntity();

            for (int i = 0; i < 100_000; i++)
            {
                _em.DestroyEntity(entity);
                entity = _em.CreateEntity();

                Assert.AreEqual(1, entity.Index);
                Assert.AreEqual(baseVer + i, entity.Version);
            }
        }

        [Test]
        public void DefaultEntityDoesNotExist()
        {
            var entity = new Entity();

            Assert.IsFalse(_em.Exists(entity));
        }

        [Test]
        public void CreateEntity2()
        {
            var entity1 = _em.CreateEntity();
            var entity2 = _em.CreateEntity();

            Assert.AreEqual(2, entity2.Index);
            Assert.AreEqual(0, entity2.Version);
        }

        [Test]
        public void ReuseEntityIndex()
        {
            var entity1 = _em.CreateEntity();
            _em.DestroyEntity(entity1);
            var entity2 = _em.CreateEntity();

            Assert.AreEqual(1, entity2.Index);
            Assert.AreEqual(1, entity2.Version);
        }

        [Test]
        public void ReuseEntityIndexThenCreate()
        {
            var entity1 = _em.CreateEntity();
            _em.DestroyEntity(entity1);
            var entity2 = _em.CreateEntity();
            var entity3 = _em.CreateEntity();

            Assert.AreEqual(2, entity3.Index);
            Assert.AreEqual(0, entity3.Version);
        }

        [Test]
        public void CreatedEntityExistsOnce()
        {
            var entity1 = _em.CreateEntity();

            Assert.IsTrue(_em.Exists(entity1));
        }

        [Test]
        public void CreatedEntityExistsTwice()
        {
            var entity1 = _em.CreateEntity();
            var entity2 = _em.CreateEntity();

            Assert.IsTrue(_em.Exists(entity1));
            Assert.IsTrue(_em.Exists(entity2));
        }

        [Test]
        public void DestroyedEntityDoesNotExist()
        {
            var entity1 = _em.CreateEntity();
            _em.DestroyEntity(entity1);

            Assert.IsFalse(_em.Exists(entity1));
        }

        [Test]
        public void NullEntityIsZero()
        {
            var entity = Entity.Null;
            Assert.AreEqual(0, entity.Index);
            Assert.AreEqual(0, entity.Version);
        }

        [Test]
        public void NullEntityIsDefault()
        {
            var entity = Entity.Null;
            Assert.IsTrue(entity == default);
        }

        [Test]
        public void CanNotDestroyEntityTwice()
        {
            var entity = _em.CreateEntity();
            Assert.IsTrue(_em.DestroyEntity(entity));
            Assert.IsFalse(_em.DestroyEntity(entity));
        }

        [Test]
        public void AddComponentDoesNotFail()
        {
            var e = _em.CreateEntity();
            Assert.DoesNotThrow(() => _em.AddComponent<TestComponent>(e));
        }

        [Test]
        public void AddComponentDataDoesNotThrow()
        {
            var e = _em.CreateEntity();
            Assert.DoesNotThrow(() => _em.AddComponentData(e, new TestComponent()));
        }

        [Test]
        public void GetComponentWorksAfterAddComponent()
        {
            for (int i = -100; i < 100; i++)
            {
                var e = _em.CreateEntity();
                var t = new TestComponent { Value = i };
                _em.AddComponentData(e, t);
                var r = _em.GetComponentData<TestComponent>(e);
                
                Assert.AreEqual(r.Value, t.Value);
            }
        }
        
        [Test]
        public void AddComponentThrowsOnNullEntity()
        {
            var e = Entity.Null;
            Assert.Throws<Exception>(() => _em.AddComponent<TestComponent>(e));
        }

        [Test]
        public void AddComponentThrowsOnRandomNonCreatedEntity()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var e = new Entity(i, j);
                    Assert.Throws<Exception>(() => _em.AddComponent<TestComponent>(e));
                }
            }
        }

        [Test]
        public void AddComponentThrowsOnDestroyedEntity()
        {
            var e = _em.CreateEntity();
            _em.DestroyEntity(e);
            Assert.Throws<Exception>(() => _em.AddComponent<TestComponent>(e));
        }
        
        [Test]
        public void AddComponentTwiceThrows()
        {
            var e = _em.CreateEntity();
            Assert.Throws<Exception>(() =>
            {
                _em.AddComponent<TestComponent>(e);
                _em.AddComponent<TestComponent>(e);
            });
        }

        [Test]
        public void ComponentValueOfOtherEntitiesDoesNotChangeOnDestroy()
        {
            for (int i = 0; i < 100; i++)
            {
                var e = _em.CreateEntity();
                var t = new TestComponent { Value = e.Index };
                _em.AddComponentData(e, t);
            }
            
            _em.DestroyEntity(_em.GetLastCreatedEntity());
        }

        [Test]
        public void LastCreatedEntityNullByDefault()
        {
            Assert.AreEqual(Entity.Null, _em.GetLastCreatedEntity());
        }

        [Test]
        public void LastCreatedEntityWorks()
        {
            
        }

        [Test]
        public void LastCreatedEntityDoesNotChangeAfterDestroy()
        {
            
        }

        [Test]
        public void GetAllEntitiesEmptyByDefault()
        {
            
        }

        [Test]
        public void GetComponentThrowsOnDestroyedEntity()
        {
            var e = _em.CreateEntity();
            _em.AddComponent<TestComponent>(e);
            _em.DestroyEntity(e);

            Assert.Throws<Exception>(() =>
            {
                _em.GetComponentData<TestComponent>(e);
            });
        }

        [Test]
        public void GetComponentThrowsOnNotCreatedEntity()
        {
            var e = Entity.Null;
            Assert.Throws<Exception>(() => _em.GetComponentData<TestComponent>(e));
        }

        [Test]
        public void GetComponentThrowsOnNotAddedEntity()
        {
            var e = _em.CreateEntity();
            Assert.Throws<Exception>(() => _em.GetComponentData<TestComponent>(e));
        }

        [Test]
        public void GetComponentHasDefaultValueOnAddedEntity()
        {
            var e = _em.CreateEntity();
            _em.AddComponent<TestComponent>(e);
            Assert.AreEqual(new TestComponent(), _em.GetComponentData<TestComponent>(e));
        }
        
        // TODO: Archetype with no components isn't allowed
    }
}