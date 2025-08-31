using System;
using System.Text;

namespace Code.Algorithms
{
    public static class MergeSort
    {
        public static string ArrToStr<T>(T[] arr)
        {
            return ArrToStr(arr, 0, arr.Length - 1);
        }

        public static string ArrToStr<T>(T[] arr, int startIdx, int endIdx)
        {
            var sb = new StringBuilder();

            for (int i = startIdx; i < endIdx + 1; i++)
            {
                sb.Append(arr[i]);
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            // Divide array into 2 each step
            MergeSort_Impl(array, 0, array.Length - 1);
        }

        // Notice: All the work is done in merge, this method just provides the recursion
        private static void MergeSort_Impl<T>(T[] array, int leftIdx, int rightIdx) where T : IComparable<T>
        {
            if (leftIdx >= rightIdx)
                return;

            var mid = (leftIdx + rightIdx) / 2;
            MergeSort_Impl(array, leftIdx, mid);
            MergeSort_Impl(array, mid + 1, rightIdx);
            Merge(array, leftIdx, mid, rightIdx);
        }

        private static void Merge<T>(T[] array, int leftIdx, int midIdx, int rightIdx) where T : IComparable<T>
        {
            // Debug.Log($"Merging {ArrToStr(array, leftIdx, midIdx)} and {ArrToStr(array, midIdx + 1, rightIdx)}");

            var lengthLeft = midIdx - leftIdx + 1;
            var lengthRight = rightIdx - midIdx;

            var tempLeft = new T[lengthLeft];
            var tempRight = new T[lengthRight];

            for (int i = 0; i < lengthLeft; i++)
            {
                tempLeft[i] = array[leftIdx + i];
            }

            for (int i = 0; i < lengthRight; i++)
            {
                tempRight[i] = array[midIdx + 1 + i];
            }

            var tempLeftIdx = 0;
            var tempRightIdx = 0;
            var mergeIdx = leftIdx;

            while (tempLeftIdx < lengthLeft && tempRightIdx < lengthRight)
            {
                // If Left < Right
                var left = tempLeft[tempLeftIdx];
                var right = tempRight[tempRightIdx];
                if (left.CompareTo(right) < 0)
                {
                    // Debug.Log($"{left} < {right}");
                    // Write left
                    array[mergeIdx++] = left;
                    tempLeftIdx++;
                }
                else
                {
                    // Debug.Log($"{left} >= {right}");
                    // Write right
                    array[mergeIdx++] = right;
                    tempRightIdx++;
                }
            }

            // Copy remaining elements (if ran out of comparisons to make -- one of the subarrays finished)
            while (tempLeftIdx < lengthLeft)
            {
                array[mergeIdx++] = tempLeft[tempLeftIdx++];
            }

            while (tempRightIdx < lengthRight)
            {
                array[mergeIdx++] = tempRight[tempRightIdx++];
            }

            // Debug.Log($"MergeResult {ArrToStr(array, leftIdx, mergeIdx - 1)}");
        }
    }
}