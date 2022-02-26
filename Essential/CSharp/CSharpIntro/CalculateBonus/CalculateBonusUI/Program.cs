using System;
using EmployeeParser;

namespace CalculateBonusUI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Employee employee;
            ReturnCode returnedCode = Parser.TryParseEmployee(args, out employee);
            if (returnedCode == ReturnCode.Success)
            {
                decimal bonus = employee.CalculateBonus();
                byte taxRate;
                bool isTaxed = employee.PayTax(ref bonus, out taxRate);

                ShowResult(employee, bonus, taxRate, isTaxed);
            }

            return (int)returnedCode;
        }

        private static void ShowResult(Employee employee, decimal bonus, decimal taxRate, bool isTaxed)
        {
            Console.WriteLine("Сотрудник: {0}.", employee.Surname.ToUpper());
            Console.WriteLine("Отдел: {0}.", employee.Department);
            Console.WriteLine("Должность: {0}.", employee.Rank);

            string paymentMessageFormat = isTaxed ? "Включая налог: {0}%." : "Не включая налог: {0}%.";
            Console.WriteLine(paymentMessageFormat, taxRate);
            Console.WriteLine("Премия: {0}.", bonus);
        }
    }
}
