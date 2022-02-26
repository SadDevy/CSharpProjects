using System;
using System.Collections.Generic;

namespace PointProcessor
{
    /// <summary>
    /// Представляет логику приложения.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Обрабатывает координаты точки.
        /// </summary>
        /// <param name="line">Координаты точки.</param>
        /// <returns>Обработанные коориднаты точки.</returns>
        public static string ProcessLine(string line)
        {
            Point point;
            if (Parser.TryParsePoint(line, out point))
                return Formatter.Format(point);

            return null;
        }

        public static void ProcessLines(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                string processedLine = ProcessLine(line);
                if (processedLine != null)
                    Console.WriteLine(processedLine);
            }
        }

        public static void ProcessLines(Func<IEnumerable<string>> getLines)
        {
            ProcessLines(getLines());
        }
    }
}
