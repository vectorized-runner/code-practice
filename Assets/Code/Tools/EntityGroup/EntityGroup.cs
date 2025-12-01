using System;
using Unity.Collections;

namespace Code
{
    public struct EntityGroup<T> : IDisposable where T : unmanaged
    {
        public NativeList<T> Entities;
        public NativeList<Id> Ids;
        public NativeHashMap<Id, int> IdToIndex;
        public bool IsCreated;

        public int Length => Entities.Length;

        public EntityGroup(int initialCapacity)
        {
            this = Create(initialCapacity);
        }

        public static EntityGroup<T> Create(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new Exception("Capacity should be positive");

            return new EntityGroup<T>
            {
                Entities = new NativeList<T>(initialCapacity, Allocator.Persistent),
                IdToIndex = new NativeHashMap<Id, int>(initialCapacity, Allocator.Persistent),
                Ids = new NativeList<Id>(initialCapacity, Allocator.Persistent),
                IsCreated = true,
            };
        }

        public void Add(Id id, T entity)
        {
            var count = Entities.Length;
            if (!IdToIndex.TryAdd(id, count))
            {
                throw new Exception($"Entity '{id}' already exists.");
            }

            Entities.Add(entity);
            Ids.Add(id);
        }

        public int IndexOf(Id id)
        {
            if (IdToIndex.TryGetValue(id, out var idx))
            {
                return idx;
            }

            return -1;
        }

        public bool Contains(Id id)
        {
            return IndexOf(id) != -1;
        }

        public void RemoveAtSwapBack(int idx)
        {
            var id = Ids[idx];
            var lastIdx = Ids.Length - 1;

            // Implementation could be correct without lastIdx check
            // if (idx == lastIdx)
            // {
            //     Ids.RemoveAt(lastIdx);
            //     Entities.RemoveAt(lastIdx);
            // }
            // else
            {
                var lastId = Ids[lastIdx];
                Entities.RemoveAtSwapBack(idx);
                Ids.RemoveAtSwapBack(idx);
                IdToIndex[lastId] = idx;
            }

            IdToIndex.Remove(id);
        }

        public void RemoveAtSwapBack(Id id)
        {
            RemoveAtSwapBack(IdToIndex[id]);
        }

        public void Dispose()
        {
            if (IsCreated)
            {
                Entities.Dispose();
                IdToIndex.Dispose();
                Ids.Dispose();
                IsCreated = false;
            }
        }
    }
}