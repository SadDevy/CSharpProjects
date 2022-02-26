using BinarySearch;
using System;
using System.Collections.Generic;

namespace TreesTests
{
    public abstract class IterativeTreeTests<TItem> : BinaryTreeTests<TItem>
        where TItem : IComparable<TItem>
    {
        public override BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection)
        {
            return new IterativeTree<TItem>(collection);
        }

        public override BinarySearchTree<TItem> CreateTree(IComparer<TItem> comparer)
        {
            return new IterativeTree<TItem>(comparer);
        }

        public override BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection, IComparer<TItem> comparer)
        {
            return new IterativeTree<TItem>(collection, comparer);
        }
    }
}
