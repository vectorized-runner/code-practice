using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public static unsafe class NativeExtensions
    {
        public static bool ValueEquals<T>(this NativeArray<T> a, NativeArray<T> b) where T : unmanaged, IEquatable<T>
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValueEquals<T>(this NativeHashSet<T> a, NativeHashSet<T> b)
            where T : unmanaged, IEquatable<T>
        {
            if (a.Count != b.Count)
                return false;

            foreach (var element in a)
            {
                if (!b.Contains(element))
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