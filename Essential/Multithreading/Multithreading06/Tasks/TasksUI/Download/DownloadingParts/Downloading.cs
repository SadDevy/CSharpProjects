using NLog;
using System.ComponentModel;

namespace TasksUI
{
    public class Downloading : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string File { get; private set; }

        private DownloadingStatus status;
        public DownloadingStatus Status 
        {
            get => status; 
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        private string fileSize;
        public string FileSize 
        {
            get => fileSize; 
            set
            {
                if (value != fileSize)
                {
                    fileSize = value;
                    OnPropertyChanged(nameof(FileSize));
                }
            }
        }

        private int progress;
        public int Progress 
        {
            get => progress; 
            set 
            { 
                if (value != progress) 
                { 
                    progress = value; 
                    OnPropertyChanged(nameof(Progress)); 
                }  
            } 
        }

        public string Url { get; private set; }

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Downloading(string file, DownloadingStatus status, string fileSize = null, int progress = 0, string url = null)
        {
            logger.Trace("Создание новой загрузки.");

            File = file;
            Status = status;
            FileSize = fileSize;
            Progress = progress;
            Url = url;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
