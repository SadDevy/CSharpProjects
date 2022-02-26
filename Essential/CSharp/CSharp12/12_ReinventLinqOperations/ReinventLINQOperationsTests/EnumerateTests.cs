using System;
using System.Collections.Generic;
using NUnit.Framework;
using LINQOperations;
using LinqEnumerable = System.Linq.Enumerable;

namespace ReinventLINQOperationsTests
{
    public class EnumerateTests
    {
        [TestCaseSource(nameof(GetTestRepeatCases))]
        public IEnumerable<TResult> TestRepeat<TResult>(TResult element, int count)
        {
            return Enumerate.Repeat(element, count);
        }

        public static IEnumerable<TestCaseData> GetTestRepeatCases()
        {
            yield return new TestCaseData('X', 0).Returns(new char[] { });
            yield return new TestCaseData(1, 1).Returns(new int[] { 1 });
            yield return new TestCaseData(2, 2).Returns(new int[] { 2, 2 });
            yield return new TestCaseData(3, 3).Returns(new int[] { 3, 3, 3 });
            yield return new TestCaseData("Abc", 5).Returns(new string[] { "Abc", "Abc", "Abc", "Abc", "Abc" });
        }

        [Test]
        public void TestRepeat_NegativeCount_Exception()
        {
            void RepeatNegativeCount()
            {
                var result = Enumerate.Repeat('X', -1);
                LinqEnumerable.ToList(result);
            }

            Assert.Throws<ArgumentOutOfRangeException>(RepeatNegativeCount);
        }


        [TestCaseSource(nameof(GetTestConcatCases))]
        public IEnumerable<TSource> TestConcat<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return Enumerate.Concat(first, second);
        }

