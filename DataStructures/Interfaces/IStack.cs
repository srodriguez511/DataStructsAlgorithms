using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stack
{
    interface IStack<T>
    {
        void Push(T item);
        T Pop();
        T Peek();
        bool Contains(T item);
        void Clear();
        int Count { get; }
    }
}
