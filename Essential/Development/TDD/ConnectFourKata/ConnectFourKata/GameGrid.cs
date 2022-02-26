using System;
using System.Drawing;

namespace ConnectFourKata
{
    public class GameGrid
    {
        public int Columns { get; }
        public int Rows { get; }
        public int NumberToWin { get; }

        private Color[,] grid;
        public Color[,] Grid { get => grid; }

        public Color Winner { get; }

        private GameGrid(int columns, int rows, int numberToWin)
        {
            Columns = columns;
            Rows = rows;
            NumberToWin = numberToWin;

            grid = new Color[columns, rows];
        }

        public static GameGrid CreateGrid(int columns = 7, int rows = 6, int numberToWin = 4)
        {
            if (CouldMakeGrid(columns, rows, numberToWin))
                return new GameGrid(columns, rows, numberToWin);

            string failureMessage = string.Format("Нельзя создать сетку, где число столбцов: {0}, число строк: {1} и количество фишек для выигрыша: {2}",
                                                  columns, rows, numberToWin);

            throw new InvalidOperationException(failureMessage);
        }

        private static bool CouldMakeGrid(int columns, int rows, int numberToWin) => numberToWin < columns && numberToWin < rows;

        public bool Add(Player p, int column)
        {
            if (p == null)
                return false;

            if (ColumnNotExists(column) || ColumnFilled(column))
                return false;

            Add(p.Color, column);

            return true;
        }

        private bool ColumnNotExists(int column) => column < 0 || column > Columns;

        private bool ColumnFilled(int column) => grid[column, 0] != Color.Empty;

        private void Add(Color color, int column)
        {
            int row = Rows - 1;
            while (row > -1 && grid[column, row] != Color.Empty)
                row -= 1;

            grid[column, row] = color;
        }

        public bool HasWinner(Player p, Point position)
        {
            if (p == null)
                return false;

            return HasWinnerInColumn(p, position) || HasWinnerInRow(p, position.Y) || HasWinnerInDiagonal(p, position);
        }

        private bool HasWinnerInColumn(Player p, Point position)
        {
            int count = 0;
            for (int i = position.Y; i < Rows && count != NumberToWin; i++)
            {
                if (grid[position.X, i] != p.Color)
                    break;

                count++;
            }

            return count == NumberToWin;
        }

        private bool HasWinnerInRow(Player p, int row)
        {
            int count = 0;
            for (int i = 0; i < Columns && count != NumberToWin; i++)
            {
                if (grid[i, row] != p.Color)
                    count = 0;

                count++;
            }

            return count == NumberToWin;
        }

        private bool HasWinnerInDiagonal(Player p, Point position) => HasWinnerInMainDiagonal(p, position)
                                                                   || HasWinnerInSideDiagonal(p, position);

        private bool HasWinnerInMainDiagonal(Player p, Point position)
        {
            Point checkedPosition = position;

            int lastRowIndex = Rows - 1;
            int offset = (checkedPosition.Y + checkedPosition.X > lastRowIndex) ? lastRowIndex - checkedPosition.Y
                                                                                : checkedPosition.X;
            checkedPosition.X -= offset;
            checkedPosition.Y += offset;

            int count = 0;
            while (checkedPosition.X < Columns && checkedPosition.Y >= 0 && count != NumberToWin)
            {
                if (grid[checkedPosition.X, checkedPosition.Y] != p.Color)
                    count = 0;

                count++;
                checkedPosition.X++;
                checkedPosition.Y--;
            }

            return count == NumberToWin;
        }

        private bool HasWinnerInSideDiagonal(Player p, Point position)
        {
            int checkedColumn = position.X;
            int checkedRow = position.Y;

            int offset = (checkedRow - checkedColumn < 0) ? checkedRow : checkedColumn;
            checkedColumn -= offset;
            checkedRow -= offset;

            int count = 0;
            int i;
            int j;
            for (i = checkedColumn, j = checkedRow; i < Columns && j < Rows && count != NumberToWin; i++, j++)
            {
                if (grid[i, j] != p.Color)
                    count = 0;

                count++;
            }

            return count == NumberToWin;
        }
    }
}
