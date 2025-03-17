using System;

namespace CodePractice
{
    public struct Entity : IEquatable<Entity>
    {
        public int Index;
        public int Version;

        public static readonly Entity Null = new Entity();

        public Entity(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public static bool operator ==(Entity lhs, Entity rhs)
        {
            return lhs.Index == rhs.Index && lhs.Version == rhs.Version;
        }

        public static bool operator !=(Entity lhs, Entity rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Entity other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity other && Equals(other);
        }

        public override int GetHashCode()
        {
            // Smart implementation: Two entities with the same index is only possible if one is destroyed (rare)
            return Index;
        }

        public override string ToString()
        {
            return Equals(Null) ? "Entity.Null" : $"Entity({Index},{Version})";
        }
    }
}