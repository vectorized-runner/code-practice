using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
    public unsafe class SmallBufferTests
    {
        [Test]
        public void SmallBufferSize()
        {
            Assert.AreEqual(20, sizeof(SmallBuffer<int>));
        }

        [Test]
        public void CheckIsUsingSmallBuffer()
        {
            var buf = new SmallBuffer<int>(new[] { 10 });
            Assert.IsTrue(buf.Data == null);
            Assert.IsFalse(IsBufferZero(buf.Buffer, 4));
        }
        
        [Test]
        public void CheckIsUsingSmallBuffer_Boundary()
        {
            var buf = new SmallBuffer<int>(new[] { 10, 5, 2, 1 });
            Assert.IsTrue(buf.Data == null);
            Assert.IsFalse(IsBufferZero(buf.Buffer, 4));
        }

        [Test]
        public void CheckContent_SmallBufferUsed()
        {
            var buf = new SmallBuffer<int>(new[] { 10, 5, 2, 1 });
            Assert.AreEqual(10, buf[0]);
            Assert.AreEqual(5, buf[1]);
            Assert.AreEqual(2, buf[2]);
            Assert.AreEqual(1, buf[3]);
        }

        [Test]
        public void ThrowsOutOfBounds()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var buf = new SmallBuffer<int>(new[] { 10, 5, 2, 1 });
                var myItem = buf[5];
            });
        }

        [Test]
        public void BothZeroByDefault()
        {
            var buf = new SmallBuffer<int>();
            Assert.IsTrue(buf.Data == null);
            Assert.IsTrue(IsBufferZero(buf.Buffer, 16));
        }

        [Test]
        public void CheckIsUsingPointer()
        {
            var buf = new SmallBuffer<int>(new int[] { 2, -100, 43249023, 0, 348727452 });
            Assert.IsTrue(buf.Data != null);
            Assert.IsTrue(IsBufferZero(buf.Buffer, 16));
        }

        private static bool IsBufferZero(byte* buf, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (buf[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}