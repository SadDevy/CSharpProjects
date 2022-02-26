using System;
using System.Collections;
using System.Collections.Generic;
using BinarySearch;
using NUnit.Framework;

namespace BinaryTreeTests
{
    [TestFixture]
    public class StudentsComparerTests
    {
        [Test]
        public void TestConstructorWithOrder()
        {
            GeneralComparer<StudentTestInfo> a = new GeneralComparer<StudentTestInfo>(true);

            Assert.IsNotNull(a);
        }

        [Test]
        public void TestConstructorWithComparer()
        {
            GeneralComparer<StudentTestInfo> a = new GeneralComparer<StudentTestInfo>(Comparer<StudentTestInfo>.Default, true);

            Assert.IsNotNull(a);
        }

        [Test]
        public void TestConstructorWithComparisonDelegate()
        {
            static int A(StudentTestInfo x, StudentTestInfo y) { return default; }

            GeneralComparer<StudentTestInfo> a = new GeneralComparer<StudentTestInfo>((Comparison<StudentTestInfo>)A, true);

            Assert.IsNotNull(a);
        }

        [Test]
        public void TestConstructorWithFuncDelegate()
        {
            static int A(StudentTestInfo x, StudentTestInfo y) { return default; }

            GeneralComparer<StudentTestInfo> a = new GeneralComparer<StudentTestInfo>((Func<StudentTestInfo, StudentTestInfo, int>)A, true);

            Assert.IsNotNull(a);
        }

        [TestCaseSource(nameof(GetTestCompareTestCases))]
        public int TestCompare(StudentTestInfo a, StudentTestInfo b)
        {
            GeneralComparer<StudentTestInfo> comparer = new GeneralComparer<StudentTestInfo>(true);
            return comparer.Compare(a, b);
        }

        private static IEnumerable GetTestCompareTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
                StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);

                yield return new TestCaseData(null, a).Returns(-1);
                yield return new TestCaseData(a, b).Returns(-1);

                yield return new TestCaseData(a, null).Returns(1);

                yield return new TestCaseData(null, null).Returns(0);
            }
        }
    }
}
