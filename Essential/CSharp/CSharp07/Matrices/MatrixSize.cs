namespace Matrices
{
    public struct MatrixSize
    {
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public MatrixSize(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }
    }
}
