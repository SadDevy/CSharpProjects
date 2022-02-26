using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class RecursiveTree<TItem> : BinarySearchTree<TItem> where TItem : IComparable<TItem>
    {
        public RecursiveTree(IComparer<TItem> comparer) : base(comparer)
        {
        }

        public RecursiveTree(IEnumerable<TItem> collection) : base(collection)
        {
        }

        public RecursiveTree(IEnumerable<TItem> collection, IComparer<TItem> comparer) : base(collection, comparer)
        {
        }

        public override void Add(TItem data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (IsEmpty)
            {
                Root = new Node<TItem>(data, null, null);
                return;
            }

            Add(data, Root);
        }

        private void Add(TItem data, Node<TItem> current)
        {
            if (data.CompareTo(current.Data) <= 0)
            {
                if (current.Left == null)
                    current.Left = new Node<TItem>(data);
                else
                    Add(data, current.Left);
            }
            else
            {
                if (current.Right == null)
                    current.Right = new Node<TItem>(data);
                else
                    Add(data, current.Right);
            }
        }

        public override Node<TItem> Find(TItem data)
        {
            return Find(data, out Node<TItem> parent);
        }

        protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        {
            parent = null;
            return Find(data, Root, ref parent); ;
        }

        private Node<TItem> Find(TItem data, Node<TItem> current, ref Node<TItem> parent)
        {
            if (current == null || data.CompareTo(current.Data) == 0)
                return current;

            parent = current;
            if (data.CompareTo(current.Data) <= 0)
            {
                if (current.Left == null)
                    return current.Left;

                return Find(data, current.Left, ref parent);
            }
            else
            {
                if (current.Right == null)
                    return current.Right;

                return Find(data, current.Right, ref parent);
            }
        }

        private IEnumerable<TItem> GetInOrder(Node<TItem> root)
        {
            if (root == null)
                yield break;

            if (root.Left != null)
            {
                foreach (TItem item in GetInOrder(root.Left))
                    yield return item;
            }

            yield return root.Data;

            if (root.Right != null)
            {
                foreach (TItem item in GetInOrder(root.Right))
                    yield return item;
            }
        }

        public override IEnumerator<TItem> GetEnumerator()
        {
            return GetInOrder(Root).GetEnumerator();
        }

        public override IEnumerator<TItem> GetReversedEnumarator()
        {
            return new ReversedEnumerator<TItem>(this);
        }

        public override Node<TItem> GetRight(Node<TItem> root)
        {
            if (root.Right == null)
                return root;

            return GetRight(root.Right);
        }

        public override Node<TItem> GetLeft(Node<TItem> root)
        {
            if (root.Left == null)
                return root;

            return GetLeft(root.Left);
        }
    }
}
