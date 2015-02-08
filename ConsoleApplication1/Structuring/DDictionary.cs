using System.Collections;
using System.Collections.Generic;

namespace ParticleSimulation.Structuring
{
    public class DDictionary<K, V> : IDictionary<K, V>
    {
        private readonly IDictionary<K, V> leftDictionary ;
        private readonly IDictionary<V, K> rightDictionary;

        public DDictionary(IDictionary<K, V> leftDictionary, IDictionary<V, K> rightDictionary)
        {
            this.leftDictionary = leftDictionary;
            this.rightDictionary = rightDictionary;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return leftDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            if(leftDictionary.ContainsKey(item.Key))
                leftDictionary.Add(item);//to throw the exception
            if(rightDictionary.ContainsKey(item.Value))
                rightDictionary.Add(item.Value,item.Key);
            leftDictionary.Add(item);
            rightDictionary.Add(item.Value,item.Key);
        }
        
        public void Clear()
        {
            leftDictionary.Clear();
            rightDictionary.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return leftDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            leftDictionary.CopyTo(array,arrayIndex);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            if (!leftDictionary.ContainsKey(item.Key) || !rightDictionary.ContainsKey(item.Value))
                return false;
            leftDictionary.Remove(item.Key);
            rightDictionary.Remove(item.Value);
            return true;
        }

        public int Count
        {
            get { return leftDictionary.Count; }}
        public bool IsReadOnly
        {
            get { return leftDictionary.IsReadOnly && rightDictionary.IsReadOnly; }
        }
        public bool ContainsKey(K key)
        {
            return leftDictionary.ContainsKey(key);
        }

        public void Add(K key, V value)
        {
             Add(new KeyValuePair<K, V>(key,value));
        }

        public bool Remove(K key)
        {
            if (!leftDictionary.ContainsKey(key))
                return false;
            var value = leftDictionary[key];
            rightDictionary.Remove(value);
            leftDictionary.Remove(key);
            return true;
        }

        public bool TryGetValue(K key, out V value)
        {
            return leftDictionary.TryGetValue(key, out value);
        }
        public bool TryGetKey(V value, out K key)
        {
            return rightDictionary.TryGetValue(value, out key);
        }

        public V this[K key]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public ICollection<K> Keys
        {
            get { return leftDictionary.Keys; }
        }
        public ICollection<V> Values
        {
            get { return rightDictionary.Keys; }
        }
    }
}