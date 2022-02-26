using System.Reflection;
using System.Threading;
using Entities;

namespace MultithreadingUI
{
    class Program
    {
        static void Main()
        {
            const string logFileName = "Resulter.log";
            Logger logger = GetLogger(logFileName);

            var appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            LogMessage(Thread.CurrentThread.ManagedThreadId, $"Запуск приложения. Версия: {appVersion}.");
            
            Resulter resulter = GetResulter();

            Thread[] readers = GetReadThreads(resulter);
            Thread[] writers = GetWriteThreads(resulter);

            StartThreads(readers, writers);

            LogMessage(Thread.CurrentThread.ManagedThreadId, "Завершение приложения.");
        }

        private static Logger GetLogger(string fileName)
        {
            return new Logger(fileName);
        }

        private static Resulter GetResulter()
        {
            return new Resulter();
        }

        private static void LogMessage(int id, string message)
        {
            Logger.WriteTraceLine(id, message);
        }

        private static void StartThreads(Thread[] readers, Thread[] writers)
        {
            StartThreads(readers, 0, 4);
            StartThreads(writers, 0, 1);
            StartThreads(readers, 5, 7);
            StartThreads(writers, 1, 2);
            StartThreads(readers, 8, 10);
        }

        private static void StartThreads(Thread[] threads, int startIndex, int lastIndex)
        {
            for (int i = startIndex; i < lastIndex; i++)
            {
                StartThread(threads[i]);
            }
        }

        private static void StartThread(Thread thread)
        {
            LogMessage(thread.ManagedThreadId, "Запуск потока.");
            thread.Start();
        }

        private static Thread[] GetReadThreads(Resulter resulter)
        {
            const int length = 10;

            Thread[] result = new Thread[length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Thread(() =>
                {
                    var _ = resulter.SharedRes;
                });
            }

            return result;
        }

        private static Thread[] GetWriteThreads(Resulter resulter)
        {
            const int length = 2;

            Thread[] result = new Thread[length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Thread(() =>
                {
                    resulter.SharedRes = string.Empty;
                });
            }

            return result;
        }
    }
}
