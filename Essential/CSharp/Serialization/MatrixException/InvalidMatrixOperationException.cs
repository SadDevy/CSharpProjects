using System;
using System.Runtime.Serialization;

namespace MatrixException
{
    [Serializable]
    public class InvalidMatrixOperationException : ApplicationException
    {
        public int ARowCount { get; private set; }
        public int AColumnCount { get; private set; }
        public int BRowCount { get; private set; }
        public int BColumnCount { get; private set; }

        public InvalidMatrixOperationException(MatrixSize a, MatrixSize b) : base()
        {
            Init(a, b);
        }

        public InvalidMatrixOperationException(string message, MatrixSize a, MatrixSize b) 
            : base(GetMessage(message, a, b))
        {
            Init(a, b);
        }

        public InvalidMatrixOperationException(string message, Exception innerException, MatrixSize a, MatrixSize b)
            : base(GetMessage(message, a, b), innerException)
        {
            Init(a, b);
        }

        protected InvalidMatrixOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                ARowCount = (int)info.GetValue(nameof(ARowCount), typeof(int));
                AColumnCount = (int)info.GetValue(nameof(AColumnCount), typeof(int));
                BRowCount = (int)info.GetValue(nameof(BRowCount), typeof(int));
                BColumnCount = (int)info.GetValue(nameof(BColumnCount), typeof(int));
            }
        }

        private static string GetMessage(string message, MatrixSize a, MatrixSize b)
        {
            return string.Format("{0} {1}x{2} и {3}x{4}", message, a.RowCount, a.ColumnCount, b.RowCount, b.ColumnCount);
        }

        private void Init(MatrixSize a, MatrixSize b)
        {
            ARowCount = a.RowCount;
            AColumnCount = a.ColumnCount;
            BRowCount = b.RowCount;
            BColumnCount = b.ColumnCount;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue(nameof(ARowCount), ARowCount, typeof(int));
                info.AddValue(nameof(AColumnCount), AColumnCount, typeof(int));
                info.AddValue(nameof(BRowCount), BRowCount, typeof(int));
                info.AddValue(nameof(BColumnCount), BColumnCount, typeof(int));
            }
        }
    }
}
