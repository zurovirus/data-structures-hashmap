using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    /// <summary>
    /// The Map Interface
    /// </summary>
    /// <typeparam name="K">A default value</typeparam>
    /// <typeparam name="V">Another default value</typeparam>
    public interface IMap<K, V>
    {
        int Size();
        bool IsEmpty();
        void Clear();
        V Get(K key);
        V Put(K key, V Value);
        V Remove(K key);
        IEnumerator<K> Keys();
        IEnumerator<V> Values();
    }
}
