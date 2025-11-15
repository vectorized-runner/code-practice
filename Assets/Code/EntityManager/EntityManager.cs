using System;
using Unity.Collections;

namespace CodePractice
{
    public struct EntityManager
    {
        private int _lastUsedIndex;
        private NativeQueue<int> _freeIndices;
        private NativeHashMap<int, int> _versionByIndex;
        private NativeList<Archetype> _archetypes;
        
        public static EntityManager Create()
        {
            return new EntityManager
            {
                _lastUsedIndex = 0,
                _freeIndices = new NativeQueue<int>(Allocator.Persistent),
                _versionByIndex = new NativeHashMap<int, int>(32, Allocator.Persistent),
                _archetypes = new NativeList<Archetype>(Allocator.Persistent),
            };
        }

        public void Dispose()
        {
            _freeIndices.Dispose();
            _versionByIndex.Dispose();

            for (int i = 0; i < _archetypes.Length; i++)
            {
                _archetypes[i].Dispose();
            }
            
            _archetypes.Dispose();
        }

        public void AddComponentData<T>(Entity entity, T component) where T : unmanaged, IComponent
        {
            throw new NotImplementedException();
        }
        
        public void AddComponent<T>(Entity entity) where T : unmanaged, IComponent
        {
            throw new NotImplementedException();
        }

        public T GetComponentData<T>(Entity entity) where T : unmanaged, IComponent
        {
            throw new NotImplementedException();
        }

        public Entity GetLastCreatedEntity()
        {
            throw new NotImplementedException();
        }

        public NativeArray<Entity> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public Entity Instantiate(ComponentType type)
        {
            // From a list of components
            // I need to find the Archetype
            // Archetype consists of a list of unique component types

            var temp = new NativeArray<ComponentType>(1, Allocator.Temp);
            temp[0] = type;
            var archetypeIndex = GetOrCreateArchetypeIndex(temp);
            ref var archetype = ref _archetypes.ElementAt(archetypeIndex);

            for (int i = 0; i < archetype.Chunks.Length; i++)
            {
                ref var chunk = ref archetype.Chunks.ElementAt(i);
            }
            
            // TODO-ECS: Left in here. You can continue
            // (need to get or add chunk from archetype)
            // Then you need to index the entity (entity in chunk or something)
            // then you can fnish

            throw new NotImplementedException();
        }

        private int GetOrCreateArchetypeIndex(NativeArray<ComponentType> types)
        {
            types.Sort();
            
            // TODO-ECS: This is a very naive implementation
            for (int i = 0; i < _archetypes.Length; i++)
            {
                ref var archetype = ref _archetypes.ElementAt(i);
                if (archetype.Components.ValueEquals(types))
                {
                    return i;
                }
            }

            // Create new archetype and return the index
            var idx = _archetypes.Length;
            _archetypes.Add(new Archetype { Components = types });
            return idx;
        }
        
        public Entity CreateEntity()
        {
            if (_freeIndices.TryDequeue(out var freeIdx))
            {
                return new Entity(freeIdx, _versionByIndex[freeIdx]);
            }

            var newIdx = ++_lastUsedIndex;
            _versionByIndex[newIdx] = 0;
            return new Entity(newIdx, 0);
        }

        public bool DestroyEntity(Entity entity)
        {
            if (!Exists(entity))
                return false;

            var idx = entity.Index;
            _freeIndices.Enqueue(idx);
            _versionByIndex[idx]++;
            return true;
        }

        public bool Exists(Entity entity)
        {
            var idx = entity.Index;
            return idx > 0 && idx <= _lastUsedIndex && entity.Version == _versionByIndex[idx];
        }
    }
}