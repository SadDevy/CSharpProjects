using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPrimitivesUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = GetMutex(out bool existed);
            Menu menu = GetConsoleMenu(mutex, 2000);

            if (existed)
            {
                DoIfNotLocked(menu);
            }
            else
            {
                DoIfLocked(menu);
            }

            Console.ReadLine();
        }

        private static void DoIfLocked(Menu menu)
        {
            try
            {
                ShowOperationsIfLocked();
                Operation operation = InputOperationIfLocked("Введите операцию.", "Введите необходимую операцию.");
                menu.TakeUpOperation(operation);

                DoIfNotLocked(menu);
            }
            catch (Exception ex)
            {
                ShowFailureMessage(ex.Message);
            }
        }

        private static void DoIfNotLocked(Menu menu)
        {
            try
            {
                ShowOperationsIfNotLocked();
                Operation operation = InputOperationIfNotLocked("Введите операцию.", "Введите необходимую операцию.");
                menu.TakeUpOperation(operation);
            }
            catch (Exception ex)
            {
                ShowFailureMessage(ex.Message);
            }
        }

        private static void ShowFailureMessage(string message)
        {
            Console.WriteLine(message);
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
            return new Menu(mutex, milliseconds);
        }


        private static Operation InputOperationIfNotLocked(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            Operation result;
            while (!OperationExists(Console.ReadLine(), out result) || result == Operation.Lock)
                Console.WriteLine(failureMessage);

            return result;
        }

        private static Operation InputOperationIfLocked(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            Operation result;
            while (!OperationExists(Console.ReadLine(), out result) || result != Operation.Lock)
                Console.WriteLine(failureMessage);

            return result;
        }

        private static bool OperationExists(string element, out Operation result)
        {
            result = default;
            if (!Enum.TryParse(element, out Operation menuElement))
                return false;

            result = menuElement;
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
