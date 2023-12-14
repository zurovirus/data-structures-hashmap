using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    /// <summary>
    /// The entry class
    /// </summary>
    /// <typeparam name="K">A default value of any type</typeparam>
    /// <typeparam name="V">Another default value of any type</typeparam>
    public class Entry<K, V>
    {
        public K? Key;
        public V? Value;

        /// <summary>
        /// Initializes an instance of the entry class.
        /// </summary>
        /// <param name="key">A value type to be entered.</param>
        /// <param name="value">Another value type to be entered.</param>
        public Entry(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
