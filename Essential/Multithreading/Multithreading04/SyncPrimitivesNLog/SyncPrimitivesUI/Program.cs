using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using NLog;
using NLog.Config;

namespace SyncPrimitivesUI
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string fileName = args.First();
            LogManager.Configuration = new XmlLoggingConfiguration(fileName);
            logger.Info("Запуск приложения.");

            logger.Debug($"Создание экземпляра {nameof(Mutex)}.");
            Mutex mutex = GetMutex(out bool existed);
            logger.Trace($"Экземпляр {nameof(Mutex)} создан.");

            logger.Trace($"Создание экземпляра {nameof(Menu)}.");
            Menu menu = GetConsoleMenu(mutex, 2000);
            logger.Trace($"Экземпляр {nameof(Menu)} создан.");

            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

            logger.Trace("Выбор операции.");
            if (existed)
            {
                DoIfNotLocked(menu);
            }
            else
            {
                logger.Trace("Приложение используется другим процессом.");
                DoIfLocked(menu);
            }

            logger.Info("Завершение приложения.");
        }

        private static void HandleUnhandledException(object obj, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            logger.Fatal(exception);
        }

        private static void DoIfLocked(Menu menu)
        {
            ShowOperationsIfLocked();
            Operation operation = InputOperationIfLocked("Введите операцию.", "Введите необходимую операцию.");
            menu.TakeUpOperation(operation);

            DoIfNotLocked(menu);
        }

        private static void DoIfNotLocked(Menu menu)
        {
            ShowOperationsIfNotLocked();
            Operation operation = InputOperationIfNotLocked("Введите операцию.", "Введите необходимую операцию.");
            menu.TakeUpOperation(operation);
        }

        private static Mutex GetMutex(out bool existed)
        {
            string guid = Marshal.GetTypeFromCLSID(
                    Assembly.GetExecutingAssembly()
                            .GetType()
                            .GUID).ToString();

            return new Mutex(true, guid, out existed);
        }

        private static Menu GetConsoleMenu(Mutex mutex, int milliseconds)
        {
            logger.Debug("Создание меню.");

            return new Menu(mutex, logger, milliseconds);
        }


        private static Operation InputOperationIfNotLocked(string inputMessage, string failureMessage)
        {
            logger.Debug("Ввод названия операции.");

            logger.Trace($"Вызов метода {nameof(InputOperationIfNotLocked)}");

            Console.WriteLine(inputMessage);

            Operation result;
            while (!OperationExists(Console.ReadLine(), out result) || result == Operation.Lock)
            {
                Console.WriteLine(failureMessage);
                logger.Warn($"Ошибка ввода операции: {result}.");
            }

            logger.Trace($"Завершение метода {nameof(InputOperationIfNotLocked)}");
            return result;
        }

        private static Operation InputOperationIfLocked(string inputMessage, string failureMessage)
        {
            logger.Debug("Ввод названия операции.");

            logger.Trace($"Вызов метода {nameof(InputOperationIfLocked)}.");

            Console.WriteLine(inputMessage);

            Operation result;
            while (!OperationExists(Console.ReadLine(), out result) || result != Operation.Lock)
            {
                Console.WriteLine(failureMessage);
                logger.Warn($"Ошибка ввода операции: {result}.");
            }

            logger.Trace($"Завершение метода {nameof(InputOperationIfLocked)}.");
            return result;
        }

        private static bool OperationExists(string element, out Operation result)
        {
            logger.Debug("Проверка существования операции.");

            logger.Trace($"Вызов метода {nameof(OperationExists)}");

            result = default;
            if (!Enum.TryParse(element, out Operation menuElement))
            {
                logger.Warn($"Операция {menuElement} не определена.");
                logger.Trace($"Завершение метода {nameof(OperationExists)}");
                return false;
            }

            result = menuElement;

            logger.Trace($"Завершение метода {nameof(OperationExists)}");
            return true;
        }

        private static void ShowOperationsIfNotLocked()
        {
            IEnumerable<string> fieldsName = GetOperationsName()
                .Where(n => n != Operation.Lock.ToString());

            foreach (string fieldName in fieldsName)
                ShowOperation(fieldName);
        }

        private static void ShowOperationsIfLocked()
        {
            ShowOperation(Operation.Lock.ToString());
        }

        private static IEnumerable<string> GetOperationsName()
        {
            return typeof(Operation)
                .GetFields()
                .Where(n => n.FieldType.Name == nameof(Operation))
                .Select(n => n.Name);
        }

        private static void ShowOperation(string menuElement)
        {
            Console.WriteLine(menuElement);
        }
    }
}
