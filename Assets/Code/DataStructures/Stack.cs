using System;
using Unity.Mathematics;
using UnityEngine;

namespace CodePractice
{
    public class Stack<T>
    {
        public T[] Array;
        public int Length;
        public int Capacity => Array.Length;

        public Stack(int initialCapacity = 0)
        {
            if (initialCapacity < 0)
                throw new Exception("Capacity must be greater than zero");
            
            Array = new T[initialCapacity];
            Length = 0;
        }
        
        public void Push(T item)
        {
            if (Length == Capacity)
            {
                // Resize
                var newCapacity = math.ceilpow2(Length + 1);
                var newArray = new T[newCapacity];
                for (int i = 0; i < Length; i++)
                {
                    newArray[i] = Array[i];
                }
                Array = newArray;
            }

            Array[Length++] = item;
        }

        public T Pop()
        {
            if (Length == 0)
            {
                throw new Exception("Stack length is zero.");
            }

            return Array[--Length];
        }

        public T Peek()
        {
            if (Length == 0)
            {
                throw new Exception("Stack length is zero.");
            }

            return Array[Length - 1];
        }
    }
}