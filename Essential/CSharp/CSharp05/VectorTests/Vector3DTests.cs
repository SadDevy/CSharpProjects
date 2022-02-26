using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Vector;

namespace VectorTests
{
    [TestFixture]
    public class Vector3DTests
    {
        [TestCase(30.3d, 20.2d, 10.1d)]
        [TestCase(0, 0, 0)]
        [TestCase(-1.01d, -2.02d, -3.03d)]
        public void TestProperties(double x, double y, double z)
        {
            Vector3D actual = new Vector3D(x, y, z);

            Assert.IsNotNull(actual);
            Assert.AreEqual(x, actual.X);
            Assert.AreEqual(y, actual.Y);
            Assert.AreEqual(z, actual.Z);
        }

        [TestCaseSource(nameof(GetEqualsTestCases))]
        public bool TestEquals(Vector3D a, object b)
        {
            return a.Equals(b);
        }

        private static IEnumerable GetEqualsTestCases
        {
            get
            {
                Vector3D a = new Vector3D(1, 2, 3);
                yield return new TestCaseData(a, a).Returns(true);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, 3)).Returns(true);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(-1, 2, 3)).Returns(false);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, -2, 3)).Returns(false);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, -3)).Returns(false);

                yield return new TestCaseData(new Vector3D(1, 2, 3), null).Returns(false);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new object()).Returns(false);
            }
        }

        [Test]
        public void TestEqualsException()
        {
            static void A()
            {
                Vector3D a = null;
                Vector3D b = new Vector3D();
                _ = a.Equals(b);
            }

            Assert.Throws<NullReferenceException>(A);
        }

        [Test]
        public void TestGetHashCode(
            [ValueSource(nameof(GetHashCodeValues))] double x,
            [ValueSource(nameof(GetHashCodeValues))] double y,
            [ValueSource(nameof(GetHashCodeValues))] double z,
            [ValueSource(nameof(GetHashCodeValues))] double n,
            [ValueSource(nameof(GetHashCodeValues))] double l,
            [ValueSource(nameof(GetHashCodeValues))] double m
            )
        {
            Vector3D a = new Vector3D(x, y, z);
            Vector3D b = new Vector3D(n, l, m);
            bool expected = a.Equals(b);

            bool actual = a.GetHashCode() == b.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<double> GetHashCodeValues
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

        [TestCaseSource(nameof(GetToStringTestCases))]
        public string TestToString(Vector3D a)
        {
            return a.ToString();
        }

        private static IEnumerable GetToStringTestCases
        {
            get
            {
                yield return new TestCaseData(new Vector3D(100d, 200d, 300d)).Returns("{100; 200; 300}");
                yield return new TestCaseData(new Vector3D(10d, 20d, 30d)).Returns("{10; 20; 30}");
                yield return new TestCaseData(new Vector3D(1d, 2d, 3d)).Returns("{1; 2; 3}");
                yield return new TestCaseData(new Vector3D()).Returns("{0; 0; 0}");
                yield return new TestCaseData(new Vector3D(-1, -2, -3)).Returns("{-1; -2; -3}");
                yield return new TestCaseData(new Vector3D(-10, -20, -30)).Returns("{-10; -20; -30}");
                yield return new TestCaseData(new Vector3D(-100, -200, -300)).Returns("{-100; -200; -300}");

                yield return new TestCaseData(new Vector3D(1.1, 2.2, 3.3)).Returns("{1,1; 2,2; 3,3}");
                yield return new TestCaseData(new Vector3D(1.01, 2.02, 3.03)).Returns("{1,01; 2,02; 3,03}");
                yield return new TestCaseData(new Vector3D(-1.1, -2.2, -3.3)).Returns("{-1,1; -2,2; -3,3}");
                yield return new TestCaseData(new Vector3D(-1.01, -2.02, -3.03)).Returns("{-1,01; -2,02; -3,03}");
            }
        }

        [TestCaseSource(nameof(GetTestPlusTestCases))]
        public Vector3D TestPlus(Vector3D a, Vector3D b)
        {
            return a + b;
        }

        private static IEnumerable GetTestPlusTestCases
        {
            get
            {
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(11, 22, 33));
                yield return new TestCaseData(new Vector3D(-1, -2, -3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(9, 18, 27));
                yield return new TestCaseData(new Vector3D(10, 20, 30), new Vector3D(-1, -2, -3))
                    .Returns(new Vector3D(9, 18, 27));
                yield return new TestCaseData(new Vector3D(-1, -2, -3), new Vector3D(-10, -20, -30))
                    .Returns(new Vector3D(-11, -22, -33));

                yield return new TestCaseData(new Vector3D(), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(10, 20, 30));  
                yield return new TestCaseData(new Vector3D(), new Vector3D(-10, -20, -30))
                    .Returns(new Vector3D(-10, -20, -30)); 
                
                yield return new TestCaseData(new Vector3D(1, 0, 0), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(11, 20, 30));
                yield return new TestCaseData(new Vector3D(0, 2, 0), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(10, 22, 30));
                yield return new TestCaseData(new Vector3D(0, 0, 3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(10, 20, 33));

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(-1, -2, -3))
                    .Returns(new Vector3D(0, 0, 0));
            }
        }

        [Test]
        public void TestPlusException()
        {
            static void A()
            {
                Vector3D a = null;
                Vector3D b = new Vector3D();
                _ = a + b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetMinusTestCases))]
        public Vector3D TestMinus(Vector3D a, Vector3D b)
        {
            return a - b;
        }

        private static IEnumerable GetMinusTestCases
        {
            get
            {
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-9, -18, -27));
                yield return new TestCaseData(new Vector3D(-1, -2, -3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-11, -22, -33));
                yield return new TestCaseData(new Vector3D(10, 20, 30), new Vector3D(-1, -2, -3))
                    .Returns(new Vector3D(11, 22, 33));
                yield return new TestCaseData(new Vector3D(-1, -2, -3), new Vector3D(-10, -20, -30))
                    .Returns(new Vector3D(9, 18, 27));

                yield return new TestCaseData(new Vector3D(), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-10, -20, -30));
                yield return new TestCaseData(new Vector3D(), new Vector3D(-10, -20, -30))
                    .Returns(new Vector3D(10, 20, 30));

                yield return new TestCaseData(new Vector3D(1, 0, 0), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-9, -20, -30));
                yield return new TestCaseData(new Vector3D(0, 2, 0), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-10, -18, -30));
                yield return new TestCaseData(new Vector3D(0, 0, 3), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D(-10, -20, -27));

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, 3))
                    .Returns(new Vector3D(0, 0, 0));
            }
        }

        [Test]
        public void TestMinusException()
        {
            static void A()
            {
                Vector3D a = null;
                Vector3D b = new Vector3D();
                _ = a - b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }


        [TestCaseSource(nameof(GetDotMultiplicationTestCases))]
        public double TestDotMultiplication(Vector3D a, Vector3D b)
        {
            return a * b;
        }

        private static IEnumerable GetDotMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Vector3D(10, 20, 30), new Vector3D(1, 2, 3))
                    .Returns(140);
                yield return new TestCaseData(new Vector3D(10, 20, 30), new Vector3D(-1, -2, -3))
                    .Returns(-140);
                yield return new TestCaseData(new Vector3D(-1, -2, -3), new Vector3D(-10, -20, -30))
                    .Returns(140);

                yield return new TestCaseData(new Vector3D(), new Vector3D(10, 20, 30))
                    .Returns(0);
                yield return new TestCaseData(new Vector3D(), new Vector3D())
                    .Returns(0);

                yield return new TestCaseData(new Vector3D(0, 2, 0), new Vector3D(10, 0, 30))
                    .Returns(0);
                yield return new TestCaseData(new Vector3D(1, 0, 0), new Vector3D(10, 0, 30))
                    .Returns(10);
                yield return new TestCaseData(new Vector3D(0, 0, 3), new Vector3D(10, 0, 30))
                    .Returns(90);
            }
        }

        [Test]
        public void TestDotMultiplicationException()
        {
            static void A()
            {
                Vector3D a = null;
                Vector3D b = new Vector3D();
                _ = a * b;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetCrossMultiplicationTestCases))]
        public Vector3D TestCrossMultiplication(Vector3D a, Vector3D b)
        {
            return Vector3D.Cross(a, b);
        }

        private static IEnumerable GetCrossMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Vector3D(10, 20, 30), new Vector3D(1, 2, 3))
                    .Returns(new Vector3D());
                yield return new TestCaseData(new Vector3D(10, 20, 31), new Vector3D(-1, -2, -3))
                    .Returns(new Vector3D(2, -1, 0));
                yield return new TestCaseData(new Vector3D(-1, -2, -5), new Vector3D(-10, -20, -30))
                    .Returns(new Vector3D(-40, 20, 0));

                yield return new TestCaseData(new Vector3D(), new Vector3D(10, 20, 30))
                    .Returns(new Vector3D());
                yield return new TestCaseData(new Vector3D(), new Vector3D())
                    .Returns(new Vector3D());

                yield return new TestCaseData(new Vector3D(0, 2, 0), new Vector3D(10, 0, 30))
                    .Returns(new Vector3D(60, 0, -20));
                yield return new TestCaseData(new Vector3D(1, 0, 0), new Vector3D(10, 0, 30))
                    .Returns(new Vector3D(0, -30, 0));
                yield return new TestCaseData(new Vector3D(0, 0, 3), new Vector3D(10, 0, 30))
                    .Returns(new Vector3D(0, 30, 0));
            }
        }

        [Test]
        public void TestCrossMultiplicationException()
        {
            static void A()
            {
                Vector3D a = null;
                Vector3D b = new Vector3D();
                _ = Vector3D.Cross(a, b);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetEqualityOperatorTestCases))]
        public bool TestEqualityOperator(Vector3D a, Vector3D b)
        {
            return a == b;
        }

        private static IEnumerable GetEqualityOperatorTestCases
        {
            get
            {
                Vector3D a = new Vector3D(1, 2, 3);
                yield return new TestCaseData(a, a).Returns(true);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, 3)).Returns(true);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(-1, 2, 3)).Returns(false);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, -2, 3)).Returns(false);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, -3)).Returns(false);

                yield return new TestCaseData(new Vector3D(1, 2, 3), null).Returns(false);
            }
        }

        [TestCaseSource(nameof(GetInequalityOperatorTestCases))]
        public bool TestInequalityOperator(Vector3D a, Vector3D b)
        {
            return a != b;
        }

        private static IEnumerable GetInequalityOperatorTestCases 
        {
            get
            {
                Vector3D a = new Vector3D(1, 2, 3);
                yield return new TestCaseData(a, a).Returns(false);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, 3)).Returns(false);

                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(-1, 2, 3)).Returns(true);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, -2, 3)).Returns(true);
                yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(1, 2, -3)).Returns(true);

                yield return new TestCaseData(new Vector3D(1, 2, 3), null).Returns(true);
            }
        }

    }
}
