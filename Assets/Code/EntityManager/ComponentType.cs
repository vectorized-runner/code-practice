using System;

namespace CodePractice
{
    public struct ComponentType : IEquatable<ComponentType>, IComparable<ComponentType>
    {
        public int TypeIndex;

        public static ComponentType Get<T>() where T : unmanaged, IComponent
        {
            return new ComponentType
            {
                TypeIndex = TypeManager.GetTypeIndex<T>()
            };
        }

        public static bool operator ==(ComponentType left, ComponentType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ComponentType left, ComponentType right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return obj is ComponentType other && Equals(other);
        }


        public bool Equals(ComponentType other)
        {
            return TypeIndex == other.TypeIndex;
        }

        public int CompareTo(ComponentType other)
        {
            return TypeIndex.CompareTo(other.TypeIndex);
        }

        public override int GetHashCode()
        {
            return TypeIndex;
        }
    }
}