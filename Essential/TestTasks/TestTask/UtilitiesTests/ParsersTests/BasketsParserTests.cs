using System.Collections;
using System.Collections.Generic;
using Entities;
using NUnit.Framework;
using Utilities.Parsers;

namespace UtilitiesTests.ParsersTests
{
    [TestFixture]
    public class BasketsParserTests
    {
        [TestCaseSource(nameof(GetTestTryParseBasketsTestCases))]
        public IEnumerable<Basket> TestTryParseBaskets(string data, bool expected)
        {
            bool actual = BasketsParser.TryParseBaskets(data, out IEnumerable<Basket> baskets);

            Assert.AreEqual(expected, actual);
            return baskets;
        }

        private static IEnumerable GetTestTryParseBasketsTestCases
        {
            get
            {
                yield return new TestCaseData(
@"Shopping Basket 1:
1 16lb bag of Skittles at 16.00
1 Walkman at 99.99
1 bag of microwave Popcorn at 0.99

Shopping Basket 2:
1 imported bag of Vanilla-Hazelnut Coffee at 11.00
1 Imported Vespa at 15,001.25

Shopping Basket 3:
1 imported crate of Almond Snickers at 75.99
1 Discman at 55.00
1 Imported Bottle of Wine at 10.00
1 300# bag of Fair-Trade Coffee at 997.99", true)
                    .Returns(new List<Basket>()
                    {
                        new Basket() 
                        {
                            Goods = new List<Goods>()
                            {
                                new Goods("1 16lb bag of Skittles", 16.00m),
                                new Goods("1 Walkman", 99.99m),
                                new Goods("1 bag of microwave Popcorn", 0.99m)
                            }
                        },
                        new Basket()
                        {
                            Goods = new List<Goods>()
                            {
                                new Goods("1 imported bag of Vanilla-Hazelnut Coffee", 11.00m),
                                new Goods("1 Imported Vespa", 15001.25m)
                            }
                        },
                        new Basket()
                        {
                            Goods = new List<Goods>()
                            {
                                new Goods("1 imported crate of Almond Snickers", 75.99m),
                                new Goods("1 Discman", 55.00m),
                                new Goods("1 Imported Bottle of Wine", 10.00m),
                                new Goods("1 300# bag of Fair-Trade Coffee", 997.99m)
                            }
                        }
                    });

                yield return new TestCaseData("", false).Returns(null);
            }
        }
    }
}
