using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public unsafe struct Chunk : IDisposable
    {
        // Chunk Layout
        // EntityIds

        public NativeArray<byte> Memory;
        public NativeArray<int> ComponentOffsets;
        public NativeArray<int> ComponentSizes;
        public int Count;
        public int Capacity;

        public const int KiB = 1024;
        public const int ChunkSize = 16 * KiB;

        private Archetype _archetype;
        
        // TODO: Probably should be clear memory, otherwise newly created entities will require mem-write (but do they already require it?)
        public static Chunk Create()
        {
            return new Chunk
            {
                Memory = new NativeArray<byte>(ChunkSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory),
                Count = 0,
                Capacity = 0
            };
        }

        // TODO: You're left in here
        // Calculate how many entities can fit (including the Entity size)
        // Calculate offsets by type and save
        // Adding entity -> Write to each individual array
        public void SetArchetype(Archetype archetype)
        {
            var totalComponentSize = sizeof(Entity);
            var componentCount = archetype.Components.Length;
            ComponentOffsets = new NativeArray<int>(componentCount, Allocator.Persistent);
            ComponentSizes = new NativeArray<int>(componentCount, Allocator.Persistent);
            
            for (int i = 0; i < componentCount; i++)
            {
                var comp = archetype.Components[i];
                var size = TypeManager.GetTypeInfo(comp.TypeIndex).Size;
                ComponentSizes[i] = size;
                totalComponentSize += size;
            }

            Capacity = ChunkSize / totalComponentSize;

            var baseOffset = sizeof(Entity) * Capacity;
            ComponentOffsets[0] = baseOffset;
            
            for (int i = 1; i < componentCount; i++)
            {
                ComponentOffsets[i] = baseOffset + ComponentSizes[i - 1] * Capacity;
            }
            
            _archetype = archetype;
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