using System;
using System.Collections;
using System.Reflection;
using ExceptionRegeneration;

namespace ExceptionRegenerationUI
{
    class Program
    {
        static void Main()
        {
            HandleCaughtByDeafCatch();
            HandleRetrownAsNewBaseException();
            HandleRethrownAsNewBaseExceptionWithInner();
            HandleRethrownOriginalException();
            HandleRethrownOriginalOnceAga();
        }

        private static void HandleCaughtByDeafCatch()
        {
            CatchByDeafCatch();
            
            // Неизвестно было ли исключение
        }

        private static void CatchByDeafCatch()
        {
            try
            {
                DoBoom();
            }
            catch
            {
                // по заданию (я так делать не буду, а если буду, то...)
            }
        }

        private static void HandleRetrownAsNewBaseException()
        {
            try
            {
                RethrowAsNewBaseException();
            }
            catch (BaseException ex)
            {
                ShowFull(ex);

                Console.WriteLine(ex);
            }
        }

        private static void RethrowAsNewBaseException()
        {
            try
            {
                DoBoom();
            }
            catch
            {
                BaseException e = new BaseException("Rethrow as new BaseException.");
                e.Data.Add(BaseException.DataKeys.Rethrown, DateTime.Now);
                throw e;
            }
        }

        private static void HandleRethrownAsNewBaseExceptionWithInner()
        {
            try
            {
                RethrowAsNewBaseExceptionWithInner();
            }
            catch (Exception ex)
            {
                ShowFull(ex);

                Console.WriteLine(ex);
            }
        }

        private static void RethrowAsNewBaseExceptionWithInner()
        {
            try
            {
                DoBoom();
            }
            catch (FormatException ex)
            {
                BaseException e = new BaseException("Rethrow with inner.", ex);
                e.Data.Add(BaseException.DataKeys.Rethrown, DateTime.Now);
                throw e;
            }
        }

        private static void HandleRethrownOriginalException()
        {
            try
            {
                RethrowOriginalException();
            }
            catch (Exception ex)
            {
                ShowFull(ex);

                Console.WriteLine(ex);
            }
        }

        private static void RethrowOriginalException()
        {
            try
            {
                DoBoom();
            }
            catch (FormatException ex)
            {
                ex.Data.Add(BaseException.DataKeys.Rethrown, DateTime.Now);
                throw; //!!!
            }
        }

        private static void HandleRethrownOriginalOnceAga()
        {
            try
            {
                RethrowOriginalOnceAgain();
            }
            catch (Exception ex)
            {
                ShowFull(ex);

                Console.WriteLine(ex);
            }
        }

        private static void RethrowOriginalOnceAgain()
        {
            try
            {
                DoBoom();
            }
            catch (FormatException ex)
            {
                ex.Data.Add(BaseException.DataKeys.Rethrown, DateTime.Now);
                throw ex;
            }
        }

        private static void DoBoom()
        {
            double.Parse("Boom!");
        }

        private static void ShowFull(Exception exception)
        {
            Console.WriteLine("-------------------exception-------------------");
            ShowException(exception);

            if (exception.InnerException != null)
                ShowInnerException(exception.InnerException);
        }

        private static void ShowException(Exception exception)
        {
            Console.WriteLine("Exception Type: {0}", exception.GetType().Name);
            Console.WriteLine("Message: {0}", exception.Message);
            Console.WriteLine("Source: {0}", exception.Source);
            Console.WriteLine("TargetSite: {0}", exception.TargetSite);
            Console.WriteLine("HResult: {0}", exception.HResult);

            if (exception is BaseException)
                Console.WriteLine("Guid: {0}", ((BaseException)exception).Guid);

            string[] shownProperties = new string[]
            {
                nameof(Exception.Message),
                nameof(Exception.Source),
                nameof(Exception.TargetSite),
                nameof(Exception.HResult),
                nameof(BaseException.Guid),
                nameof(Exception.InnerException),
                nameof(Exception.StackTrace),
                nameof(Exception.Data)
            };

            const int indexNotFound = -1;
            PropertyInfo[] properties = exception.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {

                if (Array.IndexOf(shownProperties, property.Name) == indexNotFound)
                {
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(exception));
                }
            }

            foreach (DictionaryEntry entry in exception.Data)
            {
                Console.WriteLine("Key: {0}, Value: {1}", entry.Key, entry.Value);
            }

            Console.WriteLine("StackTrace: {0}", exception.StackTrace);
        }

        private static void ShowInnerException(Exception innerException)
        {
            Console.WriteLine("----------------inner exception----------------");

            ShowException(innerException);

            if (innerException.InnerException != null)
                ShowInnerException(innerException.InnerException);

            Console.WriteLine("-------------end of inner exception-------------");
        }
    }
}
