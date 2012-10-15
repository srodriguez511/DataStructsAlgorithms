using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkList
{
    public class LinkedList<T> where T : IComparable
    {
        /// <summary>
        /// Head reference, start of the list
        /// </summary>
        private Node head;

        /// <summary>
        /// Number of elements in the list
        /// </summary>
        public int Count { get; private set; }

        public LinkedList()
        {
            head = null;
        }

        /// <summary>
        /// Sort the linked list in increasing order
        /// </summary>
        public void Sort()
        {
            head = MergeSort(head);
        }

        private Node MergeSort(Node localHead)
        {
            if (localHead == null || localHead.Next == null)
            {
                return localHead;
            }

            var middle = FindMiddleNode(localHead);
            var right = middle.Next;
            middle.Next = null;
            return Merge(MergeSort(localHead), MergeSort(right));
        }

        /// <summary>
        /// merge sort routine
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Node Merge(Node left, Node right)
        {
            Node newHead = new Node(default(T), null); //Dummy head 
            Node curr = newHead;

            while (left != null && right != null)
            {
                //Left is smaller
                if (((IComparable)left.Data).CompareTo((IComparable)right.Data) <= 0)
                {
                    curr.Next = left;
                    left = left.Next;
                }
                //Right is smaller
                else
                {
                    curr.Next = right;
                    right = right.Next;
                }
                curr = curr.Next;
            }

            curr.Next = (left == null) ? right : left;
            return newHead.Next;
        }

        /// <summary>
        /// Find the middle node from the starting node
        /// </summary>
        /// <returns></returns>
        private Node FindMiddleNode(Node start)
        {
            if (start == null)
            {
                return null;
            }

            if (start.Next == null)
            {
                return start;
            }

            var delayed = start; //trailing ptr
            var curr = start.Next;

            while (curr != null && curr.Next != null)
            {
                delayed = delayed.Next;
                curr = curr.Next.Next;
            }

            return delayed;
        }

        /// <summary>
        /// Returns the middle element value
        /// </summary>
        /// <returns></returns>
        public T FindMiddle()
        {
            return FindMiddleNode(head).Data;
        }

        /// <summary>
        /// Finds the element N positions from the last
        /// </summary>
        /// <param name="N"></param>
        public T FindNthFromLast(int N)
        {
            if (head == null)
            {
                return default(T);
            }

            //Assume we don't know the size of the list
            var curr = head;
            Node trailingPtr = null;
            int count = 0;

            while (curr != null)
            {
                if (count == N)
                {
                    trailingPtr = head;
                }
                else if (count > N)
                {
                    trailingPtr = trailingPtr.Next;
                }

                curr = curr.Next;
                count++;
            }

            if (trailingPtr != null)
            {
                return trailingPtr.Data;
            }
            return default(T);
        }

        /// <summary>
        /// Reverse a linked list
        /// </summary>
        public void ReverseList()
        {
            var curr = head;
            Node prev = null;
            Node next = null;

            while (curr != null)
            {
                next = curr.Next;
                curr.Next = prev;
                prev = curr;
                curr = next;
            }
            head = prev;
        }

        /// <summary>
        /// Reverse linked list using recursion
        /// </summary>
        public void ReverseListRecursion()
        {
            head = ReverseListRecurse(head, null);
        }

        /// <summary>
        /// Use recursion to reverse the list
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="prev"></param>
        private Node ReverseListRecurse(Node headRef, Node prevRef)
        {
            if (headRef == null)
            {
                return null;
            }

            if (headRef.Next == null)
            {
                headRef.Next = prevRef;
                return headRef;
            }

            var reverse = ReverseListRecurse(headRef.Next, headRef);
            headRef.Next = prevRef;
            return reverse;
        }

        /// <summary>
        /// Add item to the end of the list
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            var newNode = new Node(data, null);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                var last = Traverse(null);
                last.Next = newNode;
            }
            Count++;
        }

        /// <summary>
        /// Remove the first item in the list with the data 
        /// </summary>
        /// <param name="data"></param>
        public void RemoveFirst(T data)
        {
            Traverse((prev, curr) =>
            {
                return Remove(prev, curr, data, true);
            });
        }

        /// <summary>
        /// Removes all the items from the list
        /// </summary>
        /// <param name="data"></param>
        public void RemoveAll(T data)
        {
            Traverse((prev, curr) =>
            {
                return Remove(prev, curr, data, false);
            });
        }

        /// <summary>
        /// Remove the last item in the list containing the data
        /// </summary>
        /// <param name="data"></param>
        public void RemoveLast(T data)
        {
            Node nodeBeforeNodeToRemove = null;
            Node nodeToRemove = null;

            Traverse((prev, curr) =>
            {
                if (curr.Data.Equals(data))
                {
                    nodeBeforeNodeToRemove = prev;
                    nodeToRemove = curr;
                }
                return false;
            });

            DoRemoval(nodeBeforeNodeToRemove, nodeToRemove);
        }

        /// <summary>
        /// Helper to perform the reference re-ordering for removal
        /// </summary>
        private void DoRemoval(Node prev, Node curr)
        {
            if (curr.Equals(head))
            {
                head = curr.Next;
            }
            else
            {
                prev.Next = curr.Next;
            }
            Count--;
        }

        /// <summary>
        /// Removes an item from the list. Stop or continue deleting.
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="curr"></param>
        /// <param name="data"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        private bool Remove(Node prev, Node curr, T data, bool stop)
        {
            if (curr.Data.Equals(data))
            {
                DoRemoval(prev, curr);
                return stop;
            }
            return false;
        }

        /// <summary>
        /// Prints the contents of the list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Traverse((prev, curr) =>
            {
                sb.Append(curr.Data);
                sb.Append("\n");
                return false;
            });
            return sb.ToString();
        }

        /// <summary>
        /// Common function to traverse the list start to head
        /// Return true from process to end traversal
        /// </summary>
        /// <param name="Process"></param>
        /// <returns>Last item on the list</returns>
        private Node Traverse(Func<Node, Node, bool> Process)
        {
            var curr = head;
            Node prev = null; //keep track so we can return the last one at the end

            while (curr != null)
            {
                if (Process != null && Process(prev, curr) == true)
                {
                    break;
                }

                prev = curr;
                curr = curr.Next;
            }

            return prev;
        }

        /// <summary>
        /// Node entry
        /// </summary>
        internal class Node
        {
            internal Node Next { get; set; }
            internal T Data { get; set; }

            internal Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }
    }


}
