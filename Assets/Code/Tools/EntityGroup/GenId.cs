using System;

namespace Code
{
    public struct GenId : IEquatable<GenId>
    {
        public int Index;
        public int Version;
        
        public GenId(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public static bool operator ==(GenId left, GenId right)
        {
            return left.Index == right.Index && left.Version == right.Version;
        }

        public static bool operator !=(GenId left, GenId right)
        {
            return !(left == right);
        }

        public bool Equals(GenId other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is GenId other && Equals(other);
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