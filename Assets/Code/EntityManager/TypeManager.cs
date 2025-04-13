using System;
using System.Collections.Generic;

namespace CodePractice
{
    public static class TypeManager
    {
        private static Dictionary<Type, int> _typeIndexByType;
        private static int _lastTypeIndex;

        public static void Initialize()
        {
            _typeIndexByType = new Dictionary<Type, int>();
            _lastTypeIndex = 0;
        }
        
        public static int GetTypeIndex<T>() where T : IComponent
        {
            // TODO: Maybe there's something better? Without resorting to underlying type?
            var type = typeof(T);
            
            if (_typeIndexByType.TryGetValue(type, out var idx))
                return idx;

            var newTypeIdx = ++_lastTypeIndex;
            _typeIndexByType.Add(type, newTypeIdx);
            return newTypeIdx;
        }
    }
}