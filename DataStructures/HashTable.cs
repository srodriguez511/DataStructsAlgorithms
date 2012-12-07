using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTable
{
    public sealed class HashTable<K, V>
    {
        /// <summary>
        /// Initial hash table size
        /// </summary>
        private const int INITIALSIZE = 10;

        /// <summary>
        /// The current size of the buckets
        /// </summary>
        private int currentBucketSize = INITIALSIZE;

        /// <summary>
        /// Array of hash table items
        /// </summary>
        private HashTableEntry[] bucketsArray = new HashTableEntry[INITIALSIZE];

        /// <summary>
        /// Add an item into the hash table
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(K key, V value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Null key");
            }

            //Next pointer is null by default. No need to set
            var newEntry = new HashTableEntry(key, value);
            var hash = key.GetHashCode();

            //Resize making the max this current hash
            if (hash >= currentBucketSize)
            {
                currentBucketSize = hash + 1;
                Array.Resize<HashTableEntry>(ref bucketsArray, currentBucketSize);
            }


            var firstItem = bucketsArray[hash];

            //First check if this bucket is empty
            if (firstItem == null)
            {
                //Make it the first item in the chain
                bucketsArray[hash] = newEntry;
            }
            //There are other items in the chain. Add it to the end
            else
            {
                var curr = firstItem;

                //Keep going until we hit the end of the list
                while (curr.nextEntry != null)
                {
                    curr = curr.nextEntry;
                }

                //Make the current guy point to the new entry we are adding
                curr.nextEntry = newEntry;
            }

        }

        /// <summary>
        /// Finds an item in the hash table. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>V if found or default(V) if not found</returns>
        public V Find(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Null key");
            }

            var hash = key.GetHashCode();
            var firstItem = bucketsArray[hash];

            if (firstItem == null)
            {
                return default(V);
            }

            var curr = firstItem;
            while (curr != null)
            {
                if (curr.Key.Equals(key))
                {
                    return curr.Value;
                }
                curr = curr.nextEntry;
            }
            return default(V);
        }

        /// <summary>
        /// Remove an item from the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Null key");
            }

            var hash = key.GetHashCode();
            var firstItem = bucketsArray[hash];

            if (firstItem == null)
            {
                return false;
            }

            if (firstItem.Key.Equals(key))
            {
                bucketsArray[hash] = firstItem.nextEntry;
                //firstItem = null ... not needed? no more references will be gced...
                return true;
            }

            var prev = firstItem;
            var curr = firstItem.nextEntry;

            while (curr != null)
            {
                if (curr.Key.Equals(key))
                {
                    prev.nextEntry = curr.nextEntry;
                    return true;
                }

                prev = curr;
                curr = curr.nextEntry;
            }

            return false;
        }

        /// <summary>
        /// Print the hashtable to the console
        /// </summary>
        public void PrintHashTable()
        {
            for (int i = 0; i < currentBucketSize; i++)
            {
                Console.Write("Bucket[{0}]: ", i);

                var item = bucketsArray[i];
                if (item == null)
                {
                    Console.Write("NULL");
                }
                else
                {
                    while (item != null)
                    {
                        Console.Write("KEY {0} Value {1} ", item.Key, item.Value);
                        item = item.nextEntry;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Represents an entry in the hash table of Key K and Value V
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        private class HashTableEntry
        {
            /// <summary>
            /// Reference to the next entry in the chain
            /// </summary>
            internal HashTableEntry nextEntry { get; set; }

            /// <summary>
            /// The Key
            /// </summary>
            internal K Key { get; set; }

            /// <summary>
            /// The value
            /// </summary>
            internal V Value { get; set; }

            internal HashTableEntry(K key, V value)
            {
                Key = key;
                Value = value;
                nextEntry = null;
            }
        }

    }
}
