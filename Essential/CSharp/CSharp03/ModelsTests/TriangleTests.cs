using NUnit.Framework;
using Models;
using System;

namespace ModelsTests
{
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void TestCouldExist_AZero_Failure()
        {
            const double a = 0;
            const double b = 3;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_ANegative_Failure()
        {
            const double a = -3;
            const double b = 4;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_BigA_Failure()
        {
            const double a = 300;
            const double b = 4;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_BZero_Failure()
        {
            const double a = 4;
            const double b = 0;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_BNegative_Failure()
        {
            const double a = 3;
            const double b = -4;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_BigB_Failure()
        {
            const double a = 3;
            const double b = 400;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_CZero_Failure()
        {
            const double a = 4;
            const double b = 5;
            const double c = 0;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_CNegative_Failure()
        {
            const double a = 3;
            const double b = 4;
            const double c = -5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_BigC_Failure()
        {
            const double a = 3;
            const double b = 4;
            const double c = 500;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestCouldExist_EquilateralTriangleSides_Success()
        {
            const double a = 1;
            const double b = 1;
            const double c = 1;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestCouldExit_RectangularTriangleSides_Success()
        {
            const double a = 3;
            const double b = 4;
            const double c = 5;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestCouldExist_IsoscelesTriangleSides_Success()
        {
            const double a = 2;
            const double b = 2;
            const double c = 3;

            bool actual = Triangle.CouldExist(a, b, c);

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestCreateTriangle_EquilateralTriangleSides_Success()
        {
            Triangle triangle = Triangle.CreateTriangle(1, 1, 1);

            Assert.IsNotNull(triangle);
        }

        [Test]
        public void TestCreateTriangle_TriangleNotExists_Exception()
        {
            const double a = 1;
            const double b = 2;
            const double c = 300;
            string expected = string.Format("Треугольник не может существовать со сторонами: {0}, {1}, {2}.", a, b, c);

            var actual = Assert.Throws<InvalidOperationException>(() => Triangle.CreateTriangle(a, b, c));
            
            Assert.That(actual.Message, Is.EqualTo(expected));
        }

        [Test]
        public void TestCreateTriangle_TriangleHasThreeSides_Success()
        {
            const double a = 3;
            const double b = 4;
            const double c = 5;

            Triangle triangle = Triangle.CreateTriangle(a, b, c);

            Assert.AreEqual(a, triangle.A);
            Assert.AreEqual(b, triangle.B);
            Assert.AreEqual(c, triangle.C);
        }

        [Test]
        public void TestCalculatePerimeter_RectangularTriangleSides_Success()
        {
            Triangle triangle = Triangle.CreateTriangle(3, 4, 5);
            const double expected = 12;

            double actual = triangle.CalculatePerimeter();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateArea_RectangularTriangleSides_Success()
        {
            Triangle triangle = Triangle.CreateTriangle(3, 4, 5);
            const double expected = 6;

            double actual = triangle.CalculateArea();

            Assert.AreEqual(expected, actual);
        }
    }
}
