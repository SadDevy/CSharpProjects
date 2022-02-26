using System;
using Models;

namespace TriangleUI
{
    class Program
    {
        static void Main()
        {
            double a = InputSide("Сторона A:");
            double b = InputSide("Сторона B:");
            double c = InputSide("Сторона C:");

            try
            {
                Triangle triangle = Triangle.CreateTriangle(a, b, c);
                
                double perimeter = triangle.CalculatePerimeter();
                double area = triangle.CalculateArea();

                ShowResult(triangle, perimeter, area);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static double InputSide(string inputMessage)
        {
            Console.WriteLine(inputMessage);

            double side;
            while (!double.TryParse(Console.ReadLine(), out side) || side <= 0)
                Console.WriteLine("Введите положительное вещественное число.");

            return side;
        }

        private static void ShowResult(Triangle triangle, double perimeter, double area)
        {
            Console.WriteLine("Треугольник со сторонами: {0}, {1}, {2}.", triangle.A, triangle.B, triangle.C);
            Console.WriteLine("Периметр: {0}.", perimeter);
            Console.WriteLine("Площадь: {0}.", area);
        }
    }
}
