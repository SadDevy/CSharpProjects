using System.Diagnostics;

namespace Matrices
{
    public static class ArrayExtensions
    {
        private const int rowIndex = 0;
        private const int columnIndex = 1;

        public static int GetRowCount(this double[,] array)
        {
            return array.GetLength(rowIndex); 
        }

        public static int GetColumnCount(this double[,] array) 
        {
            return array.GetLength(columnIndex);
        }
    }
}
