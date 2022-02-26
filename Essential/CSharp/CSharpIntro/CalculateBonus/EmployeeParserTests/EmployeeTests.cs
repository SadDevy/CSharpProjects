using NUnit.Framework;
using EmployeeParser;

namespace EmployeeParserTests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void TestContructor_OnlyRequiredParameter_Success()
        {
            const Rank rank = Rank.Director;
            const Department department = Department.RD;
            const string surname = "Сидоров";
            const int defaultSalary = 500;
            const byte defaultSalaryPercent = 10;
            const double defaultCoeff = 0.96;

            Employee employee = new Employee(surname, rank, department);

            Assert.NotNull(employee);
            Assert.AreEqual(surname, employee.Surname);
            Assert.AreEqual(rank, employee.Rank);
            Assert.AreEqual(department, employee.Department);

            Assert.AreEqual(defaultSalary, employee.Salary);
            Assert.AreEqual(defaultSalaryPercent, employee.SalaryPercent);
            Assert.AreEqual(defaultCoeff, employee.Coeff);
        }

        [Test]
        public void TestContructor_AllParameters_Success()
        {
            const Rank rank = Rank.Director;
            const Department department = Department.RD;
            const string surname = "Сидоров";
            const int salary = 300;
            const byte salaryPercent = 9;
            const double coeff = 0.3;

            Employee employee = new Employee(surname, rank, department, salary, salaryPercent, coeff);

            Assert.NotNull(employee);
            Assert.AreEqual(surname, employee.Surname);
            Assert.AreEqual(rank, employee.Rank);
            Assert.AreEqual(department, employee.Department);

            Assert.AreEqual(salary, employee.Salary);
            Assert.AreEqual(salaryPercent, employee.SalaryPercent);
            Assert.AreEqual(coeff, employee.Coeff);
        }

        [Test]
        public void TestCalculateBonus_DefaultSalary_Success()
        {
            Employee employee = new Employee("Сидоров", Rank.Employee, Department.QA);
            const decimal expected = 48m;

            decimal actual = employee.CalculateBonus();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestPayTax_BonusMoreTaxBorder_Success()
        {
            Employee employee = new Employee("Сидоров", Rank.Employee, Department.QA, 1200, 10, 0.1);
            const decimal expectedTaxRate = 13m;
            const decimal expected = 10.44m;
            decimal actual = employee.CalculateBonus();

            byte actualTaxRate;
            bool result = employee.PayTax(ref actual, out actualTaxRate);

            Assert.AreEqual(expectedTaxRate, actualTaxRate);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestPayTax_BonusIsTaxBorder_Success()
        {
            Employee employee = new Employee("Сидоров", Rank.Employee, Department.QA, 1000, 10, 0.1);
            const decimal expectedTaxRate = 13m;
            const decimal expected = 8.7m;
            decimal actual = employee.CalculateBonus();

            byte actualTaxRate;
            bool result = employee.PayTax(ref actual, out actualTaxRate);

            Assert.AreEqual(expectedTaxRate, actualTaxRate);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestPayTax_BonusLessTaxBorder_Success()
        {
            Employee employee = new Employee("Сидоров", Rank.Employee, Department.QA, 300, 10, 0.1);
            const decimal expectedRate = 13m;
            const decimal expected = 3m;
            decimal actual = employee.CalculateBonus();

            byte actualTaxRate;
            bool result = employee.PayTax(ref actual, out actualTaxRate);

            Assert.AreEqual(expectedRate, actualTaxRate);
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(result);
        }

    }
}
