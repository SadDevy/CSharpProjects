using System;
using System.ComponentModel;
using System.Drawing;

namespace AsyncAndAwaitUI
{
    public class AsyncImageDownloadingCompletedEventArgs : AsyncCompletedEventArgs
    {
        public Image Image { get; private set; }

        public AsyncImageDownloadingCompletedEventArgs(Exception error, Image image, bool cancelled, object userState) 
            : base(error, cancelled, userState)
        {
            Image = image;
        }
    }
}
