using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stack
{
    public class StackArray<T> : IStack<T>
    {
        /// <summary>
        /// Maintains the current size of the array
        /// </summary>
        private int ARRAY_SIZE = 50;

        /// <summary>
        /// The last item on the list and first item to come off (array index)
        /// </summary>
        private int currentTopIndex = -1;

        /// <summary>
        /// The stack array of items T
        /// </summary>
        private T[] stackArray;

        public StackArray()
        {
            stackArray = new T[ARRAY_SIZE];
        }

        /// <summary>
        /// Add an item to the stack at the end of the array
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            //If currently the last item is at the max size of the array we need to resize the array
            if (currentTopIndex == ARRAY_SIZE)
            {
                ARRAY_SIZE += 50;
                Array.Resize(ref stackArray, ARRAY_SIZE);
            }

            currentTopIndex++;
            stackArray[currentTopIndex] = item;
        }

        /// <summary>
        /// Takes the topmost and last added item off the list
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (currentTopIndex == -1)
            {
                return default(T);
            }

            var item = stackArray[currentTopIndex];
            stackArray[currentTopIndex] = default(T);
            currentTopIndex--;
            return item;
        }

        /// <summary>
        /// Shows the top most item without removing it from the list
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (currentTopIndex == -1)
            {
                return default(T);
            }

            return stackArray[currentTopIndex];
        }

        /// <summary>
        /// Does the item exist
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if (currentTopIndex == -1)
            {
                return false;
            }

            for(int i = 0; i <= currentTopIndex; i++)
            {
                if (stackArray[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Reset the array
        /// </summary>
        public void Clear()
        {
            currentTopIndex = -1;
            stackArray = new T[ARRAY_SIZE];
        }

        /// <summary>
        /// Number of items currently in the array
        /// </summary>
        public int Count
        {
            get { return currentTopIndex + 1; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int i = currentTopIndex; i >= 0; i--)
            {
                sb.Append(stackArray[i] + " ");
            }
            return sb.ToString();
        }
    }
}
