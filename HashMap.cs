using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    /// <summary>
    /// The HashMap class.
    /// </summary>
    /// <typeparam name="K">A generic value to be stored.</typeparam>
    /// <typeparam name="V">A generic value to be stored.</typeparam>
    public class HashMap<K, V> : IMap<K, V>
    {
        public const int CAPACITY = 11;
        public const double LOAD_FACTOR = .75;
        private double LoadFactor;
        private int InitialCapacity;
        private int size;
        public Entry<K, V>[] Table { get; set; }
        public int Placeholder { get; set; }

        /// <summary>
        /// Initializes an instance of the hashmap class with an initial capacity and a load factor.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity of the hashmap.</param>
        /// <param name="loadFactor">The load factor of the hashmap.</param>
        /// <exception cref="ArgumentException">If the capacity is less than or equal to 0 or if the load factor is greater than 1, throw an error.</exception>
        public HashMap(int initialCapacity, double loadFactor)
        {
            if (loadFactor > 1 || loadFactor < 0)
            {
                throw new ArgumentException("Load Factor cannot be 1 or greater.");
            }

            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Capacity cannot be less than or equal to 0.");
            }
            this.Table = new Entry<K, V>[initialCapacity];
            this.InitialCapacity = initialCapacity;
            this.LoadFactor = loadFactor;
        }

        /// <summary>
        /// Initializes an instance of the hashmap with an initial capacity and the default load factor.
        /// </summary>
        /// <param name="initialCapacity"></param>
        public HashMap(int initialCapacity) : this(initialCapacity, LOAD_FACTOR)
        {
        }

        /// <summary>
        /// Initializes an instance of the hashmap with the default capcity and load factor.
        /// </summary>
        public HashMap() : this(CAPACITY)
        {
        }
        
        /// <summary>
        /// Returns the size of the hashmap
        /// </summary>
        /// <returns>The size of the hashmap.</returns>
        public int Size()
        {
            return size;
        }

        /// <summary>
        /// Returns true if the size is empty.
        /// </summary>
        /// <returns>The boolean value of the hashmap.</returns>
        public bool IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// Resets the hashmap to its default values.
        /// </summary>
        public void Clear()
        {
            this.Table = new Entry<K, V>[InitialCapacity];
            this.LoadFactor = 0;
            this.size = 0;
            this.Placeholder = 0;
        }
           
        /// <summary>
        /// Returns a matching bucket or the next bucket until an empty bucket is found.
        /// </summary>
        /// <param name="key">The key of the bucket</param>
        /// <returns>The index of the empty bucket.</returns>
        /// <exception cref="ArgumentNullException">Throws an error if the key entered is null.</exception>
        public int GetMatchingOrNextAvailableBucket(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key cannot be null");
            }

            int index = Math.Abs(key.GetHashCode() % Table.Length);

            while (Table[index] != null && !Table[index].Key.Equals(key))
            {
                index++;
                if (index == Table.Length) { index = 0; }
            }

            return index;
        }
        
        /// <summary>
        /// Returns the bucket of the key entered or null if it does not exist.
        /// </summary>
        /// <param name="key">The key to be found.</param>
        /// <returns>The index of the key.</returns>
        public V Get(K key)
        {
            int bucket = GetMatchingOrNextAvailableBucket(key);

            Entry<K, V> foundBucket = Table[bucket];

            return foundBucket != null ? foundBucket.Value : default(V);
        }

        /// <summary>
        /// Inserts a key and value pair into a bucket.
        /// </summary>
        /// <param name="key">The key of the entered pair.</param>
        /// <param name="value">The value of the entered pair.</param>
        /// <returns>Returns the default value if the bucket was empty, else the old value is returned.</returns>
        /// <exception cref="ArgumentNullException">Throws an error if the value entered is null.</exception>
        public V? Put(K key, V value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value cannot be null");
            }

            int bucket = GetMatchingOrNextAvailableBucket(key);

            if (Table[bucket] == null)
            {
                Table[bucket] = new Entry<K, V>(key, value);
                size++;

                if (size + Placeholder >= Math.Floor(InitialCapacity * LoadFactor))
                {
                    ReHash();
                }

                return default(V);
            }
            else
            {
                V oldValue = Table[bucket].Value;
                Table[bucket].Value = value;
                return oldValue;
            }
        }

        /// <summary>
        /// Removes a key value pair based off the entered key.
        /// </summary>
        /// <param name="key">The key to be removed.</param>
        /// <returns>The value removed.</returns>
        public V? Remove(K key)
        {
            int bucket = GetMatchingOrNextAvailableBucket(key);

            if (Table[bucket] != null)
            {
                if (Table[bucket].Key.Equals(key))
                {
                    V oldValue = Table[bucket].Value;
                    Table[bucket].Value = default(V);
                    Placeholder++;
                    size--;

                    return oldValue;
                }
            }

            return default(V);
        }

        /// <summary>
        /// Resizes the hashmap.
        /// </summary>
        /// <returns>The new capacity of the hashmap.</returns>
        private int Resize()
        {
            int newCapacity = InitialCapacity * 2 + 1;

            while (true)
            {
                bool isPrime = true;
                int sqrt = (int)Math.Sqrt(newCapacity);

                for (int i = 2; i <= sqrt; i++)
                {
                    if (newCapacity % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    return newCapacity;
                }

                newCapacity += 2;
            }
        }

        /// <summary>
        /// Rehashes the hashmap.
        /// </summary>
        public void ReHash()
        {
            int newCapacity = Resize();
            Entry<K, V>[] newTable = new Entry<K, V>[newCapacity];
            Entry<K, V>[] oldTable = Table;

            Table = newTable;  
            size = 0;
            Placeholder = 0;

            foreach (Entry<K, V> entry in oldTable)
            {
                if (entry != null && entry.Value != null)
                {
                    int bucket = GetMatchingOrNextAvailableBucket(entry.Key);
                    newTable[bucket] = entry;
                    size++;
                }
            }

            InitialCapacity = newCapacity;
        }

        /// <summary>
        /// Returns all the values in the hashmap.
        /// </summary>
        /// <returns>A list of all the filled values in the hashmap.</returns>
        public IEnumerator<V> Values()
        {
            List<V> values = new List<V>();

            foreach (Entry<K, V> entry in Table)
            {
                if (entry != null && entry.Value != null)
                {
                    values.Add(entry.Value);
                }
            }

            return values.GetEnumerator();
        }

        /// <summary>
        /// Returns all the keys in the hashmap.
        /// </summary>
        /// <returns>A list of all the keys in the hashmap that have a value.s</returns>
        public IEnumerator<K> Keys()
        {
            List<K> keys = new List<K>();

            foreach (Entry<K, V> entry in Table)
            {
                if (entry != null && entry.Value != null)
                {
                    keys.Add(entry.Key);
                }
            }
            
            return keys.GetEnumerator();
        }
    }
}
