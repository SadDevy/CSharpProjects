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

                    LogMessage($"Изменение общего свойства [Thread name: {Thread.CurrentThread.Name}].");
                    LogMessage("Завершение метода записи.");
                }
            }
        }

        public Resulter()
        {
            LogMessage($"Создание экземпляра {nameof(Resulter)}");
        }

        private void LogMessage(string message)
        {
            Logger.WriteTraceLine(Thread.CurrentThread.ManagedThreadId, message);
        }

        ~Resulter()
        {
            LogMessage($"Освобождение экземпляра {nameof(Resulter)}");
        }
    }
}
