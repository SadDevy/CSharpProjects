using Matrices;
using System;

namespace MatrixUI
{
    class Program
    {
        static void Main()
        {

            try
            {
                Matrix a = InputMatrix("Ввод первой матрицы:");
                Matrix b = InputMatrix("Ввод второй матрицы:");

                ShowResult("Матрица a:", a);
                ShowResult("Матрица b:", b);

                Summarize(a, b);
                Subtract(a, b);
                Multiply(a, b);
            }
            catch (InvalidMatrixOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Summarize(Matrix a, Matrix b)
        {
            try
            {
                Matrix summation = a + b;
                ShowResult("Сумма:", summation);
            }
            catch (InvalidMatrixOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ShowResult(string message, Matrix a)
        {
            Console.WriteLine(message);
            Console.WriteLine(a);
        }

        private static void Subtract(Matrix a, Matrix b)
        {
            try
            {
                Matrix substraction = a - b;
                ShowResult("Разность:", substraction);
            }
            catch (InvalidMatrixOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Multiply(Matrix a, Matrix b)
        {
            try
            {
                Matrix multiplication = a - b;
                ShowResult("Произведение:", multiplication);
            }
            catch (InvalidMatrixOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static double InputNumber(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            double value;
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine(failureMessage);
            }

            return value;
        }

        private static int InputInteger(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            double value;
            while (!double.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.WriteLine(failureMessage);
            }

            return (int)value;
        }

        private static Matrix InputMatrix(string inputMessage)
        {
            Console.WriteLine(inputMessage);

            int rowCount = InputInteger("Введите количество строк.", "Введите пложительное целое число.");
            int colCount = InputInteger("Введите количество столбцов.", "Введите пложительное целое число.");

            double[,] result = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    string inputNumberMessage = string.Format("Введите a[{0}, {1}].", i, j);
                    result[i, j] = InputNumber(inputNumberMessage, "Введите число.");
                }
            }

            return new Matrix(result);
        }
    }
}
