using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace SyncPrimitivesUI
{
    public class Menu
    {
        private Mutex mutex;
        private int milliseconds;
        private Logger logger;

        public Menu(Mutex mutex, Logger logger, int milliseconds)
        {
            logger.Trace($"Создание экземпляра {nameof(Menu)}.");

            this.mutex = mutex;
            this.milliseconds = milliseconds;
            this.logger = logger;
        }

        public void TakeUpOperation(Operation element)
        {
            logger.Info("Вызов операции.");

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

            logger.Info("Завершение вызова операции.");
        }

        private void Unlock(Mutex mutex)
        {
            logger.Debug("Снятие блокировки.");

            logger.Trace($"Вызов операции {nameof(Unlock)}.");
            logger.Trace("Снятие блокировки процесса.");

            logger.Trace("Освобождение Mutex.");
            mutex.ReleaseMutex();
            logger.Trace("Mutex освобожден.");

            logger.Trace("Блокировка снята.");
            logger.Trace($"Завершение выполнения операции {nameof(Unlock)}.");

            CloseOnEscapeClick();
        }

        private void CloseOnEscapeClick()
        {
            logger.Trace($"Вызов метода {nameof(CloseOnEscapeClick)}.");

            Console.WriteLine("Введите Escape, чтобы закрыть приложение.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Введите Escape.");
            }

            logger.Trace($"Завершение метода {nameof(CloseOnEscapeClick)}.");
            Exit();
        }

        private void ErrorTask(int milliseconds)
        {
            logger.Debug("Вызов исключения в Task.");
            logger.Trace($"Начало выполнения операции {nameof(ErrorTask)}.");

            Task task = GetTask(milliseconds);
            try
            {
                logger.Debug($"Вызов метода {nameof(Task.Wait)}.");
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);

                logger.Error(ex.InnerException);
            }

            logger.Trace("Освобождение Mutex.");
            mutex.ReleaseMutex();
            logger.Trace("Mutex освобожден.");

            logger.Trace($"Завершения выполнения операции {nameof(ErrorTask)}.");
        }

        private Task GetTask(int milliseconds)
        {
            logger.Trace("Создание нового Task.");

            return Task.Run(() =>
            {
                Sleeper sleeper = new Sleeper(logger, milliseconds);
                sleeper.Break();
            });
        }

        private void ErrorThread(int milliseconds)
        {
            logger.Debug("Вызов исключение в потоке.");

            logger.Trace($"Вызов операции {nameof(ErrorThread)}.");
            logger.Trace($"Вызов исключения в Thread.");

            Sleeper sleeper = new Sleeper(logger, milliseconds);
            Exception exception = ErrorThread(sleeper);

            if (exception != null)
                Console.WriteLine(exception.Message);

            logger.Trace("Освобождение Mutex.");
            mutex.ReleaseMutex();
            logger.Trace("Mutex освобожден.");

            logger.Debug($"Завершение выполнения операции {nameof(ErrorThread)}.");
        }

        private Exception ErrorThread(Sleeper sleeper)
        {
            Exception exception = null;

            logger.Trace("Создание нового Thread.");
            Thread thread = new Thread(() =>
            {
                try
                {
                    logger.Trace($"Вызов метода {nameof(Sleeper.Break)}.");
                    sleeper.Break();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    exception = ex;
                }
            });

            logger.Trace("Запуск нового потока.");
            thread.Start();

            logger.Trace("Блокировка потока.");
            thread.Join();

            logger.Trace("Снятие блокировки потока.");

            return exception;
        }

        private async void ErrorAsyncAwait(int milliseconds)
        {
            logger.Debug("Вызов исключения в Async/Await.");

            logger.Trace($"Вызов операции {nameof(ErrorAsyncAwait)}.");
            logger.Trace("Вызов исключения с помощью async/await.");

            try
            {
                Task task = GetTask(milliseconds);

                logger.Trace("Вызов ожидание завершения Task.");
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                logger.Error(ex);
            }

            logger.Trace("Освобождение Mutex.");
            mutex.ReleaseMutex();
            logger.Trace("Mutex освобожден.");

            logger.Debug($"Завершение выполнения операции {nameof(ErrorAsyncAwait)}.");
        }

        private void ErrorAsyncDelegate(int milliseconds)
        {
            logger.Debug("Вызов исключения в AsyncDelegate.");

            logger.Debug($"Вызов операции {nameof(ErrorAsyncDelegate)}.");

            Sleeper sleeper = new Sleeper(logger, milliseconds);
            Exception exception = ErrorAsyncDelegate(sleeper);

            if (exception != null)
                Console.WriteLine(exception.Message);

            logger.Trace("Освобождение Mutex.");
            mutex.ReleaseMutex();
            logger.Trace("Mutex освобожден.");

            logger.Debug($"Завершение выполнения операции {nameof(ErrorAsyncDelegate)}.");
        }

        private Exception ErrorAsyncDelegate(Sleeper sleeper)
        {
            Exception exception = null;

            logger.Trace("Создание нового Action.");
            Action action = () =>
            {
                try
                {
                    logger.Trace($"Вызов метода {nameof(Sleeper.Break)}.");
                    sleeper.Break();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    exception = ex;
                }
            };

            logger.Trace("Вызов AsynDelegate.");
            IAsyncResult asyncResult = action.BeginInvoke(null, null);

            logger.Trace("Завершение выполнения AsyncDelegate.");
            action.EndInvoke(asyncResult);
            logger.Trace("Выполнение AsyncDelegate завершено.");

            return exception;
        }

        private void Lock(Mutex mutex)
        {
            logger.Debug("Ожидание блокировки процесса.");

            logger.Trace($"Вызов операции {nameof(Lock)}");
            logger.Trace("Ожидание блокировки процесса.");

            mutex.WaitOne();

            logger.Trace("Получена блокировка.");
        }

        private void Exit()
        {
            logger.Debug("Закрытие приложения.");

            logger.Trace($"Вызов операции {nameof(Exit)}");

            Process.GetCurrentProcess().Close();
        }

        ~Menu()
        {
            logger.Debug($"Освобождение экземпляра {nameof(Menu)}.");
        }
    }
}
