//using System;
//using System.Globalization;
//using Models;

//namespace TriangleParser
//{
//    /// <summary>
//    /// Парсит строку в треугольник.
//    /// </summary>
//    public static class Parser
//    {
//        const char lineSeparator = ',';
//        const int sidesCount = 3;
//        const int indexA = 0;
//        const int indexB = 1;
//        const int indexC = 2;

//        /// <summary>
//        /// Преобразует строку со сторонами треугольника в треугольник.
//        /// </summary>
//        /// <param name="line">Стороны треугольника.</param>
//        /// <param name="triangle">Треугольник.</param>
//        /// <returns>Значение true, если line успешно преобразован, иначе - false.</returns>
//        public static bool TryParseTriangle(string line, out Triangle triangle)
//        {
//            triangle = null;

//            if (line == null)
//                return false;

//            string[] lineParts = line.Split(lineSeparator);
//            if (lineParts.Length != sidesCount)
//                return false;

//            double a;
//            double b;
//            double c;
//            if (!TryParseSide(lineParts[indexA], out a) || !TryParseSide(lineParts[indexB], out b) || !TryParseSide(lineParts[indexC], out c))
//            {
//                return false;
//            }

//            if (Triangle.IsTriangle(a, b, c) && Triangle.ArePositive(a, b, c))
//            {
//                triangle = new Triangle(a, b, c);
//                return true;
//            }
            
//            return false;
//        }

//        private static bool TryParseSide(string value, out double side)
//        {
//            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out side);
//        }

        
//    }
//}
