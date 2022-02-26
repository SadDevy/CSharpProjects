using NUnit.Framework;

namespace NamingAndCommenting
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup() => calculator = new Calculator();

        [Test]
        public void TestCalculateDiscount_NoProductsBought_ZeroDiscount()
        {
            const double expected = 0;

            double discount = calculator.CalculateDiscount(0, 10);

            Assert.AreEqual(expected, discount);
        }

        [Test]
        public void TestCalculateDiscount_OneProductWithFivePriceBought_ZeroDiscount()
        {
            const double expected = 0;

            double discount = calculator.CalculateDiscount(1, 5d);

            Assert.AreEqual(expected, discount);
        }

        [Test]
        public void TestCalculateDiscount_SixProductsWithFivePriceBought_TenDiscount()
        {
            const double expected = 10d;

            double discount = calculator.CalculateDiscount(6, 5d);

            Assert.AreEqual(expected, discount);
        }

        [Test]
        public void TestCalculateDiscount_FiveProductsWithFivePriceBought_FiveDiscount()
        {
            const double expected = 5d;

            double discount = calculator.CalculateDiscount(5, 5d);

            Assert.AreEqual(expected, discount);
        }

        [Test]
        public void TestCalculateDiscount_TwelveProductsWithFivePriceBought_ThirtyDiscount()
        {
            const double expected = 30d;

            double discount = calculator.CalculateDiscount(12, 5d);

            Assert.AreEqual(expected, discount);
        }
    }
}
