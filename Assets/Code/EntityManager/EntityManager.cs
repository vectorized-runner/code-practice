using Unity.Collections;

namespace CodePractice
{
    public struct EntityManager
    {
        private int _lastUsedIndex;
        private NativeQueue<int> _freeIndices;
        private NativeHashMap<int, int> _versionByIndex;

        public static EntityManager Create()
        {
            return new EntityManager
            {
                _lastUsedIndex = 0,
                _freeIndices = new NativeQueue<int>(Allocator.Persistent),
                _versionByIndex = new NativeHashMap<int, int>(32, Allocator.Persistent)
            };
        }

        public void Dispose()
        {
            _freeIndices.Dispose();
            _versionByIndex.Dispose();
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