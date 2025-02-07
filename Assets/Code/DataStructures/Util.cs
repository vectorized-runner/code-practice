using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Code
{
	public static unsafe class Util
	{
		public static void* Malloc<T>(int count) where T : unmanaged
		{
			return UnsafeUtility.Malloc(count * sizeof(T), UnsafeUtility.AlignOf<T>(), Allocator.Persistent);
		}
	}
}