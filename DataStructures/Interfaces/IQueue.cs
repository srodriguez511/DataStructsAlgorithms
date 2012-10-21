using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Interfaces
{
    interface IQueue<T>
    {
        int Count { get; }
        void Clear();
        bool Contains(T item);
        T Dequeue();
        void Enqueue(T item);
        T Peek();
    }
}
