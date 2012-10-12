using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = new HashTable<Test, string>();

            var Test1 = new Test(1);
            var Test2 = new Test(1);
            var Test3 = new Test(1);
            var Test4 = new Test(1);
            var Test5 = new Test(2);
            var Test6 = new Test(2);
            var Test7 = new Test(2);
            var Test8 = new Test(2);

            temp.Add(Test1, "a");
            temp.Add(Test2, "b");
            temp.Add(Test3, "c");
            temp.Add(Test4, "d");
            temp.Add(Test5, "a");
            temp.Add(Test6, "b");
            temp.Add(Test7, "c");
            temp.Add(Test8, "d");

            temp.PrintHashTable();

            Console.WriteLine("REMOVE TEST 1");
            Console.ReadLine();

            temp.Remove(Test1);

            temp.PrintHashTable();

            Console.WriteLine("REMOVE TEST 4");
            Console.ReadLine();

            temp.Remove(Test4);

            temp.PrintHashTable();

            Console.WriteLine("REMOVE ALL");
            Console.ReadLine();

            temp.Remove(Test2);
            temp.Remove(Test3);

            temp.PrintHashTable();
        }


        public class Test
        {
            public int x;

            public Test(int item)
            {
                x = item;
            }

            public override int GetHashCode()
            {
                return x;
            }

            public override string ToString()
            {
                return "X: " + x;
            }
        }
    }
}
