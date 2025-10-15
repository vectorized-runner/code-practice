using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
    public class StackTests
    {
        [Test]
        public void LengthIncreaseAfterAdd()
        {
            var s = new Stack<int>();
            s.Push(10);
            
            Assert.AreEqual(1, s.Length);
        }

        [Test]
        public void LengthDecreaseAfterRemove()
        {
            var s = new Stack<int>();
            s.Push(10);
            s.Pop();
            
            Assert.AreEqual(0, s.Length);
        }
        
        [Test]
        public void StackAdd()
        {
            var s = new Stack<int>();
            s.Push(5);
            s.Push(10);
            s.Push(-1);
            s.Push(int.MaxValue);
            
            Assert.AreEqual(int.MaxValue, s.Pop());
            Assert.AreEqual(-1, s.Pop());
            Assert.AreEqual(10, s.Pop());
            Assert.AreEqual(5, s.Pop());
            Assert.AreEqual(0, s.Length);
            Assert.AreEqual(4, s.Capacity);
        }

        [Test]
        public void ThrowOnLength0()
        {
            var s = new Stack<int>();
            s.Push(10);
            s.Push(10);
            s.Pop();
            s.Pop();

            Assert.Throws<Exception>(() =>
            {
                s.Pop();
            });
        }
        
        [Test]
        public void NegativeCapacityThrows()
        {
            Assert.Throws<Exception>(() =>
            {
                var s = new Stack<int>(-1);
            });
        }
    }
}