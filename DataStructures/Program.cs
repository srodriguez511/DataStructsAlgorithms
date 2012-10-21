using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HashTable;
using Stack;
using LinkList;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            var ht = new HashTable<int, int>();
            var ll = new MyLinkedList<int>();
            var stackLL = new StackLL<int>();
            var stackArr = new StackArray<int>();
            var queueArr = new QueueArr<int>();

            queueArr.Enqueue(1);
            queueArr.Enqueue(2);
            queueArr.Enqueue(3);
            queueArr.Enqueue(4);

            var result = queueArr.Dequeue();

            queueArr.Enqueue(5);
            result = queueArr.Dequeue();
            result = queueArr.Dequeue();
            result = queueArr.Dequeue();
            queueArr.Enqueue(1);
        }
    }
}
