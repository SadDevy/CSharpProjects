using System;
using System.Collections;
using Matrices;
using NUnit.Framework;


namespace MatricesTests
{
    [TestFixture]
    public class MatrixTests
    {
        [TestCaseSource(nameof(GetTestColCountTestCases))]
        public int TestColCount(double[,] values)
        {
            Matrix a = new Matrix(values);

            return a.ColumnCount;
        }

        public static IEnumerable GetTestColCountTestCases
        {
            get
            {
                yield return new TestCaseData(new double[1, 1]).Returns(1);
                yield return new TestCaseData(new double[,] { { 1 }, { 1 } }).Returns(1);

                yield return new TestCaseData(new double[,] { { 1, 0 }, { 1, 0 } }).Returns(2);
                yield return new TestCaseData(new double[,] { { 0, 1 }, { 0, 1 } }).Returns(2);

                yield return new TestCaseData(new double[,] { { 0, 0, 0 }, { 0, 0, 0 } }).Returns(3);
                yield return new TestCaseData(new double[,] { { 0, 1, 0 }, { 0, 1, 0 } }).Returns(3);
                yield return new TestCaseData(new double[,] { { 1, 0, 1 }, { 1, 0, 1 } }).Returns(3);

                yield return new TestCaseData(new double[,] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 } }).Returns(4);
                yield return new TestCaseData(new double[,] { { 0, 1, 1, 0 }, { 0, 1, 1, 0 } }).Returns(4);
                yield return new TestCaseData(new double[,] { { 0, 1, 1, 1 }, { 0, 1, 1, 1 } }).Returns(4);

