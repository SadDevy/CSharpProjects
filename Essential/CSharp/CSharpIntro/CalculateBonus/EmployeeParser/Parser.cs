using System;
using System.Runtime.CompilerServices;
using System.Globalization;

[assembly: InternalsVisibleTo("EmployeeParserTests")]

namespace EmployeeParser
{
    public static class Parser
    {
        const int requiredParameterPartsCount = 2;

        const char lineSeparator = ',';

        const int requiredParameterIndex = 0;
        const int salaryIndex = 1;
        const int salaryPercentIndex = 2;
        const int coeffIndex = 3;

        const int employeeCodeIndex = 0;
        const int surnameIndex = 1;
        const int departmentIndex = 0;
        const int rankIndex = 1;

        const int employeeCodeLength = 2;

        static internal ReturnCode TryParseRank(char code, out Rank rank)
        {
            rank = Rank.NotDefined;

            Rank parsedRank;
            if (!Enum.TryParse(code.ToString(), out parsedRank))
                return ReturnCode.ErrorInvalidRank;

            if (!Enum.IsDefined(typeof(Rank), parsedRank))
                return ReturnCode.ErrorRankNotExists;

            rank = parsedRank;
            return ReturnCode.Success;
        }

        static internal ReturnCode TryParseDepartment(char code, out Department department)
        {
            department = Department.NotDefined;

            Department parsedDepartment;
            if (!Enum.TryParse(code.ToString(), out parsedDepartment))
                return ReturnCode.ErrorInvalidDepartment;

            if (!Enum.IsDefined(typeof(Department), parsedDepartment))
                return ReturnCode.ErrorDepartmentNotExists;

            department = parsedDepartment;
            return ReturnCode.Success;
        }

        static internal ReturnCode TryParseRequiredParameter(string value, out Employee employee)
        {
            employee = null;

            string[] valueParts = value.Split(lineSeparator);
            if (valueParts.Length != requiredParameterPartsCount)
                return ReturnCode.ErrorInvalidFormatRequiredParameter;

            string employeeCode = valueParts[employeeCodeIndex];
            if (employeeCode.Length != employeeCodeLength)
                return ReturnCode.ErrorInvalidEmployeeCodeFormat;

            string surname = valueParts[surnameIndex];
            if (string.IsNullOrEmpty(surname))
                return ReturnCode.ErrorEmptySurname;

            Department department;
            ReturnCode departmentReturnedCode = TryParseDepartment(employeeCode[departmentIndex], out department);
            if (departmentReturnedCode != ReturnCode.Success)
                return departmentReturnedCode;

            Rank rank;
            ReturnCode rankReturnedCode = TryParseRank(employeeCode[rankIndex], out rank);
            if (rankReturnedCode != ReturnCode.Success)
                return rankReturnedCode;

            employee = new Employee(surname, rank, department);
            return ReturnCode.Success;
        }

        static internal ReturnCode TryParseSalary(string value, out int salary)
        {
            salary = 0;

            int parsedSalary;
            if (!int.TryParse(value, out parsedSalary) || parsedSalary < 0)
                return ReturnCode.ErrorInvalidSalary;

            salary = parsedSalary;
            return ReturnCode.Success;
        }

        static internal ReturnCode TryParseSalaryPercent(string value, out byte salaryPercent)
        {
            salaryPercent = 0;

            byte parsedSalatyPercent;
            if (!byte.TryParse(value, out parsedSalatyPercent) || parsedSalatyPercent < 0)
                return ReturnCode.ErrorInvalidSalaryPercent;

            salaryPercent = parsedSalatyPercent;
            return ReturnCode.Success;
        }

        static internal ReturnCode TryParseCoeff(string value, out double coeff)
        {
            coeff = 0;

            double parsedCoeff;
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedCoeff) || parsedCoeff < 0)
                return ReturnCode.ErrorInvalidCoeff;

            coeff = parsedCoeff;
            return ReturnCode.Success;
        }

        public static ReturnCode TryParseEmployee(string[] values, out Employee employee)
        {
            employee = null;

            if (values == null)
                return ReturnCode.ErrorEmptyParameters;

            if (values.Length == requiredParameterIndex)
                return ReturnCode.ErrorNotRequiredParameter;

            ReturnCode requiredParameterReturnedCode = TryParseRequiredParameter(values[requiredParameterIndex], out employee);
            if (requiredParameterReturnedCode != ReturnCode.Success)
                return requiredParameterReturnedCode;

            if (values.Length > salaryIndex)
            {
                int salary;
                ReturnCode salaryReturnedCode = TryParseSalary(values[salaryIndex], out salary);
                if (salaryReturnedCode != ReturnCode.Success)
                    return salaryReturnedCode;

                employee.Salary = salary;
            }

            if (values.Length > salaryPercentIndex)
            {
                byte salaryPercent;
                ReturnCode salaryPercentReturnedCode = TryParseSalaryPercent(values[salaryPercentIndex], out salaryPercent);
                if (salaryPercentReturnedCode != ReturnCode.Success)
                    return salaryPercentReturnedCode;

                employee.SalaryPercent = salaryPercent;
            }

            if (values.Length > coeffIndex)
            {
                double coeff;
                ReturnCode coeffReturnedCode = TryParseCoeff(values[coeffIndex], out coeff);
                if (coeffReturnedCode != ReturnCode.Success)
                    return coeffReturnedCode;

                employee.Coeff = coeff;
            }

            return ReturnCode.Success;
        }
    }
}
