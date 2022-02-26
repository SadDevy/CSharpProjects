using System.Linq;
using Entities;
using Entities.Formatters;
using NUnit.Framework;

namespace EntitiesTests
{
    [TestFixture]
    public class CustomerTests
    {
        private static string name;
        [SetUp]
        public void Init() => name = "customer";

        [Test]
        public void TestConstructor()
        {
            Customer customer = new Customer(name, default);
            Assert.IsNotNull(customer);
        }

        [Test]
        public void TestName()
        {
            Customer customer = new Customer(name, default);
            Assert.AreEqual(name, customer.Name);
        }

        [Test]
        public void TestAddRental()
        {
            Rental expected = new Rental(new Movie(string.Empty));
            Customer customer = new Customer(name, default);

            customer.AddRental(expected);

            Rental actual = customer._rentals.First();

            Assert.NotZero(customer._rentals.Count);
            Assert.AreEqual(expected.Movie.Title, actual.Movie.Title);
            Assert.AreEqual(expected.Movie.PriceCode, actual.Movie.PriceCode);
            Assert.AreEqual(expected.DaysRented, actual.DaysRented);
        }

        [Test]
        public void TestGetStatement()
        {
            Customer customer = new Customer(name, new DefaultStatementFormatter());

            const string title = "title";
            const int priceCode = 1;
            Movie movie = new Movie(title, priceCode);

            const int daysRented = 3;
            Rental rental = new Rental(movie, daysRented);

            customer.AddRental(rental);

            const string expected = @"Rental Record for customer
title 9
Amount owed is 9
You earned 2 frequent renter points";

            string actual = customer.GetStatement();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGetHtmlStatement()
        {
            Customer customer = new Customer(name, new HtmlStatementFormatter());

            const string title = "title";
            const int priceCode = 1;
            Movie movie = new Movie(title, priceCode);

            const int daysRented = 3;
            Rental rental = new Rental(movie, daysRented);

            customer.AddRental(rental);

            const string expected = @"<H1>Rentals for <EM>customer</EM></H1><P>
title: 9<BR>
<P>You owe <EM>9</EM><P>
On this rental you earned <EM>2</EM> frequent renter points<P>";

            string actual = customer.GetStatement();

            Assert.AreEqual(expected, actual);
        }
    }
}
