using System;
using System.Drawing;
using System.Windows.Forms;

namespace TasksUI
{
    public class DataGridViewProgressBarCell : DataGridViewCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds,
            Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates cellState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            Rectangle changedCellBounds = new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width, cellBounds.Height - 1);
            graphics.FillRectangle(Brushes.White, changedCellBounds);

            Rectangle progressBar = new Rectangle(changedCellBounds.X + 5, changedCellBounds.Y + 5, changedCellBounds.Width - 10, changedCellBounds.Height - 10);

            int progress = GetProgressWidth((int)value, progressBar.Width);
            Rectangle progressRect = new Rectangle(progressBar.X, progressBar.Y, progress, progressBar.Height);
            graphics.FillRectangle(Brushes.Green, progressRect);
            graphics.DrawRectangle(Pens.Black, progressBar);
        }

        private int GetProgressWidth(int value, int progressBarWidth) 
            => (int)Math.Round(((progressBarWidth / 100.0) * value), 0);
    }
}
