using System;

namespace SyncPrimitivesUI
{
    public class InvalidSleeperOperationException : ApplicationException
    {
        public DateTime ExceptionDateTime { get; private set; }

        public InvalidSleeperOperationException(string message, Exception innerException, DateTime exceptionDateTime) 
            : base(GetMessage(message, exceptionDateTime), innerException)
        {
            ExceptionDateTime = exceptionDateTime;
        }

        private static string GetMessage(string message, DateTime dateTime)
        {
            return $"[{dateTime}]: {message}";
        }
    }
}
