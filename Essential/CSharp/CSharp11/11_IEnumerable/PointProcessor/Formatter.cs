using System;

namespace PointProcessor
{
    /// <summary>
    /// Форматирует координаты местоположения объекта.
    /// </summary>
    public class Formatter
    {
        /// <summary>
        /// Форматирует координаты точки.
        /// </summary>
        /// <param name="point">Координаты точки.</param>
        /// <returns>Копия format, где все элементы формата заменены строками координат.</returns>
        public static string Format(Point point)
        {
            if (point == null)
                return null;

            decimal fractionalX;
            decimal integerX = GetIntegerPart(point.X, out fractionalX);

            decimal fractionalY;
            decimal integerY = GetIntegerPart(point.Y, out fractionalY);

            return string.Format("X:{0,5:###0.}{1,-5:.0###} Y:{2,5:###0.}{3,-5:.0###}", integerX, fractionalX, integerY, fractionalY);
        }

        /// <summary>
        /// Возвращает целую и дробную части числа.
        /// </summary>
        /// <param name="value">Число.</param>
        /// <param name="fractionalPart">Дробная часть.</param>
        /// <returns>Целая часть.</returns>
        private static decimal GetIntegerPart(decimal value, out decimal fractionalPart)
        {
            decimal integerPart = Math.Truncate(value);
            fractionalPart = value - integerPart;
            return integerPart;
        }
    }
}
