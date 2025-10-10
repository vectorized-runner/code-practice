using System;
using Unity.Mathematics;
using UnityEngine;

namespace CodePractice
{
    // Why is sentinel used in the implementation
    // 1- Clean Math: Parent -> 1/2, LeftChild -> i*2, RightChild -> i*2 + 1
    // 2- We need to Terminate at root, but we don't want to check holeIdx > 1 each iteration (extra branch every iteration)
    public class MinHeap<T> where T : IComparable<T>
    {
        private T[] _array;
        private int _count;

        public int Count => _count;
        // Notice index 0 is reserved for sentinel, capacity=16 can hold 15 items.
        public int Capacity => _array.Length;
        public bool IsEmpty => Count != 0;

        public MinHeap(int initialCapacity = 16)
        {
            const int minCapacity = 4;
            _array = new T[math.max(minCapacity, math.ceilpow2(initialCapacity))];
        }

        public MinHeap(T[] items)
        {
            _array = items;
            _count = items.Length;
            BuildHeap();
        }

        public T[] GetInternalArray()
        {
            return _array;
        }

        public void Clear()
        {
            _count = 0;
        }

        public void BuildHeap()
        {
        }

        public void Add(T newElement)
        {
            // Check require resize
            var capacity = _array.Length;
            var requiredCapacity = _count + 1;
            if (capacity == requiredCapacity)
            {
                var newCapacity = math.ceilpow2(capacity * 2);
                var tmp = new T[newCapacity];

                for (int i = 0; i < capacity; i++)
                {
                    tmp[i] = _array[i];
                }

                _array = tmp;
            }
            
            _array[0] = newElement; // Init sentinel

            // Example: Count=0, HoleIdx=1, makes sense since we leave sentinel at index 0
            var holeIdx = ++_count;

            // Bubble up
            // While (Parent > NewElement), shift parent down
            while (_array[holeIdx / 2].CompareTo(newElement) > 0)
            {
                _array[holeIdx] = _array[holeIdx / 2];
                holeIdx /= 2;
            }

            _array[holeIdx] = newElement;
        }

        public T Peek()
        {
            return _array[1];
        }

        public T Remove()
        {
            if (_count == 0)
                throw new Exception("Heap is empty.");

            var result = _array[1];
            // Notice post-decrement since we start from index 1
            _array[1] = _array[_count--];
            BubbleDown(1);
            return result;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count + 1; i++)
            {
                if (_array[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        private void BubbleDown(int holeIdx)
        {
            var temp = _array[holeIdx];

            while (holeIdx * 2 <= _count) // While at least one child
            {
                // Assume left child
                var smallerChildIdx = holeIdx * 2;

                // Choose smaller child, compare left with right
                if (smallerChildIdx <= _count && _array[smallerChildIdx].CompareTo(_array[smallerChildIdx + 1]) < 0)
                {
                    // Use right child instead
                    smallerChildIdx++;
                }

                // If Parent > Smaller Child, move up
                if (_array[holeIdx].CompareTo(_array[smallerChildIdx]) > 0)
                {
                    _array[holeIdx] = _array[smallerChildIdx];
                    holeIdx = smallerChildIdx;
                }
                else
                {
                    break;
                }
            }

            _array[holeIdx] = temp;
        }
    }
}