using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace CodePractice
{
    public struct Chunk : IDisposable
    {
        // Chunk Layout
        // EntityIds

        public NativeArray<byte> Memory;
        public int Count;
        public int Capacity;

        public const int KiB = 1024;
        public const int ChunkSize = 16 * KiB;
        
        public static Chunk Create()
        {
            return new Chunk
            {
                Memory = new NativeArray<byte>(ChunkSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory),
                Count = 0,
                Capacity = 0
            };
        }

        public void Dispose()
        {
            Memory.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFull()
        {
            return Count == Capacity;
        }
        
        public void AddEntity()
        {
            // If full, throw
        }
        
        public int GetEntityIndex(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}