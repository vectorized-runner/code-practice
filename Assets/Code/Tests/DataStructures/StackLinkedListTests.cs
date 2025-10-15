using System;
using NUnit.Framework;
using Random = Unity.Mathematics.Random;

namespace CodePractice.Tests
{
    public class StackLinkedListTests
    {
        [Test]
        public void LengthIncreaseAfterAdd()
        {
            var s = new StackLinkedList<int>();
            s.Push(10);
            
            Assert.AreEqual(1, s.Length);
        }

        [Test]
        public void LengthDecreaseAfterRemove()
        {
            var s = new StackLinkedList<int>();
            s.Push(10);
            s.Pop();
            
            Assert.AreEqual(0, s.Length);
        }

        [Test]
        public void StackAdd_Big()
        {
            var s = new StackLinkedList<int>();
            var arr = new int[10_000];
            var rand = new Random(248935);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.NextInt();
            }

            for (int i = 0; i < arr.Length; i++)
            {
                s.Push(arr[i]);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Assert.AreEqual(arr[arr.Length - i - 1], s.Pop());
            }
        }
        
        [Test]
        public void StackAdd()
        {
            var s = new StackLinkedList<int>();
            s.Push(5);
            s.Push(10);
            s.Push(-1);
            s.Push(int.MaxValue);
            
            Assert.AreEqual(int.MaxValue, s.Pop());
            Assert.AreEqual(-1, s.Pop());
            Assert.AreEqual(10, s.Pop());
            Assert.AreEqual(5, s.Pop());
            Assert.AreEqual(0, s.Length);
        }

        [Test]
        public void ThrowOnLength0()
        {
            var s = new StackLinkedList<int>();
            s.Push(10);
            s.Push(10);
            s.Pop();
            s.Pop();

            Assert.Throws<Exception>(() =>
            {
                s.Pop();
            });
        }
    }
}