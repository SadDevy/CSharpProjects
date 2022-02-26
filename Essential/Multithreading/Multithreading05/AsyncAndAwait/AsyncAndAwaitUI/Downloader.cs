using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace AsyncAndAwaitUI
{
    public class Downloader
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private WebClient client = new WebClient();

        public event EventHandler<RespondedInformationEventArgs> DownloadStarted;
        public event EventHandler<ProgressChangedEventArgs> DownloadProgressChanged;
        public event EventHandler<AsyncImageDownloadingCompletedEventArgs> DownloadImageCompleted;

        private CancellationToken cancellationToken;

        public Downloader(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
        }

        public async Task<Bitmap> DownloadImageTaskAsync(string address)
        {
            Exception exception = null;
            Bitmap result = null;
            bool cancelled = false;
            try
            {
                result = await DownloadTaskAsync(address);
            }
            catch (Exception ex)
            {
                exception = ex;
                cancelled = true;

                logger.Error(ex);

                if (cancellationToken.IsCancellationRequested)
                    logger.Trace("Отмена скачивания.");
            }
            finally
            {
                DownloadImageCompleted?.Invoke(null,
                    new AsyncImageDownloadingCompletedEventArgs(exception, result, cancelled, null));
            }

            return result;
        }

        private async Task<Bitmap> DownloadTaskAsync(string address)
        {
            logger.Trace("Запск скачивания файла.");
            logger.Debug($"Вызов метода {nameof(DownloadTaskAsync)}");

            CheckUrl(address);

            logger.Debug($"Открытие потока {nameof(Stream)} для скачивания файла.");

            using (Stream stream = await client.OpenReadTaskAsync(address))
            {
                CheckImageUrl(client, address);

                CallDownloadStarted(client);

                logger.Debug($"Открытие потока {nameof(MemoryStream)} для записи файла.");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await RewriteToStream(stream, memoryStream);

                    logger.Debug($"Завершение метода {nameof(DownloadTaskAsync)}");
                    logger.Trace("Завершено скачивание файла.");

                    return await CreateImage(memoryStream, address);
                }
            }
        }

        private void CheckUrl(string address)
        {
            logger.Trace("Проверка корректности Url.");
            logger.Debug($"Вызов метода {nameof(CheckUrl)}");

            if (!Uri.IsWellFormedUriString(address, UriKind.Absolute))
                throw new InvalidOperationException($"Не корректный Url: {address}.");

            logger.Debug($"Завершение метода {nameof(CheckUrl)}");
            logger.Trace("Пройдена проверка корректности Url.");
        }

        private void CheckImageUrl(WebClient client, string address)
        {
            logger.Trace($"Проверка размещения картинки по Url: {address}.");
            logger.Debug($"Вызов метода {nameof(CheckImageUrl)}.");

            string[] types = { "jpeg", "png", "gif" };
            string responsedType = GetContentType(client);

            if (!types.Contains(responsedType))
                throw new InvalidOperationException($"По url: {address} расположена не картинка.");

            logger.Debug($"Завершение метода {nameof(CheckImageUrl)}.");
            logger.Trace($"Проверка размещения картинки по Url: {address} пройдена.");
        }

        private async Task<Bitmap> CreateImage(MemoryStream stream, string address)
        {
            logger.Trace($"Создание изображения из потока {stream}.");
            logger.Debug($"Вызов метода {nameof(CreateImage)}.");

            Task<Bitmap> resultTask = Task.Run(() =>
                (Bitmap)Image.FromStream(stream), cancellationToken);

            logger.Debug($"Завершение метода {nameof(CreateImage)}.");
            logger.Trace($"Завершение создания изображения из потока {stream}.");

            return await resultTask;
        }

        private async Task RewriteToStream(Stream outputStream, MemoryStream inputStream)
        {
            logger.Trace($"Начало записи файла в поток {nameof(MemoryStream)}.");
            logger.Debug($"Вызов метода {nameof(RewriteToStream)}.");

            int contentLength = GetContentLength(client);
            byte[] buffer = new byte[contentLength];

            int onePart = GetOnePartContentLength(contentLength);

            int bytesRead;
            double progress = 0;
            while ((bytesRead = await outputStream.ReadAsync(buffer, 0, onePart, cancellationToken)) > 0)
            {
                logger.Debug($"Считывание очередной порции данных: {bytesRead} байтов.");

                await inputStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);

                logger.Debug($"Запись очередной порции данных: {bytesRead} байтов.");

                ChangeProgress(bytesRead, contentLength, ref progress);
                await CallDownloadProgressChanged(progress);
            }

            logger.Debug($"Завершение метода {nameof(RewriteToStream)}.");
            logger.Trace($"Завершение записи файла в поток {nameof(MemoryStream)}.");
        }

        private void ChangeProgress(int bytesRead, int contentLength, ref double progress)
        {
            logger.Debug($"Вызов метода {nameof(ChangeProgress)}.");

            progress += (bytesRead * 100.0 / contentLength);
            logger.Trace($"Изменение прогресса скачивания файла: {progress}.");

            logger.Debug($"Завершение метода {nameof(ChangeProgress)}.");
        }

        private async Task CallDownloadProgressChanged(double progress)
        {
            logger.Trace("Вызов события изменения прогресса скачивания файла.");
            logger.Debug($"Вызов метода {nameof(CallDownloadProgressChanged)}.");

            const int oneHundredPercent = 100;
            const int oneSecond = 1000;

            int integralProgress = (int)Math.Round(progress, 0);
            if (integralProgress == oneHundredPercent)
            {
                logger.Debug($"Задержка в {oneSecond} мс перед завершение скачивания файла.");
                await Task.Delay(oneSecond, cancellationToken);
            }

            ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(integralProgress, null);
            DownloadProgressChanged?.Invoke(null, progressChangedEventArgs);

            logger.Debug($"Завершение метода {nameof(CallDownloadProgressChanged)}.");
        }

        private int GetOnePartContentLength(int contentLength)
        {
            return contentLength / 100;
        }

        private void CallDownloadStarted(WebClient client)
        {
            logger.Trace("Вызов события начала скачивания файла.");
            logger.Debug($"Вызов метода {nameof(CallDownloadStarted)}.");

            int length = GetContentLength(client);
            string type = GetContentType(client);
            RespondedInformationEventArgs eventArgs = new RespondedInformationEventArgs(length, type);

            DownloadStarted?.Invoke(null, eventArgs);

            logger.Debug($"Завершение метода {nameof(CallDownloadStarted)}.");
        }

        private int GetContentLength(WebClient client)
        {
            logger.Trace("Получение длины контента.");
            logger.Debug($"Вызов метода {nameof(GetContentType)}");

            string length = client.ResponseHeaders.Get("Content-Length");

            logger.Debug($"Завершение метода {nameof(GetContentType)}");
            return int.Parse(length);
        }

        private string GetContentType(WebClient client)
        {
            logger.Trace("Получения типа контента.");
            logger.Debug($"Вызов метода {nameof(GetContentType)}.");

            const char separator = '/';
            string type = client.ResponseHeaders.Get("Content-Type");

            logger.Debug($"Завершение метода {nameof(GetContentType)}.");
            return type.Split(separator).Last();
        }
    }
}
