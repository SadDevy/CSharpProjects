using CreditGiver;
using NUnit.Framework;

namespace CreditGiverTests
{
    [TestFixture]
    public class WorldOfTanksCheckerTests
    {
        private static string personName;
        [SetUp]
        public void Init() => personName = "James";

        [Test]
        public void TestConstructor()
        {
            WorldOfTanksChecker worldOfTanksChecker = new WorldOfTanksChecker(personName);
            Assert.IsNotNull(worldOfTanksChecker);
        }

        [Test]
        public void TestWasOnlineMoreThanMonthAgo()
        {
            WorldOfTanksChecker worldOfTanksChecker = new WorldOfTanksChecker(personName);

            bool actual = worldOfTanksChecker.WasOnlineMoreThanMonthAgo();

            Assert.IsTrue(actual);
        }
    }
}
