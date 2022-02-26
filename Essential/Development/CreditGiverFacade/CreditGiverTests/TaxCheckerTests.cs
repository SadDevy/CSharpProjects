using CreditGiver;
using NUnit.Framework;
using System.Collections;

namespace CreditGiverTests
{
    [TestFixture]
    public class TaxCheckerTests
    {
        [TestCaseSource(nameof(GetTestGreaterThanPayedForLastYear))]
        public bool TestGreaterThanPayedForLastYear(decimal creditSum)
        {
            return TaxChecker.GreaterThanPayedForLastYear(creditSum);
        }

        private static IEnumerable GetTestGreaterThanPayedForLastYear
        {
            get
            {
                yield return new TestCaseData(500m).Returns(false);
                yield return new TestCaseData(1500m).Returns(true);
            }
        }
    }
}
