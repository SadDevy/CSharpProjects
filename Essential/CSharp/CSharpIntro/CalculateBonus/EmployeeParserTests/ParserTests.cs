using NUnit.Framework;
using EmployeeParser;

namespace EmployeeParserTests
{ 
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void TestTryParseRequiredParameter_TwelveSidorov_Success()
        {
            const string line = "12,Сидоров";

            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.NotNull(employee);
        }

        [Test]
        public void TestTryParseRequiredParameter_RequiredParameterNull_Failure()
        {
            const string line = "Сидоров";
            
            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidFormatRequiredParameter, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryRequiredParameter_DepartmentEight_Failure()
        {
            const string line = "81,Сидоров";

            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.ErrorDepartmentNotExists, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryRequiredParameter_DepartmentLetter_Failure()
        {
            const string line = "a1,Сидоров";

            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidDepartment, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryRequiredParameter_RankEight_Failure()
        {
            const string line = "18,Сидоров";

            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.ErrorRankNotExists, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryRequiredParameter_RankLetter_Failure()
        {
            const string line = "1a,Сидоров";

            Employee employee;
            ReturnCode result = Parser.TryParseRequiredParameter(line, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidRank, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryParseDepartment_One_Success()
        {
            const char code = '1';
            Department expected = Department.QA;

            Department actual;
            ReturnCode result = Parser.TryParseDepartment(code, out actual);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseDepartment_Eight_Failure()
        {
            const char code = '8';
            Department expected = Department.NotDefined;

            Department actual;
            ReturnCode result = Parser.TryParseDepartment(code, out actual);

            Assert.AreEqual(ReturnCode.ErrorDepartmentNotExists, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseDepartment_Letter_Failure()
        {
            const char code = 'a';
            Department expected = Department.NotDefined;

            Department actual;
            ReturnCode result = Parser.TryParseDepartment(code, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidDepartment, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseRank_One_Success()
        {
            const char code = '1';
            const Rank expected = Rank.Lead;

            Rank actual;
            ReturnCode result = Parser.TryParseRank(code, out actual);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseRank_Eight_Failure()
        {
            const char code = '8';
            Rank expected = Rank.NotDefined;

            Rank actual;
            ReturnCode result = Parser.TryParseRank(code, out actual);

            Assert.AreEqual(ReturnCode.ErrorRankNotExists, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseRank_Letter_Failure()
        {
            const char code = 'a';
            Rank expected = Rank.NotDefined;

            Rank actual;
            ReturnCode result = Parser.TryParseRank(code, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidRank, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalary_OneHundred_Success()
        {
            const string line = "100";
            int expected = 100;

            int actual;
            ReturnCode result = Parser.TryParseSalary(line, out actual);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalary_Letter_Failure()
        {
            const string line = "a";
            int expected = 0;

            int actual;
            ReturnCode result = Parser.TryParseSalary(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalary, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalary_Negative_Failure()
        {
            const string line = "-10";
            int expected = 0;

            int actual;
            ReturnCode result = Parser.TryParseSalary(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalary, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalary_Empty_Failure()
        {
            string line = string.Empty;
            int expected = 0;

            int actual;
            ReturnCode result = Parser.TryParseSalary(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalary, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalaryPercent_Fifty_Success()
        {
            const string line = "50";
            byte expected = 50;

            byte actual;
            ReturnCode result = Parser.TryParseSalaryPercent(line, out actual);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalaryPercent_Letter_Failure()
        {
            const string line = "a";
            byte expected = 0;

            byte actual;
            ReturnCode result = Parser.TryParseSalaryPercent(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalaryPercent, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalaryPercent_Negative_Failure()
        {
            const string line = "-10";
            byte expected = 0;

            byte actual;
            ReturnCode result = Parser.TryParseSalaryPercent(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalaryPercent, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseSalaryPercent_Empty_Failure()
        {
            string line = string.Empty;
            byte expected = 0;

            byte actual;
            ReturnCode result = Parser.TryParseSalaryPercent(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalaryPercent, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseCoeff_ZeroPointOne_Success()
        {
            const string line = "0.1";
            double expected = 0.1;

            double actual;
            ReturnCode result = Parser.TryParseCoeff(line, out actual);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseCoeff_Letter_Failure()
        {
            const string line = "a";
            double expected = 0;

            double actual;
            ReturnCode result = Parser.TryParseCoeff(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidCoeff, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseCoeff_Negative_Failure()
        {
            const string line = "-0.1";
            double expected = 0;

            double actual;
            ReturnCode result = Parser.TryParseCoeff(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidCoeff, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseCoeff_Empty_Failure()
        {
            string line = string.Empty;
            double expected = 0;

            double actual;
            ReturnCode result = Parser.TryParseCoeff(line, out actual);

            Assert.AreEqual(ReturnCode.ErrorInvalidCoeff, result);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTryParseEmployee_Corrected_Success()
        {
            string[] lineParts = { "12,Сидоров", "300", "7", "0.87" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.IsNotNull(employee);
        }

        [Test]
        public void TestTryParseEmployee_CoeffEmpty_Success()
        {
            string[] lineParts = { "12,Сидоров", "300", "7" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.Success, result);
            Assert.IsNotNull(employee);
        }

        [Test]
        public void TestTryParseEmployee_RequiredParameterEmpty_Failure()
        {
            string[] lineParts = { "300", "5", "0.9" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidFormatRequiredParameter, result);
            Assert.IsNull(employee);
        }

        [Test]
        public void TestTryParseEmployee_CoeffLetter_Failure()
        {
            string[] lineParts = { "12,Сидоров", "300", "7", "a" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidCoeff, result);
        }

        [Test]
        public void TestTryParseEmployee_SalatyPercentLetter_Failure()
        {
            string[] lineParts = { "12,Сидоров", "300", "a", "0.9" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalaryPercent, result);
        }

        [Test]
        public void TestTryParseEmployee_SalaryLetter_Failure()
        {
            string[] lineParts = { "12,Сидоров", "a", "5", "0.9" };

            Employee employee;
            ReturnCode result = Parser.TryParseEmployee(lineParts, out employee);

            Assert.AreEqual(ReturnCode.ErrorInvalidSalary, result);
        }
    }
}
