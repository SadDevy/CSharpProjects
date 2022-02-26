using System;
using System.Collections.Generic;

namespace BinaryTee
{
    [Serializable]
    public class IterativeTree<TItem> : BinarySearchTree<TItem> where TItem : IComparable<TItem>
    {
        public IterativeTree() : base() { }
        public IterativeTree(IComparer<TItem> comparer) : base(comparer)
        {
        }

        public IterativeTree(IEnumerable<TItem> collection) : base(collection)
        {
        }

        public IterativeTree(IEnumerable<TItem> collection, IComparer<TItem> comparer) : base(collection, comparer)
        {
        }

        public override void Add(TItem data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Node<TItem> node = new Node<TItem>(data);
            if (!IsEmpty)
            {
                Node<TItem> currentNode = Root;
                while (currentNode != null && currentNode != node)
                {
                    if (Compare(data, currentNode) <= 0)
                    {
                        if (currentNode.Left == null)
                            currentNode.Left = node;
                        else
                            currentNode = currentNode.Left;
                    }
                    else
                    {
                        if (currentNode.Right == null)
                            currentNode.Right = node;
                        else
                            currentNode = currentNode.Right;
                    }
                }
            }
            else
            {
                Root = node;
            }
        }

        public override Node<TItem> Find(TItem data)
        {
            return Find(data, out Node<TItem> parent);
        }
        protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        {
            parent = null;

            Node<TItem> child = Root;
            while (child != null && Compare(data, child) != 0)
            {
                parent = child;
                child = (Compare(data, child) <= 0) ? child.Left : child.Right;
            }

            return child;
        }

        private IEnumerable<TItem> GetArroundInOrder()
        {
            Stack<Node<TItem>> elements = new Stack<Node<TItem>>();
            Node<TItem> root = Root;
            while (elements.Count > 0 || root != null)
            {
                if (root == null)
                {
                    root = elements.Pop();
                    yield return root.Data;

                    root = root.Right;
                }
                else
                {
                    elements.Push(root);
                    root = root.Left;
                }
            }
        }

        public override IEnumerator<TItem> GetEnumerator()
        {
            return GetArroundInOrder().GetEnumerator();
        }

        public override IEnumerator<TItem> GetReversedEnumarator()
        {
            return new ReversedIterator<TItem>(this);
        }

        public override Node<TItem> GetRight(Node<TItem> root)
        {
            Node<TItem> result = null;
            if (root == null)
                return result;

            while (root.Right != null)
            {
                root = root.Right;
                result = root;
            }

            return result;
        }

        public override Node<TItem> GetLeft(Node<TItem> root)
        {
            Node<TItem> result = null;
            if (root == null)
                return result;

            while (root.Left != null)
            {
                root = root.Left;
                result = root;
            }

            return result;
        }
    }
}
