using System;
using System.Windows.Forms;
using TextEditor;
using TextEditor.Commands;

namespace TextEditorUI
{
    public partial class EditorUI : Form
    {
        private readonly Editor editor;

        public EditorUI()
        {
            InitializeComponent();

            editor = new Editor(textField);
        }

        private void btnUpperCase_Click(object sender, EventArgs e) => editor.ExecuteCommand(new UpperCaseCommand(editor));
        private void btnLowerCase_Click(object sender, EventArgs e) => editor.ExecuteCommand(new LowerCaseCommand(editor));
        private void btnCopy_Click(object sender, EventArgs e) => editor.ExecuteCommand(new CopyCommand(editor));
        private void btnCut_Click(object sender, EventArgs e) => editor.ExecuteCommand(new CutCommand(editor));
        private void btnPaste_Click(object sender, EventArgs e) => editor.ExecuteCommand(new PasteCommand(editor));
        private void btnUndo_Click(object sender, EventArgs e) => editor.Undo();
        private void btnRedo_Click(object sender, EventArgs e) => editor.Redo();

        private void itmUpperCase_Click(object sender, EventArgs e) => editor.ExecuteCommand(new UpperCaseCommand(editor));
        private void itmLowerCase_Click(object sender, EventArgs e) => editor.ExecuteCommand(new LowerCaseCommand(editor));
        private void itmCopy_Click(object sender, EventArgs e) => editor.ExecuteCommand(new CopyCommand(editor));
        private void itmCut_Click(object sender, EventArgs e) => editor.ExecuteCommand(new CutCommand(editor));
        private void itmPaste_Click(object sender, EventArgs e) => editor.ExecuteCommand(new PasteCommand(editor));
        private void itmUndo_Click(object sender, EventArgs e) => editor.Undo();
    }
}
