using System;
using System.Runtime.Serialization;

namespace Matrices
{
    [Serializable]
    public class InvalidMatrixOperationException : ApplicationException
    {
        private string _message;

        public int ARowCount { get; private set; }
        public int AColumnCount { get; private set; }
        public int BRowCount { get; private set; }
        public int BColumnCount { get; private set; }

        public override string Message { get => _message;  }

        public InvalidMatrixOperationException(MatrixSize a, MatrixSize b) : base()
        {
            Init(a, b);
        }

        public InvalidMatrixOperationException(string message, MatrixSize a, MatrixSize b) : base(message)
        {
            Init(a, b, message);
        }

        public InvalidMatrixOperationException(string message, Exception innerException, MatrixSize a, MatrixSize b) 
            : base(message, innerException)
        {
            Init(a, b, message);
        }

        protected InvalidMatrixOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ARowCount = (int)info.GetValue(nameof(ARowCount), typeof(int));
            AColumnCount = (int)info.GetValue(nameof(AColumnCount), typeof(int));
            BRowCount = (int)info.GetValue(nameof(BRowCount), typeof(int));
            BColumnCount = (int)info.GetValue(nameof(BColumnCount), typeof(int));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Message), _message, typeof(string));
            info.AddValue(nameof(ARowCount), ARowCount, typeof(int));
            info.AddValue(nameof(AColumnCount), AColumnCount, typeof(int));
            info.AddValue(nameof(BRowCount), BRowCount, typeof(int));
            info.AddValue(nameof(BColumnCount), BColumnCount, typeof(int));
        }

        private void Init(MatrixSize a, MatrixSize b, string message = null)
        {
            ARowCount = a.RowCount;
            AColumnCount = a.ColumnCount;
            BRowCount = b.RowCount;
            BColumnCount = b.ColumnCount;

            if (message != null)
                _message = string.Format("{0} {1}x{2} и {3}x{4}", message, ARowCount, AColumnCount, BRowCount, BColumnCount);
        }

    }
}
