namespace TextEditor.Commands
{
    public abstract class Command
    {
        public Editor editor;
        private string backup;
        private string redone;

        protected Command(Editor editor) => this.editor = editor;

        protected void Backup() => backup = editor.textField.Text; 

        public void Undo()
        {
            redone = editor.textField.Text;
            editor.textField.Text = backup;
        }

        public void Redo() => editor.textField.Text = redone;

        public abstract bool Execute();
    }
}
