using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BinarySearch;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Data;

namespace LinqExpressionsTests
{
    [TestFixture]
    public class IterativeTreeTests
    {
        [Test]
        public void TestFirstDate()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 3),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                });

            DateTime expected = new DateTime(2020, 1, 11);

            DateTime actual = a.Min(n => n.PassingDate);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestPassingCount()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2019, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2018, 1, 11), 3),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2015, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                });

            int expected = 4;

            int actual = a.Count(n => n.PassingDate.Year == DateTime.Now.Year);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestThreeMaxScores()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 3),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                });

            IEnumerable<int> expected = new int[] { 4, 5, 5 };

            IEnumerable<int> actual = a.Select(n => n.Score)
                                       .OrderBy(n => n)
                                       .Skip(a.Count() - 3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStudentFullName()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 3),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                });

            IEnumerable<string> expected = new string[]
            {
                "Алексей Алексеев",
                "Андрей Андреев",
                "Игорь Игорев",
                "Карл Карлов",
                "Павел Павлов",
                "Петр Петров",
                "Семен Семенов"
            };

            IEnumerable<string> actual = a.Select(n => n.Name + " " + n.Surname)
                                          .OrderBy(n => n)
                                          .Distinct();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGoodStudents()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 3),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            IEnumerable<string> expected = new string[]
            {
                "Андрей Андреев",
                "Карл Карлов",
                "Петр Петров"
            };

            IEnumerable<string> actual = a.Where(n => n.Score == 4 || n.Score == 5)
                                          .Select(n => n.Name + " " + n.Surname)
                                          .OrderBy(n => n)
                                          .Distinct();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWrittenInTwoTests()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            IEnumerable<string> expected = new string[]
            {
                "CSS-300",
                "HTML-200",
                "SQL-200",
                "SQL-300"
            };

            IEnumerable<string> actual = a.Where(n => n.Score == 2)
                                          .Select(n => n.TestName)
                                          .OrderBy(n => n)
                                          .Distinct();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAverageScoreForEveryStudent()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 3),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            var expected = new[]
            {
                new
                {
                    Student = "Алексей Алексеев",
                    AverageScore = 2.0
                },
                new
                {
                    Student = "Андрей Андреев",
                    AverageScore = 4.0
                },
                new
                {
                    Student = "Игорь Игорев",
                    AverageScore = 2.5
                },
                new
                {
                    Student = "Карл Карлов",
                    AverageScore = 3.0
                },
                new
                {
                    Student = "Павел Павлов",
                    AverageScore = 1.5
                },new
                {
                    Student = "Петр Петров",
                    AverageScore = 4.0
                },
                new
                {
                    Student = "Семен Семенов",
                    AverageScore = 2.0
                }
            };

            var actual = a.GroupBy(n => n.Name + " " + n.Surname,
                            (student, tests) => new
                            {
                                Student = student,
                                AverageScore = tests.Select(n => n.Score).Average()
                            })
                          .OrderBy(n => n.Student);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStudentsByMonthAndYear()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 3),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Андрей", "Андреев", "CSS-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            const int month = 1;
            const int year = 2020;

            IEnumerable<int> expected = new int[] { 1, 2, 3, 5 };

            IEnumerable<int> actual = a.Where(n => n.PassingDate.Month == month && n.PassingDate.Year == year)
                                       .Select(n => n.Score)
                                       .Distinct();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRegex()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL500", new DateTime(2020, 3, 12), 3),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL_200", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300fd", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Андрей", "Андреев", "-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            const string pattern = @"^\w+-\d+$";

            IEnumerable<string> expected = new string[]
            {
                "SQL_200",
                "SQL-300fd",
                "HTML",
                "SQL500",
                "-100"
            };

            IEnumerable<string> actual = a.Select(n => n.TestName)
                                          .Where(n => Regex.Matches(n, pattern).Count == 0);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStudentsTests()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-500", new DateTime(2020, 3, 12), 5),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 3),
                    new StudentTestInfo("Павел", "Павлов", "SQL-100", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Андрей", "Андреев", "HTML-100", new DateTime(2020, 5, 12), 4),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Алексей", "Алексеев", "CSS-300", new DateTime(2020, 3, 7), 2),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-300", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-200", new DateTime(2020, 1, 12), 2),
                });

            var expected = new[]
            {
                new
                {
                    Student = "Алексей Алексеев",
                    Tests = "CSS-300"
                },
                new
                {
                    Student = "Андрей Андреев",
                    Tests = "HTML-100"
                },
                new
                {
                    Student = "Игорь Игорев",
                    Tests = "HTML-200, HTML-300"
                },
                new
                {
                    Student = "Карл Карлов",
                    Tests = "HTML-100"
                },
                new
                {
                    Student = "Павел Павлов",
                    Tests = "SQL-100, SQL-200"
                },
                new
                {
                    Student = "Петр Петров",
                    Tests = "SQL-100, SQL-500"
                },
                new
                {
                    Student = "Семен Семенов",
                    Tests = "SQL-300"
                }
            };

            var actual = a.GroupBy(
                            n => n.Name + " " + n.Surname,
                            (student, studentInfo) => new
                            {
                                Student = student,
                                Tests = string.Join(", ", studentInfo.Select(n => n.TestName)
                                                                     .Distinct()) 
                            })
                          .OrderBy(n => n.Student)
                          .ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRewrittenStudentTests()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 2),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 4),
                    new StudentTestInfo("Петр", "Петров", "SQL-500", new DateTime(2020, 3, 12), 4),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2020, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-100", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-200", new DateTime(2020, 1, 16), 2),
                    new StudentTestInfo("Карл", "Карлов", "HTML-200", new DateTime(2020, 1, 16), 4),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-300", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-400", new DateTime(2020, 1, 12), 2),
                });

            var expected = new[]
            {
                new
                {
                    Student = "Карл Карлов",
                    Tests = "HTML-100, HTML-200"
                },
                new
                {
                    Student = "Петр Петров",
                    Tests = "SQL-100" 
                },
                new
                {
                    Student = "Семен Семенов",
                    Tests = "SQL-300"
                }
            };

            var actual = a.GroupBy(
                            n => n.Name + " " + n.Surname,
                            (student, studentInfo) => new
                            {
                                Student = student,
                                Tests = string.Join(", ", studentInfo.GroupBy(n => n.TestName)
                                                                     .Where(n => n.Count() > 1)
                                                                     .Select(n => n.Key))
                            })
                          .Where(n => n.Tests.Length > 0);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNotPastTestsThisYearTests()
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(
                new StudentTestInfo[]
                {
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2020, 3, 12), 2),
                    new StudentTestInfo("Петр", "Петров", "SQL-100", new DateTime(2019, 3, 12), 4),
                    new StudentTestInfo("Петр", "Петров", "SQL-500", new DateTime(2020, 3, 12), 4),
                    new StudentTestInfo("Павел", "Павлов", "SQL-200", new DateTime(2015, 2, 16), 1),
                    new StudentTestInfo("Павел", "Павлов", "SQL-100", new DateTime(2020, 2, 16), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2020, 1, 11), 2),
                    new StudentTestInfo("Семен", "Семенов", "SQL-300", new DateTime(2018, 1, 11), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 1),
                    new StudentTestInfo("Карл", "Карлов", "HTML-100", new DateTime(2020, 1, 16), 5),
                    new StudentTestInfo("Карл", "Карлов", "HTML-200", new DateTime(2020, 1, 16), 2),
                    new StudentTestInfo("Карл", "Карлов", "HTML-200", new DateTime(2020, 1, 16), 4),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-300", new DateTime(2020, 1, 12), 3),
                    new StudentTestInfo("Игорь", "Игорев", "HTML-400", new DateTime(2018, 1, 12), 2),
                });

            IEnumerable<string> expected = new string[]
            {
                "HTML-400",
                "SQL-100",
                "SQL-200",
                "SQL-300"
            };

            IEnumerable<string> actual = a.Where(n => n.PassingDate.Year != DateTime.Now.Year)
                                          .Select(n => n.TestName)
                                          .Distinct()
                                          .OrderBy(n => n);

            Assert.AreEqual(expected, actual);
        }
    }
}
