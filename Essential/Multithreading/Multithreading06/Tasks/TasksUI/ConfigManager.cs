using NLog;
using System;
using System.Configuration;
using System.Linq;

namespace TasksUI
{
    public class ConfigManager
    {
        private const string downloadsCountKeyName = "DownloadsCount";
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public int GetDownloadsCount()
        {
            CheckDownloadCountKey(downloadsCountKeyName);

            string downloadCountName = ConfigurationManager.AppSettings.Get(downloadsCountKeyName);

            CheckDownloadCount(downloadCountName, out int result);
            return result;
        }

        private void CheckDownloadCount(string downloadCountName, out int result)
        {
            logger.Trace("Проверка числа одновременных частей загрузки файла.");
            logger.Debug($"Вызов метода {nameof(GetDownloadsCount)}.");

            if (!int.TryParse(downloadCountName, out result))
                throw new InvalidCastException("Не корректное число одновременно скачиваемых частей в конфиг файле.");

            logger.Debug($"Завершение метода {nameof(GetDownloadsCount)}.");
        }

        private void CheckDownloadCountKey(string name)
        {
            logger.Trace("Проверка ключа числа одновременных частей загрузки файла.");
            logger.Debug($"Вызов метода {nameof(CheckDownloadCountKey)}.");

            bool hasKey = ConfigurationManager.AppSettings.AllKeys.Any(n => n == downloadsCountKeyName);
            if (!hasKey)
                throw new InvalidOperationException($"Ключ {name} не определен в файле конфигурации.");

            logger.Debug($"Завершение метода {nameof(CheckDownloadCountKey)}.");
        }
    }
}
