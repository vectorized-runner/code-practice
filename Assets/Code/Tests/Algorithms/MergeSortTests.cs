using NUnit.Framework;
using UnityEngine;

namespace Code.Algorithms.Tests
{
    public static class MergeSortTests
    {
        [Test]
        public static void RandomSortTest()
        {
            var arrayLength = 10_000;
            var seed = 43589354u;
            var random = new Unity.Mathematics.Random(seed);
            var arr = new int[arrayLength];

            for (int i = 0; i < arrayLength; i++)
            {
                arr[i] = random.NextInt();
            }
            
            MergeSort.Sort(arr);
            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Assert.IsTrue(arr[i] <= arr[i + 1]);
            }
            
            Debug.Log(MergeSort.ArrToStr(arr));
        }
        
        [Test]
        public static void MergeSort_1()
        {
            var arr = new int[] { -1000, 5000, 1, 435899, -23423489, 0, 4853475, 123424578, 0, -349284, 13, 5, 45264, 8123 }; 
            MergeSort.Sort(arr);
            
            Debug.Log(MergeSort.ArrToStr(arr));
            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Assert.IsTrue(arr[i] <= arr[i + 1]);
            }
        }
    }
}