using System;
using System.Collections.Generic;

namespace CodePractice
{
    // TODO: How does Unity make this efficient (with flags and shit)?
    public static unsafe class TypeManager
    {
        private static Dictionary<Type, int> _typeIndexByType;
        private static List<TypeInfo> _typeInfos;

        public static void Initialize()
        {
            _typeIndexByType = new Dictionary<Type, int>();
            _typeInfos = new List<TypeInfo>();
        }

        public static TypeInfo GetTypeInfo(int typeIndex)
        {
            return _typeInfos[typeIndex];
        }
        
        public static int GetTypeIndex<T>() where T : unmanaged, IComponent
        {
            // TODO: Maybe there's something better? Without resorting to underlying type?
            var type = typeof(T);
            
            if (_typeIndexByType.TryGetValue(type, out var idx))
                return idx;

            var newTypeIdx = _typeInfos.Count;
            var typeInfo = new TypeInfo
            {
                Size = sizeof(T),
                TypeIndex = newTypeIdx
            };
            _typeInfos.Add(typeInfo);
            _typeIndexByType.Add(type, newTypeIdx);
            return newTypeIdx;
        }
    }
}