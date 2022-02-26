using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using NUnit.Framework;
using Utilities.Parsers;

namespace UtilitiesTests.ParsersTests
{
    [TestFixture]
    public class GoodsParserTests
    {
        [TestCaseSource(nameof(GetTestTryParseGoodsTestCases))]
        public Goods TestTryParseGoods(string data, bool expected)
        {
            bool actual = GoodsParser.TryParseGoods(data, out Goods goods);

            Assert.AreEqual(expected, actual);
            return goods;
        }

        private static IEnumerable GetTestTryParseGoodsTestCases
        {
            get
            {
                yield return new TestCaseData("1 16lb bag of Skittles at 16.00", true).
                    Returns(new Goods("1 16lb bag of Skittles", 16.00m));
                yield return new TestCaseData("", false).
                    Returns(null);
            }
        }
    }
}
