using System;
using System.Threading;

namespace Entities
{
    public class ReadWriteLocker : IDisposable
    {
        public struct WriteLockToken : IDisposable
        {
            private readonly ReaderWriterLockSlim @lock;

            public WriteLockToken(ReaderWriterLockSlim @lock)
            {
                this.@lock = @lock;
                @lock.EnterWriteLock();

                if (@lock.IsWriteLockHeld)
                    Logger.WriteTraceLine(Thread.CurrentThread.ManagedThreadId, "Успешное получение блокировки на запись");
            }

            public void Dispose() => @lock.ExitWriteLock();
        }

        public struct ReadLockToken : IDisposable
        {
            private readonly ReaderWriterLockSlim @lock;

            public ReadLockToken(ReaderWriterLockSlim @lock)
            {
                this.@lock = @lock;
                @lock.EnterReadLock();

                if (@lock.IsReadLockHeld)
                    Logger.WriteTraceLine(Thread.CurrentThread.ManagedThreadId, "Успешное получение блокировки на чтение");
            }

            public void Dispose() => @lock.ExitReadLock();
        }
        
        private readonly ReaderWriterLockSlim @lock = new ReaderWriterLockSlim();

        public ReadLockToken ReadLock() => new ReadLockToken(@lock);
        public WriteLockToken WriteLock() => new WriteLockToken(@lock);

        public void Dispose() => @lock.Dispose();
    }
}
