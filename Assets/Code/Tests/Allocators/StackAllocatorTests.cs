using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
	public class StackAllocatorTests
	{
		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(-100)]
		[TestCase(int.MinValue)]
		public void NonPositiveInitFails(int len)
		{
			Assert.Throws<Exception>(() =>
			{
				using var alloc = new StackAllocator(len);
			});
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(25)]
		[TestCase(64)]
		[TestCase(99)]
		[TestCase(32427)]
		[TestCase(1_000_000)]
		public void PositiveInitDoesNotFail(int len)
		{
			Assert.DoesNotThrow(() =>
			{
				using var alloc = new StackAllocator(len);
			});
		}
		
		[Test]
		public void SingleAlloc()
		{
			Assert.DoesNotThrow(() =>
			{
				throw new NotImplementedException();
				// using var alloc = new StackAllocator();
			});
		}
	}
}