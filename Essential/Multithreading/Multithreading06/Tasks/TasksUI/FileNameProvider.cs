using NLog;
using System.IO;
using System.Linq;

namespace TasksUI
{
    public static class FileNameProvider
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool TryToExtractFileName(string url, out string extracted)
        {
            logger.Trace("Извлечение имени файла из url.");
            logger.Debug($"Вызов метода {nameof(TryToExtractFileName)}.");

            extracted = string.Empty;

            string fileName = Path.GetFileName(url);
            if (!FileNameIsValid(fileName))
                return false;

            logger.Debug($"Завершение метода {nameof(TryToExtractFileName)}.");

            extracted = fileName;
            return true;
        }

        public static bool FileNameIsValid(string fileName)
        {
            logger.Trace("Проверка валидности названия файла.");
            logger.Debug($"Вызов метода {nameof(FileNameIsValid)}.");

            const char separator = '.';
            const int maxLength = 255;
            const int fileNamePartsCount = 2;

            string[] nameParts = fileName.Split(separator);

            logger.Debug($"Завершение метода {nameof(FileNameIsValid)}.");

            return nameParts.Length == fileNamePartsCount
                && fileName.Length < maxLength;
        }

        public static string FormatFileName(string fileName, Downloading[] downloadings)
        {
            logger.Trace("Форматирование названия файла.");
            logger.Debug($"Вызов метода {nameof(FormatFileName)}.");

            const char separator = '.';
            const string format = "00";

            int number = 1;
            string result = fileName;
            while (downloadings.Any(n => n.File == result))
            {
                string[] fileNameParts = fileName.Split(separator);
                result = $"{fileNameParts.First()} ({number.ToString(format)}).{fileNameParts.Last()}";

                number++;
            }

            logger.Debug($"Завершение метода {nameof(FormatFileName)}.");

            return result;
        }
    }
}
