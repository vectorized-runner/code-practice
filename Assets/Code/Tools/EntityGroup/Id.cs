using System;

namespace Code
{
    public struct Id : IEquatable<Id>
    {
        public int Index;
        public int Version;
        
        public Id(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public static bool operator ==(Id left, Id right)
        {
            return left.Index == right.Index && left.Version == right.Version;
        }

        public static bool operator !=(Id left, Id right)
        {
            return !(left == right);
        }

        public bool Equals(Id other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is Id other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public override string ToString()
        {
            return $"({Index},{Version})";
        }
    }
}