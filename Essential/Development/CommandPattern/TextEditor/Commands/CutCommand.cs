namespace TextEditor.Commands
{
    public class CutCommand : Command
    {
        public CutCommand(Editor editor) : base(editor) { }
        public override bool Execute()
        {
            if (string.IsNullOrEmpty(editor.textField.SelectedText))
                return false;

            Backup();
            string source = editor.textField.Text;
            editor.clipboard = editor.textField.SelectedText;
            editor.textField.Text = CutString(source);
            return true;
        }

        private string CutString(string source)
        {
            string start = source.Substring(0, editor.textField.SelectionStart);
            string end = source.Substring(editor.textField.SelectionLength + start.Length);
            return start + end;
        }
    }
}
