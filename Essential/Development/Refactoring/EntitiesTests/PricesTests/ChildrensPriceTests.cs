using System.Collections;
using NUnit.Framework;
using Entities.Prices;

namespace EntitiesTests.PricesTests
{
    [TestFixture]
    public class ChildrensPriceTests : PriceTests
    {
        public override Price CreatePrice()
        {
            return new ChildrensPrice();
        }

        [TestCaseSource(nameof(GetPriceCodeTestCases))]
        public override int TestGetPriceCode()
        {
            return base.TestGetPriceCode();
        }

        private static IEnumerable GetPriceCodeTestCases
        {
            get
            {
                yield return new TestCaseData().Returns(2);
            }
        }

        [TestCaseSource(nameof(GetGetChargeTestCases))]
        public override double TestGetCharge(int daysRented)
        {
            return base.TestGetCharge(daysRented);
        }

        private static IEnumerable GetGetChargeTestCases
        {
            get
            {
                yield return new TestCaseData(4).Returns(3);
                yield return new TestCaseData(1).Returns(1.5);
            }
        }

        [TestCaseSource(nameof(GetFrequentRentedPoints))]
        public override int TestGetFrequentRenterPoints(int daysRented)
        {
            return base.TestGetFrequentRenterPoints(daysRented);
        }

        private static IEnumerable GetFrequentRentedPoints
        {
            get
            {
                yield return new TestCaseData(4).Returns(1);
            }
        }
    }
}
