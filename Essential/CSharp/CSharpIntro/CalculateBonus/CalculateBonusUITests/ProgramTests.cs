using NUnit.Framework;
using CalculateBonusUI;

namespace CalculateBonusUITests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestMain_OptionalParameters_Success()
        {
            string[] values = { "12,Сидоров" };
            const string expected =
@"Сотрудник: СИДОРОВ.
Отдел: QA.
Должность: Manager.
Включая налог: 13%.
Премия: 41,76.
";

            using (ConsoleOutput actual = new ConsoleOutput())
            {
                Program.Main(values);

                Assert.AreEqual(expected, actual.GetOuput());
            }
        }

        [Test]
        public void TestMain_SalaryTen_Success()
        {
            string[] values = { "12,Сидоров", "10" };
            const string expected =
@"Сотрудник: СИДОРОВ.
Отдел: QA.
Должность: Manager.
Не включая налог: 13%.
Премия: 0,96.
";

            using (ConsoleOutput actual = new ConsoleOutput())
            {
                Program.Main(values);

                Assert.AreEqual(expected, actual.GetOuput());
            }
        }

        [Test]
        public void TestMain_EmptyParameters_Success()
        {
            string[] values = null;
            const int expected = -13;

            int actual;
            using (ConsoleOutput result = new ConsoleOutput())
            {
                actual = Program.Main(values);

                Assert.IsEmpty(result.GetOuput());
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
