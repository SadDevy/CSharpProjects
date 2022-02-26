using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace AsyncAndAwaitUI
{
    public partial class FormUI : Form
    {
        private Downloader downloader;
        private CancellationTokenSource cancellationTokenSource;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public FormUI()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
            => DownloadImage();


        private async void DownloadImage()
        {
            LockDownloadButton();
            LockUrl();

            cancellationTokenSource = GetCancellationTokenSource();
            downloader = GetDownloader(cancellationTokenSource);

            SubscribeToEvents(downloader);

            string url = tbUrl.Text;
            Bitmap image = await DownloadImageTaskAsync(downloader, url);

            await SetImage(image);

            UnlockUrl();
            UnLockDownloadButton();
        }

        private void LockDownloadButton()
        {
            btnDownload.Enabled = false;
        }

        private void UnLockDownloadButton()
        {
            btnDownload.Enabled = true;
        }

        private void LockUrl()
        {
            tbUrl.Enabled = false;
        }

        private void UnlockUrl()
        {
            tbUrl.Enabled = true;
        }

        private Downloader GetDownloader(CancellationTokenSource source)
        {
            return new Downloader(source.Token);
        }

        private CancellationTokenSource GetCancellationTokenSource()
            => new CancellationTokenSource();

        private async Task SetImage(Image image)
            => await Task.Run(()=> pbImage.Image = image);

        private async Task<Bitmap> DownloadImageTaskAsync(Downloader downloader, string url)
        {
            Bitmap image = await downloader.DownloadImageTaskAsync(tbUrl.Text);

            return image;
        }

        private void SubscribeToEvents(Downloader downloader)
        {
            downloader.DownloadStarted += OnDownloadStarted;
            downloader.DownloadProgressChanged += OnDownloadProgressChanged;
            downloader.DownloadImageCompleted += OnDownloadImageCompleted;
        }

        private void OnDownloadImageCompleted(object sender, AsyncImageDownloadingCompletedEventArgs e)
            => ShowCompletedInfo(e);

        private void ShowCompletedInfo(AsyncImageDownloadingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (e.Error != null)
                    ShowError(e.Error);

                pbDownload.Value = 0;
                return;
            }

            ShowFileInfo(e.Image);
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void ShowFileInfo(Image image)
        {
            logger.Trace("Отображение информации о файле.");
            logger.Debug($"Вызов метода {nameof(ShowFileInfo)}.");

            lblFileWidthValue.Text = image.Width.ToString();
            lblFileHeightValue.Text = image.Height.ToString();

            logger.Debug($"Завершение метода {nameof(ShowFileInfo)}.");
        }

        private void OnDownloadStarted(object sender, RespondedInformationEventArgs e)
            => ShowImageInfo(e);

        private void ShowImageInfo(RespondedInformationEventArgs e)
        {
            logger.Trace("Отображение информации об изображении.");
            logger.Debug($"Вызов метода {nameof(ShowImageInfo)}.");

            lblImageLengthValue.Text = e.ContentLength.ToString();
            lblImageTypeValue.Text = e.ContentType;

            logger.Debug($"Завершение метода {nameof(ShowImageInfo)}.");
        }

        private void OnDownloadProgressChanged(object sender, ProgressChangedEventArgs e)
            => ChangeDownloadProgress(e.ProgressPercentage);

        private void ChangeDownloadProgress(int progress)
            => pbDownload.Value = progress;

        private void btnCancel_Click(object sender, EventArgs e)
            => CancelDownload();

        private void CancelDownload()
            => cancellationTokenSource.Cancel();

        private void btnClear_Click(object sender, EventArgs e)
            => ClearInfo();

        private void ClearInfo()
        {
            logger.Trace("Очистка содержимого.");
            logger.Debug($"Вызов метода {nameof(ClearInfo)}.");

            pbImage.Image = null;
            pbDownload.Value = 0;

            lblFileHeightValue.Text = string.Empty;
            lblFileWidthValue.Text = string.Empty;

            lblImageLengthValue.Text = string.Empty;
            lblImageTypeValue.Text = string.Empty;

            logger.Debug($"Завершеине метода {nameof(ClearInfo)}.");
        }

        private void tbUrl_MouseDown(object sender, MouseEventArgs e)
        {
            tbUrl.SelectAll();
        }
    }
}
