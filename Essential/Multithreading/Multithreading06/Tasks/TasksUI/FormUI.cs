using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TasksUI
{
    public partial class FormUI : Form
    {
        private SynchronizationContext context;

        public FormUI()
        {
            InitializeComponent();

            context = SynchronizationContext.Current;

            dgvDownloading.DataSource = bsDownloading;

            DownloadTest();
        }

        private void DownloadTest()
        {
            List<Downloading> ds = new List<Downloading>
            {
                CreateDownloading("1.jpg", "https://get.pxhere.com/photo/landscape-nature-wilderness-walking-mountain-sky-lake-adventure-view-river-valley-mountain-range-environment-rural-reflection-scenic-peaceful-glacier-scenery-serene-fjord-tourism-national-park-ridge-ecology-clouds-mountains-alps-backpacking-plateau-fell-cirque-loch-crater-lake-moraine-landform-tarn-mountain-pass-geographical-feature-mountainous-landforms-glacial-landform-848203.jpg"),
                CreateDownloading("2.jpg", "https://im0-tub-ru.yandex.net/i?id=990264f0c3292fd59ef03eab842bd79c&ref=rim&n=33&w=281&h=188"),
                CreateDownloading("3.jpg", "https://thumbs.dreamstime.com/b/%D0%BE%D0%B7%D0%B5%D1%80%D0%BE-%D0%BC%D0%BE%D1%80%D0%B5%D0%BD-%D0%B2-%D0%BD%D0%B0%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D1%8C%D0%BD%D0%BE%D0%BC-%D0%BF%D0%B0%D1%80%D0%BA%D0%B5-banff-%D0%B0%D0%BB%D1%8C%D0%B1%D0%B5%D1%80%D1%82%D0%B5-%D0%BA%D0%B0%D0%BD%D0%B0%D0%B4%D0%B5-136845637.jpg"),
                CreateDownloading("4.jpg", "https://st3.depositphotos.com/3391755/16987/i/950/depositphotos_169870566-stock-photo-beautiful-turquoise-waters-of-the.jpg"),
                CreateDownloading("5.jpg", "https://yt3.ggpht.com/a/AGF-l79Uf_qe8U6s1uGL2zqLzjKPcl35bGJ0ZEHEIQ=s900-c-k-c0xffffffff-no-rj-mo"),
                CreateDownloading("6.jpg", "https://besthqwallpapers.com/Uploads/22-10-2017/25101/alberta-peyto-lake-4k-forest-mountains.jpg"),
            };

            foreach (Downloading d in ds)
            {
                Downloader downloader = CreateDownloader(context, d);

                Download(downloader);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
            => DownloadFile();

        private void DownloadFile()
        {
            string url = GetUrl();
            string fileName = GetFormattedFileName();

            Downloading downloading = CreateDownloading(fileName, url);
            Downloader downloader = CreateDownloader(context, downloading);

            Download(downloader);

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            tbFileName.Clear();
            tbUrl.Clear();
        }

        private void Download(Downloader downloader)
            => downloader.GetDownloadFileTask();

        private Downloading CreateDownloading(string fileName, string url)
        {
            Downloading downloading = new Downloading(fileName, DownloadingStatus.InQueue, null, 0, url);
            bsDownloading.Add(downloading);

            return downloading;
        }

        private Downloader CreateDownloader(SynchronizationContext context, Downloading downloading)
        {
            Downloader downloader = new Downloader(context, downloading);

            SubscribeToEvents(downloader);
            return downloader;
        }

        private void SubscribeToEvents(Downloader downloader)
        {
            downloader.DownloadStartError += OnDownloadStartError;
            downloader.DownloadStarted += OnDownloadStarted;
            downloader.DownloadProgressChanged += OnDownloadProgressChanged;
            downloader.DownloadCompleted += OnDownloadCompleted;
        }

        private string GetFormattedFileName()
        {
            string fileName = GetFileName();

            Downloading[] downloadings = GetAllDownloadings();
            return FileNameProvider.FormatFileName(fileName, downloadings);
        }

        private string GetFileName()
            => tbFileName.Text;


        private string GetUrl()
            => tbUrl.Text;

        private void OnDownloadStartError(object sender, StartErrorEventArgs e)
        {
            bsDownloading.Remove(e.Downloading);
            MessageBox.Show(e.Exception.Message);
        }

        private void OnDownloadCompleted(object sender, DownloadInformationEventArgs e)
            => UpdateDownloadsInformation();

        private void OnDownloadProgressChanged(object sender, DownloadInformationEventArgs e)
            => UpdateDownloadsInformation();

        private void OnDownloadStarted(object sender, DownloadInformationEventArgs e) 
            => UpdateDownloadsInformation();

        private void UpdateDownloadsInformation()
        {
            Downloading[] downloadings = GetAllDownloadings();

            tsSTotal.Text = GetTotal(downloadings);
            tsSCompleted.Text = GetCompleted(downloadings);
            tsSSize.Text = GetFileSize();
            tsSInProgress.Text = GetInProgress(downloadings);
            tsSInQueue.Text = GetInQueue(downloadings);
            tsSError.Text = GetErrors(downloadings);
        }

        private Downloading[] GetAllDownloadings()
        {
            Downloading[] result = new Downloading[bsDownloading.Count];
            bsDownloading.CopyTo(result, 0);

            return result;
        }

        private string GetTotal(Downloading[] downloadings)
            => $"Total: {downloadings.Length}";

        private string GetCompleted(Downloading[] downloadings)
            => $"Completed: {downloadings.Count(n => n.Status == DownloadingStatus.Completed)}";

        private string GetFileSize()
        {
            string fileSize = string.Empty;
            if (dgvDownloading.SelectedRows.Count != 0)
            {
                string size = (string)dgvDownloading.SelectedRows[0].Cells[2].Value;
                fileSize = $"({size})";
            }

            return fileSize;
        }

        private string GetInProgress(Downloading[] downloadings)
            => $"In Progress: {downloadings.Count(n => n.Status == DownloadingStatus.InProgress)}";

        private string GetInQueue(Downloading[] downloadings)
            => $"In Queue: {downloadings.Count(n => n.Status == DownloadingStatus.InQueue)}";

        private string GetErrors(Downloading[] downloadings)
            => $"Error: {downloadings.Count(n => n.Status == DownloadingStatus.Error)}";

        private void tbFileName_MouseDown(object sender, MouseEventArgs e)
            => ChangeFileName();

        private void ChangeFileName()
        {
            string url = GetUrl();

            FileNameProvider.TryToExtractFileName(url, out string fileName);
            tbFileName.Text = fileName;
        }

        private void tbUrl_MouseDown(object sender, MouseEventArgs e)
        {
            tbUrl.SelectAll();
        }
    }
}
