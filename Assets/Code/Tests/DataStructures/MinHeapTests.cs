using NUnit.Framework;

namespace CodePractice.Tests
{
    public class MinHeapTests
    {
        [Test]
        public void ResizeProperly()
        {
            var heap = new MinHeap<int>(4);
            Assert.AreEqual(4, heap.Capacity);
            heap.Add(3);
            heap.Add(4);
            heap.Add(5);
            heap.Add(6);
            
            Assert.AreEqual(8, heap.Capacity);
        }
        
        [Test]
        public void SetCapacityBelowMinimum()
        {
            var heap = new MinHeap<int>(-1);
            Assert.AreEqual(4, heap.Capacity);
        }
        
        [Test]
        public void CapacitySetNonPow2()
        {
            var heap = new MinHeap<int>(20);
            Assert.AreEqual(heap.Capacity, 32);
        }

        [Test]
        public void SetCapacityCeilPow()
        {
            var heap = new MinHeap<int>(8);
            Assert.AreEqual(heap.Capacity, 8);
        }
        
        [Test]
        public void CapacityPositiveByDefault()
        {
            var heap = new MinHeap<int>();
            Assert.Greater(heap.Capacity, 0);
        }
        
        [Test]
        public void CapacityPositiveAfterAdd()
        {
            var heap = new MinHeap<int>();
            heap.Add(5);
            Assert.Greater(heap.Capacity, 0);
        }
        
        [Test]
        public void CountZeroByDefault()
        {
            var heap = new MinHeap<int>();
            Assert.AreEqual(0, heap.Count);
        }

        [Test]
        public void CountOneAfterAdd()
        {
            var heap = new MinHeap<int>();
            heap.Add(49345);
            Assert.AreEqual(1, heap.Count);
        }

        [Test]
        public void CountZeroAfterRemove()
        {
            var heap = new MinHeap<int>();
            heap.Add(348925);
            heap.Remove();
            Assert.AreEqual(0, heap.Count);
        }

        [Test]
        public void AddRemove_0()
        {
            var heap = new MinHeap<int>();
            heap.Add(9);
            heap.Add(1);
            heap.Add(0);
            Assert.AreEqual(0, heap.Remove());
            Assert.AreEqual(1, heap.Remove());
            Assert.AreEqual(9, heap.Remove());
        }

        [Test]
        public void Contains()
        {
            var heap = new MinHeap<int>();
            heap.Add(9);
            heap.Add(1);
            heap.Add(0);
            heap.Add(-3);
            heap.Add(15);

            Assert.IsTrue(heap.Contains(9));
            Assert.IsTrue(heap.Contains(1));
            Assert.IsTrue(heap.Contains(0));
            Assert.IsTrue(heap.Contains(-3));
            Assert.IsTrue(heap.Contains(15));
        }

        [Test]
        public void AddRemove_1()
        {
            var heap = new MinHeap<int>();
            heap.Add(9);
            heap.Add(1);
            heap.Add(0);
            heap.Add(-3);
            heap.Add(int.MinValue);

            var intArr = heap.GetInternalArray();
            for (int i = 0; i < intArr.Length; i++)
            {
                intArr[i] = int.MinValue;
            }
            
            Assert.AreEqual(5, heap.Count);
            Assert.AreEqual(int.MinValue, heap.Remove());
            Assert.AreEqual(4, heap.Count);
            Assert.AreEqual(-3, heap.Remove());
            Assert.AreEqual(0, heap.Remove());
            Assert.AreEqual(1, heap.Remove());
            Assert.AreEqual(9, heap.Remove());
        }
        
        [Test]
        public void AddRemove()
        {
            var heap = new MinHeap<int>();
            heap.Add(9);
            heap.Add(1);
            heap.Add(0);
            heap.Add(23);
            heap.Add(-1);
            heap.Add(8);
            heap.Add(0);
            heap.Add(-3);
            heap.Add(int.MaxValue);
            heap.Add(49);
            heap.Add(int.MinValue);
            heap.Add(12);
            
            Assert.AreEqual(int.MinValue, heap.Remove());
            Assert.AreEqual(-3, heap.Remove());
            Assert.AreEqual(-1, heap.Remove());
            Assert.AreEqual(0, heap.Remove());
            Assert.AreEqual(0, heap.Remove());
            Assert.AreEqual(1, heap.Remove());
            Assert.AreEqual(8, heap.Remove());
            Assert.AreEqual(9, heap.Remove());
            Assert.AreEqual(12, heap.Remove());
            Assert.AreEqual(23, heap.Remove());
            Assert.AreEqual(49, heap.Remove());
        }
    }
}