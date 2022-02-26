using System.Collections;
using NUnit.Framework;
using Entities.Prices;

namespace EntitiesTests.PricesTests
{
    [TestFixture]
    public class RegularPriceTests : PriceTests
    {
        public override Price CreatePrice()
        {
            return new RegularPrice();
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
                yield return new TestCaseData().Returns(0);
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
                yield return new TestCaseData(3).Returns(3.5);
                yield return new TestCaseData(1).Returns(2);
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
