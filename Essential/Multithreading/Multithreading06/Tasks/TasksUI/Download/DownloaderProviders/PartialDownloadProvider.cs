using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TasksUI
{
    public class PartialDownloadProvider : DownloadProvider
    {
        private readonly int downloadsCount;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PartialDownloadProvider(SynchronizationContext context, Downloading downloading, int downloadsCount)
            : base(context, downloading)
        {
            this.downloadsCount = downloadsCount;
        }

        public override Task GetDownloadTask(string address, string fileName)
        {
            return GetDownloadFileTask(address, fileName);
        }

        private Task GetDownloadFileTask(string address, string fileName)
        {
            return Task.Run(() =>
            {
                try
                {
                    logger.Trace("Запуск скачивания файла.");
                    logger.Debug($"Вызов метода {nameof(GetDownloadFileTask)}.");

                    int contentLength = GetContentLength(address);
                    int portionsCount = GetPortionsCount(contentLength);

                    ChangeFileSize(downloading, contentLength);
                    ChangeDownloadStatusInProgress(downloading);
                    CallDownloadStarted(context, downloading);

                    ConcurrentQueue<Task<DownloadedPortion>> portionsQueue = new ConcurrentQueue<Task<DownloadedPortion>>();

                    int portionNumber = 0;
                    while (portionNumber < portionsCount)
                    {
                        Portion portion = new Portion(portionNumber, portionsCount, contentLength);
                        Task<DownloadedPortion> downloadedPortionTask = GetDownloadPortionTask(portion, downloading);
                        portionsQueue.Enqueue(downloadedPortionTask);

                        portionNumber++;
                    }

                    logger.Trace("Завершение скачивания файла.");

                    CreateFile(portionsQueue, contentLength, fileName);

                    logger.Debug($"Завершение метода {nameof(GetDownloadFileTask)}.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    ChangeDownloadingStatusError(downloading);
                }

                CallDownloadCompleted(context, downloading);
            });
        }

        private void ChangeDownloadStatusInProgress(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.InProgress;
        }

        private void CreateFile(ConcurrentQueue<Task<DownloadedPortion>> downloadTasks, int contentLength, string fileName)
        {
            try
            {
                logger.Trace("Начало создания файла.");
                logger.Debug($"Вызов метода {nameof(CreateFile)}.");

                List<DownloadedPortion> portions = DownloadAllPortions(downloadTasks, contentLength);
                CreateFile(fileName, portions);

                ChangeDownloadStatusCompleted(downloading);
                
                logger.Debug($"Завершение метода {nameof(CreateFile)}.");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ChangeDownloadingStatusError(downloading);
            }
        }

        private void ChangeDownloadStatusCompleted(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.Completed;
        }

        private void ChangeDownloadingStatusError(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.Error;
        }

        private void CreateFile(string fileName, List<DownloadedPortion> portions)
        {
            logger.Trace($"Создание файла {fileName}.");
            logger.Debug($"Вызов метода {nameof(CreateFile)}.");

            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                portions.Sort((a, b) => a.Id.CompareTo(b.Id));

                foreach (DownloadedPortion portion in portions)
                {
                    stream.Write(portion.Data, 0, portion.Data.Length);
                }
            }

            logger.Debug($"Завершение метода {nameof(CreateFile)}.");
            logger.Trace($"Создание файла {fileName} завершено.");
        }

        private List<DownloadedPortion> DownloadAllPortions(ConcurrentQueue<Task<DownloadedPortion>> portionsTasks, int contentLength)
        {
            logger.Trace("Загрузка всех порций файла.");
            logger.Debug($"Вызов метода {nameof(DownloadAllPortions)}.");

            List<DownloadedPortion> portions = new List<DownloadedPortion>();
            while (!portionsTasks.IsEmpty)
            {
                DownloadPortions(portionsTasks, portions, contentLength, downloadsCount);
            }

            logger.Debug($"Завершение метода {nameof(DownloadAllPortions)}.");
            logger.Trace("Загрузка всех порций файла завершена.");
            return portions;
        }

        private void DownloadPortions(ConcurrentQueue<Task<DownloadedPortion>> portionsTasks, List<DownloadedPortion> portions, int contentLength, int count)
        {
            logger.Trace($"Загрузка очередных {count} порций файла.");
            logger.Debug($"Вызов метода {nameof(DownloadPortions)}.");

            List<Task<DownloadedPortion>> activePortionsTasks = new List<Task<DownloadedPortion>>();

            int number = 0;
            while (number < count)
            {
                if (portionsTasks.TryDequeue(out Task<DownloadedPortion> task))
                {
                    task.Start();

                    activePortionsTasks.Add(task);
                }

                number++;
            }

            AddTasksResult(portions, activePortionsTasks.ToArray());

            ChangeProgress(activePortionsTasks, downloading, contentLength, ref progress);
            CallDownloadProgressChanged(context, downloading);

            logger.Debug($"Завершение метода {nameof(DownloadPortions)}.");
            logger.Trace($"Загрузка очередных {count} порций файла завершена.");
        }

        private void ChangeProgress(List<Task<DownloadedPortion>> tasks, Downloading downloading, int contentLength, ref double progress)
        {
            logger.Trace($"Изменение текущего прогресса {progress}.");
            logger.Debug($"Вызов метода {nameof(ChangeProgress)}.");

            foreach (Task<DownloadedPortion> task in tasks)
            {
                progress += task.Result.Length * 100.0 / contentLength;
            }

            downloading.Progress = (int)Math.Round(progress, 0);

            logger.Debug($"Завершение метода {nameof(ChangeProgress)}.");
            logger.Trace($"Завершение изменения текущего прогресса {progress}.");
        }

        private void AddTasksResult(List<DownloadedPortion> results, params Task<DownloadedPortion>[] tasks)
        {
            logger.Debug($"Вызов метода {nameof(AddTasksResult)}.");

            Task.WaitAll(tasks);

            Array.ForEach(tasks, n => results.Add(n.Result));

            logger.Debug($"Завершение метода {nameof(AddTasksResult)}.");
        }

        private Task<DownloadedPortion> GetDownloadPortionTask(Portion portion, Downloading downloading)
        {
            return new Task<DownloadedPortion>(() =>
            {
                logger.Trace("Создание задачи для скачивания порции.");
                logger.Debug($"Вызов метода {nameof(GetDownloadPortionTask)}.");

                using (HttpWebResponse response = GetResponse(downloading.Url, portion.Start, portion.End))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            byte[] data = reader.ReadBytes(portion.Length);

                            logger.Debug($"Завершение метода {nameof(GetDownloadPortionTask)}.");
                            return new DownloadedPortion(portion.Number, portion.Length, data);
                        }
                    }
                }
            });
        }

        private int GetPortionsCount(int contentLength)
        {
            logger.Trace("Получение количесва порций для скачивания файла.");
            logger.Debug($"Вызов метода {nameof(GetPortionsCount)}.");

            int[] portionSizes = { 524_288, 1_048_576, 2_097_152 };

            int portionsCount = 0;
            foreach (int portionSize in portionSizes)
            {
                if (IsPortionsCountLessOrEqualsOneThousand(contentLength, portionSize, out portionsCount))
                    return portionsCount;
            }

            logger.Debug($"Завершение метода {nameof(GetPortionsCount)}.");
            
            portionsCount = 1;
            return portionsCount;
        }

        private bool IsPortionsCountLessOrEqualsOneThousand(int contentLength, int portionSize, out int portionsCount)
        {
            logger.Trace("Проверка, что количество порций не превышает 1000.");
            logger.Debug($"Вызов метода {nameof(IsPortionsCountLessOrEqualsOneThousand)}.");

            const int maxPortionsCount = 1000;
            portionsCount = contentLength / portionSize;

            logger.Debug($"Завершение метода {nameof(IsPortionsCountLessOrEqualsOneThousand)}.");
            return portionsCount <= maxPortionsCount && portionsCount > 0;
        }
    }
}
