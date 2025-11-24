using System;
using System.Collections.Generic;
using Unity.Mathematics;

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

        public int GetHeight()
        {
            return Root.GetHeight();
        }

        public int GetMax()
        {
            return Root.GetMax();
        }

        public int Sum()
        {
            return Root.Sum();
        }

        public int CountLeaves()
        {
            return Root.CountLeaves();
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

        public int CountLeaves()
        {
            if (Left == null && Right == null)
            {
                return 1;
            }

            if (Left == null)
            {
                return Right.CountLeaves();
            }

            if (Right == null)
            {
                return Left.CountLeaves();
            }

            return Left.CountLeaves() + Right.CountLeaves();
        }

        public int GetHeight()
        {
            if (Left == null && Right == null)
            {
                return 0;
            }
            if (Left == null)
            {
                return 1 + Right.GetHeight();
            }
            if (Right == null)
            {
                return 1 + Left.GetHeight();
            }

            return 1 + math.max(Left.GetHeight(), Right.GetHeight());
        }

        public int GetMax()
        {
            var max = Item.GetValue();

            if (Left != null)
            {
                max = math.max(Left.GetMax(), max);
            }

            if (Right != null)
            {
                max = math.max(Right.GetMax(), max);
            }

            return max;
        }

        public int Sum()
        {
            var sum = Item.GetValue();

            if (Left != null)
            {
                sum += Left.Sum();
            }

            if (Right != null)
            {
                sum += Right.Sum();
            }

            return sum;
        }

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