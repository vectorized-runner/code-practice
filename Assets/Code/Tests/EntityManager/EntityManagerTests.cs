using NUnit.Framework;

namespace CodePractice.Tests
{
    public class EntityManagerTests
    {
        [Test]
        public void CreateEntity()
        {
            var em = EntityManager.Create(); 
            var entity = em.CreateEntity();

            Assert.AreEqual(1, entity.Index);
            Assert.AreEqual(0, entity.Version);
        }

        [Test]
        public void IndexGoesUp()
        {
            var em = EntityManager.Create();
            var baseIdx = 1;

            for (int i = 0; i < 100_000; i++)
            {
                var entity = em.CreateEntity();
                Assert.AreEqual(baseIdx + i, entity.Index);
                Assert.AreEqual(0, entity.Version);
            }
        }

        [Test]
        public void VersionGoesUp()
        {
            var em = EntityManager.Create();
            var baseVer = 1;
            var entity = em.CreateEntity();

            for (int i = 0; i < 100_000; i++)
            {
                em.DestroyEntity(entity);
                entity = em.CreateEntity();
                
                Assert.AreEqual(1, entity.Index);
                Assert.AreEqual(baseVer + i, entity.Version);
            }
        }

        [Test]
        public void DefaultEntityDoesNotExist()
        {
            var em = EntityManager.Create();
            var entity = new Entity();

            Assert.IsFalse(em.Exists(entity));
        }

        [Test]
        public void CreateEntity2()
        {
            var em = EntityManager.Create();
            var entity1 = em.CreateEntity();
            var entity2 = em.CreateEntity();

            Assert.AreEqual(2, entity2.Index);
            Assert.AreEqual(0, entity2.Version);
        }

        [Test]
        public void ReuseEntityIndex()
        {
            var em = EntityManager.Create();
            var entity1 = em.CreateEntity();
            em.DestroyEntity(entity1);
            var entity2 = em.CreateEntity();
            
            Assert.AreEqual(1, entity2.Index);
            Assert.AreEqual(1, entity2.Version);
        }
        
        [Test]
        public void ReuseEntityIndexThenCreate()
        {
            var em = EntityManager.Create();
            var entity1 = em.CreateEntity();
            em.DestroyEntity(entity1);
            var entity2 = em.CreateEntity();
            var entity3 = em.CreateEntity();

            Assert.AreEqual(2, entity2.Index);
            Assert.AreEqual(0, entity2.Version);
        }

        [Test]
        public void CreatedEntityExists()
        {
            var em = EntityManager.Create();
            var entity1 = em.CreateEntity();
            var entity2 = em.CreateEntity();
            
            Assert.IsTrue(em.Exists(entity1));
            Assert.IsTrue(em.Exists(entity2));
        }

        [Test]
        public void DestroyedEntityDoesNotExist()
        {
            var em = EntityManager.Create();
            var entity1 = em.CreateEntity();
            em.DestroyEntity(entity1);
            
            Assert.IsFalse(em.Exists(entity1));
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
    }
}