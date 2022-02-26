using System.Collections;
using System.Collections.Generic;
using Entities;
using NUnit.Framework;
using Utilities.Formatters;

namespace UtilitiesTests.FormattersTests
{
    [TestFixture]
    public class DefaultFormatterTests
    {
        [TestCaseSource(nameof(GetTestFormatTestCases))]
        public string TestFormat(Basket basket, int number)
        {
            IFormatter formatter = new DefaultFormatter();
            string formatted = formatter.Format(basket, number);

            return formatted;
        }

        private static IEnumerable GetTestFormatTestCases
        {
            get
            {
                yield return new TestCaseData(new Basket()
                {
                    Goods = new List<Goods>()
                    {
                        new Goods("1 16lb bag of Skittles", 16.00m),
                        new Goods("1 Walkman", 99.99m),
                        new Goods("1 bag of microwave Popcorn", 0.99m)
                    }
                }, 1)
                    .Returns(
@"Output 1:
1 16lb bag of Skittles: 16.0
1 Walkman: 109.99
1 bag of microwave Popcorn: 0.99
Sales Taxes: 10.0
Total: 126.98
");

                yield return new TestCaseData(null, 1).Returns(null);
            }
        }
    }
}
