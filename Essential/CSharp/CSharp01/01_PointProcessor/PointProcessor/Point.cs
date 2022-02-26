using System;

namespace PointProcessor
{
    /// <summary>
    /// Содержит информацию о каждой координате.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Координата X.
        /// </summary>
        public decimal X { get; private set; }
        /// <summary>
        /// Координата Y.
        /// </summary>
        public decimal Y { get; private set; }

        /// <summary>
        /// Создает точку.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Сравнивает эквивалентость двух точек.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Point point = obj as Point;
            if (ReferenceEquals(point, null))
            {
                return false;
            }

            return X == point.X && Y == point.Y;
        }

        /// <summary>
        /// Возвращает хэш-код точки.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// Стандартно форматирует координаты точки.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }
    }
}
