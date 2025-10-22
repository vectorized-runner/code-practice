using System;

namespace CodePractice
{
    public class CircularQueue<T>
    {
        public T[] Array;
        public int FrontIdx;
        public int Length;

        public int Capacity => Array.Length;

        public CircularQueue(int initialCapacity = 4)
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

            if (capacity == Length)
            {
                throw new Exception("Circular Queue doesn't support resizing");
            }

            var rear = (FrontIdx + Length) % capacity;
            Array[rear] = item;
            Length++;
        }

        public T Dequeue()
        {
            if (Length == 0)
            {
                throw new Exception("Queue is empty.");
            }

            var item = Array[FrontIdx];
            FrontIdx = (FrontIdx + 1) % Capacity;
            Length--;

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