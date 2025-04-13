using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public static unsafe class NativeExtensions
    {
        public static bool ValueEquals<T>(this NativeArray<T> first, NativeArray<T> second) where T : unmanaged, IEquatable<T>
        {
            if (first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValueEquals<T>(this NativeHashSet<T> first, NativeHashSet<T> second)
            where T : unmanaged, IEquatable<T>
        {
            if (first.Count != second.Count)
                return false;

            foreach (var element in first)
            {
                if (!second.Contains(element))
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ElementAt<T>(this NativeArray<T> arr, int index) where T : unmanaged
        {
            return ref UnsafeUtility.ArrayElementAsRef<T>(arr.GetUnsafePtr(), index);
        }
    }
}