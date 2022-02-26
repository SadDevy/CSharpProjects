using NLog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TasksUI
{
    public class Downloader
    {
        private Downloading downloading;

        private readonly SynchronizationContext context;
        private readonly ConfigManager configManager;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private int downloadsCount;

        public event EventHandler<DownloadInformationEventArgs> DownloadStarted;
        public event EventHandler<DownloadInformationEventArgs> DownloadProgressChanged;
        public event EventHandler<DownloadInformationEventArgs> DownloadCompleted;
        public event EventHandler<StartErrorEventArgs> DownloadStartError; 

        public Downloader(SynchronizationContext context, Downloading downloading)
        {
            this.context = context;
            this.downloading = downloading;
            configManager = new ConfigManager();
        }

        public Task GetDownloadFileTask()
        {
            try
            {
                downloadsCount = configManager.GetDownloadsCount();
                return GetDownloadTask(downloading.Url, downloading.File);
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                DownloadStartError?.Invoke(this, new StartErrorEventArgs(ex, downloading));
                return Task.CompletedTask;
            }
        }

        private Task GetDownloadTask(string address, string fileName)
        {
            logger.Trace("Проверка возможности загрузки файла.");
            logger.Debug($"Вызов метода {nameof(GetDownloadTask)}.");

            CheckUrl(address);
            CheckFileName(fileName);

            return Task.Run(() =>
            {
                try
                {
                    DownloadProvider downloadProvider;
                    if (CouldDownloadPartial(address))
                        downloadProvider = new PartialDownloadProvider(context, downloading, downloadsCount);
                    else
                        downloadProvider = new FullDownloadProvider(context, downloading);

                    logger.Debug($"Завершение метода {nameof(GetDownloadTask)}.");

                    SubscribeToEvents(downloadProvider);
                    return downloadProvider.GetDownloadTask(address, fileName);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);

                    ChangeDownloadingStatusError(downloading);
                    context.Post(CallDownloadCompleted, downloading);

                    return Task.CompletedTask;
                }
            });
        }

        private void ChangeDownloadingStatusError(Downloading downloading)
        {
            downloading.Status = DownloadingStatus.Error;
        }

        private void CallDownloadCompleted(object status)
        {
            logger.Debug($"Вызов метода {nameof(CallDownloadCompleted)}.");

            Downloading downloading = status as Downloading;
            DownloadCompleted?.Invoke(this, new DownloadInformationEventArgs(downloading));

            logger.Debug($"Завершение метода {nameof(CallDownloadCompleted)}.");
        }

        private void SubscribeToEvents(DownloadProvider provider)
        {
            logger.Debug($"Вызов метода {nameof(SubscribeToEvents)}.");

            provider.DownloadStarted += OnDownloadStarted;
            provider.DownloadProgressChanged += OnDownloadProgressChanged;
            provider.DownloadCompleted += OnDownloadCompleted;

            logger.Debug($"Завершение метода {nameof(SubscribeToEvents)}.");
        }

        private void OnDownloadStarted(object sender, DownloadInformationEventArgs e)
        {
            logger.Debug($"Вызов метода {nameof(OnDownloadStarted)}.");

            DownloadStarted?.Invoke(sender, e);

            logger.Debug($"Завершение метода {nameof(OnDownloadStarted)}.");
        }

        private void OnDownloadProgressChanged(object sender, DownloadInformationEventArgs e)
        {
            logger.Debug($"Вызов метода {nameof(OnDownloadProgressChanged)}.");

            DownloadProgressChanged?.Invoke(sender, e);

            logger.Debug($"Завершение метода {nameof(OnDownloadProgressChanged)}.");
        }

        private void OnDownloadCompleted(object sender, DownloadInformationEventArgs e)
        {
            logger.Debug($"Вызов метода {nameof(OnDownloadCompleted)}.");

            DownloadCompleted?.Invoke(sender, e);

            logger.Debug($"Завершение метода {nameof(OnDownloadCompleted)}.");
        }

        private bool CouldDownloadPartial(string address)
        {
            logger.Trace("Проверка возможности частичной загрузки файла.");
            logger.Debug($"Вызов метода {nameof(CouldDownloadPartial)}.");

            const string headerName = "Accept-Ranges";
            const string none = "none";

            WebRequest request = WebRequest.Create(address);
            using (WebResponse response = request.GetResponse())
            {
                string acceptRanges = response.Headers.Get(headerName);

                logger.Debug($"Завершение метода {nameof(CouldDownloadPartial)}.");
                return acceptRanges != none;
            }
        }

        private void CheckFileName(string fileName)
        {
            logger.Trace("Проверка валидности названия файла.");
            logger.Debug($"Вызов метода {nameof(CheckFileName)}.");

            if (!FileNameProvider.FileNameIsValid(fileName))
                throw new InvalidOperationException($"Не корректное название файла: {fileName}.");

            logger.Debug($"Завершение метода {nameof(CheckFileName)}.");
        }

        private void CheckUrl(string url)
        {
            logger.Trace("Проверка валидности url.");
            logger.Debug($"Вызов метода {nameof(CheckUrl)}.");

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new InvalidOperationException($"Не корректный Url: {url}.");

            logger.Debug($"Завершение метода {nameof(CheckUrl)}.");
        }
    }
}
