using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Debug = UnityEngine.Debug;

namespace CodePractice
{
    public static unsafe class Util
    {
        [Conditional("DEBUG_CHECKS")]
        public static void CheckLength(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException($"Length {length} can't be negative.");
            }
        }

        [Conditional("DEBUG_CHECKS")]
        public static void CheckIndexInRange(int index, int length)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException($"Index {index} is negative");
            }

            if (index >= length)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range of length {length}");
            }
        }

        public static bool IsPow2(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public static void* AlignForward(void* ptr, int alignment)
        {
            return (void*)AlignForward((long)ptr, alignment);
        }
        public static long AlignForward(long address, int alignment)
        {
            Debug.Assert(alignment >= 0);
            Debug.Assert(IsPow2(alignment));

            // Same as (address % alignment) but faster as 'alignment' is a power of two
            var mod = address & (alignment - 1);
            if (mod == 0)
            {
                return address;
            }

            return address + alignment - mod;
        }

        public static void* Malloc(int length, int align)
        {
            return UnsafeUtility.Malloc(length, align, Allocator.Persistent);
        }

        public static void Free(void* ptr)
        {
            UnsafeUtility.Free(ptr, Allocator.Persistent);
        }

        public static void MemClear(void* ptr, int length)
        {
            UnsafeUtility.MemClear(ptr, length);
        }

        public static T* Malloc<T>(int length) where T : unmanaged
        {
            return (T*)UnsafeUtility.Malloc(length * sizeof(T), UnsafeUtility.AlignOf<T>(), Allocator.Persistent);
        }

        public static void MemClear<T>(T* memory, int length) where T : unmanaged
        {
            UnsafeUtility.MemClear(memory, sizeof(T) * length);
        }
    }
}