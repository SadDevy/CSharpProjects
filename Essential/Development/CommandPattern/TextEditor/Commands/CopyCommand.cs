namespace TextEditor.Commands
{
    public class CopyCommand : Command
    {
        public CopyCommand(Editor editor) : base(editor) { }
        public override bool Execute()
        {
            editor.clipboard = editor.textField.SelectedText;
            return false;
        }
    }
}
