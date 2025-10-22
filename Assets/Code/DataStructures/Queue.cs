using System;
using Unity.Mathematics;
using UnityEngine;

namespace CodePractice
{
    public class Queue<T>
    {
        public T[] Array;
        public int FrontIdx;
        public int Length;
        
        public int Capacity => Array.Length;

        // Queue: First in, first out
        // We need 2x pointers -> StartIdx, EndIdx
        // Circular array

        public Queue(int initialCapacity = 4)
        {
            if (initialCapacity < 0)
                throw new Exception("InitialCapacity can't be negative.");
            
            Array = new T[initialCapacity];
            FrontIdx = 0;
            Length = 0;
        }

        public void Enqueue(T item)
        {
            var capacity = Array.Length;
            var rear = (FrontIdx + Length) % capacity;
            
            Debug.Log($"Cap {capacity} len {Length}");
            if (capacity == Length)
            {
                Debug.Log("Enter resize");
                // TODO: Resize
                var newSize = math.ceilpow2(capacity + 1);
                var newArr = new T[newSize];
                
                // Only need the copy the area between front to rear
                // But there are 2 cases: Rear > Front (simple), Front > Rear (hard)

                if (rear >= FrontIdx)
                {
                    for (int i = FrontIdx; i <= rear; i++)
                    {
                        newArr[i] = Array[i];
                    }
                }
                else
                {
                    // TODO:
                }

                Array = newArr;
            }

            Array[rear] = item;
            Length++;
        }

        public T Dequeue()
        {
            if (Length == 0)
            {
                throw new Exception("Queue is empty.");
            }

            var lastIdx = Array.Length - 1;
            var item = Array[FrontIdx];
            var nextFrontIdx = FrontIdx == lastIdx ? 0 : FrontIdx + 1;
            FrontIdx = nextFrontIdx;

            return item;
        }

        public T Peek()
        {
            if (Length == 0)
            {
                throw new Exception("Queue is empty.");
            }

            return Array[FrontIdx];
        }
    }
}