namespace EmployeeParser
{
    public class Employee
    {
        private const byte taxBorder = 10;
        private const byte defaultTaxRate = 13;
        private const int defaultSalary = 500;
        private const byte defaultSalaryPercent = 10;
        private const double defaultCoeff = 0.96;

        public string Surname { get; private set; }
        public Rank Rank { get; private set; }
        public Department Department { get; private set; }

        public int Salary { get; set; }
        public byte SalaryPercent { get; set; }
        public double Coeff { get; set; }

        public Employee(string surname, Rank rank, Department department, int salary = defaultSalary, byte salaryPersent = defaultSalaryPercent, double coeff = defaultCoeff)
        {
            Surname = surname;
            Rank = rank;
            Department = department;

            Salary = salary;
            SalaryPercent = salaryPersent;
            Coeff = coeff;
        }

        public decimal CalculateBonus()
        {
            //1) (decimal)(Salary * SalaryPercent / 100.0 * Coeff);
            //2) Salary * SalaryPercent / 100m * (decimal)Coeff;
            //3) (decimal)(Salary * SalaryPercent / 100f * Coeff);
            //4) (decimal)(Salary * SalaryPercent / 100d * Coeff);
            return Salary * SalaryPercent / 100m * (decimal)Coeff;
        }

        public bool PayTax(ref decimal bonus, out byte taxRate)
        {
            taxRate = defaultTaxRate;

            if (bonus < taxBorder)
                return false;

            bonus -= bonus * taxRate / 100;
            return true;
        }
    }
}
