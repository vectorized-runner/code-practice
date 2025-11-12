using System;
using System.Collections.Generic;

namespace CodePractice
{
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root;

        public int Depth;

        public bool IsFull()
        {
            throw new NotImplementedException();
        }

        public int Height()
        {
            throw new NotImplementedException();
        }

        public int GetMax()
        {
            
        }

        public int Sum()
        {
            
        }

        public int CountLeaves()
        {
            
        }

        public List<T> GetElements()
        {
            throw new NotImplementedException();
        }

        public List<T> PreOrder()
        {
            
        }

        public List<T> InOrder()
        {
            
        }

        public List<T> PostOrder()
        {
            
        }
    }

    public class BinaryTreeNode<T>
    {
        public T Item;
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;
    }
}