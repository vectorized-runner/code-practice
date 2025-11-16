using System;
using Unity.Collections;

namespace CodePractice
{
    public struct Archetype : IDisposable
    {
        public NativeArray<int> SortedTypeIndices;

        public void Dispose()
        {
            SortedTypeIndices.Dispose();
        }
    }
}