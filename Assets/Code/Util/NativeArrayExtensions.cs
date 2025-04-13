using System;
using Unity.Collections;

namespace CodePractice
{
    public static class NativeArrayExtensions
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
    }
}