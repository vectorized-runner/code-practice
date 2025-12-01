using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Code
{
    public struct IdManager : IDisposable
    {
        private UnsafeHashMap<int, int> _versionByIndex;
        private UnsafeQueue<int> _freeIndices;
        private int _lastUsedIndex;

        public static IdManager Create()
        {
            return new IdManager
            {
                _versionByIndex = new UnsafeHashMap<int, int>(64, Allocator.Persistent),
                _freeIndices = new UnsafeQueue<int>(Allocator.Persistent),
                _lastUsedIndex = 0,
            };
        }

        public Id CreateId()
        {
            if (_freeIndices.TryDequeue(out var idx))
            {
                var version = _versionByIndex[idx];
                return new Id(idx, version);
            }

            idx = ++_lastUsedIndex;
            _versionByIndex.Add(idx, 0);
            return new Id(idx, 0);
        }

        public bool DestroyId(Id id)
        {
            if (!Exists(id))
            {
                return false;
            }

            var idx = id.Index;
            _freeIndices.Enqueue(idx);
            _versionByIndex[idx]++;
            return true;
        }

        public bool Exists(Id id)
        {
            var idx = id.Index;
            if (idx > _lastUsedIndex || idx <= 0)
            {
                return false;
            }

            return _versionByIndex[idx] == id.Version;
        }

        public void Dispose()
        {
            _versionByIndex.Dispose();
            _freeIndices.Dispose();
        }
    }
}