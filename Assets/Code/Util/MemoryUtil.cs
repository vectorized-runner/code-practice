#define DEBUG_CHECKS
using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public static unsafe class MemoryUtil
    {
        public const string DebugCondition = "DEBUG_CHECKS";
        public const int DefaultAlign = 16;
        
        [Conditional(DebugCondition)]
        public static void CheckLength(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException($"Length {length} can't be negative.");
            }
        }

        [Conditional(DebugCondition)]
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

        [Conditional(DebugCondition)]
        public static void CheckAlignment(int align)
        {
            if (!IsPowerOfTwo(align))
                throw new Exception($"Alignment {align} must be a power of two.");
        }
        
        public static bool IsPowerOfTwo(int x)
        {
            return x > 0 && (x & (x - 1)) == 0;
        }
        
        public static void* AlignForward(void* ptr, int align)
        {
            CheckAlignment(align);

            var ptrValue = (long)ptr;

            // Same as (address % alignment) but faster as 'alignment' is a power of two
            var mod = ptrValue & (align - 1);
            if (mod == 0)
            {
                return (void*)ptrValue;
            }

            return (void*)(ptrValue + align - mod);
        }

        public static void* Malloc(int length, int align = DefaultAlign)
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