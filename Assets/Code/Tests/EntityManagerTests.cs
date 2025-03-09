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