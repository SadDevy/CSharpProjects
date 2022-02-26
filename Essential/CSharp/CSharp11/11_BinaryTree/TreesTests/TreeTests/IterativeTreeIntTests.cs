using System.Collections;
using System.Collections.Generic;
using BinarySearch;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TreesTests
{
    [TestFixture]
    public class IterativeTreeIntTests : IterativeTreeTests<int>
    {
        [Test]
        public void TestEmptyTree()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { });
            Assert.IsNotNull(a);

            Assert.IsTrue(a.IsEmpty);
        }

        [Test]
        public void TestRootOnly()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 0 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(0, a.Root.Data);

            Assert.IsFalse(a.Root.HasLeft);
            Assert.IsFalse(a.Root.HasRight);
        }

        [Test]
        public void TestRootRightChild()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 0, 1 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(0, a.Root.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(1, a.Root.Right.Data);

            Assert.IsFalse(a.Root.HasLeft);
        }

        [Test]
        public void TestRootLeftChild()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 1, 0 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(1, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(0, a.Root.Left.Data);

            Assert.IsFalse(a.Root.HasRight);
        }

        [Test]
        public void TestRootBothChildren()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 2, 1, 3 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(2, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(1, a.Root.Left.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(3, a.Root.Right.Data);
        }

        [Test]
        public void TestRootLeftChildLeftChild()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 2, 1, 0 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(2, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(1, a.Root.Left.Data);

            Assert.IsTrue(a.Root.Left.HasLeft);
            Assert.AreEqual(0, a.Root.Left.Left.Data);
        }

        [Test]
        public void TestRootRightChildRightChild()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 0, 1, 2 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(0, a.Root.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(1, a.Root.Right.Data);

            Assert.IsTrue(a.Root.Right.HasRight);
            Assert.AreEqual(2, a.Root.Right.Right.Data);
        }

        [Test]
        public void TestTwoChildrenRootAndLeft()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 3, 2, 4, 1 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(3, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(2, a.Root.Left.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(4, a.Root.Right.Data);

            Assert.IsTrue(a.Root.Left.HasLeft);
            Assert.AreEqual(1, a.Root.Left.Left.Data);
        }

        [Test]
        public void TestTwoChildrenRootAndRight()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 2, 1, 3, 4 });
            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(2, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(1, a.Root.Left.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(3, a.Root.Right.Data);

            Assert.IsTrue(a.Root.Right.HasRight);
            Assert.AreEqual(4, a.Root.Right.Right.Data);
        }

        [Test]
        public void TestThreeLevelsAllElements()
        {
            BinarySearchTree<int> a = CreateTree(new List<int> { 4, 2, 1, 3, 6, 5, 7 });

            Assert.IsNotNull(a);

            Assert.IsFalse(a.IsEmpty);
            Assert.AreEqual(4, a.Root.Data);

            Assert.IsTrue(a.Root.HasLeft);
            Assert.AreEqual(2, a.Root.Left.Data);

            Assert.IsTrue(a.Root.HasRight);
            Assert.AreEqual(6, a.Root.Right.Data);

            Assert.IsTrue(a.Root.Left.HasLeft);
            Assert.AreEqual(1, a.Root.Left.Left.Data);

            Assert.IsTrue(a.Root.Left.HasRight);
            Assert.AreEqual(3, a.Root.Left.Right.Data);

            Assert.IsTrue(a.Root.Right.HasLeft);
            Assert.AreEqual(5, a.Root.Right.Left.Data);

            Assert.IsTrue(a.Root.Right.HasRight);
            Assert.AreEqual(7, a.Root.Right.Right.Data);
        }

        [TestCaseSource(nameof(GetTestConstructorWithComparerIntegerTestCases))]
        public override void TestConstructorWithComparer(IComparer<int> comparer)
        {
            base.TestConstructorWithComparer(comparer);
        }

        public static IEnumerable GetTestConstructorWithComparerIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(Comparer<int>.Default);
                yield return new TestCaseData(null);
            }
        }

        [TestCaseSource(nameof(GetTestConstructorWithCollectionIntegerTestCases))]
        public override List<int> TestConstructorWithCollection(List<int> values)
        {
            return base.TestConstructorWithCollection(values);
        }

        public static IEnumerable GetTestConstructorWithCollectionIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>()).Returns(new List<int>(0));

                yield return new TestCaseData(new List<int>() { 0 }).Returns(new List<int>() { 0 });

                yield return new TestCaseData(new List<int>() { 0, 1 })
                                             .Returns(new List<int>() { 0, 1 });
                yield return new TestCaseData(new List<int>() { 1, 0 })
                                             .Returns(new List<int> { 0, 1 });

                yield return new TestCaseData(new List<int>() { 1, 0, 2 })
                                             .Returns(new List<int>() { 0, 1, 2 });
                yield return new TestCaseData(new List<int>() { 0, 1, 2 })
                                             .Returns(new List<int>() { 0, 1, 2 });
                yield return new TestCaseData(new List<int>() { 2, 1, 0 })
                                             .Returns(new List<int>() { 0, 1, 2 });

                yield return new TestCaseData(new List<int>() { 2, 1, 3, 0 })
                                             .Returns(new List<int>() { 0, 1, 2, 3 });
                yield return new TestCaseData(new List<int>() { 2, 1, 3, 2 })
                                             .Returns(new List<int>() { 1, 2, 2, 3 });
                yield return new TestCaseData(new List<int>() { 2, 1, 3, 3 })
                                             .Returns(new List<int>() { 1, 2, 3, 3 });
                yield return new TestCaseData(new List<int>() { 2, 1, 3, 4 })
                                             .Returns(new List<int>() { 1, 2, 3, 4 });

                yield return new TestCaseData(new List<int>() { 2, 1, 1, 2, 3 })
                                             .Returns(new List<int>() { 1, 1, 2, 2, 3 });
                yield return new TestCaseData(new List<int>() { 2, 1, 1, 1, 3 })
                                             .Returns(new List<int>() { 1, 1, 1, 2, 3 });
                yield return new TestCaseData(new List<int>() { 3, 1, 2, 3, 4 })
                                             .Returns(new List<int>() { 1, 2, 3, 3, 4 });
                yield return new TestCaseData(new List<int>() { 3, 1, 4, 4, 5 })
                                             .Returns(new List<int>() { 1, 3, 4, 4, 5 });
                yield return new TestCaseData(new List<int>() { 3, 1, 4, 4, 4 })
                                             .Returns(new List<int>() { 1, 3, 4, 4, 4 });
                yield return new TestCaseData(new List<int>() { 3, 1, 4, 5, 6 })
                                             .Returns(new List<int>() { 1, 3, 4, 5, 6 });

                yield return new TestCaseData(new List<int>() { 2, 1, 1, 2, 3, 3 })
                                             .Returns(new List<int>() { 1, 1, 2, 2, 3, 3 });
                yield return new TestCaseData(new List<int>() { 2, 1, 1, 2, 3, 1 })
                                             .Returns(new List<int>() { 1, 1, 1, 2, 2, 3 });
                yield return new TestCaseData(new List<int>() { 3, 1, 2, 3, 4, 2 })
                                             .Returns(new List<int>() { 1, 2, 2, 3, 3, 4 });
                yield return new TestCaseData(new List<int>() { 3, 1, 2, 3, 4, 3 })
                                             .Returns(new List<int>() { 1, 2, 3, 3, 3, 4 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 4, 5 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 4, 5 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 5, 6 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 5, 6 });

                yield return new TestCaseData(new List<int>() { 4, 2, 1, 1, 3, 5, 5 })
                                             .Returns(new List<int>() { 1, 1, 2, 3, 4, 5, 5 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 2, 3, 5, 5 })
                                             .Returns(new List<int>() { 1, 2, 2, 3, 4, 5, 5 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 3, 5, 5 })
                                             .Returns(new List<int>() { 1, 2, 3, 3, 4, 5, 5 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 4, 5, 5 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 4, 5, 5 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 6, 5, 5 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 5, 5, 6 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 6, 5, 6 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 5, 6, 6 });
                yield return new TestCaseData(new List<int>() { 4, 2, 1, 3, 6, 5, 7 })
                                             .Returns(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
            }
        }

        [TestCaseSource(nameof(GetTestAddIntegerTestCases))]
        public override List<int> TestAdd(List<int> values, int value)
        {
            return base.TestAdd(values, value);
        }

        public static IEnumerable GetTestAddIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>(), 5)
                                             .Returns(new List<int>() { 5 });

                yield return new TestCaseData(new List<int>() { 5 }, 7)
                                             .Returns(new List<int>() { 5, 7 });
                yield return new TestCaseData(new List<int>() { 5, 7 }, 3)
                                             .Returns(new List<int>() { 3, 5, 7 });

                yield return new TestCaseData(new List<int>() { 5, 7, 3 }, 2)
                                             .Returns(new List<int>() { 2, 3, 5, 7 });
                yield return new TestCaseData(new List<int>() { 5, 7, 3, 2 }, 4)
                                             .Returns(new List<int>() { 2, 3, 4, 5, 7 });
                yield return new TestCaseData(new List<int>() { 5, 7, 3, 2, 4 }, 6)
                                             .Returns(new List<int>() { 2, 3, 4, 5, 6, 7 });
                yield return new TestCaseData(new List<int>() { 5, 7, 3, 2, 4, 6 }, 8)
                                             .Returns(new List<int>() { 2, 3, 4, 5, 6, 7, 8 });
            }
        }

        [TestCaseSource(nameof(GetTestGetMaxIntegerTestCases))]
        public override int TestGetMax(List<int> values)
        {
            return base.TestGetMax(values);
        }

        public static IEnumerable GetTestGetMaxIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>() { 2 })
                                             .Returns(2);

                yield return new TestCaseData(new List<int>() { 2, 3, 4 })
                                             .Returns(4);
                yield return new TestCaseData(new List<int>() { 4, 2, 1 })
                                             .Returns(4);

                yield return new TestCaseData(new List<int>() { 2, 1, 2, 3, 4 })
                                             .Returns(4);
                yield return new TestCaseData(new List<int>() { 2, 1, 2, 3, 4, 5, 6 })
                                             .Returns(6);
                yield return new TestCaseData(new List<int>() { 2, 1, 2, 3, 4, 5, 6, 7 })
                                             .Returns(7);
            }
        }

        [TestCaseSource(nameof(GetTestGetMinIntegerTestCases))]
        public override int TestGetMin(List<int> values)
        {
            return base.TestGetMin(values);
        }

        public static IEnumerable GetTestGetMinIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>() { 2 })
                                             .Returns(2);

                yield return new TestCaseData(new List<int>() { 4, 3, 2 })
                                             .Returns(2);
                yield return new TestCaseData(new List<int>() { 2, 3, 4 })
                                             .Returns(2);

                yield return new TestCaseData(new List<int>() { 2, 1, 2, 3, 0 })
                                             .Returns(0);
                yield return new TestCaseData(new List<int>() { 2, 1, 2, 3, 1, 1, 2 })
                                             .Returns(1);
                yield return new TestCaseData(new List<int>() { 2, 6, 2, 3, 4, 5, 6, 7 })
                                             .Returns(2);
            }
        }

        [TestCaseSource(nameof(GetTestGetEnumeratorIntegerTestCases))]
        public override List<int> TestGetEnumerator(List<int> values)
        {
            return base.TestGetEnumerator(values);
        }

        public static IEnumerable GetTestGetEnumeratorIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>() { 5, 7, 3, 2, 1, 6 })
                                             .Returns(new List<int>() { 1, 2, 3, 5, 6, 7 });
                yield return new TestCaseData(new List<int>() { 7, 6, 5, 3, 2, 1 })
                                             .Returns(new List<int>() { 1, 2, 3, 5, 6, 7 });
                yield return new TestCaseData(new List<int>() { 1, 2, 3, 5, 6, 7 })
                                             .Returns(new List<int>() { 1, 2, 3, 5, 6, 7 });
            }
        }

        [TestCaseSource(nameof(GetTestGetRevesedEnumeratorIntegerTestCases))]
        public override List<int> TestGetReversedEnumerator(List<int> values)
        {
            return base.TestGetReversedEnumerator(values);
        }

        public static IEnumerable GetTestGetRevesedEnumeratorIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>() { 5, 7, 3, 2, 1, 6 })
                                             .Returns(new List<int>() { 7, 6, 5, 3, 2, 1 });
                yield return new TestCaseData(new List<int>() { 7, 6, 5, 3, 2, 1 })
                                             .Returns(new List<int>() { 7, 6, 5, 3, 2, 1 });
                yield return new TestCaseData(new List<int>() { 1, 2, 3, 5, 6, 7 })
                                             .Returns(new List<int>() { 7, 6, 5, 3, 2, 1 });
            }
        }

        [TestCaseSource(nameof(GetTestFindIntegerTestCases))]
        public override Node<int> TestFind(List<int> values, int value)
        {
            return base.TestFind(values, value);
        }

        public static IEnumerable GetTestFindIntegerTestCases
        {
            get
            {
                List<int> values = new List<int>() { 5, 7, 6, 2, 3, 1 };
                yield return new TestCaseData(values, 5)
                                             .Returns(new Node<int>(5));

                yield return new TestCaseData(values, 1)
                                             .Returns(new Node<int>(1));
                yield return new TestCaseData(values, 6)
                                             .Returns(new Node<int>(6));
                yield return new TestCaseData(values, 2)
                                             .Returns(new Node<int>(2));

                yield return new TestCaseData(values, 0)
                                             .Returns(null);
            }
        }

        [TestCaseSource(nameof(GetTestRemoveIntegerTestCases))]
        public override bool TestRemove(List<int> values, int value)
        {
            return base.TestRemove(values, value);
        }

        public static IEnumerable GetTestRemoveIntegerTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>(), 1)
                                             .Returns(false);

                yield return new TestCaseData(new List<int>() { 1 }, 1)
                                             .Returns(true);

                yield return new TestCaseData(new List<int>() { 1, 2 }, 2)
                                             .Returns(true);
                yield return new TestCaseData(new List<int>() { 2, 1 }, 1)
                                             .Returns(true);

                yield return new TestCaseData(new List<int>() { 1, 2, 3 }, 2)
                                             .Returns(true);
                yield return new TestCaseData(new List<int>() { 2, 1, 0 }, 1)
                                             .Returns(true);
                yield return new TestCaseData(new List<int>() { 1, 2, 0 }, 1)
                                             .Returns(true);
            }
        }
    }
}
