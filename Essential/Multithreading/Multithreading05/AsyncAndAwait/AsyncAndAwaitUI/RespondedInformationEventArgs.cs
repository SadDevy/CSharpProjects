using System;

namespace AsyncAndAwaitUI
{
    public class RespondedInformationEventArgs : EventArgs
    {
        public int ContentLength { get; private set; }
        public string ContentType { get; private set; }

        public RespondedInformationEventArgs(int contentLength, string contentType)
        {
            ContentLength = contentLength;
            ContentType = contentType;
        }
    }
}
