using System;
using NUnit.Framework;

namespace Code.Tests
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