using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearch
{
    [Serializable]
    public class KeyValue<TKey, TValue> 
        : IComparable<KeyValue<TKey, TValue>>, IEquatable<KeyValue<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValue() { }

        public KeyValue(TKey key = default, TValue value = default)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("[{0}, {1}]", Key, Value);
            
            return s.ToString();
        }

        public int CompareTo(KeyValue<TKey, TValue> a)
        {
            if (CompareTo(a.Key) != 0)
                return CompareTo(a.Key);

            return CompareTo(a.Value);
        }

        public int CompareTo(TKey key)
        {
            int result;
            if (Key is string)
                result = string.Compare(Key.ToString(), key.ToString(), true);
            else
                result = Key.CompareTo(key);

            return result;
        }

        public int CompareTo(TValue value)
        {
            Comparer<TValue> c = Comparer<TValue>.Default;
            return c.Compare(value, value);
        }

        public bool Equals(KeyValue<TKey, TValue> a)
        {
            return CompareTo(a) == 0;
        }
    }
}
