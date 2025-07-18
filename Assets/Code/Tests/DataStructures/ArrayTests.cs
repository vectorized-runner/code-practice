using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
	public static class ArrayTests
	{
		private static Array<int> _cleanupArray;

		[TearDown]
		public static void TearDown()
		{
			_cleanupArray.Dispose();
		}

		[Test]
		public static void RefIterator()
		{
			// var arr = new Array<int>(10);
			//
			// foreach (ref var item in arr)
			// {
			// 	
			// }
		}
		
		[Test]
		public static void NegativeIndexThrows()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				using var arr = new Array<int>(-1);
			});
		} 
		
		[Test]
		public static void InRangeAccess()
		{
			var arr = new Array<int>(100);

			for (int i = 0; i < 5; i++)
			{
				arr[i] = i;
			}

			for (int i = 0; i < 5; i++)
			{
				Assert.AreEqual(i, arr[i]);
			}
			
			arr.Dispose();
		}

		[Test]
		public static void Equals()
		{
			var arr = new Array<int>(1);
			var arr2 = new Array<int>(1);
			var copy = arr;
			
			Assert.AreEqual(arr, copy);
			Assert.AreNotEqual(arr, arr2);
			Assert.AreNotEqual(copy, arr2);
		}

		[Test]
		public static void GetHashCodeDoesNotThrowOnEmpty()
		{
			Assert.DoesNotThrow(() =>
			{
				var arr = new Array<int>();
				var hash = arr.GetHashCode();
			});
		}
		
		[Test]
		public static void OutOfRangeAccessThrows()
		{
			_cleanupArray = new Array<int>(10);

			Assert.Throws<IndexOutOfRangeException>(() =>
			{
				var item = _cleanupArray[100];
			});
		}
	}
}