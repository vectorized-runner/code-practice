using System;
using System.Collections.Generic;

namespace CodePractice
{
    public struct MyInt : IValue
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
    }
    
    public interface IValue
    {
        public int GetValue();
    }

    public class BinaryTree<T> where T : IValue
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
            throw new NotImplementedException();
        }

        public int Sum()
        {
            throw new NotImplementedException();
        }

        public int CountLeaves()
        {
            throw new NotImplementedException();
        }

        public List<T> GetElements()
        {
            throw new NotImplementedException();
        }

        public List<T> PreOrder()
        {
            var result = new List<T>();
            Root.PreOrder(result);
            return result;
        }

        public List<T> InOrder()
        {
            var result = new List<T>();
            Root.InOrder(result);
            return result;
        }

        public List<T> PostOrder()
        {
            var result = new List<T>();
            Root.PostOrder(result);
            return result;
        }
    }

    public class BinaryTreeNode<T> where T : IValue
    {
        public T Item;
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;

        public void PreOrder(List<T> result)
        {
            result.Add(Item);

            if (Left != null)
            {
                Left.PreOrder(result);
            }

            if (Right != null)
            {
                Right.PreOrder(result);
            }
        }

        public void InOrder(List<T> result)
        {
            if (Left != null)
            {
                Left.InOrder(result);
            }
            
            result.Add(Item);

            if (Right != null)
            {
                Right.InOrder(result);
            }
        }

        public void PostOrder(List<T> result)
        {
            if (Left != null)
            {
                Left.PostOrder(result);
            }
            
            if (Right != null)
            {
                Right.PostOrder(result);
            }
            
            result.Add(Item);
        }
    }
}