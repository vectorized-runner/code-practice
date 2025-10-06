using System;

namespace Code.Tricks
{
    public static class SentinelSearch
    {
        public static int IndexOf<T>(T[] items, T item) where T : IEquatable<T>
        {
            // Naive implementation:
            // Problem: 2 branches per loop
            /*
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }
            */

            var len = items.Length;
            if (len == 0)
                return -1;

            var lastIdx = len - 1;
            var original = items[lastIdx];
            
            // Place sentinel
            items[lastIdx] = item;

            var i = 0;
            while (!items[i].Equals(item))
            {
                i++;
            }

            // Restore
            items[lastIdx] = original;
            
            if (i < lastIdx || original.Equals(item))
                return i;

            return -1;
        } 
    }
}