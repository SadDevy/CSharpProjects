using System.Windows.Forms;

namespace TasksUI
{
    public class DataGridViewProgressBarColumn : DataGridViewColumn
    {
        public DataGridViewProgressBarColumn() 
            => CellTemplate = new DataGridViewProgressBarCell();
    }
}
