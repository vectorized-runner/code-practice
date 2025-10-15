using System;

namespace CodePractice
{
    public class StackNode<T>
    {
        public T Item;
        public StackNode<T> Next;
    }

    public class StackLinkedList<T>
    {
        public StackNode<T> Head;
        public int Length;

        public void Push(T item)
        {
            if (Head == null)
            {
                Head = new StackNode<T>
                {
                    Item = item
                };
            }
            else
            {
                var tmp = Head;
                while (tmp.Next != null)
                {
                    tmp = tmp.Next;
                }

                tmp.Next = new StackNode<T> { Item = item };
            }

            Length++;
        }

        public T Pop()
        {
            if (Length == 0)
            {
                throw new Exception("Stack length is zero.");
            }

            T res;

            if (Length == 1)
            {
                res = Head.Item;
                Head = null;
            }
            else
            {
                var tmp = Head;
                while (tmp.Next.Next != null)
                {
                    tmp = tmp.Next;
                }
            
                // At 1 Element before last
                res = tmp.Next.Item;
                tmp.Next = null;
            }

            Length--;
            return res;
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }
    }
}