using System;
using System.Collections;
using NUnit.Framework;
using Random = Unity.Mathematics.Random;

namespace CodePractice.Tests
{
    public class CircularQueueTests
    {
        [Test]
        public void Length0ByDefault()
        {
            var q = new CircularQueue<int>();
            Assert.AreEqual(0, q.Length);
        }

        [Test]
        public void Length1AfterAdd()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(10);
            Assert.AreEqual(1, q.Length);
        }

        [Test]
        public void Length0AfterAddRemove()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(10);
            q.Dequeue();
            Assert.AreEqual(0, q.Length);
        }
        
        [Test]
        public void ResizeThrows()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);

            Assert.Throws<Exception>(() =>
            {
                q.Enqueue(5);
            });
        }

        [Test]
        public void PeekThrowsOnLength0()
        {
            var q = new CircularQueue<int>();
            Assert.Throws<Exception>(() =>
            {
                q.Peek();
            });
        }

        [Test]
        public void DequeueThrowsOnLength0()
        {
            var q = new CircularQueue<int>();
            Assert.Throws<Exception>(() =>
            {
                q.Dequeue();
            });
        }
        
        [Test]
        public void PeekSimple_2()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(5);
            q.Enqueue(10);
            
            Assert.AreEqual(5, q.Peek());
        }
        
        [Test]
        public void EnqueueDequeueForeverLength0()
        {
            var q = new CircularQueue<int>();

            for (int i = 0; i < 1_000; i++)
            {
                q.Enqueue(i);
                q.Dequeue();
                Assert.AreEqual(0, q.Length);
            }
        }

        [Test]
        public void EnqueueDequeueForever()
        {
            var q = new CircularQueue<int>();

            for (int i = 0; i < 1_000; i++)
            {
                q.Enqueue(i);
                Assert.AreEqual(i, q.Dequeue());
            }
        }

        [Test]
        public void PeekSimple_3()
        {
            var q = new CircularQueue<int>(8);
            q.Enqueue(5);
            q.Enqueue(10);
            q.Enqueue(15);
            q.Enqueue(20);
            q.Enqueue(25);
            
            Assert.AreEqual(5, q.Peek());
        }

        [Test]
        public void PeekSimple()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(5);
            Assert.AreEqual(5, q.Peek());
        }

        [Test]
        public void EnqueueSimple()
        {
            var q = new CircularQueue<int>();
            q.Enqueue(5);
            Assert.AreEqual(5, q.Dequeue());
        }

        [Test]
        public void CircularSimple()
        {
            // [1, 2, 3, 4]
            var q = new CircularQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            
            Assert.AreEqual(4, q.Capacity);
            Assert.AreEqual(1, q.Dequeue());
            Assert.AreEqual(2, q.Dequeue());
            Assert.AreEqual(3, q.Dequeue());
            Assert.AreEqual(4, q.Dequeue());
        }

        [Test]
        public void CircularWrap()
        {
            // [1, 2, 3, 4] -> [5, 2, 3, 4]
            var q = new CircularQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Dequeue();
            q.Enqueue(5);
            
            Assert.AreEqual(4, q.Capacity);
            Assert.AreEqual(2, q.Dequeue());
            Assert.AreEqual(3, q.Dequeue());
            Assert.AreEqual(4, q.Dequeue());
            Assert.AreEqual(5, q.Dequeue());
        }

        [Test]
        public void LengthIncreaseAfterAdd()
        {
            var q = new CircularQueue<int>(1024);
            var rnd = new Random(248243875);
            
            for (int i = 0; i < 1_000; i++)
            {
                q.Enqueue(rnd.NextInt());
                Assert.AreEqual(i + 1, q.Length);
            }
        }

        [Test]
        public void LengthDecreaseAfterRemove()
        {
            var q = new CircularQueue<int>(1024);
            var rnd = new Random(305893568);
            var cnt = 1_000;
            
            for (int i = 0; i < cnt; i++)
            {
                q.Enqueue(rnd.NextInt());
            }

            for (int i = 0; i < cnt; i++)
            {
                q.Dequeue();
                Assert.AreEqual(cnt - i - 1, q.Length);
            }
        }

        [Test]
        public void FirstInFirstOut()
        {
            var q = new CircularQueue<int>(1024);
            var cnt = 1_000;
            var arr = new int[cnt];
            var rnd = new Random(34857359);

            for (int i = 0; i < cnt; i++)
            {
                var val = rnd.NextInt();
                arr[i] = val;
                q.Enqueue(val);
            }

            for (int i = 0; i < cnt; i++)
            {
                var val = q.Dequeue();
                Assert.AreEqual(arr[cnt - i - 1], val);
            }
        }
    }
}