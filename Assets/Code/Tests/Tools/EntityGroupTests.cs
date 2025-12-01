using System;
using NUnit.Framework;

namespace Code.Tests
{
    public class EntityGroupTests
    {
        [Test]
        public void CreatedAfterCreate()
        {
            var grp = EntityGroup<int>.Create(1);
            Assert.IsTrue(grp.IsCreated);
        }

        [Test]
        public void ExceptionOnNegativeCapacity()
        {
            Assert.Throws<Exception>(() => { EntityGroup<int>.Create(-1); });
        }

        [Test]
        public void NotCreatedAfterDispose()
        {
            var grp = EntityGroup<int>.Create(1);
            grp.Dispose();
            Assert.IsFalse(grp.IsCreated);
        }

        [Test]
        public void NoExceptionAfterDoubleFree()
        {
            Assert.DoesNotThrow(() =>
            {
                var grp = EntityGroup<int>.Create(1);
                grp.Dispose();
                grp.Dispose();
            });
        }

        [Test]
        public void AddThenContains()
        {
            var grp = EntityGroup<int>.Create(1);
            var id = new Id(1, 1);
            grp.Add(id, 0);

            Assert.IsTrue(grp.Contains(id));
        }

        [Test]
        public void DoesNotContainByDefault()
        {
            var grp = EntityGroup<int>.Create(1);
            Assert.IsFalse(grp.Contains(new Id(1, 1)));
        }

        [Test]
        public void RemoveThrowsWithoutAdd()
        {
            var grp = EntityGroup<int>.Create(1);
            Assert.Throws<IndexOutOfRangeException>(() => { grp.RemoveAtSwapBack(0); });
        }

        [Test]
        public void RemoveWorksAfterAdd()
        {
            var grp = EntityGroup<int>.Create(1);
            var id = new Id(1, 1);
            grp.Add(id, 0);
            grp.RemoveAtSwapBack(id);

            Assert.AreEqual(0, grp.Length);
        }

        [Test]
        public void CheckItemAfterRemove()
        {
            var grp = EntityGroup<int>.Create(1);
            var id0 = new Id(1, 1);
            var id1 = new Id(2, 1);
            var id2 = new Id(3, 1);

            grp.Add(id0, 0);
            grp.Add(id1, 1);
            grp.Add(id2, 2);

            // [0, 1, 2] -> [2, 1]
            grp.RemoveAtSwapBack(id0);

            Assert.AreEqual(2, grp.Length);
            Assert.AreEqual(2, grp.Entities[0]);
            Assert.AreEqual(1, grp.Entities[1]);

            // [2, 1] -> [1]
            grp.RemoveAtSwapBack(id2);

            Assert.AreEqual(1, grp.Length);
            Assert.AreEqual(1, grp.Entities[0]);

            // [1] -> []
            grp.RemoveAtSwapBack(id1);

            Assert.AreEqual(0, grp.Length);
        }
    }
}