using System;

namespace Models
{
    /// <summary>
    /// Содержит информацию о треугольнике.
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// Сторона A.
        /// </summary>
        public double A { get; private set; }

        /// <summary>
        /// Сторона B.
        /// </summary>
        public double B { get; private set; }

        /// <summary>
        /// Сторона C.
        /// </summary>
        public double C { get; private set; }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="a">Сторона a.</param>
        /// <param name="b">Сторона b.</param>
        /// <param name="c">Сторона c.</param>
        private Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static Triangle CreateTriangle(double a, double b, double c)
        {
            if (CouldExist(a, b, c))
            {
                return new Triangle(a, b, c);
            }

            throw new InvalidOperationException(string.Format("Треугольник не может существовать со сторонами: {0}, {1}, {2}.", a, b, c));
        }

        /// <summary>
        /// Вычисляет периметр.
        /// </summary>
        /// <returns>Значение периметра.</returns>
        public double CalculatePerimeter()
        {
            return A + B + C;
        }

        /// <summary>
        /// Вычисляет площадь.
        /// </summary>
        /// <returns>Значение площади.</returns>
        public double CalculateArea()
        {
            double halfPerimeter = CalculatePerimeter() * 0.5;
            return Math.Pow((halfPerimeter) * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C), 0.5);
        }

        /// <summary>
        /// BBB.
        /// </summary>
        /// <param name="a">Сторона A.</param>
        /// <param name="b">Сторона B.</param>
        /// <param name="c">Сторона C.</param>
        /// <returns>Значение true, если CCC, иначе - false.</returns>
        public static bool CouldExist(double a, double b, double c)
        {
            return (a > 0) && (b > 0) && (c > 0) &&
                (a < b + c) && (b < c + a) && (c < a + b);
        }
    }
}
