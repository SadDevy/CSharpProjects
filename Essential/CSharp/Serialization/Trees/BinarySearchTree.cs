using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinaryTee
{
    [Serializable]
    public abstract class BinarySearchTree<TItem> : IEnumerable<TItem> where TItem : IComparable<TItem>
    {
        [XmlArray("Tree")]
        public Node<TItem> Root { get; protected set; }
        public bool IsEmpty
        {
            get { return Root == null; }
        }

        protected IComparer<TItem> comparer;

        public BinarySearchTree() : this(new TItem[0], null) { }

        public BinarySearchTree(IComparer<TItem> comparer) : this(Array.Empty<TItem>(), comparer) { }

        public BinarySearchTree(IEnumerable<TItem> collection) : this(collection, null) { }

        public BinarySearchTree(IEnumerable<TItem> collection, IComparer<TItem> comparer)
        {
            if (comparer == null)
                this.comparer = Comparer<TItem>.Default;
            else
                this.comparer = comparer;

            AddRange(collection);
        }

        public abstract void Add(TItem data);

        public void AddRange(IEnumerable<TItem> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            foreach (TItem item in data)
                Add(item);
        }

        public abstract Node<TItem> Find(TItem data);
        protected abstract Node<TItem> Find(TItem data, out Node<TItem> parent);

        public abstract IEnumerator<TItem> GetEnumerator();
        public abstract IEnumerator<TItem> GetReversedEnumarator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return IsForward ? GetEnumerator() : GetReversedEnumarator();
        }

        public abstract Node<TItem> GetRight(Node<TItem> root);

        public abstract Node<TItem> GetLeft(Node<TItem> root);

        public TItem GetMax()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Дерево пустое.");

            Node<TItem> max = IsForward ? GetRight(Root) : GetLeft(Root);
            return (max != null) ? max.Data : Root.Data;
        }

        public TItem GetMin()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Дерево пустое.");

            Node<TItem> min = IsForward ? GetLeft(Root) : GetRight(Root);
            return (min != null) ? min.Data : Root.Data;
        }

        private bool IsForward
        {
            get
            {
                if (comparer is GeneralComparer<TItem>)
                {
                    GeneralComparer<TItem> c = (GeneralComparer<TItem>)comparer;
                    return c.IsDirect;
                }

                return true;
            }
        }

        protected int Compare(TItem data, Node<TItem> node)
        {
            int result;
            if (node == null)
            {
                result = 1;
            }
            else
            {
                result = comparer.Compare(data, node.Data);
            }

            result *= IsForward ? 1 : -1;
            return result;
        }

        public bool Remove(TItem data)
        {
            if (data == null)
                return false;

            Node<TItem> b;
            Node<TItem> a = Find(data, out b);

            if (a == null)
                return false;

            if (!a.HasLeft && !a.HasRight)
            {
                RemoveNodeHavingNoChilds(a, b);
                return true;
            }

            if (a.HasLeft && a.HasRight)
            {
                RemoveNodeHavingBothChilds(a, b);
                return true;
            }

            RemoveNodeHavingOneChild(a, b);
            return true;
        }

        private void RemoveNodeHavingNoChilds(Node<TItem> nodeToRemove, Node<TItem> parent)
        {
            if (nodeToRemove == Root)
            {
                Root = null;
                return;
            }

            if (parent.Left == nodeToRemove)
            {
                parent.Left = null;
                return;
            }

            parent.Right = null;
        }

        private void RemoveNodeHavingOneChild(Node<TItem> nodeToRemove, Node<TItem> parent)
        {
            Node<TItem> node = (nodeToRemove.HasLeft) ? nodeToRemove.Left : nodeToRemove.Right;
            if (nodeToRemove == Root)
            {
                Root = node;
                return;
            }

            if (parent.Left == nodeToRemove)
            {
                parent.Left = node;
                return;
            }

            parent.Right = node;
        }

        private void RemoveNodeHavingBothChilds(Node<TItem> nodeToRemove, Node<TItem> parent)
        {
            if (nodeToRemove != Root)
            {
                if (parent.Left == nodeToRemove)
                {
                    parent.Left = nodeToRemove.Right;
                }

                if (parent.Right == nodeToRemove)
                {
                    parent.Right = nodeToRemove.Right;
                }
            }
            else
            {
                Root = Root.Right;
            }

            Node<TItem> a;
            Find(nodeToRemove.Left.Data, out a);

            a.Left = nodeToRemove.Left;
        }

    }
}
