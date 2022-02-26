using NUnit.Framework;
using GCDCalculator;
using System;

namespace GCDCalculatorTests
{
    [TestFixture]
    public class GCDTests
    {
        [Test]
        public void TestCalculateEuclidean_BothEqual_Success()
        {
            const int a = 320;
            const int b = 320;
            const int expected = 320;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_ALess_Success()
        {
            const int a = 120;
            const int b = 320;
            const int expected = 40;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_BLess_Success()
        {
            const int a = 320;
            const int b = 120;
            const int expected = 40;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_BothEven_Success()
        {
            const int a = 4;
            const int b = 356;
            const int expected = 4;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_BUneven_Success()
        {
            const int a = 4;
            const int b = 11;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_BothUneven_Success()
        {
            const int a = 11;
            const int b = 251;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_AUneven_Success()
        {
            const int a = 11;
            const int b = 12;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_FirstFiveSimpleWithoutOne_Success()
        {
            const int a = 3;
            const int b = 5;
            const int c = 7;
            const int d = 11;
            const int e = 13;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b, c, d, e);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_ThreeValues_Success()
        {
            const int a = 16;
            const int b = 256;
            const int c = 64;
            const int expected = 16;

            int actual = GCD.CalculateEuclidean(a, b, c);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_FourValues_Success()
        {
            const int a = 653;
            const int b = 166;
            const int c = 12;
            const int d = 15;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b, c, d);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateEuclidean_FiveValues_Success()
        {
            const int a = 653;
            const int b = 166;
            const int c = 12;
            const int d = 15;
            const int e = 357;
            const int expected = 1;

            int actual = GCD.CalculateEuclidean(a, b, c, d, e);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_BothEqual_Success()
        {
            const int a = 320;
            const int b = 320;
            const int expected = 320;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_ALess_Success()
        {
            const int a = 120;
            const int b = 320;
            const int expected = 40;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_BLess_Success()
        {
            const int a = 320;
            const int b = 120;
            const int expected = 40;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_BothEven_Success()
        {
            const int a = 56;
            const int b = 664;
            const int expected = 8;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_AUneven_Success()
        {
            const int a = 49;
            const int b = 56;
            const int expected = 7;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_BothUneven_Success()
        {
            const int a = 11;
            const int b = 121;
            const int expected = 11;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_BUneven_Success()
        {
            const int a = 10;
            const int b = 121;
            const int expected = 1;

            int actual = GCD.CalculateStein(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCalculateStein_FifteenValues_Success()
        {
            int[] values = { 300, 600, 18, 9, 3, 36, 72, 324, 6, 27, 15, 33, 30, 66, 54 };
            const int expected = 3;

            TimeSpan elapsed;
            int actual = GCD.CalculateStein(out elapsed, values);

            Assert.AreEqual(expected, actual);
            Assert.Positive(elapsed.TotalMilliseconds);
        }

        [Test]
        public void TestCalculateEuclidean_FifteenValues_Success()
        {
            int[] values = { 300, 600, 18, 9, 3, 36, 72, 324, 6, 27, 15, 33, 30, 66, 54 };
            const int expected = 3;

            TimeSpan elapsed;
            int actual = GCD.CalculateEuclidean(out elapsed, values);

            Assert.AreEqual(expected, actual);
            Assert.Positive(elapsed.TotalMilliseconds);
        }
    }
}
