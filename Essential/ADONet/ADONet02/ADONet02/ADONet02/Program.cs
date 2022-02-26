using System;
using System.Configuration;
using System.Data.SqlClient;
using Utilities;

namespace ADONet02
{
    class Program
    {
        static void Main()
        {
            int id = InputId("Введите id теста.", "Введите целое число.");
            string fileName = InputFileName("Введите название файла.");
            Guid guid = InputGuid("Введите guid теста.", $"Введите строку формата {Guid.Empty} в шестнадцатиричной системе счисления.");

            try
            {
                ExportToXml(id, fileName);
                ImportFromXml(fileName);
                RemoveTest(guid);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private static void ShowError(Exception ex)
        {
            string message = string.Empty;
            if (ex is SqlException)
                message = $"Ошибка работы с БД: {ex.Message}";
            else if (ex is InvalidOperationException)
                message = $"Ошибка выполнения операции: {ex.Message}";
            else if (ex is ConfigurationErrorsException)
                message = $"Ошибка работы с файлом конфигурации: {ex.Message}";
            else
                message = ex.Message;

            Console.WriteLine(message);
        }

        private static void RemoveTest(Guid guid)
        {
            Remover remover = Remover.CreateRemover();
            remover.RemoveTest(guid);
        }

        private static void ImportFromXml(string fileName)
        {
            Importer importer = Importer.CreateImporter();
            importer.ImportTestFromXml(fileName);
        }

        private static void ExportToXml(int id, string fileName)
        {
            Exporter exporter = Exporter.CreateExporter();
            exporter.ExportTestToXml(id, fileName);
        }

        private static Guid InputGuid(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            Guid guid;
            while (!Guid.TryParse(Console.ReadLine(), out guid))
            {
                Console.WriteLine(failureMessage);
            }

            return guid;
        }

        private static string InputFileName(string inputMessage)
        {
            Console.WriteLine(inputMessage);

            string fileNameFormat = @"{0}.xml";
            string fileName = Console.ReadLine();

            return string.Format(fileNameFormat, fileName);
        }

        private static int InputId(string inputMessage, string failureMessage)
        {
            Console.WriteLine(inputMessage);

            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine(failureMessage);
            }

            return id;
        }
    }
}
