using System;

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
        public bool IsEmpty => Count != 0;
        
        public MinHeap()
        {
        }

        public MinHeap(T[] items)
        {
        }

        public void Clear()
        {
            
        }
        
        public void Add(T newElement)
        {
            _array[0] = newElement; // Init sentinel

            // Check require resize
            // TODO: Use a better logic here, why x * 2 + 1
            if (_array.Length == _count + 1)
            {
                var currentSize = _array.Length;
                var newSize = currentSize * 2 + 1;
                var tmp = new T[newSize];

                for (int i = 0; i < currentSize; i++)
                {
                    tmp[i] = _array[i];
                }

                _array = tmp;
            }
            
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

        public void BuildHeap()
        {
            
        }


    }
}