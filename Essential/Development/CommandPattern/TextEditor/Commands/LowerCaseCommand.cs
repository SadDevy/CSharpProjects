using System;
using System.Collections.Generic;
using System.Text;

namespace TextEditor.Commands
{
    public class LowerCaseCommand : Command
    {
        public LowerCaseCommand(Editor editor) : base(editor) { }
        public override bool Execute()
        {
            if (editor.textField == null)
                return false;

            Backup();

            if (string.IsNullOrEmpty(editor.textField.SelectedText))
                editor.textField.Text = editor.textField.Text.ToLower();
            else
            {
                editor.clipboard = editor.textField.SelectedText;
                editor.textField.SelectedText = editor.textField.SelectedText.ToLower();
            }

            return true;
        }
    }
}
