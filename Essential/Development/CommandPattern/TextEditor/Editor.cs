using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TextEditor.Commands;

namespace TextEditor
{
    public class Editor
    {
        public TextBox textField;
        public string clipboard;
        public CommandHistory history = new CommandHistory();
        public UndoingHistory undoings = new UndoingHistory();

        public Editor(TextBox textField) => this.textField = textField;

        public void ExecuteCommand(Command command)
        {
            if (command.Execute())
                history.Push(command);
        }

        public void Undo()
        {
            if (history.IsEmpty)
                return;

            Command command = history.Pop();
            undoings.Push(command);

            command?.Undo();
        }

        public void Redo()
        {
            if (undoings.IsEmpty)
                return;

            Command command = undoings.Pop();
            history.Push(command);

            command.Redo();
        }
    }
}
