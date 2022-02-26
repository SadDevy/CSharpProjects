using System;
using System.Collections.Generic;
using BinarySearch;
using NUnit.Framework;

namespace TreesTests
{
    public abstract class BinaryTreeTests<TItem> where TItem : IComparable<TItem>
    {
        public abstract BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection);
        public abstract BinarySearchTree<TItem> CreateTree(IComparer<TItem> comparer);
        public abstract BinarySearchTree<TItem> CreateTree(ICollection<TItem> collection, IComparer<TItem> comparer);

        public virtual void TestConstructorWithComparer(IComparer<TItem> comparer)
        {
            BinarySearchTree<TItem> a = CreateTree(comparer);
            Assert.IsNotNull(a);
        }

        public virtual List<TItem> TestConstructorWithCollection(List<TItem> values)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            Assert.IsNotNull(a);

            List<TItem> result = new List<TItem>();
            result.AddRange(a);

            return result;
        }

        [Test]
        public void TestConstructorWithCollectionAndComparer()
        {
            BinarySearchTree<TItem> a = CreateTree(new List<TItem> { }, Comparer<TItem>.Default);
            Assert.IsNotNull(a);
        }

        public virtual List<TItem> TestAdd(List<TItem> values, TItem value)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            a.Add(value);

            List<TItem> result = new List<TItem>();
            result.AddRange(a);

            return result;
        }

        [Test]
        public void TestAddRangeException()
        {
            void A() 
            {
                BinarySearchTree<TItem> a = CreateTree(new List<TItem>() { });
                a.AddRange(null); 
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        public virtual TItem TestGetMax(List<TItem> values)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            Assert.IsNotNull(a);

            return a.GetMax();
        }

        [Test]
        public void TestGetMaxException()
        {
            void A() 
            {
                BinarySearchTree<TItem> a = CreateTree(new List<TItem>() { });
                a.GetMax(); 
            }
            Assert.Throws<InvalidOperationException>(A);
        }

        public virtual TItem TestGetMin(List<TItem> values)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            Assert.IsNotNull(a);

            return a.GetMin();
        }

        [Test]
        public void TestGetMinException()
        {
            void A() 
            {
                BinarySearchTree<TItem> a = CreateTree(new List<TItem>() { });
                a.GetMin(); 
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        public virtual List<TItem> TestGetEnumerator(List<TItem> values)
        {
            BinarySearchTree<TItem> a = CreateTree(values);

            List<TItem> result = new List<TItem>();
            result.AddRange(a);

            return result;
        }

        public virtual List<TItem> TestGetReversedEnumerator(List<TItem> values)
        {
            BinarySearchTree<TItem> a = CreateTree(values);

            IEnumerator<TItem> enumerator = a.GetReversedEnumarator();
            List<TItem> result = new List<TItem>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }

            return result;
        }

        public virtual Node<TItem> TestFind(List<TItem> values, TItem value)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            return a.Find(value);
        }

        public virtual bool TestRemove(List<TItem> values, TItem value)
        {
            BinarySearchTree<TItem> a = CreateTree(values);
            bool actual = a.Remove(value);

            foreach (TItem item in a)
                Assert.AreNotEqual(item, value);

            return actual;
        }
    }
}
