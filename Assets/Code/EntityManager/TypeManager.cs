using System;
using System.Collections.Generic;

namespace CodePractice
{
    public static unsafe class TypeManager
    {
        private static Dictionary<Type, TypeInfo> _typeInfoByType;
        private static int _lastTypeIndex;

        public static void Initialize()
        {
            _typeInfoByType = new Dictionary<Type, TypeInfo>();
            _lastTypeIndex = 0;
        }
        
        public static int GetTypeIndex<T>() where T : unmanaged, IComponent
        {
            // TODO: Maybe there's something better? Without resorting to underlying type?
            var type = typeof(T);
            
            if (_typeInfoByType.TryGetValue(type, out var idx))
                return idx.TypeIndex;

            var newTypeIdx = ++_lastTypeIndex;
            var typeInfo = new TypeInfo
            {
                Size = sizeof(T),
                TypeIndex = newTypeIdx
            };
            
            _typeInfoByType.Add(type, typeInfo);
            return newTypeIdx;
        }
    }
}