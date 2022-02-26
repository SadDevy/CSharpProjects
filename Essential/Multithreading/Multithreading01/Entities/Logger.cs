using System;
using System.Diagnostics;
using System.IO;

namespace Entities
{
    public class Logger
    {
        private static TextWriter writer;

        public Logger(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            SetWriter(fileName);

            SetupListener();
        }

        public static void WriteTraceLine(int id, string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            string dateTime = DateTime.Now.ToString("yy.mm.dd hh:mm:ss.fff");
            Trace.WriteLine($"[{dateTime}] [Thread Id: {id}]: {message}");

            writer.Flush();
        }

        private void SetupListener()
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        private void SetWriter(string fileName)
        {
            writer = new StreamWriter(fileName, append: false);
        }


        ~Logger()
        {
            writer?.Dispose();
        }
    }
}
