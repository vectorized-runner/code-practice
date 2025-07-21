using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
	public unsafe class LinearAllocatorTests
	{
		[Test]
		public void SizeMatch()
		{
			using var allocator = new LinearAllocator(128);
			Assert.AreEqual(128, allocator.Length);
		}

		[Test]
		public void DefaultAllocFails()
		{
			Assert.Throws<Exception>(() =>
			{
				using var alloc = new LinearAllocator();
				alloc.Alloc(10);
			});
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(4)]
		[TestCase(10)]
		[TestCase(16)]
		[TestCase(32)]
		[TestCase(64)]
		[TestCase(99)]
		[TestCase(1024)]
		[TestCase(1_000_000)]
		public void ExactSizeAllocDoesNotThrow(int size)
		{
			Assert.DoesNotThrow(() =>
			{
				using var allocator = new LinearAllocator(size);
				allocator.Alloc(size);
			});
		}

		[TestCase(129)]
		[TestCase(256)]
		[TestCase(512)]
		[TestCase(int.MaxValue)]
		public void OutOfBoundsAllocThrows(int allocSize)
		{
			Assert.Throws<Exception>(() =>
			{
				using var allocator = new LinearAllocator(128);
				allocator.Alloc(allocSize);
			});
		}

		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(-2)]
		[TestCase(-100)]
		[TestCase(int.MinValue)]
		public void NonPositiveAllocThrows(int size)
		{
			Assert.Throws<Exception>(() =>
			{
				using var alloc = new LinearAllocator(128);
				alloc.Alloc(size);
			});
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
			Assert.AreEqual(false, MemoryUtil.IsPowerOfTwo(int.MinValue));
		}

		[Test]
		public void ConsecutiveAllocDoesNotFail()
		{
			Assert.DoesNotThrow(() =>
			{
				using var alloc = new LinearAllocator(32);
				alloc.Alloc(1);
				alloc.Alloc(10);
				alloc.Alloc(9);
				alloc.Alloc(5);
				alloc.Alloc(7);
			});
		}
	}
}