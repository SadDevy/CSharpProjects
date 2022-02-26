using System.Collections;
using NUnit.Framework;
using Entities.Prices;

namespace EntitiesTests.PricesTests
{
    [TestFixture]
    public class NewReleasePriceTests : PriceTests
    {
        public override Price CreatePrice()
        {
            return new NewReleasePrice();
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
                yield return new TestCaseData().Returns(1);
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
                yield return new TestCaseData(4).Returns(12);
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
                yield return new TestCaseData(3).Returns(2);
                yield return new TestCaseData(0).Returns(1);
            }
        }
    }
}
