using System;
using NUnit.Framework;

namespace CodePractice.Tests
{
	public class HashMapTests
	{
		[Test]
		public void AddSingleDoesNotThrow()
		{
			var map = new HashMap<int, int>();
			Assert.DoesNotThrow(() =>
			{
				map.Add(1, 1);
			});
		}
		
		[Test]
		public void AddDoubleKeyThrows()
		{
			var map = new HashMap<int, int>();
			map.Add(1, 1);
			
			Assert.Throws<Exception>(() =>
			{
				map.Add(1, 2);
			});
		}

		[Test]
		public void AddDoubleValueDoesNotThrow()
		{
			Assert.DoesNotThrow(() =>
			{
				var map = new HashMap<int, int>();
				map.Add(1, 1);
				map.Add(0, 1);
			});
		}

		[Test]
		public void CanNotAddTwice()
		{
			var map = new HashMap<int, int>();
			map.Add(1, 1);
			
			Assert.IsFalse(map.TryAdd(1, 2));
		}

		[Test]
		public void DoesNotContainBeforeAdd()
		{
			var map = new HashMap<int, int>();
			
			Assert.IsFalse(map.ContainsKey(0));
			Assert.IsFalse(map.ContainsKey(1));
		}

		[Test]
		public void RemoveDoesNotThrow()
		{
			var map = new HashMap<int, int>();
			map.Add(4359834, 3498534);
			
			Assert.DoesNotThrow(() =>
			{
				Assert.IsFalse(map.Remove(343486736));
			});
		}

		[Test]
		public void DoesNotExistAfterRemove()
		{
			var map = new HashMap<int, int>();
			map.Add(4359834, 3498534);
			map.Add(21314, 4539);

			Assert.IsTrue(map.Remove(4359834));
			Assert.IsFalse(map.ContainsKey(4359834));
			Assert.IsTrue(map.ContainsKey(21314));
		}
	}
}