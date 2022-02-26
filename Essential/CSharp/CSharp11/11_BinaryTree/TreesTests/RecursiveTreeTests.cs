using BinarySearch;
using System;
using System.Collections.Generic;

namespace TreesTests
{
    public abstract class RecursiveTreeTests<TItem> : BinaryTreeTests<TItem>
        where TItem : IComparable<TItem>
    {
        public override BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection)
        {
            return new RecursiveTree<TItem>(collection);
        }

        public override BinarySearchTree<TItem> CreateTree(IComparer<TItem> comparer)
        {
            return new RecursiveTree<TItem>(comparer);
        }

        public override BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection, IComparer<TItem> comparer)
        {
            return new RecursiveTree<TItem>(collection, comparer);
        }
    }
}
