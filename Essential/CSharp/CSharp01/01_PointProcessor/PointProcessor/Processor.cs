using System;
using System.IO;

namespace PointProcessor
{
    /// <summary>
    /// Представляет логику приложения.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Обрабатывает файлы поочередно.
        /// </summary>
        /// <param name="filenames">Файлы.</param>
        public static void ProcessFiles(string[] filenames)
        {
            foreach (string fileName in filenames)
            {
                ProcessFile(fileName);
            }
        }

        /// <summary>
        /// Обрабатывает файл.
        /// </summary>
        /// <param name="fileName">Файл.</param>
        private static void ProcessFile(string fileName)                
        {
            if (!File.Exists(fileName))
                return;

            using (TextReader reader = new StreamReader(fileName))
            {
                ProcessAllLines(reader);
            }
        }

        /// <summary>
        /// Обрабатывает ввод из консоли.
        /// </summary>
        public static void ProcessConsole()
        {
            ProcessAllLines(Console.In);
        }

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

        /// <summary>
        /// Обрабатывает все строки в потоке ввода.
        /// </summary>
        /// <param name="reader">Поток ввода.</param>
        private static void ProcessAllLines(TextReader reader)
        {
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string processedLine = ProcessLine(line);
                if (processedLine != null)
                    Console.WriteLine(processedLine);
            }

        }
    }
}
