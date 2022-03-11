using System;

namespace AreaCalculator
{
    public class CircleAreaCalculator : IAreaCalculator
    {
        public double Radius { get; }

        private CircleAreaCalculator(double radius)
        {
            Radius = radius;
        }

        public static CircleAreaCalculator Create(double radius)
        {
            if (!CouldExists(radius))
                throw new InvalidOperationException($"Круг не может существовать с радиусом {radius}.");

            return new CircleAreaCalculator(radius);
        }

        private static bool CouldExists(double radius)
        {
            return radius > 0;
        }

        public double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }
}
