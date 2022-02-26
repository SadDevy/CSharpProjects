using System;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestAdd_EmtyString_Zero()
        {
            const int expected = 0;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_OneValue_Value()
        {
            const string value = "1";

            const int expected = 1;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(value);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_TwoValues_ValuesSum()
        {
            const string values = "1,2";

            const int expected = 3;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,2,3,4", 10)]
        [TestCase("1,2,3", 6)]
        public void TestAdd_MultipleValues_ValuesSum(string values, int expected)
        {
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_WithNewLine_ValuesSum()
        {
            const string values = "1\n2,3";

            const int expected = 6;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_NewLineAndCommaInOrder_ExtractedValuesSum()
        {
            const string values = "1,\n";

            const int expected = 1;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_WithNewSeparator_ValuesSum()
        {
            const string values = "//;\n1;2";

            const int expected = 3;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_OneNegative_Failure()
        {
            static void A()
            {
                const string values = "1,-2,3";
                Calculator calculator = new Calculator();

                calculator.Add(values);
            }

            Assert.Throws<InvalidOperationException>(A, "Error: negatives are not allowed");
        }

        [Test]
        public void TestAdd_MultipleNegatives_Failure()
        {
            static void A()
            {
                const string values = "1,-2,3,-6\n-10";
                Calculator calculator = new Calculator();

                calculator.Add(values);
            }

            Assert.Throws<InvalidOperationException>(A, "Error: negatives are not allowed: -2,-6,-10");
        }

        [Test]
        public void TestAdd_WithBiggerThousand_SummedValus()
        {
            const string values = "2,1001";

            const int expected = 2;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_LongDelimiter_SummedValus()
        {
            const string values = "//[***]\n1***2***3";

            const int expected = 6;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_MultipleDelimiters_SummedValus()
        {
            const string values = "//[*][%]\n1*2%3";

            const int expected = 6;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAdd_Delimiters_SummedValus()
        {
            const string values = "//;[%]\n1;2%3";

            const int expected = 6;
            Calculator calculator = new Calculator();

            int actual = calculator.Add(values);

            Assert.AreEqual(expected, actual);
        }
    }
}
