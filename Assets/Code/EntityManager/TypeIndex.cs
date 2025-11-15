using System;

namespace CodePractice
{
    public struct TypeIndex : IEquatable<TypeIndex>
    {
        public int Value;

        public bool Equals(TypeIndex other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is TypeIndex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(TypeIndex left, TypeIndex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TypeIndex left, TypeIndex right)
        {
            return !left.Equals(right);
        }
    }
}