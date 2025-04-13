using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public static unsafe class MemoryUtil
    {
        // Notice that this won't work in Uninitialized memory (alignment padding could be different)
        public static bool ByteCompare<T>(this T first, T second) where T : unmanaged
        {
            return UnsafeUtility.MemCmp(&first, &second, sizeof(T)) == 0;
        }
    }
}