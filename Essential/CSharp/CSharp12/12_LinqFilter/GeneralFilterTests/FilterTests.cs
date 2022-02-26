using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneralFilter;
using NUnit.Framework;

namespace GeneralFilterTests
{
    [TestFixture]
    public class FilterTests
    {
        [Test]
        public void TestFilter_NullCollection_Failure()
        {
            static void A()
            {
                Filter<StudentTestInfo> a = new Filter<StudentTestInfo>();

                _ = a.ApplyFilter(null);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestFiler_EmptyCollection_Success()
        {
            Filter<StudentTestInfo> a = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { };

            StudentTestInfo[] exprected = { };

            a.AndGreaterThan(nameof(StudentTestInfo.Score), 0);
            StudentTestInfo[] actual = a.ApplyFilter(source).ToArray();

            Assert.AreEqual(exprected, actual);
        }

        [Test]
        public void TestFilter_EmptyFilter_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] exprected = { a, b, c, d };

            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(exprected, actual);
        }

        [Test]
        public void TestEqual_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 1;

            StudentTestInfo[] expected = { a };

            filter.AndEqual(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThan_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 1;

            StudentTestInfo[] expected = { b, c, d };

            filter.AndGreaterThan(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThan_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 3;

            StudentTestInfo[] expected = { a, b };

            filter.AndLessThan(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThanOrEqual_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 1;

            StudentTestInfo[] expected = { a, b, c, d };

            filter.AndGreaterThanOrEqual(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThanOrEqual_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 3;

            StudentTestInfo[] expected = { a, b, c };

            filter.AndLessThanOrEqual(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBetween_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int least = 1;
            const int greatest = 3;

            StudentTestInfo[] expected = { b };

            filter.AndBetween(nameof(StudentTestInfo.Score), least, greatest);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTwoConditions_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const int value = 3;
            const int least = 1;
            const int greatest = 4;

            StudentTestInfo[] expected = { b };

            filter.AndBetween(nameof(StudentTestInfo.Score), least, greatest);
            filter.AndLessThan(nameof(StudentTestInfo.Score), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEqual_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Сергей";

            StudentTestInfo[] expected = { a };

            filter.AndEqual(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThan_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Петр";

            StudentTestInfo[] expected = { a, c };

            filter.AndGreaterThan(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThan_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Сергей";

            StudentTestInfo[] expected = { b, c, d };

            filter.AndLessThan(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThanOrEqual_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Петр";

            StudentTestInfo[] expected = { a, b, c };

            filter.AndGreaterThanOrEqual(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThanOrEqual_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Сергей";

            StudentTestInfo[] expected = { a, b, c, d };

            filter.AndLessThanOrEqual(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBetween_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string least = "Петр";
            const string greatest = "Сергей";

            StudentTestInfo[] expected = { c };

            filter.AndBetween(nameof(StudentTestInfo.Name), least, greatest);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestContains_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "ерг";

            StudentTestInfo[] expected = { a };

            filter.AndContains(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMatchesPattern_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = @"^\w*(мен)$";

            StudentTestInfo[] expected = { c };

            filter.AndMatchesPattern(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTwoConditions_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            const string value = "Семен";
            const string least = "Карл";
            const string greatest = "Сергей";

            StudentTestInfo[] expected = { b };

            filter.AndBetween(nameof(StudentTestInfo.Name), least, greatest);
            filter.AndLessThan(nameof(StudentTestInfo.Name), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEqual_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 09);

            StudentTestInfo[] expected = { a };

            filter.AndEqual(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThan_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 09);

            StudentTestInfo[] expected = { b, c, d };

            filter.AndGreaterThan(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThan_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 11);

            StudentTestInfo[] expected = { a, b };

            filter.AndLessThan(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThanOrEqual_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 09);

            StudentTestInfo[] expected = { a, b, c, d };

            filter.AndGreaterThanOrEqual(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThanOrEqual_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 11);

            StudentTestInfo[] expected = { a, b, c };

            filter.AndLessThanOrEqual(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBetween_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime least = new DateTime(2020, 10, 09);
            DateTime greatest = new DateTime(2020, 10, 11);

            StudentTestInfo[] expected = { b };

            filter.AndBetween(nameof(StudentTestInfo.PassingDate), least, greatest);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTwoConditions_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 3);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            DateTime value = new DateTime(2020, 10, 11);
            DateTime least = new DateTime(2020, 10, 09);
            DateTime greatest = new DateTime(2020, 10, 12);

            StudentTestInfo[] expected = { b };

            filter.AndBetween(nameof(StudentTestInfo.PassingDate), least, greatest);
            filter.AndLessThan(nameof(StudentTestInfo.PassingDate), value);
            StudentTestInfo[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByAsc_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 4);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 3);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 2);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 1);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { d, c, b, a };

            filter.SortByAsc<int>(nameof(StudentTestInfo.Score));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByDesc_IntegerProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };
            StudentTestInfo[] expected = { d, c, b, a };

            filter.SortByDesc<int>(nameof(StudentTestInfo.Score));
            IEnumerable<StudentTestInfo> actual = filter.ApplySort(source);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByAsc_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 4);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 3);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 2);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 1);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { d, b, c, a };

            filter.SortByAsc<string>(nameof(StudentTestInfo.Name));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByDesc_StringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { a, c, b, d };

            filter.SortByDesc<string>(nameof(StudentTestInfo.Name));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByAsc_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 1);
            StudentTestInfo b = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 2);
            StudentTestInfo c = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 3);
            StudentTestInfo d = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { d, c, b, a };

            filter.SortByAsc<DateTime>(nameof(StudentTestInfo.PassingDate));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByDesc_DateTimeProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 2);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 3);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 4);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { d, c, b, a };

            filter.SortByDesc<DateTime>(nameof(StudentTestInfo.PassingDate));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTestByAsc_IntegerThenStringProperty_Success()
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", new DateTime(2020, 10, 09), 4);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 10, 10), 3);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 10, 11), 1);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", new DateTime(2020, 10, 12), 1);

            Filter<StudentTestInfo> filter = new Filter<StudentTestInfo>();
            StudentTestInfo[] source = { a, b, c, d };

            StudentTestInfo[] expected = { d, c, b, a };

            filter.SortByAsc<int>(nameof(StudentTestInfo.Score));
            filter.SortByAsc<string>(nameof(StudentTestInfo.Name));
            StudentTestInfo[] actual = filter.ApplySort(source).ToArray();

            Assert.AreEqual(expected, actual);
        }
    }
}
