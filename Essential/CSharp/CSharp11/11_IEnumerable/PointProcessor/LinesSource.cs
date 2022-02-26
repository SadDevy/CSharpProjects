using System;
using System.Collections.Generic;
using System.IO;

namespace PointProcessor
{
    public static class LinesSource
    {
        public static IEnumerable<string> FromFiles(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException("Файл не найден.");

                using (TextReader reader = new StreamReader(fileName))
                {
                    foreach (string line in GetSource(reader))
                        yield return line;
                }
            }
        }

        public static IEnumerable<string> FromConsole()
        {
            return GetSource(Console.In);
        }

        private static IEnumerable<string> GetSource(TextReader reader)
        {
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                yield return line;
            }
        }

    }
}
