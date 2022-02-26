using CreditGiver;
using NUnit.Framework;

namespace CreditGiverTests
{
    [TestFixture]
    public class CreditBureauCheckerTests
    {
        private static string personName;
        [SetUp]
        public void Init() => personName = "James";

        [Test]
        public void TestConstructor()
        {
            CreditBureauChecker creditBureauTests = new CreditBureauChecker(personName);
            Assert.IsNotNull(creditBureauTests);
        }

        [Test]
        public void TestCreditHistoryIsGood()
        {
            CreditBureauChecker creditBureauTests = new CreditBureauChecker(personName);

            bool actual = creditBureauTests.CreditHistoryIsGood();

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestThereIsNoUnpayed()
        {
            CreditBureauChecker creditBureauTests = new CreditBureauChecker(personName);

            bool actual = creditBureauTests.ThereIsNoUnpayed();

            Assert.IsTrue(actual);
        }
    }
}
