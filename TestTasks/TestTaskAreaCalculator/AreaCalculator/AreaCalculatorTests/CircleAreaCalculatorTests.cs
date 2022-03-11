using System;
using AreaCalculator;
using NUnit.Framework;

namespace AreaCalculatorTests
{
    [TestFixture]
    public class CircleAreaCalculatorTests
    {
        [Test]
        public void TestCreate_RadiusGreaterThanZero_Success()
        {
            const double radius = 3;

            CircleAreaCalculator areaCalculator = CircleAreaCalculator.Create(radius);

            Assert.IsNotNull(areaCalculator);
        }

        [Test]
        public void TestCreate_RadiusLessThanZero_Exception()
        {
            const double radius = -3;

            Assert.Throws<InvalidOperationException>(() => CircleAreaCalculator.Create(radius));
        }

        [Test]
        public void TestCalculateArea()
        {
            const double radius = 3;
            double expected = Math.PI * 9;

            CircleAreaCalculator areaCalculator = CircleAreaCalculator.Create(radius);
            double actual = areaCalculator.CalculateArea();

            Assert.AreEqual(expected, actual);
        }
    }
}
