using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stack
{
    /// <summary>
    /// Stack implemented as linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StackLL<T> : IStack<T>
    {
        /// <summary>
        /// Head == top == first item in the stack
        /// </summary>
        private Node Top = null;

        public StackLL()
        {
        }

        /// <summary>
        /// Number of elements
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// if the head is null than we have no items
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            return Top == null;
        }

        /// <summary>
        /// Add an item to the stack, which adds to the beginning of the linked list. new head
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            var newNode = new Node(item, null);

            //Empty list just make the head
            if (IsEmpty())
            {
                Top = newNode;
            }
            //Make this the new head and realign link
            else
            {
                newNode.Next = Top;
                Top = newNode;
            }
            Count++;
        }

        /// <summary>
        /// Removes the first element (head)
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            var nodeToReturn = Top;
            Top = Top.Next;
            Count--;
            return nodeToReturn.Data;
        }

        /// <summary>
        /// Show the next item without physically removing it from the list
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            return Top.Data;
        }

        /// <summary>
        /// Is this item in the list O(N).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var curr = Top;
            while (curr != null)
            {
                if (curr.Data.Equals(item))
                {
                    return true;
                }
                curr = curr.Next;
            }
            return false;
        }

        /// <summary>
        /// Clear all the items in the stack
        /// </summary>
        public void Clear()
        {
            Top = default(Node);
            Count = 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var curr = Top;

            while (curr != null)
            {
                sb.Append(curr.Data);
                sb.Append("\n");
                curr = curr.Next;
            }

            return sb.ToString();
        }

        private class Node
        {
            internal T Data;
            internal Node Next;

            internal Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

    }

}
