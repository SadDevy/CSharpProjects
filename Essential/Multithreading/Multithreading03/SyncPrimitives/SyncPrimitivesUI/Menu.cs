using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPrimitivesUI
{
    public class Menu
    {
        private Mutex mutex;
        private int milliseconds;

        public Menu(Mutex mutex, int milliseconds)
        {
            this.mutex = mutex;
            this.milliseconds = milliseconds;
        }

        public void TakeUpOperation(Operation element)
        {
            switch (element)
            {
                case Operation.Unlock:
                    Unlock(mutex);
                    break;
                case Operation.ErrorTask:
                    ErrorTask(milliseconds);
                    break;
                case Operation.ErrorThread:
                    ErrorThread(milliseconds);
                    break;
                case Operation.ErrorAsyncAwait:
                    ErrorAsyncAwait(milliseconds);
                    break;
                case Operation.ErrorAsyncDelegate:
                    ErrorAsyncDelegate(milliseconds);
                    break;
                case Operation.Exit:
                    Exit();
                    break;
                case Operation.Lock:
                    Lock(mutex);
                    break;
            }
        }

        private void Unlock(Mutex mutex)
        {
            mutex.ReleaseMutex();

            CloseOnEscapeClick();
        }

        private void CloseOnEscapeClick()
        {
            Console.WriteLine("Введите Escape, чтобы закрыть приложение.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                Console.WriteLine("Введите Escape.");

            Exit();
        }

        private void ErrorTask(int milliseconds)
        {
            Task task = GetTask(milliseconds);
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            mutex.ReleaseMutex();
        }

        private Task GetTask(int milliseconds)
        {
            return Task.Run(() =>
            {
                Sleeper sleeper = new Sleeper(milliseconds);
                sleeper.Break();
            });
        }

        private void ErrorThread(int milliseconds)
        {
            Sleeper sleeper = new Sleeper(milliseconds);
            Exception exception = ErrorThread(sleeper);
            
            if (exception != null)
                Console.WriteLine(exception.Message);

            mutex.ReleaseMutex();
        }

        private Exception ErrorThread(Sleeper sleeper)
        {
            Exception exception = null;

            Thread thread = new Thread(() =>
            {
                try
                {
                    sleeper.Break();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            thread.Start();
            thread.Join();

            return exception;
        }

        private async void ErrorAsyncAwait(int milliseconds)
        {
            try
            {
                Task task = GetTask(milliseconds);
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            mutex.ReleaseMutex();
        }

        private void ErrorAsyncDelegate(int milliseconds)
        {
            Sleeper sleeper = new Sleeper(milliseconds);
            Exception exception = ErrorAsyncDelegate(sleeper);

            if (exception != null)
                Console.WriteLine(exception.Message);

            mutex.ReleaseMutex();
        }

        private Exception ErrorAsyncDelegate(Sleeper sleeper)
        {
            Exception exception = null;
            Action action = () =>
            {
                try
                {
                    sleeper.Break();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            };

            IAsyncResult asyncResult = action.BeginInvoke(null, null);
            action.EndInvoke(asyncResult);

            return exception;
        }

        private void Lock(Mutex mutex)
        {
            mutex.WaitOne();
        }

        private void Exit()
        {
            Process.GetCurrentProcess().Close();
        }
    }
}
