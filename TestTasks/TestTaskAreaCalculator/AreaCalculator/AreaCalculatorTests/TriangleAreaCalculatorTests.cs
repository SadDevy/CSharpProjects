using System;
using System.Collections;
using AreaCalculator;
using NUnit.Framework;

namespace AreaCalculatorTests
{
    [TestFixture]
    public class TriangleAreaCalculatorTests
    {
        [Test]
        public void TestCreate_AOneBTwoCTwo_Success()
        {
            const double a = 1;
            const double b = 2;
            const double c = 2;

            TriangleAreaCalculator areaCalculator = TriangleAreaCalculator.Create(a, b, c);

            Assert.IsNotNull(areaCalculator);
        }

        [Test]
        public void TestCreate_AOneBThreeCOne_Exception()
        {
            const double a = 1;
            const double b = 3;
            const double c = 1;

            Assert.Throws<InvalidOperationException>(() => TriangleAreaCalculator.Create(a, b, c));
        }

        [Test]
        public void TestCalculateArea()
        {
            const double a = 3;
            const double b = 4;
            const double c = 5;
            const double expected = 6;

            TriangleAreaCalculator areaCalculator = TriangleAreaCalculator.Create(a, b, c);
            double actual = areaCalculator.CalculateArea();

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(TestIsSquareTestCases))]
        public bool TestIsSquare(double a, double b, double c)
        {
            TriangleAreaCalculator areaCalculator = TriangleAreaCalculator.Create(a, b, c);
            return areaCalculator.IsSquare();
        }

        private static IEnumerable TestIsSquareTestCases
        {
            get
            {
                yield return new TestCaseData(3, 4, 5).Returns(true);
                yield return new TestCaseData(1, 2, 2).Returns(false);
            }
        }
    }
}
