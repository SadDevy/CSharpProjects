using CreditGiver;
using NUnit.Framework;

namespace CreditGiverTests
{
    [TestFixture]
    public class LabourMinistryCheckerTests
    {
        private static string personName;
        [SetUp]
        public void Init() => personName = "James";

        [Test]
        public void TestConstructor()
        {
            LabourMinistryChecker labourMinistryChecker = new LabourMinistryChecker(personName);
            Assert.IsNotNull(labourMinistryChecker);
        }

        [Test]
        public void TestIsEmployee()
        {
            LabourMinistryChecker labourMinistryChecker = new LabourMinistryChecker(personName);

            bool actual = labourMinistryChecker.IsEmployee();

            Assert.IsTrue(actual);
        }
    }
}
