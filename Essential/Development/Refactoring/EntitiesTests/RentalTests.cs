using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Entities;
using NUnit.Framework;

namespace EntitiesTests
{
    [TestFixture]
    public class RentalTests
    {
        [Test]
        public void TestConstructor()
        {
            Movie movie = new Movie(string.Empty);
            Rental rental = new Rental(movie);

            Assert.IsNotNull(rental);
        }

        [Test]
        public void TestMovie()
        {
            Movie expected = new Movie(string.Empty);
            Rental rental = new Rental(expected);

            Movie actual = rental.Movie;

            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PriceCode, actual.PriceCode);
        }

        [Test]
        public void TestDaysRented()
        {
            const int expected = 1;
            Movie movie = new Movie(string.Empty);
            Rental rental = new Rental(movie, expected);

            int actual = rental.DaysRented;

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(GetTestGetChrgeTestCases))]
        public double TestGetCharge(int priceCode, int daysRented)
        {
            Movie movie = new Movie(string.Empty, priceCode);
            Rental rental = new Rental(movie, daysRented);

            return rental.GetCharge();
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
            Movie movie = new Movie(string.Empty, priceCode);
            Rental rental = new Rental(movie, daysRented);

            return rental.GetFrequentRenterPoints();
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
