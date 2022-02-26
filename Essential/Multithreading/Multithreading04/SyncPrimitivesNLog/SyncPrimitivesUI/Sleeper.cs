using System;
using System.IO;
using System.Threading;
using NLog;

namespace SyncPrimitivesUI
{
    public class Sleeper
    {
        private Logger logger;
        private int milliseconds;

        public Sleeper(Logger logger, int milliseconds)
        {
            this.logger = logger;
            this.milliseconds = milliseconds;
        }

        public void Break()
        {
            logger.Debug("Вызов исключения.");

            logger.Trace($"Вызов метода {nameof(Break)}.");

            string fileName = $"{nameof(SyncPrimitivesUI)}.exe";
            try
            {
                Thread.Sleep(milliseconds);

                logger.Trace("Создание StreamWriter.");
                using StreamWriter writer = new StreamWriter(fileName);
            }
            catch (IOException ex)
            {
                throw new InvalidSleeperOperationException(ex.Message, ex, DateTime.Now);
            }
        }
    }
}