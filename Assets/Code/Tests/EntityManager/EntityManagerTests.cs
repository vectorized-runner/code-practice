using NUnit.Framework;

namespace CodePractice.Tests
{
    public class EntityManagerTests
    {
        private EntityManager _em;

        [SetUp]
        public void SetUp()
        {
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
    }
}