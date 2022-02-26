using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearch
{
    public class ReversedIterator<TItem> : IEnumerator<TItem> where TItem : IComparable<TItem>
    {
        private BinarySearchTree<TItem> tree;
        private Stack<Node<TItem>> elements;
        private Node<TItem> root;

        private int currentIndex;
        private TItem currentItem;

        public TItem Current { get => currentItem; }

        object IEnumerator.Current
        {
            get => Current;
        }

        public ReversedIterator(BinarySearchTree<TItem> tree)
        {
            elements = new Stack<Node<TItem>>();
            this.tree = tree;
            root = tree.Root;

            currentIndex = -1;
            currentItem = default;
        }

        private Node<TItem> GetCurrentItem()
        {
            Node<TItem> current = null;
            while (current == null)
            {
                if (root == null)
                {
                    root = elements.Pop();
                    current = root;

                    root = root.Left;

                }
                else
                {
                    elements.Push(root);
                    root = root.Right;
                }
            }

            return current;
        }

        public bool MoveNext()
        {
            if (elements.Count <= 0 && root == null)
                return false;

            currentIndex++;
            currentItem = GetCurrentItem().Data;

            return true;
        }

        public void Reset() { currentIndex = -1; }

        void IDisposable.Dispose() { }
    }
}
