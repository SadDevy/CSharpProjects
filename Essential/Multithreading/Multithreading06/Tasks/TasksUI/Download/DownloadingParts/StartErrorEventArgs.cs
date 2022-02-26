using System;

namespace TasksUI
{
    public class StartErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public Downloading Downloading { get; private set; }

        public StartErrorEventArgs(Exception exception, Downloading downloading)
        {
            Exception = exception;
            Downloading = downloading;
        }
    }
}