        public static IEnumerable<TestCaseData> GetTestConcatCases()
        {
            yield return new TestCaseData(new int[] { }, new int[] { }).Returns(new int[] { });
            yield return new TestCaseData(new int[] { 1 }, new int[] { }).Returns(new int[] { 1 });
            yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { }).Returns(new int[] { 1, 2, 3 });
            yield return new TestCaseData(new int[] { }, new int[] { 10 }).Returns(new int[] { 10 });
            yield return new TestCaseData(new int[] { }, new int[] { 10, 20, 30 }).Returns(new int[] { 10, 20, 30 });
            yield return new TestCaseData(new int[] { 1 }, new int[] { 10 }).Returns(new int[] { 1, 10 });
            yield return new TestCaseData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 10, 20, 30 }).Returns(new int[] { 1, 2, 3, 4, 5, 10, 20, 30 });
            yield return new TestCaseData(new char[] { 'A', 'B' }, new char[] { 'x', 'y', 'z' }).Returns(new char[] { 'A', 'B', 'x', 'y', 'z' });
        }

        [Test]
        public void TestConcat_FirstNull_Exception()
        {
            void ConcatFirstNull()
            {
                var result = Enumerate.Concat(null, new int[] { });
                LinqEnumerable.ToList(result);
            }

            Assert.Throws<ArgumentNullException>(ConcatFirstNull);
        }

        [Test]
        public void TestConcat_SecondNull_Exception()
        {
            void ConcatSecondNull()
            {
                var result = Enumerate.Concat(new int[] { }, null);
                LinqEnumerable.ToList(result);
            }

            Assert.Throws<ArgumentNullException>(ConcatSecondNull);
        }


        [TestCaseSource(nameof(GetTestJoinCases))]
        public IEnumerable<Tuple<string, DateTime>> TestJoin(
            StudentTestResult[] students, Tuple<string, string>[] teachers,
            Func<StudentTestResult, string> studentTestSubjectSelector,
            Func<Tuple<string, string>, string> teacherTestSubjectSelector,
            Func<StudentTestResult, Tuple<string, string>, Tuple<string, DateTime>> teacherTestDatesSelector)
        {
            // Вычисляет список объектов преподаватель, дата, в которую он принимал тест у студента.
            return students.Join(teachers,
                studentTestSubjectSelector,
                teacherTestSubjectSelector,
                teacherTestDatesSelector);
        }

        public static IEnumerable<TestCaseData> GetTestJoinCases()
        {
            StudentTestResult[] studentTestResults = new[]
            {
                new StudentTestResult("James", "Smith", "SQL-100",  4, new DateTime(2020, 04, 22)),
                new StudentTestResult("Anthony", "Brown",  "SQL-200", 3, new DateTime(2017, 12, 01)),
                new StudentTestResult("John",   "Lee",  "SQL-200",  2, new DateTime(2017, 12, 02)),
                new StudentTestResult("Robert", "Johnson", ".NET-100", 5, new DateTime(2020, 01, 11)),
                new StudentTestResult("Michael", "King",   "HTML-150", 4, new DateTime(2019, 07, 21))
            };

            Tuple<string, string>[] teachers = new[]
            {
                Tuple.Create("Victor", "SQL-200"),
                Tuple.Create("Oleg", ".NET-100"),
                Tuple.Create("Sergey", "HTML-150")
            };

            Tuple<string, DateTime>[] expectedResults = new[]
            {
                Tuple.Create("Victor", new DateTime(2017, 12, 01)),
                Tuple.Create("Victor", new DateTime(2017, 12, 02)),
                Tuple.Create("Oleg", new DateTime(2020, 01, 11)),
                Tuple.Create("Sergey", new DateTime(2019, 07, 21))
            };

            Func<StudentTestResult, string> studentTestSubjectSelector =
                new Func<StudentTestResult, string>(test => test.TestSubject);
            Func<Tuple<string, string>, string> teacherTestSubjectSelector =
                new Func<Tuple<string, string>, string>(teacher => teacher.Item2);
            Func<StudentTestResult, Tuple<string, string>, Tuple<string, DateTime>> teacherTestDatesSelector =
                new Func<StudentTestResult, Tuple<string, string>, Tuple<string, DateTime>>((test, teacher) => new Tuple<string, DateTime>(teacher.Item1, test.Date));

            yield return new TestCaseData(
                studentTestResults,
                new Tuple<string, string>[] { },
                studentTestSubjectSelector,
                teacherTestSubjectSelector,
                teacherTestDatesSelector).Returns(new Tuple<string, DateTime>[] { });

            yield return new TestCaseData(
                new StudentTestResult[] { },
                teachers,
                studentTestSubjectSelector,
                teacherTestSubjectSelector,
                teacherTestDatesSelector).Returns(new Tuple<string, DateTime>[] { });

            yield return new TestCaseData(
                studentTestResults,
                teachers,
                studentTestSubjectSelector,
                teacherTestSubjectSelector,
                teacherTestDatesSelector).Returns(expectedResults);
        }








        // === old tests ==========================================================================

        private static readonly object[] studentTestResults =
        {
                    new object[]
                    {
                        new StudentTestResult[]
                        {
                            new StudentTestResult("James", "Smith", "SQL-100",  4, new DateTime(2020, 04, 22)),
                            new StudentTestResult("John",   "Lee",  "SQL-200",  2, new DateTime(2017, 12, 01)),
                            new StudentTestResult("Robert", "Johnson", ".NET-100", 5, new DateTime(2020, 01, 11)),
                            new StudentTestResult("Michael", "King",   "HTML-100", 4, new DateTime(2019, 07, 21))
                        }
                    }
                };

        private static readonly object[] lastNames =
        {
                    new object[]
                    {
                        new string[]
                        {
                            "Ramirez",
                            "Griffin",
                            "Murphy",
                            "Evans"
                        }
                    }
                };


        [TestCaseSource(nameof(studentTestResults))]
        public void Test_Prepend(IEnumerable<StudentTestResult> results)
        {
            StudentTestResult[] expectedResults =
            {
                        new StudentTestResult("Finley",   "Brooks",  "SQL-200",  3, new DateTime(2018, 01, 21)),
                        new StudentTestResult("James", "Smith", "SQL-100",  4, new DateTime(2020, 04, 22)),
                        new StudentTestResult("John",   "Lee",  "SQL-200",  2, new DateTime(2017, 12, 01)),
                        new StudentTestResult("Robert", "Johnson", ".NET-100", 5, new DateTime(2020, 01, 11)),
                        new StudentTestResult("Michael", "King",   "HTML-100", 4, new DateTime(2019, 07, 21))
                    };

            IEnumerable<StudentTestResult> actualResults = results.Prepend(new StudentTestResult("Finley", "Brooks", "SQL-200", 3, new DateTime(2018, 01, 21))).ToList();

            CollectionAssert.AreEqual(expectedResults, actualResults);
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void Test_Take(IEnumerable<StudentTestResult> results)
        {
            const int expectedAmount = 3;

            IEnumerable<StudentTestResult> actualResults = results.Take(expectedAmount);

            Assert.AreEqual(expectedAmount, actualResults.Count());
        }

        [TestCase(new int[] { 10, 20, 30, 1, 2, 3 })]
        public void Test_SkipWhile(int[] numbers)
        {
            int[] expectedNumbers = { 1, 2, 3 };

            IEnumerable<int> actualNumbers = numbers.SkipWhile((n, index) => n > 9);

            CollectionAssert.AreEqual(expectedNumbers, actualNumbers);
        }

        [Test]
        public void Test_OfType()
        {
            char[] expectedElements = { 'a', 'c' };

            IEnumerable<char> actualElements = new object[] { 'a', 1, 'c' }.OfType<char>();

            CollectionAssert.AreEqual(expectedElements, actualElements);
        }

        [Test]
        public void Test_Cast()
        {
            char[] expectedElements = { 'a', 'b', 'c' };

            IEnumerable<char> actualElements = new object[] { 'a', 'b', 'c' }.Cast<char>();

            CollectionAssert.AreEqual(expectedElements, actualElements);
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void Test_ToList(IEnumerable<StudentTestResult> results)
        {
            List<StudentTestResult> expectedResults = new List<StudentTestResult>()
                    {
                        new StudentTestResult("James", "Smith", "SQL-100", 4, new DateTime(2020, 04, 22)),
                        new StudentTestResult("John", "Lee", "SQL-200", 2, new DateTime(2017, 12, 01)),
                        new StudentTestResult("Robert", "Johnson", ".NET-100", 5, new DateTime(2020, 01, 11)),
                        new StudentTestResult("Michael", "King", "HTML-100", 4, new DateTime(2019, 07, 21))
                    };

            List<StudentTestResult> actualResults = results.ToList();

            CollectionAssert.AreEqual(expectedResults, actualResults);
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void Test_ToDictionary(IEnumerable<StudentTestResult> results)
        {
            Dictionary<string, int> expectedResults = new Dictionary<string, int>
                    {
                        {"Smith", 4 },
                        {"Lee", 2 },
                        {"Johnson", 5 },
                        {"King", 4 }
                    };

            Dictionary<string, int> actualResults = results.ToDictionary(result => result.LastName, result => result.TestScore, EqualityComparer<string>.Default);

            CollectionAssert.AreEqual(expectedResults, actualResults);
        }

        [TestCaseSource(nameof(lastNames))]
        public void TestWhere_LastNameLengthGreaterSix_SequenceEqual(string[] lastNames)
        {
            const int length = 6;
            string[] expectedLastNames = { "Ramirez", "Griffin" };

            IEnumerable<string> actualLastNames = lastNames.Where((lastName, index) => lastName.Length > length);

            CollectionAssert.AreEqual(expectedLastNames, actualLastNames);
        }

        [TestCase(new int[] { 0, 7, 14 }, 7, ExpectedResult = true)]
        [TestCase(new int[] { 0, 7, 14 }, 100, ExpectedResult = false)]
        public bool TestAny_NumberGreaterValue(int[] numbers, int value)
        {
            return numbers.Any(n => n > value);
        }

        [TestCase(new int[] { 0, 7, 14 }, 15, ExpectedResult = true)]
        [TestCase(new int[] { 0, 7, 14 }, 1, ExpectedResult = false)]
        public bool TestAll_NumberLessValue(int[] numbers, int value)
        {
            return numbers.All(n => n < value);
        }

        [TestCaseSource(nameof(lastNames))]
        public void TestContains(string[] lastNames)
        {
            Assert.IsTrue(lastNames.Contains("Murphy"));
            Assert.IsFalse(lastNames.Contains("Ivanov"));
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void TestSelect(IEnumerable<StudentTestResult> results)
        {
            string[] expectedTestSubjects = { "SQL-100", "SQL-200", ".NET-100", "HTML-100" };

            IEnumerable<string> actualTestSubjects = results.Select(test => test.TestSubject);

            CollectionAssert.AreEqual(expectedTestSubjects, actualTestSubjects);
        }

        [Test]
        public void TestSelectMany_NumbersAndLetters_CombinedLettersAndNumbers()
        {
            string[] expectedSequence = new string[] { "A1", "B1", "A2", "B2", "A3", "B3", };

            int[] numbers = { 1, 2, 3 };
            string[] letters = { "A", "B" };

            IEnumerable<string> actualSequence = numbers.SelectMany(number => letters, (number, letter) => letter + number);

            CollectionAssert.AreEqual(expectedSequence, actualSequence);
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void TestCount(IEnumerable<StudentTestResult> results)
        {
            const int expectedCount = 4;

            int actualCount = results.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void TestAggreate()
        {
            string expectedString = "a,b,c,d";

            string actualString = new[] { "a", "b", "c", "d" }.Aggregate((x, y) => x + "," + y);

            Assert.AreEqual(expectedString, actualString);
        }

        [TestCase(new char[] { 'a', 'b', 'a', 'c' }, ExpectedResult = new char[] { 'a', 'b', 'c' })]
        public IEnumerable<char> TestDistinct(char[] input)
        {
            return input.Distinct();
        }

        [Test]
        public void TestIntersect()
        {
            string[] expectedSequence = { "a" };

            string[] first = new string[] { "d", "a", "e" };
            string[] second = new string[] { "a", "b", "c" };
            IEnumerable<string> actualSequence = first.Intersect(second);

            CollectionAssert.AreEqual(expectedSequence, actualSequence);
        }

        [TestCaseSource(nameof(lastNames))]
        public void TestSequenceEqual(string[] lastNames)
        {
            string[] sequence = { "Ramirez", "Griffin", "Murphy", "Evans" };

            bool areEquals = lastNames.SequenceEqual(sequence);

            Assert.IsTrue(areEquals);
        }

        [Test]
        public void TestZip()
        {
            string[] expectedSequence = { "1a", "2b", "3c" };

            int[] first = new int[] { 1, 2, 3 };
            string[] second = new string[] { "a", "b", "c" };

            IEnumerable<string> actualSequence = first.Zip(second, (x, y) => x.ToString() + y);

            CollectionAssert.AreEqual(expectedSequence, actualSequence);
        }

        [TestCaseSource(nameof(studentTestResults))]
        public void TestGroupBy(IEnumerable<StudentTestResult> results)
        {
            var expectedResults = new[]
            {
                        new {Key = 4, Count = 2},
                        new {Key = 2, Count = 1},
                        new {Key = 5, Count = 1}
                    };

            var actualResults = results.GroupBy(test => test.TestScore, test => test.LastName, (x, y) => new { Key = x, Count = y.Count() });

            CollectionAssert.AreEqual(expectedResults, actualResults);
        }
    }
}