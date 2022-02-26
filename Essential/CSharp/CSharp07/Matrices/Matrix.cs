using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Matrices
{
    public class Matrix
    {
        private double[,] elements;

        public int ColumnCount { get => elements.GetColumnCount(); }
        public int RowCount { get => elements.GetRowCount(); }
        public MatrixSize Size { get => new MatrixSize(RowCount, ColumnCount); } 

        public double this[int row, int column]
        {
            get
            {
                CheckRow(row);
                CheckColumn(column);

                return elements[row, column];
            }
            private set
            {
                CheckRow(row);
                CheckColumn(column);

                elements[row, column] = value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckRow(int row)
        {
            if (row < 0 || row >= RowCount)
                throw new InvalidOperationException(string.Format("Строка {0} не существует.", row));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckColumn(int column)
        {
            if (column < 0 || column >= ColumnCount)
                throw new InvalidOperationException(string.Format("Столбец {0} не существует.", column));
        }

        public Matrix(int rowCount, int columnCount)
        {
            if (rowCount == 0 && columnCount == 0)
                throw new InvalidOperationException(string.Format("Матрица {0}х{1} не может существовать.", rowCount, columnCount));

            elements = new double[rowCount, columnCount];
        }

        public Matrix(double[,] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(values));

            elements = new double[values.GetRowCount(), values.GetColumnCount()];
            for (int i = 0; i < values.GetRowCount(); i++)
            {
                for (int j = 0; j < values.GetColumnCount(); j++)
                {
                    elements[i, j] = values[i, j];
                }
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Matrix))
                return false;

            Matrix a = (Matrix)obj;
            return Equals(a);
        }

        public bool Equals(Matrix a)
        {
            if (a == null)
                return false;

            if (RowCount != a.RowCount || ColumnCount != a.ColumnCount)
                return false;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (a[i, j] != elements[i, j])
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result ^= (result << 5) + (result >> 2) + (i * j) + elements[i, j].GetHashCode(); 
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull(Matrix a, string aName)
        {
            if (a == null)
                throw new ArgumentNullException(aName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull(Matrix a, string aName, Matrix b, string bName)
        {
            CheckNull(a, aName);
            CheckNull(b, bName);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            if (a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount)
                throw new InvalidMatrixOperationException("Нельзя сложить матрицы:", a.Size, b.Size);

            Matrix result = new Matrix(a.RowCount, a.ColumnCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            if (a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount)
                throw new InvalidMatrixOperationException("Нельзя вычесть матрицы:", a.Size, b.Size);

            return a + b * (-1);
        }

        public static Matrix operator *(Matrix a, double value)
        {
            CheckNull(a, nameof(a));

            Matrix result = new Matrix(a.RowCount, a.ColumnCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    result[i, j] = a[i, j] * value;
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            if (a.ColumnCount != b.RowCount)
                throw new InvalidMatrixOperationException("Нельзя перемножить матрицы:", a.Size, b.Size);

            Matrix result = new Matrix(a.RowCount, b.ColumnCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < b.ColumnCount; j++)
                {
                    for (int k = 0; k < b.RowCount; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            decimal fractionalPart;
            decimal integerPart;
            for (int i = 0; i < RowCount; i++)
            {
                result.Append("|");
                for (int j = 0; j < ColumnCount; j++)
                {
                    integerPart = GetIntegerPart((decimal)elements[i, j], out fractionalPart);
                    result.AppendFormat("{0,5:###0.}{1,-5:.0###;.0###} ", integerPart, fractionalPart);
                }

                result.Append("|");
                result.AppendLine();
            }

            return result.ToString();
        }

        private decimal GetIntegerPart(decimal value, out decimal fractionalPart)
        {
            decimal integerPart = Math.Truncate(value);
            fractionalPart = value - integerPart;
            return integerPart;
        }
    }
}
