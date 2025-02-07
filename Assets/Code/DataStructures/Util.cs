using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Code
{
	public static unsafe class Util
	{
		[Conditional("DEBUG_CHECKS")]
		public static void CheckLength(int length)
		{
			if (length < 0)
			{
				throw new Exception($"Length {length} can't be negative.");
			}
		}

		[Conditional("DEBUG_CHECKS")]
		public static void CheckIndexInRange(int index, int length)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException($"Index {index} is negative");
			}

			if (index >= length)
			{
				throw new ArgumentOutOfRangeException($"Index {index} is out of range of length {length}");
			}
		}

		public static T* Malloc<T>(int length) where T : unmanaged
		{
			return (T*)UnsafeUtility.Malloc(length * sizeof(T), UnsafeUtility.AlignOf<T>(), Allocator.Persistent);
		}
		}

		public static void Free(void* ptr)
		{
			UnsafeUtility.Free(ptr, Allocator.Persistent);
		}
	}
}