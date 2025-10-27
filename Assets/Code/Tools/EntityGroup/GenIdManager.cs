using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Code
{
    public struct GenIdManager : IDisposable
    {
        private UnsafeHashMap<int, int> _versionByIndex;
        private UnsafeQueue<int> _freeIndices;
        private int _lastUsedVersion;
        
        public static GenIdManager Create()
        {
            return new GenIdManager()
            {
                _versionByIndex = new UnsafeHashMap<int, int>(64, Allocator.Persistent),
                _freeIndices = new UnsafeQueue<int>(Allocator.Persistent),
                _lastUsedVersion = 0,
            };
        }

        public GenId CreateId()
        {
            if (_freeIndices.TryDequeue(out var idx))
            {
                var version = _versionByIndex[idx];
                return new GenId(idx, version);
            }

            idx = ++_lastUsedVersion;
            return new GenId(idx, 0);
        }

        public bool DestroyId(GenId id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(GenId id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _versionByIndex.Dispose();
            _freeIndices.Dispose();
        }
    }
}