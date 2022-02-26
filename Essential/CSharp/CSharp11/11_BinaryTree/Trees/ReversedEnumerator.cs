using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearch
{
    public class ReversedEnumerator<TItem> : IEnumerator<TItem> where TItem : IComparable<TItem>
    {
        private BinarySearchTree<TItem> tree;
        private Stack<Node<TItem>> elements;
        private Node<TItem> node;

        private TItem currentItem;

        public TItem Current { get => currentItem; }

        object IEnumerator.Current
        {
            get => Current;
        }

        public ReversedEnumerator(BinarySearchTree<TItem> tree)
        {
            this.tree = tree;
            Reset();
        }

        public bool MoveNext()
        {
            while (node != null || elements.Count > 0)
            {
                if (node == null)
                {
                    node = elements.Pop();
                    currentItem = node.Data;

                    node = node.Left;
                    return true;
                }
                else
                {
                    elements.Push(node);
                    node = node.Right;
                }
            }

            return false;
        }

        public void Reset() 
        {
            elements = new Stack<Node<TItem>>();
            node = tree.Root;

            currentItem = default;
        }

        void IDisposable.Dispose() { }
    }
}
