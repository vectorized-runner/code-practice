using NUnit.Framework;

namespace Code.Tests
{
    public class GenIdManagerTests
    {
        // Work with Index & Version 
        // Default Index = 0, Version for each index is 0
        // Version is incremented once that index is destroyed
        // Exists -> Index >= LastUsedIndex
        // No Exception on Double dispose

        private static GenIdManager _genIdManager;

        [SetUp]
        public void Setup()
        {
            _genIdManager = GenIdManager.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _genIdManager.Dispose();
        }

        [Test]
        public void DefaultDoesNotExist()
        {
            Assert.IsFalse(_genIdManager.Exists(new GenId()));
        }

        [Test]
        public void DoubleDisposeWorks()
        {
            Assert.DoesNotThrow(() =>
            {
                _genIdManager.Dispose();
                _genIdManager.Dispose();
            });
        }
        
        [Test]
        public static void FirstIdIsOne()
        {
            Assert.AreEqual(new GenId(1, 0), _genIdManager.CreateId());
        }

        [Test]
        public static void SecondId()
        {
            _genIdManager.CreateId();
            Assert.AreEqual(new GenId(2, 0), _genIdManager.CreateId());
        }

        [Test]
        public static void DestroyIdWorksAfterCreated()
        {
            var id = _genIdManager.CreateId();
            Assert.IsTrue(_genIdManager.DestroyId(id));
        }

        [Test]
        public static void IndexRecycle()
        {
            var id = _genIdManager.CreateId();
            _genIdManager.DestroyId(id);
            
            Assert.AreEqual(new GenId(1, 1), _genIdManager.CreateId());
        }
        
        [Test]
        public static void DestroyIdDoesNotWorkTwice()
        {
            var id = _genIdManager.CreateId();
            _genIdManager.DestroyId(id);
            Assert.IsFalse(_genIdManager.DestroyId(id));
        }

        [Test]
        public static void DoesNotExistByDefault()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    Assert.IsFalse(_genIdManager.Exists(new GenId(i, j)));
                }
            }
            
        }

        [Test]
        public static void ExistsAfterCreated()
        {
            var id = _genIdManager.CreateId();
            Assert.IsTrue(_genIdManager.Exists(id));
        }

        [Test]
        public static void DoesNotExistAfterDestroyed()
        {
            var id = _genIdManager.CreateId();
            _genIdManager.DestroyId(id);
            Assert.IsFalse(_genIdManager.Exists(id));
        }

        [Test]
        public static void IncrementalIds()
        {
            for (int i = 0; i < 1_000; i++)
            {
                Assert.AreEqual(new GenId(i + 1, 0), _genIdManager.CreateId());
            }
        }

        [Test]
        public static void IndexRecycledOnDestroy()
        {
            var previous = _genIdManager.CreateId();

            for (int i = 0; i < 100; i++)
            {
                _genIdManager.DestroyId(previous);
                var newId = _genIdManager.CreateId();
                Assert.AreEqual(previous.Version + 1, newId.Version);
                Assert.AreEqual(previous.Index, newId.Index);
                previous = newId;
            }
        }

        [Test]
        public static void RealWorldScenario()
        {
            // Get 3 idx, Kill 2, then get 3 next one
            var id10 = _genIdManager.CreateId(); // (1, 0)
            var id20 = _genIdManager.CreateId(); // (2, 0)
            var id30 = _genIdManager.CreateId(); // (3, 0)

            _genIdManager.DestroyId(id10);
            _genIdManager.DestroyId(id20);

            var id11 = _genIdManager.CreateId();
            var id21 = _genIdManager.CreateId();

            Assert.AreEqual(id11.Index, id10.Index);
            Assert.AreEqual(id10.Version + 1, id11.Version);

            Assert.AreEqual(id20.Index, id21.Index);
            Assert.AreEqual(id20.Version + 1, id21.Version);

            var id40 = _genIdManager.CreateId();

            Assert.AreEqual(new GenId(4, 0), id40);
        }

        [Test]
        public static void TestEqual_0()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    Assert.AreEqual(new GenId(i, j), new GenId(i, j));
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
                        Assert.AreNotEqual(new GenId(i, j), new GenId(j, i));
                    }
                }
            }
        }
    }
}