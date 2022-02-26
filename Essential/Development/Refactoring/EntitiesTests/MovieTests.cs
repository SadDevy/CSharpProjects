using System;
using System.Collections;
using Entities;
using NUnit.Framework;

namespace EntitiesTests
{
    [TestFixture]
    public class MovieTests
    {
        private static string title;
        [SetUp]
        public void Init() => title = "title";

        [Test]
        public void TestConstructor()
        {
            Movie movie = new Movie(title);
            Assert.IsNotNull(movie);
        }

        [Test]
        public void TestTitle()
        {
            const int priceCode = 1;
            Movie movie = new Movie(title, priceCode);

            string actual = movie.Title;

            Assert.AreEqual(title, actual);
        }

        [TestCaseSource(nameof(GetTestPriceCodeTestCases))]
        public int TestPriceCode(int expected)
        {
            Movie movie = new Movie(title);

            movie.PriceCode = expected;

            return movie.PriceCode;
        }

        private static IEnumerable GetTestPriceCodeTestCases
        {
            get
            {
                yield return new TestCaseData(2).Returns(2);
                yield return new TestCaseData(1).Returns(1);
                yield return new TestCaseData(0).Returns(0);
            }
        }

        [Test]
        public void TestPriceCodeException()
        {
            static void A()
            {
                const int priceCode = 5;
                Movie movie = new Movie(title, priceCode);

                movie.PriceCode = priceCode;
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        [TestCaseSource(nameof(GetTestGetChrgeTestCases))]
        public double TestGetCharge(int priceCode, int daysRented)
        {
            Movie movie = new Movie(title, priceCode);

            return movie.GetCharge(daysRented);
        }

        private static IEnumerable GetTestGetChrgeTestCases
        {
            get
            {
                yield return new TestCaseData(0, 3).Returns(3.5);
                yield return new TestCaseData(0, 1).Returns(2);

                yield return new TestCaseData(1, 3).Returns(9);

                yield return new TestCaseData(2, 4).Returns(3);
                yield return new TestCaseData(2, 1).Returns(1.5);
            }
        }

        [TestCaseSource(nameof(GetTestGetFrequentRenterPointsTestCases))]
        public int TestGetFrequentRenterPoints(int priceCode, int daysRented)
        {
            Movie movie = new Movie(title, priceCode);

            return movie.GetFrequentRenterPoints(daysRented);
        }

        private static IEnumerable GetTestGetFrequentRenterPointsTestCases
        {
            get
            {
                yield return new TestCaseData(2, 1).Returns(1);

                yield return new TestCaseData(0, 1).Returns(1);

                yield return new TestCaseData(1, 3).Returns(2);
                yield return new TestCaseData(1, 1).Returns(1);
            }
        }
    }
}
