using BinarySearch;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TreesTests.TreeTests
{
    [TestFixture]
    public class RecursiveTreeStudentsTests : RecursiveTreeTests<StudentTestInfo>
    {
        [TestCaseSource(nameof(GetestConstructorWithComparerCases))]
        public override void TestConstructorWithComparer(IComparer<StudentTestInfo> comparer)
        {
            base.TestConstructorWithComparer(comparer);
        }

        public static IEnumerable GetestConstructorWithComparerCases
        {
            get
            {
                yield return new TestCaseData(Comparer<StudentTestInfo>.Default);
                yield return new TestCaseData(null);
            }
        }
        [TestCaseSource(nameof(GetTestConstructorWithCollectionStudentsTestCases))]
        public override List<StudentTestInfo> TestConstructorWithCollection(List<StudentTestInfo> values)
        {
            return base.TestConstructorWithCollection(values);
        }

        public static IEnumerable GetTestConstructorWithCollectionStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo d = new StudentTestInfo("Игорь", "Игорев", "Test", DateTime.Now, 4);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);

                yield return new TestCaseData(new List<StudentTestInfo>(0))
                                             .Returns(new List<StudentTestInfo>(0));

                yield return new TestCaseData(new List<StudentTestInfo>() { a })
                                             .Returns(new List<StudentTestInfo>() { a });

                yield return new TestCaseData(new List<StudentTestInfo>() { a, b })
                                             .Returns(new List<StudentTestInfo>() { a, b });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a })
                                             .Returns(new List<StudentTestInfo>() { a, b });

                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, c })
                                             .Returns(new List<StudentTestInfo>() { a, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c })
                                             .Returns(new List<StudentTestInfo>() { a, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, b, a })
                                             .Returns(new List<StudentTestInfo>() { a, b, c });

                yield return new TestCaseData(new List<StudentTestInfo>() { c, b, d, a })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, c, b })
                                             .Returns(new List<StudentTestInfo>() { a, b, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, c, c })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, c, d })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d });

                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, a, b, c })
                                             .Returns(new List<StudentTestInfo>() { a, a, b, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, a, a, c })
                                             .Returns(new List<StudentTestInfo>() { a, a, a, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, a, b, c, d })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, c, d });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, a, d, d, e })
                                             .Returns(new List<StudentTestInfo>() { a, c, d, d, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, a, d, d, d })
                                             .Returns(new List<StudentTestInfo>() { a, c, d, d, d });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, a, d, e, f })
                                             .Returns(new List<StudentTestInfo>() { a, c, d, e, f });

                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, a, b, c, c })
                                             .Returns(new List<StudentTestInfo>() { a, a, b, b, c, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, a, b, c, a })
                                             .Returns(new List<StudentTestInfo>() { a, a, a, b, b, c });
                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c, c, d, b })
                                             .Returns(new List<StudentTestInfo>() { a, b, b, c, c, d });
                yield return new TestCaseData(new List<StudentTestInfo>() { c, a, b, c, d, c })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, c, c, d });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, d, e })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, d, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, e, f })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, e, f });

                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, a, c, e, e })
                                             .Returns(new List<StudentTestInfo>() { a, a, b, c, d, e, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, b, c, e, e })
                                             .Returns(new List<StudentTestInfo>() { a, b, b, c, d, e, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, c, e, e })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, c, d, e, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, d, e, e })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, d, e, e });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, f, e, e })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, e, e, f });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, f, e, f })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, e, f, f });
                yield return new TestCaseData(new List<StudentTestInfo>() { d, b, a, c, f, e, g })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, d, e, f, g });
            }
        }

        [TestCaseSource(nameof(GetTestAddStudentsTestCases))]
        public override List<StudentTestInfo> TestAdd(List<StudentTestInfo> values, StudentTestInfo value)
        {
            return base.TestAdd(values, value);
        }

        public static IEnumerable GetTestAddStudentsTestCases
        {
            get
            {
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo d = new StudentTestInfo("Игорь", "Игорев", "Test", DateTime.Now, 4);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);
                StudentTestInfo h = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 8);

                yield return new TestCaseData(new List<StudentTestInfo>(), e)
                                             .Returns(new List<StudentTestInfo>() { e });

                yield return new TestCaseData(new List<StudentTestInfo>() { e }, g)
                                             .Returns(new List<StudentTestInfo>() { e, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { e, g }, c)
                                             .Returns(new List<StudentTestInfo>() { c, e, g });

                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c }, b)
                                             .Returns(new List<StudentTestInfo>() { b, c, e, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c, b }, d)
                                             .Returns(new List<StudentTestInfo>() { b, c, d, e, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c, b, d }, f)
                                             .Returns(new List<StudentTestInfo>() { b, c, d, e, f, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c, b, d, f }, h)
                                             .Returns(new List<StudentTestInfo>() { b, c, d, e, f, g, h });
            }
        }

        [TestCaseSource(nameof(GetTestGetMaxStudentsTestCases))]
        public override StudentTestInfo TestGetMax(List<StudentTestInfo> values)
        {
            return base.TestGetMax(values);
        }

        public static IEnumerable GetTestGetMaxStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo d = new StudentTestInfo("Игорь", "Игорев", "Test", DateTime.Now, 4);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);

                yield return new TestCaseData(new List<StudentTestInfo>() { b }).Returns(b);

                yield return new TestCaseData(new List<StudentTestInfo>() { b, c, d }).Returns(d);
                yield return new TestCaseData(new List<StudentTestInfo>() { d, c, b }).Returns(d);

                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, b, c, d }).Returns(d);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, b, c, d, e }).Returns(e);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, b, c, d, e, f }).Returns(f);
            }
        }

        [TestCaseSource(nameof(GetTestGetMinStudentsTestCases))]
        public override StudentTestInfo TestGetMin(List<StudentTestInfo> values)
        {
            return base.TestGetMin(values);
        }

        public static IEnumerable GetTestGetMinStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo d = new StudentTestInfo("Игорь", "Игорев", "Test", DateTime.Now, 4);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);
                StudentTestInfo h = new StudentTestInfo("Давид", "Давидов", "Test", DateTime.Now, 8);

                yield return new TestCaseData(new List<StudentTestInfo>() { b }).Returns(b);

                yield return new TestCaseData(new List<StudentTestInfo>() { d, c, b }).Returns(b);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, c, d }).Returns(b);

                yield return new TestCaseData(new List<StudentTestInfo>() { b, a, b, c, d }).Returns(a);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, b, b, c, d, e }).Returns(b);
                yield return new TestCaseData(new List<StudentTestInfo>() { h, d, g, c, d, e, f }).Returns(c);
            }
        }

        [TestCaseSource(nameof(GetTestGetEnumeratorStudentsTestCases))]
        public override List<StudentTestInfo> TestGetEnumerator(List<StudentTestInfo> values)
        {
            return base.TestGetEnumerator(values);
        }

        public static IEnumerable GetTestGetEnumeratorStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);

                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c, b, a, f })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, e, f, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { g, e, f, c, b, a })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, e, f, g });
                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c, e, f, g })
                                             .Returns(new List<StudentTestInfo>() { a, b, c, e, f, g });
            }
        }

        [TestCaseSource(nameof(GetTestGetRevesedEnumeratorStudentsTestCases))]
        public override List<StudentTestInfo> TestGetReversedEnumerator(List<StudentTestInfo> values)
        {
            return base.TestGetReversedEnumerator(values);
        }
        public static IEnumerable GetTestGetRevesedEnumeratorStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);

                yield return new TestCaseData(new List<StudentTestInfo>() { e, g, c, b, a, f })
                                            .Returns(new List<StudentTestInfo>() { g, f, e, c, b, a });
                yield return new TestCaseData(new List<StudentTestInfo>() { g, f, e, c, b, a })
                                             .Returns(new List<StudentTestInfo>() { g, f, e, c, b, a });
                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c, e, f, g })
                                             .Returns(new List<StudentTestInfo>() { g, f, e, c, b, a });
            }
        }

        [TestCaseSource(nameof(GetTestFindStudentsTestCases))]
        public override Node<StudentTestInfo> TestFind(List<StudentTestInfo> values, StudentTestInfo value)
        {
            return base.TestFind(values, value);
        }

        public static IEnumerable GetTestFindStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
                StudentTestInfo e = new StudentTestInfo("Александр", "Александров", "Test", DateTime.Now, 5);
                StudentTestInfo f = new StudentTestInfo("Дмитрий", "Дмитриев", "Test", DateTime.Now, 6);
                StudentTestInfo g = new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 7);

                List<StudentTestInfo> values = new List<StudentTestInfo> { e, f, g, c, b, a };
                yield return new TestCaseData(values, e).Returns(new Node<StudentTestInfo>(e));

                yield return new TestCaseData(values, a).Returns(new Node<StudentTestInfo>(a));
                yield return new TestCaseData(values, f).Returns(new Node<StudentTestInfo>(f));
                yield return new TestCaseData(values, b).Returns(new Node<StudentTestInfo>(b));

                yield return new TestCaseData(values, new StudentTestInfo("Павел", "Павлов", "Test", DateTime.Now, 8))
                                             .Returns(null);
            }
        }

        [TestCaseSource(nameof(GetTestRemoveStudentsTestCases))]
        public override bool TestRemove(List<StudentTestInfo> values, StudentTestInfo value)
        {
            return base.TestRemove(values, value);
        }

        public static IEnumerable GetTestRemoveStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
                StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);

                yield return new TestCaseData(new List<StudentTestInfo>(), a)
                                             .Returns(false);
                yield return new TestCaseData(new List<StudentTestInfo>() { a }, null)
                                             .Returns(false);

                yield return new TestCaseData(new List<StudentTestInfo>() { a }, a)
                                             .Returns(true);

                yield return new TestCaseData(new List<StudentTestInfo>() { a, b }, b)
                                             .Returns(true);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, a }, a)
                                             .Returns(true);

                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c }, b)
                                             .Returns(true);
                yield return new TestCaseData(new List<StudentTestInfo>() { c, b, a }, b)
                                             .Returns(true);
                yield return new TestCaseData(new List<StudentTestInfo>() { b, c, a }, b)
                                             .Returns(true);
            }
        }
    }
}
