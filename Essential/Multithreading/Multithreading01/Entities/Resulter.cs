using System.Threading;

namespace Entities
{
    public class Resulter
    {
        private ReadWriteLocker resLock = new ReadWriteLocker();

        private string sharedRes;
        public string SharedRes
        {
            get
            {
                LogMessage("Ожидаение блокировки на чтение.");
                
                using (resLock.ReadLock())
                {
                    LogMessage("Старт метода чтения.");
                    
                    Thread.Sleep(500);

                    LogMessage("Завершение метода чтения.");
                    
                    return sharedRes;
                }
            }

            set
            {
                LogMessage("Ожидаение блокировки на запись.");

                using (resLock.WriteLock())
                {
                    LogMessage("Старт метода записи.");

                    Thread.Sleep(1000);

                    sharedRes = value;

                    LogMessage("Завершение метода записи.");
                }
            }
        }

        private void LogMessage(string message)
        {
            Logger.WriteTraceLine(Thread.CurrentThread.ManagedThreadId, message);
        }
    }
}
