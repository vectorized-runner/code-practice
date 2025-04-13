using System;
using Unity.Collections;

namespace CodePractice
{
    public struct Archetype : IDisposable
    {
        public NativeArray<Chunk> Chunks;
        public NativeArray<ComponentType> Types;
        
        public void Dispose()
        {
            for (int i = 0; i < Chunks.Length; i++)
            {
                Chunks[i].Dispose();
            }
            
            Chunks.Dispose();
            Types.Dispose();
        }
    }
}