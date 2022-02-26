using System;
using PointProcessor;

namespace PointProcessorUI
{
    /// <summary>
    /// Запускает консольное приложение.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Запускает приложение в одном из режимов: если есть аргументы командной строки, то в режиме обработки файлов, иначе - в режиме обработки данных из консоли.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0)
                Processor.ProcessFiles(args);
            else
                Processor.ProcessConsole();
        }

    }
}
