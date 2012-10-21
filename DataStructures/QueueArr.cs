using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructures.Interfaces;

namespace DataStructures
{
    public class QueueArr<T> : IQueue<T>
    {
        /// <summary>
        /// Array of elements
        /// </summary>
        private T[] array;

        /// <summary>
        /// Current size of the array
        /// </summary>
        private const int ARRAY_SIZE = 4;

        /// <summary>
        /// Index of the first most item added
        /// </summary>
        private int indexFront;

        /// <summary>
        /// index of the last item added
        /// </summary>
        private int indexRear;

        public QueueArr()
        {
            Clear();
        }

        /// <summary>
        /// Number of actual elements in the queue
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Clear all the elements in the queue
        /// </summary>
        public void Clear()
        {
            array = new T[ARRAY_SIZE];
            indexFront = -1;
            indexRear = -1;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the first item in the list
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            //Empty 
            if (indexFront == -1)
            {
                throw new Exception("Empty queue");
            }

            var item = array[indexFront];
            array[indexFront] = default(T);

            if (indexFront == indexRear)
            {
                Clear();
            }
            else
            {
                IncrementIndex(ref indexFront);
            }

            return item;
        }

        /// <summary>
        /// Add an item to the queue (FIFO) order
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            //First item 
            if (indexFront == -1)
            {
                indexFront = 0;
                indexRear = 0;
                array[indexRear] = item;
            }
            else
            {
                IncrementIndex(ref indexRear);

                if (indexRear == indexFront)
                {
                    throw new Exception("Queue is full");
                }
                array[indexRear] = item;
            }
        }

        /// <summary>
        /// Looks at the front of the queue
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return array[indexFront];
        }
        
        /// <summary>
        /// Incremements the index safely within the bounds of the array size
        /// </summary>
        /// <param name="index"></param>
        private void IncrementIndex(ref int index)
        {
            //At the end
            if (index == (ARRAY_SIZE - 1))
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
}
