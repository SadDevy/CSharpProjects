using System;

namespace TasksUI
{
    public class DownloadInformationEventArgs : EventArgs
    {
        public Downloading Downloading { get; private set; }

        public DownloadInformationEventArgs(Downloading downloading)
        {
            Downloading = downloading;
        }
    }
}
