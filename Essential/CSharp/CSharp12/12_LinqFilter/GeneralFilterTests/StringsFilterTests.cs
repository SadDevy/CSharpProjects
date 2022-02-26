using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralFilter;
using NUnit.Framework;

namespace GeneralFilterTests
{
    [TestFixture]
    public class StringsFilterTests
    {
        [Test]
        public void TestEqual_Strings_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл"  };
            const string value = "Сергей";

            string[] expected = { "Сергей" };

            filter.AndEqual(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThan_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = "Петр";

            string[] expected = { "Сергей", "Семен" };

            filter.AndGreaterThan(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThan_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = "Сергей";

            string[] expected = { "Семен", "Петр", "Карл" };

            filter.AndLessThan(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGreaterThanOrEqual_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = "Петр";

            string[] expected = { "Сергей", "Семен", "Петр" };

            filter.AndGreaterThanOrEqual(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLessThanOrEqual_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = "Сергей";

            string[] expected = { "Сергей", "Семен", "Петр", "Карл" };

            filter.AndLessThanOrEqual(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBetween_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string least = "Петр";
            const string greatest = "Сергей";

            string[] expected = { "Семен" };

            filter.AndBetween(least, greatest);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestContains_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = "ерг";

            string[] expected = { "Сергей" };

            filter.AndContains(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMatchesPattern_String_Success()
        {
            Filter<string> filter = new Filter<string>();
            string[] source = { "Сергей", "Семен", "Петр", "Карл" };
            const string value = @"^\w*(мен)$";

            string[] expected = { "Семен" };

            filter.AndMatchesPattern(value);
            string[] actual = filter.ApplyFilter(source).ToArray();

            Assert.AreEqual(expected, actual);
        }
    }
}
