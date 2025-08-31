using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace Code.Algorithms.Tests
{
    public class MergeSortTests
    {
        [Test]
        public static void Sort_Empty()
        {
        }

        [Test]
        public static void Sort_1()
        {
            var arr = new int[] { -1000, 5000, 1, 435899, -23423489, 0, 4853475, 123424578, 0, -349284, 13, 5, 45264, 8123 }; 
            MergeSort.Sort(arr);
            
            Debug.Log(MergeSort.ArrToStr(arr));
            
            for (int i = 0; i < arr.Length; i++)
            {
                Assert.IsTrue(arr[i] <= arr[i + 1]);
            }
        }


    }
}