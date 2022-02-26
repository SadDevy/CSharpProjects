using System;
using System.IO;
using System.Threading;

namespace SyncPrimitivesUI
{
    public class Sleeper
    {
        private int milliseconds;

        public Sleeper(int milliseconds)
        {
            this.milliseconds = milliseconds;
        }

        public void Break()
        {
            string fileName = $"{nameof(SyncPrimitivesUI)}.exe";
            try
            {
                Thread.Sleep(milliseconds);

                using StreamWriter writer = new StreamWriter(fileName);
            }
            catch (IOException ex)
            {
                throw new InvalidSleeperOperationException(ex.Message, ex, DateTime.Now);
            }
        }

    }
}