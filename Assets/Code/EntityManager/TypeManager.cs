using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CodePractice
{
    public struct TypeIndex<T> where T: IComponent
    {
        // ReSharper disable once StaticMemberInGenericType
        public static readonly int Value = GlobalTypeIndex.GetUniqueValue();
    }

    public static class GlobalTypeIndex
    {
        private static int _value;

        public static int GetUniqueValue()
        {
            return Interlocked.Increment(ref _value);
        }
    }
    
    // TODO-ECS: How does Unity make this efficient (with flags and shit)?
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
            return TypeIndex<T>.Value;
        }
    }
}