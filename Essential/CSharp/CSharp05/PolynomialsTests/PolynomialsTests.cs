using NUnit.Framework;
using Polynomials;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PolynomialsTests
{
    [TestFixture]
    public class PolynomialialTests
    {
        [TestCaseSource(nameof(GetTestDegreeTestCases))]
        public int TestDegree(params double[] values)
        {
            Polynomial a = new Polynomial(values);

            return a.Degree;
        }

        private static IEnumerable GetTestDegreeTestCases
        {
            get
            {
                yield return new TestCaseData(new double[1]).Returns(0);
                yield return new TestCaseData(new double[] { 1 }).Returns(0);
                yield return new TestCaseData(new double[] { 1, 0 }).Returns(0);
                yield return new TestCaseData(new double[] { 0, 0, 0 }).Returns(0);

                yield return new TestCaseData(new double[] { 0, 1 }).Returns(1);
                yield return new TestCaseData(new double[] { 0, 1, 0 }).Returns(1);
                yield return new TestCaseData(new double[] { 0, 1, 0, 0, 0 }).Returns(1);

                yield return new TestCaseData(new double[] { 1, 0, 1 }).Returns(2);
                yield return new TestCaseData(new double[] { 0, 0, 1 }).Returns(2);
                yield return new TestCaseData(new double[] { 0, 1, 1 }).Returns(2);
                yield return new TestCaseData(new double[] { 0, 1, 1, 0, 0, 0 }).Returns(2);

                yield return new TestCaseData(new double[] { 0, 1, 0, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 0, 1, 1, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 1, 1, 1, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 1, 1, 1, 1, 0, 0, 0 }).Returns(3);

                yield return new TestCaseData(new double[] { 1, 0, -3, 0, 5 }).Returns(4);
                yield return new TestCaseData(new double[] { 0, 0, 0, 0, 5 }).Returns(4);
                yield return new TestCaseData(new double[] { 0, 1, 0, 0, 5, 0, 0, 0 }).Returns(4);
            }
        }

        [TestCaseSource(nameof(GetTestLengthTestCases))]
        public int TestLength(params double[] values)
        {
            Polynomial a = new Polynomial(values);

            return a.Length;
        }

        private static IEnumerable GetTestLengthTestCases
        {
            get
            {
                yield return new TestCaseData(new double[1]).Returns(1);
                yield return new TestCaseData(new double[] { 0, 0, 0 }).Returns(1);
                yield return new TestCaseData(new double[] { 1 }).Returns(1);
                yield return new TestCaseData(new double[] { 1, 0 }).Returns(1);

                yield return new TestCaseData(new double[] { 0, 1 }).Returns(2);
                yield return new TestCaseData(new double[] { 0, 1, 0 }).Returns(2);
                yield return new TestCaseData(new double[] { 0, 1, 0, 0, 0 }).Returns(2);

                yield return new TestCaseData(new double[] { 1, 0, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 0, 0, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 0, 1, 1 }).Returns(3);
                yield return new TestCaseData(new double[] { 0, 1, 1, 0, 0, 0 }).Returns(3);

                yield return new TestCaseData(new double[] { 0, 1, 0, 1 }).Returns(4);
                yield return new TestCaseData(new double[] { 0, 1, 1, 1 }).Returns(4);
                yield return new TestCaseData(new double[] { 1, 1, 1, 1 }).Returns(4);
                yield return new TestCaseData(new double[] { 1, 1, 1, 1, 0, 0, 0 }).Returns(4);

                yield return new TestCaseData(new double[] { 1, 0, -3, 0, 5 }).Returns(5);
                yield return new TestCaseData(new double[] { 0, 0, 0, 0, 5 }).Returns(5);
                yield return new TestCaseData(new double[] { 0, 1, 0, 0, 5, 0, 0, 0 }).Returns(5);
            }
        }

        [TestCaseSource(nameof(GetTestConstructorTestCases))]
        public double[] TestConstructor(params double[] values)
        {
            Polynomial a = new Polynomial(values);

            Assert.IsNotNull(a);

            int length = a.Degree + 1;
            double[] result = new double[length];
            for (int i = 0; i < a.Degree; i++)
            {
                result[i] = a[i];
            }

            return result;
        }

        private static IEnumerable GetTestConstructorTestCases
        {
            get
            {
                yield return new TestCaseData(new double[0])
                    .Returns(new double[] { 0 });

                yield return new TestCaseData(new double[] { -1 })
                    .Returns(new double[] { -1 });

                yield return new TestCaseData(new double[] { 0, -2, 0 })
                    .Returns(new double[] { 0, -2 });

                yield return new TestCaseData(new double[] { 0, -2, 0, 4, 0, 0 })
                    .Returns(new double[] { 0, -2, 0, 4 });

                yield return new TestCaseData(new double[] { 1, 0, -3, 0, 5, 0, 0, 0 })
                    .Returns(new double[] { 1, 0, -3, 0, 5 });
            }
        }

        [TestCaseSource(nameof(GetTestConstructorExceptionsTestCases))]
        public void TestConstructorExceptions(params double[] values)
        {
            Assert.That(() => new Polynomial(values), Throws.Exception);
        }

        public static IEnumerable GetTestConstructorExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(new double[0]);
            }
        }

        [TestCaseSource(nameof(GetTestCopyingConstructorTestCases))]
        public void TestCopyingConstructor(Polynomial polynomial)
        {
            Polynomial a = new Polynomial(polynomial);

            Assert.IsNotNull(a);
            Assert.AreEqual(polynomial, a);
        }

        private static IEnumerable GetTestCopyingConstructorTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0));
                yield return new TestCaseData(new Polynomial(-1));
                yield return new TestCaseData(new Polynomial(0, -2));
                yield return new TestCaseData(new Polynomial(0, -2, 0, 4));
                yield return new TestCaseData(new Polynomial(1, 0, -3, 0, 5));
            }
        }

        [Test]
        public void TestCopyingConstructorException()
        {
            static void A()
            {
                Polynomial a = null;

                _ = new Polynomial(a);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestIndexer()
        {
            Polynomial a = new Polynomial(1, 2, 3);
            const int index = 2;
            const double expected = 3;

            double actual = a[index];

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(GetIndexerExceptionsTestCases))]
        public void TestIndexerExceptions(Polynomial a, int index)
        {
            Assert.That(() => a[index], Throws.Exception);
        }

        public static IEnumerable GetIndexerExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), 3);
                yield return new TestCaseData(new Polynomial(1, 2, 3), -1);
            }
        }

        [TestCaseSource(nameof(GetEqualsTestCases))]
        public bool TestEquals(Polynomial a, object b)
        {
            return a.Equals(b);
        }

        public static IEnumerable GetEqualsTestCases
        {
            get
            {
                Polynomial a = new Polynomial(0);
                yield return new TestCaseData(a, a).Returns(true);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, 3)).Returns(true);

                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(-1, 2, 3)).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, -2, 3)).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, -3)).Returns(false);

                yield return new TestCaseData(new Polynomial(1, 2, 3), null).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new object()).Returns(false);
            }
        }

        [Test]
        public void TestEqualsException()
        {
            static void A()
            {
                Polynomial a = null;
                Polynomial b = new Polynomial(0);

                _ = a.Equals(b);
            }

            Assert.Throws<NullReferenceException>(A);
        }

        [Test]
        public void TestGetHashCode(
            [ValueSource(nameof(GetHashCodeValues))] double a1,
            [ValueSource(nameof(GetHashCodeValues))] double b1,
            [ValueSource(nameof(GetHashCodeValues))] double c1,
            [ValueSource(nameof(GetHashCodeValues))] double a2,
            [ValueSource(nameof(GetHashCodeValues))] double b2,
            [ValueSource(nameof(GetHashCodeValues))] double c2
            )
        {
            Polynomial a = new Polynomial(a1, b1, c1);
            Polynomial b = new Polynomial(a2, b2, c2);
            bool expected = a.Equals(b);

            bool actual = a.GetHashCode() == b.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<double> GetHashCodeValues
        {
            get
            {
                yield return -9;
                yield return -1;
                yield return 0;
                yield return 1;
                yield return 9;
            }
        }

        [TestCaseSource(nameof(PlusTestCases))]
        public Polynomial TestPlus(Polynomial a, Polynomial b)
        {
            return a + b;
        }

        public static IEnumerable PlusTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(11, 22, 33));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(9, 18, 27));
                yield return new TestCaseData(new Polynomial(10, 20, 30), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(9, 18, 27));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(-10, -20, -30))
                    .Returns(new Polynomial(-11, -22, -33));

                yield return new TestCaseData(new Polynomial(0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(10, 20, 30));
                yield return new TestCaseData(new Polynomial(0), new Polynomial(-10, -20, -30))
                    .Returns(new Polynomial(-10, -20, -30));

                yield return new TestCaseData(new Polynomial(1, 0, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(11, 20, 30));
                yield return new TestCaseData(new Polynomial(0, 2, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(10, 22, 30));
                yield return new TestCaseData(new Polynomial(0, 0, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(10, 20, 33));

                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(0));

                yield return new TestCaseData(new Polynomial(1, 2, 3, 4, 5, 6, 7), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(0, 0, 0, 4, 5, 6, 7));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(1, 2, 3, 4, 5, 6, 7))
                    .Returns(new Polynomial(0, 0, 0, 4, 5, 6, 7));
            }
        }

        [Test]
        public void TestPlusException()
        {
            static void A()
            {
                Polynomial a = null;
                Polynomial b = new Polynomial(0);

                _ = a + b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetMinusTestCases))]
        public Polynomial TestMinus(Polynomial a, Polynomial b)
        {
            return a - b;
        }

        public static IEnumerable GetMinusTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-9, -18, -27));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-11, -22, -33));
                yield return new TestCaseData(new Polynomial(10, 20, 30), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(11, 22, 33));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(-10, -20, -30))
                    .Returns(new Polynomial(9, 18, 27));

                yield return new TestCaseData(new Polynomial(0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-10, -20, -30));
                yield return new TestCaseData(new Polynomial(0), new Polynomial(-10, -20, -30))
                    .Returns(new Polynomial(10, 20, 30));

                yield return new TestCaseData(new Polynomial(1, 0, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-9, -20, -30));
                yield return new TestCaseData(new Polynomial(0, 2, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-10, -18, -30));
                yield return new TestCaseData(new Polynomial(0, 0, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-10, -20, -27));

                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(0));

                yield return new TestCaseData(new Polynomial(1, 2, 3, 4, 5, 6, 7), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(2, 4, 6, 4, 5, 6, 7));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(1, 2, 3, 4, 5, 6, 7))
                    .Returns(new Polynomial(-2, -4, -6, -4, -5, -6, -7));
            }
        }

        [Test]
        public void TestMinusException()
        {
            static void A()
            {
                Polynomial a = null;
                Polynomial b = new Polynomial(0);

                _ = a - b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetMultiplicationTestCases))]
        public Polynomial TestMultiplication(Polynomial a, double value)
        {
            return a * value;
        }

        public static IEnumerable GetMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), 0)
                  .Returns(new Polynomial(0));
                yield return new TestCaseData(new Polynomial(0), 5)
                    .Returns(new Polynomial(0));

                yield return new TestCaseData(new Polynomial(1, 2, 3), 5)
                   .Returns(new Polynomial(5, 10, 15));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), 5)
                    .Returns(new Polynomial(-5, -10, -15));

                yield return new TestCaseData(new Polynomial(1, 0, 0), 5)
                    .Returns(new Polynomial(5));
                yield return new TestCaseData(new Polynomial(0, 2, 0), 5)
                    .Returns(new Polynomial(0, 10));
                yield return new TestCaseData(new Polynomial(0, 0, 3), 5)
                    .Returns(new Polynomial(0, 0, 15));

                yield return new TestCaseData(new Polynomial(1, 0, 0), -5)
                    .Returns(new Polynomial(-5));
                yield return new TestCaseData(new Polynomial(0, 2, 0), -5)
                    .Returns(new Polynomial(0, -10));
                yield return new TestCaseData(new Polynomial(0, 0, 3), -5)
                    .Returns(new Polynomial(0, 0, -15));
            }
        }

        [Test]
        public void TestMultiplicationException()
        {
            static void A()
            {
                Polynomial a = null;

                _ = a * 5;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetPolinomialsMultiplicationTestCases))]
        public Polynomial TestPolinomialsMultiplication(Polynomial a, Polynomial b)
        {
            return a * b;
        }

        public static IEnumerable GetPolinomialsMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(10, 40, 100, 120, 90));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(-10, -40, -100, -120, -90));

                yield return new TestCaseData(new Polynomial(0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(0));

                yield return new TestCaseData(new Polynomial(1, 0, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(10, 20, 30));
                yield return new TestCaseData(new Polynomial(0, 2, 0), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(0, 20, 40, 60));
                yield return new TestCaseData(new Polynomial(0, 0, 3), new Polynomial(10, 20, 30))
                    .Returns(new Polynomial(0, 0, 30, 60, 90));

                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(1, 4, 10, 12, 9));

                yield return new TestCaseData(new Polynomial(1, 2, 3, 4, 5, 6, 7), new Polynomial(-1, -2, -3))
                    .Returns(new Polynomial(-1, -4, -10, -16, -22, -28, -34, -32, -21));
                yield return new TestCaseData(new Polynomial(-1, -2, -3), new Polynomial(1, 2, 3, 4, 5, 6, 7))
                    .Returns(new Polynomial(-1, -4, -10, -16, -22, -28, -34, -32, -21));
            }
        }

        [Test]
        public void TestPolinomialsMultiplicationException()
        {
            static void A()
            {
                Polynomial a = null;
                Polynomial b = new Polynomial(0);

                _ = a * b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetEqualityOperatorTestCases))]
        public bool TestEqualityOperator(Polynomial a, Polynomial b)
        {
            return a == b;
        }

        public static IEnumerable GetEqualityOperatorTestCases
        {
            get
            {
                Polynomial a = new Polynomial(0);
                yield return new TestCaseData(a, a).Returns(true);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, 3)).Returns(true);
                yield return new TestCaseData(null, null).Returns(true);

                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(-1, 2, 3)).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, -2, 3)).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, -3)).Returns(false);

                yield return new TestCaseData(new Polynomial(1, 2, 3), null).Returns(false);
            }
        }

        [TestCaseSource(nameof(GetInequalityOperatorTestCases))]
        public bool TestInequalityOperator(Polynomial a, Polynomial b)
        {
            return a != b;
        }

        public static IEnumerable GetInequalityOperatorTestCases
        {
            get
            {
                Polynomial a = new Polynomial(0);
                yield return new TestCaseData(a, a).Returns(false);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, 3)).Returns(false);
                yield return new TestCaseData(null, null).Returns(false);

                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(-1, 2, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, -2, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(1, 2, -3)).Returns(true);

                yield return new TestCaseData(new Polynomial(1, 2, 3), null).Returns(true);
            }
        }

        [TestCaseSource(nameof(GetToStringTestCases))]
        public string TestToString(Polynomial a)
        {
            return a.ToString();
        }


        public static IEnumerable GetToStringTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 2, 0, 3)).Returns("3x^3 + 0x^2 + 2x^1 + 0x^0");

                yield return new TestCaseData(new Polynomial(100d, 200d, 300d)).Returns("300x^2 + 200x^1 + 100x^0");
                yield return new TestCaseData(new Polynomial(10d, 20d, 30d)).Returns("30x^2 + 20x^1 + 10x^0");
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d)).Returns("3x^2 + 2x^1 + 1x^0");
                yield return new TestCaseData(new Polynomial(0)).Returns("0x^0");
                yield return new TestCaseData(new Polynomial(-1, -2, -3)).Returns("-3x^2 - 2x^1 - 1x^0");
                yield return new TestCaseData(new Polynomial(-10, -20, -30)).Returns("-30x^2 - 20x^1 - 10x^0");
                yield return new TestCaseData(new Polynomial(-100, -200, -300)).Returns("-300x^2 - 200x^1 - 100x^0");

                yield return new TestCaseData(new Polynomial(1.1, 2.2, 3.3)).Returns("3,3x^2 + 2,2x^1 + 1,1x^0");
                yield return new TestCaseData(new Polynomial(1.01, 2.02, 3.03)).Returns("3,03x^2 + 2,02x^1 + 1,01x^0");
                yield return new TestCaseData(new Polynomial(-1.1, -2.2, -3.3)).Returns("-3,3x^2 - 2,2x^1 - 1,1x^0");
                yield return new TestCaseData(new Polynomial(-1.01, -2.02, -3.03)).Returns("-3,03x^2 - 2,02x^1 - 1,01x^0");
            }
        }

        [TestCaseSource(nameof(GetPolynomialTestCases))]
        public string TestGetPolynomial(Polynomial a)
        {
            return a.ToStringWhole();
        }

        public static IEnumerable GetPolynomialTestCases
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 0, 3, 0, 4)).Returns("4x^5 + 3x^3 + 2x + 1");

                yield return new TestCaseData(new Polynomial(100d, 200d, 300d)).Returns("300x^2 + 200x + 100");
                yield return new TestCaseData(new Polynomial(10d, 20d, 30d)).Returns("30x^2 + 20x + 10");
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d)).Returns("3x^2 + 2x + 1");
                yield return new TestCaseData(new Polynomial(0)).Returns("0");
                yield return new TestCaseData(new Polynomial(-1, -2, -3)).Returns("-3x^2 - 2x - 1");
                yield return new TestCaseData(new Polynomial(-10, -20, -30)).Returns("-30x^2 - 20x - 10");
                yield return new TestCaseData(new Polynomial(-100, -200, -300)).Returns("-300x^2 - 200x - 100");

                yield return new TestCaseData(new Polynomial(1.1, 2.2, 3.3)).Returns("3,3x^2 + 2,2x + 1,1");
                yield return new TestCaseData(new Polynomial(1.01, 2.02, 3.03)).Returns("3,03x^2 + 2,02x + 1,01");
                yield return new TestCaseData(new Polynomial(-1.1, -2.2, -3.3)).Returns("-3,3x^2 - 2,2x - 1,1");
                yield return new TestCaseData(new Polynomial(-1.01, -2.02, -3.03)).Returns("-3,03x^2 - 2,02x - 1,01");
            }
        }
    }
}
