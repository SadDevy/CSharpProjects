using BinaryTee;
using System;
using System.Collections.Generic;

namespace BinarySearch
{
    [Serializable]
    public class KeyValuePairTree<TKey, TValue> 
        : IterativeTree<KeyValue<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> collection = new List<TKey>();
                foreach (KeyValue<TKey, TValue> pairs in GetArroundPreorder())
                    collection.Add(pairs.Key);

                return collection;
            }
        }

        public KeyValuePairTree() : base() { }
        public KeyValuePairTree(IComparer<KeyValue<TKey, TValue>> comparer) : base(comparer) { }
        public KeyValuePairTree(ICollection<KeyValue<TKey, TValue>> collection) : base(collection) { }
        public KeyValuePairTree(ICollection<KeyValue<TKey, TValue>> collection, IComparer<KeyValue<TKey, TValue>> comparer)
            : base(collection, comparer) { }

        public override void Add(KeyValue<TKey, TValue> data)
        {
            if (Contains(data.Key))
                throw new InvalidOperationException("Элемент с таким ключом уже существует.");

            base.Add(data);
        }

        public bool Contains(TKey key)
        {
            if (key == null)
                return false;

            if (IsEmpty)
                return false;

            //foreach (KeyValue<TKey, TValue> value in GetArroundPreorder()) //!!!
            //{
            //    if (value.CompareTo(key) == 0)
            //        return true;
            //}

            //return false;

            return Find(key) == null ? false : true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;

            if (key == null)
                return false;

            KeyValue<TKey, TValue> result = Find(key);
            if (result != null)
                value = result.Value;

            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            if (key == null)
                return null;

            Node<KeyValue<TKey, TValue>> result = Find(new KeyValue<TKey, TValue>(key));
            if (result != null)
                return result.Data;

            return null;
        }

        public bool Remove(TKey key)
        {
            TValue value;
            if (!TryGetValue(key, out value))
                return false;

            return Remove(new KeyValue<TKey, TValue>(key, value));
        }

        public void CopyTo(KeyValuePairTree<TKey, TValue> tree)
        {
            tree.AddRange(GetArroundPreorder());
        }

        private IEnumerable<KeyValue<TKey, TValue>> GetArroundPreorder()
        {
            Stack<Node<KeyValue<TKey, TValue>>> elements = new Stack<Node<KeyValue<TKey, TValue>>>();
            Node<KeyValue<TKey, TValue>> root = Root;
            elements.Push(root);
            while (elements.Count > 0)
            {
                root = elements.Pop();

                yield return root.Data;

                if (root.Right != null)
                    elements.Push(root.Right);

                if (root.Left != null)
                    elements.Push(root.Left);
            }
        }
    }
}
