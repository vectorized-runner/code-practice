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

		[Test]
		public void AllocTyped()
		{
			Assert.DoesNotThrow(() =>
			{
				using var allocator = new LinearAllocator(sizeof(int));
				var intPtr = allocator.Alloc<int>();
				*intPtr = 4;
			});
		}

		[Test]
		public void AllocMultipleTyped()
		{
			Assert.DoesNotThrow(() =>
			{
				var count = 3;
				var size = sizeof(long);
				using var allocator = new LinearAllocator(count * size);
				var longPtr = allocator.Alloc<long>(count);

				for (int i = 0; i < count; i++)
				{
					longPtr[i] = i;
				}
			});
		}

		[Test]
		public void StackAllocUsage()
		{
			Assert.DoesNotThrow(() =>
			{
				// If you free this pointer, it'll crash
				const int size = 256;
				var allocator = new LinearAllocator(stackalloc byte[size]);
				var ptr = (byte*)allocator.Alloc(size);

				for (int i = 0; i < size; i++)
				{
					ptr[i] = (byte)i;
				}
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
			Assert.AreEqual(32, (int)AllocatorHelper.AlignForward((void*)32, 16));
		}

		[Test]
		public void AlignForward_2()
		{
			Assert.AreEqual(128, (int)AllocatorHelper.AlignForward((void*)32, 128));
		}

		[Test]
		public void AlignForward_3()
		{
			Assert.AreEqual(64, (int)AllocatorHelper.AlignForward((void*)64, 16));
		}

		[Test]
		public void AlignForward_4()
		{
			Assert.AreEqual(64, (int)AllocatorHelper.AlignForward((void*)64, 16));
		}

		[Test]
		public void AlignForward_5()
		{
			Assert.AreEqual(32, (int)AllocatorHelper.AlignForward((void*)27, 8));
		}

		[Test]
		public static void IsPow_0()
		{
			Assert.AreEqual(false, AllocatorHelper.IsPowerOfTwo(0));
		}

		[Test]
		public static void IsPow_1()
		{
			Assert.AreEqual(false, AllocatorHelper.IsPowerOfTwo(-1));
			Assert.AreEqual(false, AllocatorHelper.IsPowerOfTwo(-2));
			Assert.AreEqual(false, AllocatorHelper.IsPowerOfTwo(-4));
			Assert.AreEqual(false, AllocatorHelper.IsPowerOfTwo(int.MinValue));
		}

		[TestCase(100, 4)]
		[TestCase(32, 16)]
		[TestCase(28, 4)]
		[TestCase(4, 4)]
		[TestCase(256, 2)]
		[TestCase(1024, 32)]
		public void IsAligned_Pass(long memAddress, int align)
		{
			Assert.IsTrue(AllocatorHelper.IsAligned((void*)memAddress, align));
		}

		[TestCase(50, 8)]
		[TestCase(20, 8)]
		[TestCase(100, 16)]
		[TestCase(16, 32)]
		[TestCase(40, 32)]
		[TestCase(22, 8)]
		public void IsAligned_Fail(long memAddress, int align)
		{
			Assert.IsFalse(AllocatorHelper.IsAligned((void*)memAddress, align));
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

		[Test]
		public void AlignedAlloc_Pass()
		{
			Assert.DoesNotThrow(() =>
			{
				using var allocator = new LinearAllocator(1_000_000);
				var alloc = allocator.Alloc(1, 1024);
				Assert.IsTrue(AllocatorHelper.IsAligned(alloc, 1024));
				var alloc2 = allocator.Alloc(1, 8);
				Assert.IsTrue(AllocatorHelper.IsAligned(alloc2, 8));
				var alloc3 = allocator.Alloc(1, 32);
				Assert.IsTrue(AllocatorHelper.IsAligned(alloc3, 32));
				var alloc4 = allocator.Alloc(1, 64);
				Assert.IsTrue(AllocatorHelper.IsAligned(alloc4, 64));
			});
		}

		[Test]
		public void AlignedAlloc_DoesNotFailOnExactSize()
		{
			Assert.DoesNotThrow(() =>
			{
				using var allocator = new LinearAllocator(128);
				allocator.Alloc(64, 64);
			});
		}
	}
}