                yield return new TestCaseData(new double[,] { { 0, 1, 1, 0, 0 }, { 0, 1, 1, 0, 0 } }).Returns(5);
            }
        }

        [TestCaseSource(nameof(GetTestRowCountTestCases))]
        public int TestRowCount(double[,] values)
        {
            Matrix a = new Matrix(values);

            return a.RowCount;
        }

        public static IEnumerable GetTestRowCountTestCases
        {
            get
            {
                yield return new TestCaseData(new double[1, 1]).Returns(1);
                yield return new TestCaseData(new double[,] { { 1 }, { 1 } }).Returns(2);
                yield return new TestCaseData(new double[,] { { 1, 0 }, { 1, 0 } }).Returns(2);
                yield return new TestCaseData(new double[,] { { 0, 1 }, { 0, 1 } }).Returns(2);

                yield return new TestCaseData(new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }).Returns(3);
                yield return new TestCaseData(new double[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 0, 0 } }).Returns(3);
                yield return new TestCaseData(new double[,] { { 1, 0, 1 }, { 1, 0, 1 }, { 0, 0, 0 } }).Returns(3);
                yield return new TestCaseData(new double[,] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 } }).Returns(3);
            }
        }

        [TestCaseSource(nameof(GetTestDefaultConstructorTestCases))]
        public void TestDefaultConstructor(int rowCount, int colCount)
        {
            Matrix a = new Matrix(rowCount, colCount);

            Assert.IsNotNull(a);
        }

        public static IEnumerable GetTestDefaultConstructorTestCases
        {
            get
            {
                yield return new TestCaseData(1, 0);
                yield return new TestCaseData(0, 1);
                yield return new TestCaseData(1, 1);

                yield return new TestCaseData(2, 1);
                yield return new TestCaseData(1, 2);
                yield return new TestCaseData(2, 2);
            }
        }

        [Test]
        public void TestDefaultConstructorException()
        {
            static void A()
            {
                _ = new Matrix(0, 0);
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        [TestCaseSource(nameof(GetTestConstructorTestCases))]
        public double[,] TestConstructor(double[,] values)
        {
            Matrix a = new Matrix(values);

            Assert.IsNotNull(a);

            double[,] result = new double[a.RowCount, a.ColumnCount];
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    result[i, j] = a[i, j];
                }
            }

            return result;
        }

        public static IEnumerable GetTestConstructorTestCases
        {
            get
            {
                yield return new TestCaseData(new double[1, 1])
                    .Returns(new double[1, 1]);

                yield return new TestCaseData(new double[,] { { -1 }, { -1 } })
                    .Returns(new double[,] { { -1 }, { -1 } });
                yield return new TestCaseData(new double[,] { { -1, -1 } })
                    .Returns(new double[,] { { -1, -1 } });

                yield return new TestCaseData(new double[,] { { 0, -2, 0 }, { 0, -2, 0 } })
                    .Returns(new double[,] { { 0, -2, 0 }, { 0, -2, 0 } });

                yield return new TestCaseData(new double[,] { { 0, -2, 0, 4, 0 }, { 0, -2, 0, 4, 0 } })
                    .Returns(new double[,] { { 0, -2, 0, 4, 0 }, { 0, -2, 0, 4, 0 } });
            }
        }

        [TestCaseSource(nameof(GetTestConstructorExceptionsTestCases))]
        public void TestConstructorExceptions(double[,] values)
        {
            Assert.That(() => new Matrix(values), Throws.Exception);
        }

        private static IEnumerable GetTestConstructorExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(new double[0, 0]);
                yield return new TestCaseData(null);
            }
        }

        [TestCaseSource(nameof(GetTestIndexerExceptionTestCases))]
        public void TestIndexerException(Matrix a, int rowIndex, int colIndex)
        {
            void A()
            {
                _ = a[rowIndex, colIndex];
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        private static IEnumerable GetTestIndexerExceptionTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(1, 1), 1, 0);
                yield return new TestCaseData(new Matrix(1, 1), 0, 1);
                yield return new TestCaseData(new Matrix(1, 1), 1, 1);

                yield return new TestCaseData(new Matrix(1, 1), -1, 0);
                yield return new TestCaseData(new Matrix(1, 1), 0, -1);
                yield return new TestCaseData(new Matrix(1, 1), -1, -1);
            }
        }

        [TestCaseSource(nameof(GetTestEqualsTestCases))]
        public bool TestEquals(Matrix a, object b)
        {
            return a.Equals(b);
        }

        private static IEnumerable GetTestEqualsTestCases
        {
            get
            {
                Matrix a = new Matrix(1, 1);
                yield return new TestCaseData(a, a).Returns(true);
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 } }),
                                              new Matrix(new double[,] { { 1, 2 } })).Returns(true);

                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 } }),
                                             new Matrix(new double[,] { { 1, 2 } })).Returns(false);

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2, 3 } }),
                                              new Matrix(new double[,] { { -1, 2, 3 } })).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2, 3 } }),
                                              new Matrix(new double[,] { { 1, -2, 3 } })).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2, 3 } }),
                                              new Matrix(new double[,] { { 1, 2, -3 } })).Returns(false);

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 } }), null).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 } }), new object()).Returns(false);
            }
        }

        [Test]
        public void TestGetHashCode(
            [ValueSource(nameof(TestGetHashCodeTestCases))] double a1,
            [ValueSource(nameof(TestGetHashCodeTestCases))] double b1,
            [ValueSource(nameof(TestGetHashCodeTestCases))] double c1,
            [ValueSource(nameof(TestGetHashCodeTestCases))] double a2,
            [ValueSource(nameof(TestGetHashCodeTestCases))] double b2,
            [ValueSource(nameof(TestGetHashCodeTestCases))] double c2
            )
        {
            Matrix a = new Matrix(new double[,] { { a1 }, { b1 }, { c1 } });
            Matrix b = new Matrix(new double[,] { { a2 }, { b2 }, { c2 } });
            bool expected = a.Equals(b);

            bool actual = a.GetHashCode() == b.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable TestGetHashCodeTestCases
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

        [TestCaseSource(nameof(GetTestPlusTestCases))]
        public Matrix TestPlus(Matrix a, Matrix b)
        {
            return a + b;
        }

        public static IEnumerable GetTestPlusTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 11, 22 }, { 33, 44 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 9, 18 }, { 27, 36 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }),
                                              new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }))
                                              .Returns(new Matrix(new double[,] { { 9, 18 }, { 27, 36 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }),
                                              new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }))
                                              .Returns(new Matrix(new double[,] { { -11, -22 }, { -33, -44 } }));

                yield return new TestCaseData(new Matrix(2, 2), new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }));
                yield return new TestCaseData(new Matrix(2, 2), new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }))
                                              .Returns(new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 0 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 11, 20 }, { 30, 40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 2 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 22 }, { 30, 40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 3, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 20 }, { 33, 40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 0, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 20 }, { 30, 44 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }))
                                              .Returns(new Matrix(2, 2));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 0 } }),
                                              new Matrix(new double[,] { { -1, -2 }, { -3, 0 } }))
                                              .Returns(new Matrix(2, 2));
            }
        }

        [TestCaseSource(nameof(GetTestPlusExceptionsTestCases))]
        public void TestPlusExceptions(Matrix a, Matrix b)
        {
            Assert.That(() => { _ = a + b; }, Throws.Exception);
        }

        private static IEnumerable GetTestPlusExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(1, 1), null);

                yield return new TestCaseData(new Matrix(1, 1), new Matrix(1, 2));
                yield return new TestCaseData(new Matrix(1, 1), new Matrix(2, 1));
                yield return new TestCaseData(new Matrix(1, 1), new Matrix(2, 2));
            }
        }

        [TestCaseSource(nameof(GetTestMinusTestCases))]
        public Matrix TestMinus(Matrix a, Matrix b)
        {
            return a - b;
        }

        private static IEnumerable GetTestMinusTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -9, -18 }, { -27, -36 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -11, -22 }, { -33, -44 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }),
                                              new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }))
                                              .Returns(new Matrix(new double[,] { { 11, 22 }, { 33, 44 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }),
                                              new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }))
                                              .Returns(new Matrix(new double[,] { { 9, 18 }, { 27, 36 } }));

                yield return new TestCaseData(new Matrix(2, 2), new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }));
                yield return new TestCaseData(new Matrix(2, 2), new Matrix(new double[,] { { -10, -20 }, { -30, -40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 0 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -9, -20 }, { -30, -40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 2 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -10, -18 }, { -30, -40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 3, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -10, -20 }, { -27, -40 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 0, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -10, -20 }, { -30, -36 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }))
                                              .Returns(new Matrix(2, 2));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 0 } }),
                                              new Matrix(new double[,] { { 1, 2 }, { 3, 0 } }))
                                              .Returns(new Matrix(2, 2));
            }
        }

        [TestCaseSource(nameof(GetTestMinusExceptionsTestCases))]
        public void TestMinusExceptions(Matrix a, Matrix b)
        {
            Assert.That(() => { _ = a - b; }, Throws.Exception);
        }

        private static IEnumerable GetTestMinusExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(1, 1), null);

                yield return new TestCaseData(new Matrix(1, 1), new Matrix(1, 2));
                yield return new TestCaseData(new Matrix(1, 1), new Matrix(2, 1));
                yield return new TestCaseData(new Matrix(1, 1), new Matrix(2, 2));
            }
        }

        [TestCaseSource(nameof(GetTestMultiplicationTestCases))]
        public Matrix TestMultiplication(Matrix a, double value)
        {
            return a * value;
        }

        public static IEnumerable GetTestMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }), 0)
                                              .Returns(new Matrix(2, 2));
                yield return new TestCaseData(new Matrix(2, 2), 5)
                                              .Returns(new Matrix(2, 2));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }), 5)
                                              .Returns(new Matrix(new double[,] { { 5, 10 }, { 15, 20 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }), 5)
                                              .Returns(new Matrix(new double[,] { { -5, -10 }, { -15, -20 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 0 }, { 0, 0 } }), 5)
                                              .Returns(new Matrix(new double[,] { { 5, 0 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 2 }, { 0, 0 } }), 5)
                                              .Returns(new Matrix(new double[,] { { 0, 10 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 3, 0 } }), 5)
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { 15, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 0, 4 } }), 5)
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { 0, 20 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 0 }, { 0, 0 } }), -5)
                                               .Returns(new Matrix(new double[,] { { -5, 0 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 2 }, { 0, 0 } }), -5)
                                              .Returns(new Matrix(new double[,] { { 0, -10 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 3, 0 } }), -5)
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { -15, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 0, 4 } }), -5)
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { 0, -20 } }));
            }
        }

        [Test]
        public void TestMultiplicationException()
        {
            static void A()
            {
                Matrix a = null;

                _ = a * 5;
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [TestCaseSource(nameof(GetMatrixMultiplicationTestCases))]
        public Matrix TestMatrixMultiplication(Matrix a, Matrix b)
        {
            return a * b;
        }

        public static IEnumerable GetMatrixMultiplicationTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 70, 100 }, { 150, 220 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { -70, -100 }, { -150, -220 } }));

                yield return new TestCaseData(new Matrix(2, 2),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(2, 2));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 0 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 10, 20 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 2 }, { 0, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 60, 80 }, { 0, 0 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 3, 0 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { 30, 60 } }));
                yield return new TestCaseData(new Matrix(new double[,] { { 0, 0 }, { 0, 4 } }),
                                              new Matrix(new double[,] { { 10, 20 }, { 30, 40 } }))
                                              .Returns(new Matrix(new double[,] { { 0, 0 }, { 120, 160 } }));

                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
                                              new Matrix(new double[,] { { 10 }, { 20 } }))
                                              .Returns(new Matrix(new double[,] { { 50 }, { 110 } }));
            }
        }

        [TestCaseSource(nameof(GetTestMatrixMultiplicationExceptionsTestCases))]
        public void TestMatrixMultiplicationExceptions(Matrix a, Matrix b)
        {
            Assert.That(() => { _ = a * b; }, Throws.Exception);
        }

        private static IEnumerable GetTestMatrixMultiplicationExceptionsTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(1, 1), null);

                yield return new TestCaseData(new Matrix(1, 1), new Matrix(2, 1));
            }
        }


        [TestCaseSource(nameof(GetEqualityOperatorTestCases))]
        public bool TestEqualityOperator(Matrix a, Matrix b)
        {
            return a == b;
        }

        public static IEnumerable GetEqualityOperatorTestCases
        {
            get
            {
                Matrix a = new Matrix(1, 1);
                yield return new TestCaseData(a, a).Returns(true);
                yield return new TestCaseData(null, null).Returns(true);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 } }),
                                              new Matrix(new double[,] { { 1 }, { 2 } })).Returns(true);

                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 } }),
                                             new Matrix(new double[,] { { 1, 2 } })).Returns(false);

                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { -1 }, { 2 }, { 3 } })).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { 1 }, { -2 }, { 3 } })).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { 1 }, { 2 }, { -3 } })).Returns(false);

                yield return new TestCaseData(new Matrix(1, 1), null).Returns(false);
            }
        }

        [TestCaseSource(nameof(GetInequalityOperatorTestCases))]
        public bool TestInequalityOperator(Matrix a, Matrix b)
        {
            return a != b;
        }

        public static IEnumerable GetInequalityOperatorTestCases
        {
            get
            {
                Matrix a = new Matrix(1, 1);
                yield return new TestCaseData(a, a).Returns(false);
                yield return new TestCaseData(null, null).Returns(false);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 } }),
                                              new Matrix(new double[,] { { 1 }, { 2 } })).Returns(false);

                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 } }),
                                             new Matrix(new double[,] { { 1, 2 } })).Returns(true);

                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { -1 }, { 2 }, { 3 } })).Returns(true);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { 1 }, { -2 }, { 3 } })).Returns(true);
                yield return new TestCaseData(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }),
                                              new Matrix(new double[,] { { 1 }, { 2 }, { -3 } })).Returns(true);

                yield return new TestCaseData(new Matrix(1, 1), null).Returns(true);
            }
        }

        [TestCaseSource(nameof(GetTestToStringTestCases))]
        public static string TestToString(Matrix a)
        {
            return a.ToString();
        }

        private static IEnumerable GetTestToStringTestCases
        {
            get
            {
                yield return new TestCaseData(new Matrix(1, 1))
                                              .Returns("|    0,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(2, 1))
                                              .Returns("|    0,0    |" + Environment.NewLine + 
                                                       "|    0,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(2, 2))
                                              .Returns("|    0,0        0,0    |" + Environment.NewLine +
                                                       "|    0,0        0,0    |" + Environment.NewLine);

                yield return new TestCaseData(new Matrix(new double[,] { { 1000, 2000 }, { 3000, 4000 } }))
                                              .Returns("| 1000,0     2000,0    |" + Environment.NewLine +
                                                       "| 3000,0     4000,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { 100, 200 }, { 300, 400 } }))
                                              .Returns("|  100,0      200,0    |" + Environment.NewLine +
                                                       "|  300,0      400,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }))
                                              .Returns("|    1,0        2,0    |" + Environment.NewLine +
                                                       "|    3,0        4,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { -1, -2 }, { -3, -4 } }))
                                              .Returns("|   -1,0       -2,0    |" + Environment.NewLine +
                                                       "|   -3,0       -4,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { -100, -200 }, { -300, -400 } }))
                                              .Returns("| -100,0     -200,0    |" + Environment.NewLine +
                                                       "| -300,0     -400,0    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { -1000, -2000 }, { -3000, -4000 } }))
                                              .Returns("|-1000,0    -2000,0    |" + Environment.NewLine +
                                                       "|-3000,0    -4000,0    |" + Environment.NewLine);

                yield return new TestCaseData(new Matrix(new double[,] { { 1.1, 2.2 }, { 3.3, 4.4 } }))
                                              .Returns("|    1,1        2,2    |" + Environment.NewLine +
                                                       "|    3,3        4,4    |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { 1.01, 2.02 }, { 3.03, 4.04 } }))
                                              .Returns("|    1,01       2,02   |" + Environment.NewLine +
                                                       "|    3,03       4,04   |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { -1.01, -2.02 }, { -3.03, -4.04 } }))
                                              .Returns("|   -1,01      -2,02   |" + Environment.NewLine +
                                                       "|   -3,03      -4,04   |" + Environment.NewLine);
                yield return new TestCaseData(new Matrix(new double[,] { { -1.1, -2.2 }, { -3.3, -4.4 } }))
                                              .Returns("|   -1,1       -2,2    |" + Environment.NewLine +
                                                       "|   -3,3       -4,4    |" + Environment.NewLine);
            }
        }
    }
}
