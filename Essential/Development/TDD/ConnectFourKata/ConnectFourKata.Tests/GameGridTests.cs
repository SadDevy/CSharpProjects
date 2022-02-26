using NUnit.Framework;
using System;
using System.Drawing;

namespace ConnectFourKata.Tests
{
    [TestFixture]
    public class GameGridTests
    {
        [Test]
        public void TestCreateGrid()
        {
            const int expectedColumns = 7;
            const int expectedRows = 6;
            const int expectedNumber = 4;

            GameGrid grid = GameGrid.CreateGrid();

            Assert.IsNotNull(grid);
            Assert.AreEqual(expectedColumns, grid.Columns);
            Assert.AreEqual(expectedRows, grid.Rows);
            Assert.AreEqual(expectedNumber, grid.NumberToWin);
        }

        [Test]
        public void TestCreateGrid_NumberNotLessThanRows_Failure()
        {
            static void A()
            {
                const int columns = 7;
                const int rows = 6;
                const int numberToWin = 6;

                GameGrid grid = GameGrid.CreateGrid(columns, rows, numberToWin);
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        [Test]
        public void TestCreateGrid_NumberGreaterThanColumns_Failure()
        {
            static void A()
            {
                const int columns = 6;
                const int rows = 7;
                const int numberToWin = 6;

                GameGrid grid = GameGrid.CreateGrid(columns, rows, numberToWin);
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        [Test]
        public void TestAdd()
        {
            GameGrid grid = GameGrid.CreateGrid();

            const int column = 1;
            Player player = new Player(Color.Red);
            bool actual = grid.Add(player, column);

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestAdd_NotExistsColumn_False()
        {
            GameGrid grid = GameGrid.CreateGrid();

            const int column = -1;
            Player player = new Player(Color.Red);
            bool actual = grid.Add(player, column);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestAdd_PieceToFirstColumnPosition_True()
        {
            const int column = 0;
            Color expected = Color.Red;
            GameGrid grid = GameGrid.CreateGrid();
            Player player = new Player(expected);

            bool actualAdded = grid.Add(player, column);
            Color actual = grid.Grid[column, grid.Rows - 1];

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actualAdded);
        }

        [Test]
        public void TestAdd_ExistsPieceInFirstPosition_True()
        {
            const int column = 0;
            Color expected = Color.Red;
            GameGrid grid = GameGrid.CreateGrid();
            Player player = new Player(expected);

            grid.Add(player, column);

            bool actualAdded = grid.Add(player, column);
            Color actual = grid.Grid[column, grid.Rows - 2];

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actualAdded);
        }

        [Test]
        public void TestAdd_ExistsPieceInLastColumnPosition_True()
        {
            const int column = 0;
            Color expected = Color.Red;
            GameGrid grid = GameGrid.CreateGrid();
            Player player = new Player(expected);

            int i = 0;
            while (i++ < 6)
                grid.Add(player, column);

            bool actual = grid.Add(player, column);

            Assert.IsFalse(actual);
        }

        [Test]
        public void TestHasWinner()
        {
            GameGrid grid = GameGrid.CreateGrid();

            Assert.IsFalse(grid.HasWinner(default, default));
        }

        [Test]
        public void TestHasWinner_RedWinnerColumn_True()
        {
            Color expected = Color.Red;
            GameGrid grid = GameGrid.CreateGrid();

            const int column = 0;
            const int row = 2;
            Player player = new Player(expected);

            int i = 0;
            while (i++ < 4)
                grid.Add(player, column);

            bool actual = grid.HasWinner(player, new Point(column, row));

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestHasWinner_RedWinnerRow_True()
        {
            Color expected = Color.Red;
            GameGrid grid = GameGrid.CreateGrid();
            Player player = new Player(expected);

            const int firstColumnt = 1;
            const int lastColumn = 4;
            for (int i = firstColumnt; i <= lastColumn; i++)
                grid.Add(player, i);

            bool actual = grid.HasWinner(player, new Point(lastColumn, grid.Rows - 1));

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestHasWinner_RedWinnerMainDiagonal_True()
        {
            Color c1 = Color.Red;
            Color c2 = Color.Green;
            GameGrid grid = GameGrid.CreateGrid();

            Player p1 = new Player(c1);
            Player p2 = new Player(c2);

            grid.Add(p1, 1);

            grid.Add(p2, 2);
            grid.Add(p1, 2);

            grid.Add(p1, 3);
            grid.Add(p2, 3);
            grid.Add(p1, 3);

            grid.Add(p2, 4);
            grid.Add(p2, 4);
            grid.Add(p1, 4);
            grid.Add(p1, 4);

            bool actual = grid.HasWinner(p1, new Point(3, 3));

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestHasWinner_RedWinnerSideDiagonal_True()
        {
            Color c1 = Color.Red;
            Color c2 = Color.Green;
            GameGrid grid = GameGrid.CreateGrid();

            Player p1 = new Player(c1);
            Player p2 = new Player(c2);

            grid.Add(p1, 0);
            grid.Add(p2, 0);
            grid.Add(p2, 0);
            grid.Add(p1, 0);

            grid.Add(p2, 1);
            grid.Add(p2, 1);
            grid.Add(p1, 1);

            grid.Add(p2, 2);

            grid.Add(p1, 3);

            grid.Add(p1, 2);

            bool actual = grid.HasWinner(p1, new Point(2, 4));

            Assert.IsTrue(actual);
        }
    }
}
