using System;
using Unity.Collections;

namespace Code
{
    public struct EntityGroup<T> : IDisposable where T : unmanaged, IEquatable<T>
    {
        // API:
        // Add, Remove, RemoveAtSwapBack, Contains,
        // Exception on Remove while iterating
        // IndexOf
        // T implements IEquatable

        // Reasoning for this: Data Oriented Coding booster
        // Entities are stored as array

        public NativeList<T> Entities;
        public NativeList<GenId> Ids;
        public NativeHashMap<GenId, int> IdToIndex;
        public bool IsCreated;
        
        public int Length => Entities.Length;

        public static EntityGroup<T> Create(int initialCapacity)
        {
            return new EntityGroup<T>
            {
                Entities = new NativeList<T>(initialCapacity, Allocator.Persistent),
                IdToIndex = new NativeHashMap<GenId, int>(initialCapacity, Allocator.Persistent),
                Ids = new NativeList<GenId>(initialCapacity, Allocator.Persistent),
            };
        }

        public void Add(GenId id, T entity)
        {
            var count = Entities.Length;
            if (IdToIndex.TryAdd(id, count))
            {
                Entities.Add(entity);
                Ids.Add(id);
            }

            throw new Exception($"Entity '{id}' already exists.");
        }

        public int IndexOf(GenId id)
        {
            if (IdToIndex.TryGetValue(id, out var idx))
            {
                return idx;
            }

            return -1;
        }

        public bool Contains(GenId id)
        {
            return IndexOf(id) != -1;
        }

        public void RemoveAtSwapBack(int idx)
        {
            var id = Ids[idx];
            var lastIdx = Ids.Length - 1;

            if (idx == lastIdx)
            {
                Ids.RemoveAt(lastIdx);
                Entities.RemoveAt(lastIdx);
            }
            else
            {
                var lastId = Ids[lastIdx];
                Entities.RemoveAtSwapBack(idx);
                Ids.RemoveAtSwapBack(idx);
                IdToIndex[lastId] = idx;
            }

            IdToIndex.Remove(id);

            // TODO: Do we need to handle differently if it's the last element
            // TODO: Do we need to handle differently if len == 0
        }

        public void RemoveAtSwapBack(GenId id)
        {
            RemoveAtSwapBack(IdToIndex[id]);
        }

        public void Dispose()
        {
            Entities.Dispose();
            IdToIndex.Dispose();
            Ids.Dispose();
        }
    }
}