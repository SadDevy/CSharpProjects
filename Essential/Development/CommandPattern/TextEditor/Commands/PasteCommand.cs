namespace TextEditor.Commands
{
    public class PasteCommand : Command
    {
        public PasteCommand(Editor editor) : base(editor) { }
        public override bool Execute()
        {
            if (string.IsNullOrEmpty(editor.clipboard))
                return false;

            Backup();
            editor.textField.Text = editor.textField.Text.Insert(editor.textField.SelectionStart, editor.clipboard);
            return true;
        }
    }
}
