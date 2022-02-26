using NLog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TasksUI
{
    public abstract class DownloadProvider
    {
        protected SynchronizationContext context;
        protected Downloading downloading;

        protected double progress = 0;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public event EventHandler<DownloadInformationEventArgs> DownloadStarted;
        public event EventHandler<DownloadInformationEventArgs> DownloadProgressChanged;
        public event EventHandler<DownloadInformationEventArgs> DownloadCompleted;

        public DownloadProvider(SynchronizationContext context, Downloading downloading)
        {
            this.context = context;
            this.downloading = downloading;
        }

        public abstract Task GetDownloadTask(string address, string fileName);

        public void CallDownloadStarted(SynchronizationContext context, object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadStarted)}.");

            Downloading downloading = state as Downloading;
            context.Post(CallDownloadStarted, downloading);

            logger.Debug($"Завершение метода {nameof(CallDownloadStarted)}.");
        }

        private void CallDownloadStarted(object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadStarted)}.");

            Downloading downloading = state as Downloading;
            DownloadStarted?.Invoke(null, new DownloadInformationEventArgs(downloading));

            logger.Debug($"Завершение метода {nameof(CallDownloadStarted)}.");
        }

        public void CallDownloadCompleted(SynchronizationContext context, object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadCompleted)}.");

            context.Post(CallDownloadCompleted, state);

            logger.Debug($"Завершение метода {nameof(CallDownloadCompleted)}.");
        }

        private void CallDownloadCompleted(object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadCompleted)}.");

            Downloading downloading = state as Downloading;
            DownloadCompleted?.Invoke(null, new DownloadInformationEventArgs(downloading));

            logger.Debug($"Завершение метода {nameof(CallDownloadCompleted)}.");
        }

        public void CallDownloadProgressChanged(SynchronizationContext context, object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadProgressChanged)}.");

            context.Post(CallDownloadProgressChanged, state);

            logger.Debug($"Завершение метода {nameof(CallDownloadProgressChanged)}.");
        }

        private void CallDownloadProgressChanged(object state)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadProgressChanged)}.");

            Downloading downloading = state as Downloading;
            DownloadProgressChanged?.Invoke(null, new DownloadInformationEventArgs(downloading));

            logger.Debug($"Завершение метода {nameof(CallDownloadProgressChanged)}.");
        }

        protected void ChangeFileSize(Downloading downloading, int contentLength)
        {
            logger.Trace("Отображение размера файла в Kb, Mb Gb или B.");
            logger.Debug($"Вызов метода {nameof(ChangeFileSize)}.");

            downloading.FileSize = FileSizeFormatter.Format(contentLength);

            logger.Debug($"Завершение метода {nameof(ChangeFileSize)}.");
        }

        protected int GetContentLength(string address)
        {
            logger.Trace("Получение размера файла.");
            logger.Debug($"Вызов метода {nameof(GetContentLength)}.");

            WebRequest request = WebRequest.Create(address);
            using (WebResponse response = request.GetResponse())
            {
                string length = response.Headers.Get("Content-Length");

                logger.Debug($"Завершение метода {nameof(GetContentLength)}.");
                return int.Parse(length); 
            }
        }

        protected HttpWebResponse GetResponse(string address, int startByte, int endByte)
        {
            logger.Trace("Получение ответа сервера для скачивания файла.");
            logger.Debug($"Вызов метода {nameof(GetResponse)}.");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.AddRange(startByte, endByte);

            logger.Debug($"Завершеине метода {nameof(GetResponse)}.");
            return (HttpWebResponse)request.GetResponse();
        }

        protected HttpWebResponse GetResponse(string address)
        {
            logger.Trace("Получение ответа сервера для скачивания файла.");
            logger.Debug($"Вызов метода {nameof(GetResponse)}.");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);

            logger.Debug($"Завершеине метода {nameof(GetResponse)}.");
            return (HttpWebResponse)request.GetResponse();
        }
    }
}
