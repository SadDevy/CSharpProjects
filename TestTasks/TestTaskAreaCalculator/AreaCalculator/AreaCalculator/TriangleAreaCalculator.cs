using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AreaCalculatorTests")]

namespace AreaCalculator
{
    public class TriangleAreaCalculator : IAreaCalculator
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        private TriangleAreaCalculator(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static TriangleAreaCalculator Create(double a, double b, double c)
        {
            if (!CouldExists(a, b, c))
                throw new InvalidOperationException($"Треугольник не может существовать со сторонами: {a}, {b}, {c}.");

            return new TriangleAreaCalculator(a, b, c);
        }

        private static bool CouldExists(double a, double b, double c)
        {
            return (a > 0) && (b > 0) && (c > 0) &&
                   (a < b + c) && (b < c + a) && (c < a + b);
        }

        public double CalculateArea()
        {
            double halfPerimeter = CalculatePerimeter() * 0.5;
            return Math.Pow((halfPerimeter) * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C), 0.5);
        }

        private double CalculatePerimeter()
        {
            return A + B + C;
        }

        //Is It a Square Triangle?
        internal bool IsSquare()
        {
            return (A * A == B * B + C * C) ||
                   (B * B == A * A + C * C) ||
                   (C * C == A * A + B * B);

        }
    }
}
