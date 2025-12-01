using NUnit.Framework;

namespace Code.Tests
{
    public class IdManagerTests
    {
        // Work with Index & Version 
        // Default Index = 0, Version for each index is 0
        // Version is incremented once that index is destroyed
        // Exists -> Index >= LastUsedIndex
        // No Exception on Double dispose

        private static IdManager _idManager;

        [SetUp]
        public void Setup()
        {
            _idManager = IdManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _idManager.Dispose();
        }

        [Test]
        public void DefaultDoesNotExist()
        {
            Assert.IsFalse(_idManager.Exists(new Id()));
        }

        [Test]
        public void DoubleDisposeWorks()
        {
            Assert.DoesNotThrow(() =>
            {
                _idManager.Dispose();
                _idManager.Dispose();
            });
        }
        
        [Test]
        public static void FirstIdIsOne()
        {
            Assert.AreEqual(new Id(1, 0), _idManager.CreateId());
        }

        [Test]
        public static void SecondId()
        {
            _idManager.CreateId();
            Assert.AreEqual(new Id(2, 0), _idManager.CreateId());
        }

        [Test]
        public static void DestroyIdWorksAfterCreated()
        {
            var id = _idManager.CreateId();
            Assert.IsTrue(_idManager.DestroyId(id));
        }

        [Test]
        public static void IndexRecycle()
        {
            var id = _idManager.CreateId();
            _idManager.DestroyId(id);
            
            Assert.AreEqual(new Id(1, 1), _idManager.CreateId());
        }
        
        [Test]
        public static void DestroyIdDoesNotWorkTwice()
        {
            var id = _idManager.CreateId();
            _idManager.DestroyId(id);
            Assert.IsFalse(_idManager.DestroyId(id));
        }

        [Test]
        public static void DoesNotExistByDefault()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    Assert.IsFalse(_idManager.Exists(new Id(i, j)));
                }
            }
            
        }

        [Test]
        public static void ExistsAfterCreated()
        {
            var id = _idManager.CreateId();
            Assert.IsTrue(_idManager.Exists(id));
        }

        [Test]
        public static void DoesNotExistAfterDestroyed()
        {
            var id = _idManager.CreateId();
            _idManager.DestroyId(id);
            Assert.IsFalse(_idManager.Exists(id));
        }

        [Test]
        public static void IncrementalIds()
        {
            for (int i = 0; i < 1_000; i++)
            {
                Assert.AreEqual(new Id(i + 1, 0), _idManager.CreateId());
            }
        }

        [Test]
        public static void IndexRecycledOnDestroy()
        {
            var previous = _idManager.CreateId();

            for (int i = 0; i < 100; i++)
            {
                _idManager.DestroyId(previous);
                var newId = _idManager.CreateId();
                Assert.AreEqual(previous.Version + 1, newId.Version);
                Assert.AreEqual(previous.Index, newId.Index);
                previous = newId;
            }
        }

        [Test]
        public static void RealWorldScenario()
        {
            // Get 3 idx, Kill 2, then get 3 next one
            var id10 = _idManager.CreateId(); // (1, 0)
            var id20 = _idManager.CreateId(); // (2, 0)
            var id30 = _idManager.CreateId(); // (3, 0)

            _idManager.DestroyId(id10);
            _idManager.DestroyId(id20);

            var id11 = _idManager.CreateId();
            var id21 = _idManager.CreateId();

            Assert.AreEqual(id11.Index, id10.Index);
            Assert.AreEqual(id10.Version + 1, id11.Version);

            Assert.AreEqual(id20.Index, id21.Index);
            Assert.AreEqual(id20.Version + 1, id21.Version);

            var id40 = _idManager.CreateId();

            Assert.AreEqual(new Id(4, 0), id40);
        }

        [Test]
        public static void TestEqual_0()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    Assert.AreEqual(new Id(i, j), new Id(i, j));
                }
            }
        }

        [Test]
        public static void TestEqual_1()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    if (i != j)
                    {
                        Assert.AreNotEqual(new Id(i, j), new Id(j, i));
                    }
                }
            }
        }
    }
}