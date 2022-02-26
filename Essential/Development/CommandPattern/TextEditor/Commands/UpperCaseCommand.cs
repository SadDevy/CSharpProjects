using System;
using System.Collections.Generic;
using System.Text;

namespace TextEditor.Commands
{
    public class UpperCaseCommand : Command
    {
        public UpperCaseCommand(Editor editor) : base(editor) { }
        public override bool Execute()
        {
            if (editor.textField == null)
                return false;
         
            Backup();

            if (string.IsNullOrEmpty(editor.textField.SelectedText))
                editor.textField.Text = editor.textField.Text.ToUpper();
            else
            {
                editor.clipboard = editor.textField.SelectedText;
                editor.textField.SelectedText = editor.textField.SelectedText.ToUpper();
            }

            return true;
        }
    }
}
