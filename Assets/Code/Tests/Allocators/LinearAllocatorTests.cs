using NUnit.Framework;

namespace CodePractice.Tests
{
    public unsafe class LinearAllocatorTests
    {
        [Test]
        public void SizeMatch()
        {
            var allocator = new LinearAllocator(128);
            Assert.AreEqual(128, allocator.Length);
        }

        [Test]
        public void DefaultAllocFails()
        {
            var alloc = new LinearAllocator();
            var ptr = alloc.Alloc(10);
            
            Assert.IsTrue(ptr == null);
        }

        [Test]
        public void OutOfBoundsAllocFails()
        {
            var allocator = new LinearAllocator(128);
            var ptr = allocator.Alloc(129);  
            var ptr2 = allocator.Alloc(256);
            var ptr3 = allocator.Alloc(512);
            var ptr4 = allocator.Alloc(1024);
            Assert.IsTrue(ptr == null);
            Assert.IsTrue(ptr2 == null);
            Assert.IsTrue(ptr3 == null);
            Assert.IsTrue(ptr4 == null);
        }

        [Test]
        public void AlignForward_1()
        {
            Assert.AreEqual(32, (int)MemoryUtil.AlignForward((void*)32, 16));
        }
        
        [Test]
        public void AlignForward_2()
        {
            Assert.AreEqual(128, (int)MemoryUtil.AlignForward((void*)32, 128));
        }
        
        [Test]
        public void AlignForward_3()
        {
            Assert.AreEqual(64, (int)MemoryUtil.AlignForward((void*)64, 16));
        }
        
        [Test]
        public void AlignForward_4()
        {
            Assert.AreEqual(64, (int)MemoryUtil.AlignForward((void*)64, 16));
        }
        
        [Test]
        public void AlignForward_5()
        {
            Assert.AreEqual(32, (int)MemoryUtil.AlignForward((void*)27, 8));
        }

        [Test]
        public static void IsPow_0()
        {
            Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(0));
        }
        
        [Test]
        public static void IsPow_1()
        {
            Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(-1));
            Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(-2));
            Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(-4));
            Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(-int.MaxValue));
        }

        [Test]
        public void InBoundsAllocDoesNotFail()
        {
            
        }

        [Test]
        public void ExactSizeAllocDoesNotFail()
        {
            
        }

        [Test]
        public void ConsecutiveAllocDoesNotFail()
        {
            
        }
        
        [Test]
        public void ZeroAllocFails()
        {
            // TODO:
        }

        [Test]
        public void NegativeAllocFails()
        {
            // TODO:
        }
    }
}