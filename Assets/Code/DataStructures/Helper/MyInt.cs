using System;

namespace CodePractice
{
    public struct MyInt : IValue, IEquatable<MyInt>
    {
        public int Value;
        
        public static implicit operator MyInt(int i)
        {
            return new MyInt
            {
                Value = i
            };
        }

        public int GetValue()
        {
            return Value;
        }

        public bool Equals(MyInt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is MyInt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(MyInt left, MyInt right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MyInt left, MyInt right)
        {
            return !left.Equals(right);
        }
    }
}