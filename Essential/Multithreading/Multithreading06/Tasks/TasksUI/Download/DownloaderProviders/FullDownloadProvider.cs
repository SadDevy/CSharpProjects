using NLog;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TasksUI
{
    public class FullDownloadProvider : DownloadProvider
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FullDownloadProvider(SynchronizationContext context, Downloading downloading)
            : base(context, downloading) { }

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
                    logger.Trace($"Начало загрузки файла {fileName}.");
                    logger.Debug($"Вызов метода {nameof(GetDownloadFileTask)}.");

                    using (HttpWebResponse response = GetResponse(address))
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            int contentLength = GetContentLength(address);

                            ChangeFileSize(downloading, contentLength);
                            ChangeDownloadStatusInProgress(downloading);
                            CallDownloadStarted(context, downloading);

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                Task task = RewriteToStream(stream, memoryStream, address);
                                task.Wait();

                                logger.Trace("Загрузка файла завершена.");

                                CreateFile(fileName, memoryStream.ToArray());
                            }

                            ChangeDownloadStatusOnCompleted(downloading);
                        }
                    }

                    logger.Debug($"Завершеине метода {nameof(GetDownloadFileTask)}.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    ChangeDownloadStatudOnError(downloading);
                }
                finally
                {
                    CallDownloadCompleted(context, downloading);
                }
            });
        }

        private void ChangeDownloadStatudOnError(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.Error;
        }

        private void ChangeDownloadStatusOnCompleted(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.Completed;
        }

        private Task RewriteToStream(Stream outputStream, MemoryStream inputStream, string url)
        {
            return Task.Run(() => 
            {
                logger.Trace("Загрузка файла.");
                logger.Debug($"Вызов метода {nameof(RewriteToStream)}.");

                int contentLength = GetContentLength(url);
                byte[] buffer = new byte[contentLength];

                int onePart = GetOnePartContentLength(contentLength);

                int bytesRead;
                double progress = 0;
                while ((bytesRead = outputStream.Read(buffer, 0, onePart)) > 0)
                {
                    inputStream.Write(buffer, 0, bytesRead);

                    ChangeProgress(bytesRead, contentLength, downloading, ref progress);
                    CallDownloadProgressChanged(context, downloading);
                }

                logger.Debug($"Завершение метода {nameof(RewriteToStream)}.");
            });
        }

        private int GetOnePartContentLength(int contentLength)
        {
            return contentLength / 10;
        }

        private void ChangeDownloadStatusInProgress(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.InProgress;
        }

        private void ChangeProgress(int bytesRead, int contentLength, Downloading downloading, ref double progress)
        {
            logger.Trace($"Изменение текущего прогресса {progress}.");
            logger.Debug($"Вызов метода {nameof(ChangeProgress)}.");

            progress += (bytesRead * 100.0 / contentLength);
            downloading.Progress = (int)Math.Round(progress, 0);

            logger.Debug($"Завершение метода {nameof(ChangeProgress)}.");
            logger.Trace($"Изменение текущего прогресса {progress} завершено.");
        }

        private Task CreateFile(string fileName, byte[] data)
        {
            return Task.Run(() =>
            {
                logger.Trace($"Создание файла {fileName}.");
                logger.Debug($"Вызов метода {nameof(CreateFile)}.");

                using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(data, 0, data.Length);
                }

                logger.Debug($"Завершение метода {nameof(CreateFile)}.");
                logger.Trace($"Создание файла {fileName} завершено.");
            });
        }
    }
}
