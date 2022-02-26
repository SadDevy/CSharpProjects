using CreditGiver;
using NUnit.Framework;
using System.Collections;

namespace CreditGiverTests
{
    [TestFixture]
    public class CreditProviderTests
    {
        private static string personName;
        [SetUp]
        public void Init() => personName = "James";

        [Test]
        public void TestConstructor()
        {
            CreditProvider creditProvider = new CreditProvider();
            Assert.IsNotNull(creditProvider);
        }

        [TestCaseSource(nameof(GetTestGiveCreditTestCases))]
        public bool TestGiveCredit(decimal creditSum)
        {
            CreditProvider creditProvider = new CreditProvider();
            return creditProvider.GiveCredit(personName, creditSum);
        }

        private static IEnumerable GetTestGiveCreditTestCases
        {
            get
            {
                yield return new TestCaseData(500m).Returns(false);
                yield return new TestCaseData(1500m).Returns(true);
            }
        }
    }
}
