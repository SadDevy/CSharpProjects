using BinarySearch;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace TreesTests.TreeTests
{
    [TestFixture]
    public class RecursiveTreeStringTests : RecursiveTreeTests<string>
    {
        [TestCaseSource(nameof(GetTestConstructorWithComparerCases))]
        public override void TestConstructorWithComparer(IComparer<string> comparer)
        {
            base.TestConstructorWithComparer(comparer);
        }

        public static IEnumerable GetTestConstructorWithComparerCases
        {
            get
            {
                yield return new TestCaseData(Comparer<string>.Default);
                yield return new TestCaseData(null);
            }
        }

        [TestCaseSource(nameof(GetTestConstructorWithCollectionStringTestCases))]
        public override List<string> TestConstructorWithCollection(List<string> values)
        {
            return base.TestConstructorWithCollection(values);
        }

        public static IEnumerable GetTestConstructorWithCollectionStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>(0))
                                             .Returns(new List<string>(0));

                yield return new TestCaseData(new List<string>() { "" })
                                             .Returns(new List<string>() { "" });

                yield return new TestCaseData(new List<string>() { "a", "b" })
                                             .Returns(new List<string>() { "a", "b" });
                yield return new TestCaseData(new List<string>() { "b", "a" })
                                             .Returns(new List<string>() { "a", "b" });

                yield return new TestCaseData(new List<string>() { "b", "a", "c" })
                                             .Returns(new List<string>() { "a", "b", "c" });
                yield return new TestCaseData(new List<string>() { "a", "b", "c" })
                                             .Returns(new List<string>() { "a", "b", "c" });
                yield return new TestCaseData(new List<string>() { "c", "b", "a" })
                                             .Returns(new List<string>() { "a", "b", "c" });

                yield return new TestCaseData(new List<string>() { "c", "b", "d", "a" })
                                             .Returns(new List<string>() { "a", "b", "c", "d" });
                yield return new TestCaseData(new List<string>() { "b", "a", "c", "b" })
                                             .Returns(new List<string>() { "a", "b", "b", "c" });
                yield return new TestCaseData(new List<string>() { "b", "a", "c", "c" })
                                             .Returns(new List<string>() { "a", "b", "c", "c" });
                yield return new TestCaseData(new List<string>() { "b", "a", "c", "d" })
                                             .Returns(new List<string>() { "a", "b", "c", "d" });

                yield return new TestCaseData(new List<string>() { "b", "a", "a", "b", "c" })
                                             .Returns(new List<string>() { "a", "a", "b", "b", "c" });
                yield return new TestCaseData(new List<string>() { "b", "a", "a", "a", "c" })
                                             .Returns(new List<string>() { "a", "a", "a", "b", "c" });
                yield return new TestCaseData(new List<string>() { "c", "a", "b", "c", "d" })
                                             .Returns(new List<string>() { "a", "b", "c", "c", "d" });
                yield return new TestCaseData(new List<string>() { "c", "a", "d", "d", "e" })
                                             .Returns(new List<string>() { "a", "c", "d", "d", "e" });
                yield return new TestCaseData(new List<string>() { "c", "a", "d", "d", "d" })
                                             .Returns(new List<string>() { "a", "c", "d", "d", "d" });
                yield return new TestCaseData(new List<string>() { "c", "a", "d", "e", "f" })
                                             .Returns(new List<string>() { "a", "c", "d", "e", "f" });

                yield return new TestCaseData(new List<string>() { "b", "a", "a", "b", "c", "c" })
                                             .Returns(new List<string>() { "a", "a", "b", "b", "c", "c" });
                yield return new TestCaseData(new List<string>() { "b", "a", "a", "b", "c", "a" })
                                             .Returns(new List<string>() { "a", "a", "a", "b", "b", "c" });
                yield return new TestCaseData(new List<string>() { "a", "b", "c", "c", "d", "b" })
                                             .Returns(new List<string>() { "a", "b", "b", "c", "c", "d" });
                yield return new TestCaseData(new List<string>() { "c", "a", "b", "c", "d", "c" })
                                             .Returns(new List<string>() { "a", "b", "c", "c", "c", "d" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "d", "e" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "d", "e" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "e", "f" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "e", "f" });

                yield return new TestCaseData(new List<string>() { "d", "b", "a", "a", "c", "e", "e" })
                                             .Returns(new List<string>() { "a", "a", "b", "c", "d", "e", "e" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "b", "c", "e", "e" })
                                             .Returns(new List<string>() { "a", "b", "b", "c", "d", "e", "e" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "c", "e", "e" })
                                             .Returns(new List<string>() { "a", "b", "c", "c", "d", "e", "e" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "d", "e", "e" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "d", "e", "e" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "f", "e", "e" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "e", "e", "f" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "f", "e", "f" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "e", "f", "f" });
                yield return new TestCaseData(new List<string>() { "d", "b", "a", "c", "f", "e", "g" })
                                             .Returns(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });
            }
        }

        [TestCaseSource(nameof(GetTestAddStringTestCases))]
        public override List<string> TestAdd(List<string> values, string value)
        {
            return base.TestAdd(values, value);
        }

        public static IEnumerable GetTestAddStringTestCases
        {
            get
            {
                IterativeTree<string> a = new IterativeTree<string>(new List<string>());
                yield return new TestCaseData(new List<string>(), "e")
                                             .Returns(new List<string>() { "e" });

                yield return new TestCaseData(new List<string>() { "e" }, "g")
                                             .Returns(new List<string>() { "e", "g" });
                yield return new TestCaseData(new List<string>() { "e", "g" }, "c")
                                             .Returns(new List<string>() { "c", "e", "g" });

                yield return new TestCaseData(new List<string>() { "e", "g", "c" }, "b")
                                             .Returns(new List<string>() { "b", "c", "e", "g" });
                yield return new TestCaseData(new List<string>() { "e", "g", "c", "b" }, "d")
                                             .Returns(new List<string>() { "b", "c", "d", "e", "g" });
                yield return new TestCaseData(new List<string>() { "e", "g", "c", "b", "d" }, "f")
                                             .Returns(new List<string>() { "b", "c", "d", "e", "f", "g" });
                yield return new TestCaseData(new List<string>() { "e", "g", "c", "b", "d", "f" }, "h")
                                             .Returns(new List<string>() { "b", "c", "d", "e", "f", "g", "h" });
            }
        }

        [TestCaseSource(nameof(GetTestGetMaxStringTestCases))]
        public override string TestGetMax(List<string> values)
        {
            return base.TestGetMax(values);
        }

        public static IEnumerable GetTestGetMaxStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>() { "b" })
                                             .Returns("b");

                yield return new TestCaseData(new List<string>() { "b", "c", "d" })
                                             .Returns("d");
                yield return new TestCaseData(new List<string>() { "d", "c", "b" })
                                             .Returns("d");

                yield return new TestCaseData(new List<string>() { "b", "a", "b", "c", "d" })
                                             .Returns("d");
                yield return new TestCaseData(new List<string>() { "b", "a", "b", "c", "d", "e" })
                                             .Returns("e");
                yield return new TestCaseData(new List<string>() { "b", "a", "b", "c", "d", "e", "f" })
                                             .Returns("f");
            }
        }

        [TestCaseSource(nameof(GetTestGetMinStringTestCases))]
        public override string TestGetMin(List<string> values)
        {
            return base.TestGetMin(values);
        }

        public static IEnumerable GetTestGetMinStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>() { "b" })
                                             .Returns("b");

                yield return new TestCaseData(new List<string>() { "d", "c", "b" })
                                             .Returns("b");
                yield return new TestCaseData(new List<string>() { "b", "c", "d" })
                                             .Returns("b");

                yield return new TestCaseData(new List<string>() { "b", "a", "b", "c", "d" })
                                             .Returns("a");
                yield return new TestCaseData(new List<string>() { "b", "b", "b", "c", "d", "e" })
                                             .Returns("b");
                yield return new TestCaseData(new List<string>() { "h", "d", "g", "c", "d", "e", "f" })
                                             .Returns("c");
            }
        }

        [TestCaseSource(nameof(GetTestGetEnumeratorStringTestCases))]
        public override List<string> TestGetEnumerator(List<string> values)
        {
            return base.TestGetEnumerator(values);
        }

        public static IEnumerable GetTestGetEnumeratorStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>() { "e", "g", "c", "b", "a", "f" })
                                             .Returns(new List<string>() { "a", "b", "c", "e", "f", "g" });
                yield return new TestCaseData(new List<string>() { "g", "e", "f", "c", "b", "a" })
                                             .Returns(new List<string>() { "a", "b", "c", "e", "f", "g" });
                yield return new TestCaseData(new List<string>() { "a", "b", "c", "e", "f", "g" })
                                             .Returns(new List<string>() { "a", "b", "c", "e", "f", "g" });
            }
        }

        [TestCaseSource(nameof(GetTestGetRevesedEnumeratorStringTestCases))]
        public override List<string> TestGetReversedEnumerator(List<string> values)
        {
            return base.TestGetReversedEnumerator(values);
        }

        public static IEnumerable GetTestGetRevesedEnumeratorStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>() { "e", "g", "c", "b", "a", "f" })
                                            .Returns(new List<string>() { "g", "f", "e", "c", "b", "a" });
                yield return new TestCaseData(new List<string>() { "g", "f", "e", "c", "b", "a" })
                                             .Returns(new List<string>() { "g", "f", "e", "c", "b", "a" });
                yield return new TestCaseData(new List<string>() { "a", "b", "c", "e", "f", "g" })
                                             .Returns(new List<string>() { "g", "f", "e", "c", "b", "a" });
            }
        }

        [TestCaseSource(nameof(GetTestFindStringTestCases))]
        public override Node<string> TestFind(List<string> values, string value)
        {
            return base.TestFind(values, value);
        }

        public static IEnumerable GetTestFindStringTestCases
        {
            get
            {
                List<string> values = new List<string> { "e", "f", "g", "c", "b", "a" };
                yield return new TestCaseData(values, "g").Returns(new Node<string>("g"));

                yield return new TestCaseData(values, "a").Returns(new Node<string>("a"));
                yield return new TestCaseData(values, "f").Returns(new Node<string>("f"));
                yield return new TestCaseData(values, "b").Returns(new Node<string>("b"));

                yield return new TestCaseData(values, "").Returns(null);
            }
        }

        [TestCaseSource(nameof(GetTestRemoveStringTestCases))]
        public override bool TestRemove(List<string> values, string value)
        {
            return base.TestRemove(values, value);
        }

        public static IEnumerable GetTestRemoveStringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>(), "a")
                                             .Returns(false);
                yield return new TestCaseData(new List<string>() { "" }, null)
                                             .Returns(false);

                yield return new TestCaseData(new List<string>() { "a" }, "a")
                                             .Returns(true);

                yield return new TestCaseData(new List<string>() { "a", "b" }, "b")
                                             .Returns(true);
                yield return new TestCaseData(new List<string>() { "b", "a" }, "a")
                                             .Returns(true);

                yield return new TestCaseData(new List<string>() { "a", "b", "c" }, "b")
                                             .Returns(true);
                yield return new TestCaseData(new List<string>() { "c", "b", "a" }, "b")
                                             .Returns(true);
                yield return new TestCaseData(new List<string>() { "b", "c", "a" }, "b")
                                             .Returns(true);
            }
        }
    }
}
