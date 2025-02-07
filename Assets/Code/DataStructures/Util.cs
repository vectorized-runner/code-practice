using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Code
{
	public static unsafe class Util
	{
		public static T* Malloc<T>(int count) where T : unmanaged
		{
			return (T*)UnsafeUtility.Malloc(count * sizeof(T), UnsafeUtility.AlignOf<T>(), Allocator.Persistent);
		}

		public static void Free(void* ptr)
		{
			UnsafeUtility.Free(ptr, Allocator.Persistent);
		}
	}
}