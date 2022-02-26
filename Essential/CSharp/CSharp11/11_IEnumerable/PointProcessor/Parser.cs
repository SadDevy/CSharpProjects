using System;
using System.Globalization;

namespace PointProcessor
{
    /// <summary>
    /// Парсит строку в координаты x и y.
    /// </summary>
    public static class Parser
    {
        const char lineSeparator = ',';
        const int coordinatesCount = 2;
        const int indexX = 0;
        const int indexY = 1;

        /// <summary>
        /// Преобразует строку с координатами в координаты точки.
        /// </summary>
        /// <param name="line">Строка с точкой.</param>                                 
        /// <param name="point">Координаты точки.</param>                     
        /// <returns>Значение true, если параметр line успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParsePoint(string line, out Point point)
        {
            point = null;
            if (line == null)
            {
                return false;
            }

            string[] lineParts = line.Split(lineSeparator);
            if (lineParts.Length != coordinatesCount)
            {
                return false;
            }

            decimal x; 
            decimal y;
            if (TryParseCoordinate(lineParts[indexX], out x) && TryParseCoordinate(lineParts[indexY], out y))
            {
                point = new Point(x, y);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Преобразует строку с координатой в координату точки.
        /// </summary>
        /// <param name="value">Строка с координатой.</param>
        /// <param name="coordinate">Координата точки.</param>
        /// <returns>Значение true, если параметр value успешно преобразован; в противном случае — значение false.</returns>
        private static bool TryParseCoordinate(string value, out decimal coordinate)
        {
            return decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out coordinate);
        }
    }
}
