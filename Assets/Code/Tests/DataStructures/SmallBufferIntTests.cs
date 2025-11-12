using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
    public unsafe class SmallBufferIntTests
    {
        [Test]
        public void IntPtrSize()
        {
            Assert.AreEqual(8, sizeof(int*));
        }

        [Test]
        public void SmallBufferSize()
        {
            Assert.AreEqual(20, sizeof(SmallBufferInt));
        }

        [Test]
        public void DataIsNullByDefault()
        {
            var buf = new SmallBufferInt();
            Assert.IsTrue(buf.Data == null);
        }

        [Test]
        public void UseBufferByDefault()
        {
            var buf = new SmallBufferInt();
            Assert.IsTrue(buf.IsUsingBuffer());
        }
        
        [Test]
        public void CheckContent_SmallBufferUsed_Simple()
        {
            var buf = new SmallBufferInt(new[] { 10 });
            Assert.AreEqual(10, buf[0]);
        }
        
        [Test]
        public void CheckContent_SmallBufferUsed_Simple2()
        {
            var buf = new SmallBufferInt(new[] { 10, 5, 2 });
            Assert.AreEqual(10, buf[0]);
            Assert.AreEqual(5, buf[1]);
            Assert.AreEqual(2, buf[2]);
        }

        [Test]
        public void CheckContent_SmallBufferUsed()
        {
            var buf = new SmallBufferInt(new[] { 10, 5, 2, 1 });
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
                var buf = new SmallBufferInt(new[] { 10, 5, 2, 1 });
                var myItem = buf[5];
            });
        }

        [Test]
        public void BothZeroByDefault()
        {
            var buf = new SmallBufferInt();
            Assert.IsTrue(buf.Data == null);
            Assert.IsTrue(IsBufferZero(buf.Buffer, 16));
        }

        [Test]
        public void CheckContent_UsingPointer()
        {
            var buf = new SmallBufferInt(new int[] { 2, -100, 43249023, 0, 348727452 });

            Assert.AreEqual(2, buf[0]);
            Assert.AreEqual(-100, buf[1]);
            Assert.AreEqual(43249023, buf[2]);
            Assert.AreEqual(0, buf[3]);
            Assert.AreEqual(348727452, buf[4]);
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