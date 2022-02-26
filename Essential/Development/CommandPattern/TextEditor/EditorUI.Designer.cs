namespace TextEditorUI
{
    partial class EditorUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textField = new System.Windows.Forms.TextBox();
            this.btnUpperCase = new System.Windows.Forms.Button();
            this.btnLowerCase = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.itmUpperCase = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLowerCase = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.itmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRedo = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // textField
            // 
            this.textField.Dock = System.Windows.Forms.DockStyle.Top;
            this.textField.Location = new System.Drawing.Point(0, 24);
            this.textField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textField.Multiline = true;
            this.textField.Name = "textField";
            this.textField.Size = new System.Drawing.Size(627, 94);
            this.textField.TabIndex = 0;
            // 
            // btnUpperCase
            // 
            this.btnUpperCase.Location = new System.Drawing.Point(10, 116);
            this.btnUpperCase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpperCase.Name = "btnUpperCase";
            this.btnUpperCase.Size = new System.Drawing.Size(82, 22);
            this.btnUpperCase.TabIndex = 1;
            this.btnUpperCase.Text = "Upper Case";
            this.btnUpperCase.UseVisualStyleBackColor = true;
            this.btnUpperCase.Click += new System.EventHandler(this.btnUpperCase_Click);
            // 
            // btnLowerCase
            // 
            this.btnLowerCase.Location = new System.Drawing.Point(98, 116);
            this.btnLowerCase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLowerCase.Name = "btnLowerCase";
            this.btnLowerCase.Size = new System.Drawing.Size(82, 22);
            this.btnLowerCase.TabIndex = 1;
            this.btnLowerCase.Text = "Lower Case";
            this.btnLowerCase.UseVisualStyleBackColor = true;
            this.btnLowerCase.Click += new System.EventHandler(this.btnLowerCase_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(186, 116);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(82, 22);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCut
            // 
            this.btnCut.Location = new System.Drawing.Point(273, 116);
            this.btnCut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(82, 22);
            this.btnCut.TabIndex = 1;
            this.btnCut.Text = "Cut";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(360, 116);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(82, 22);
            this.btnPaste.TabIndex = 1;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(448, 116);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(82, 22);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEdit});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(627, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmUpperCase,
            this.itmLowerCase,
            this.itmCopy,
            this.itmCut,
            this.itmPaste,
            this.itmUndo});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "Edit";
            // 
            // itmUpperCase
            // 
            this.itmUpperCase.Name = "itmUpperCase";
            this.itmUpperCase.Size = new System.Drawing.Size(134, 22);
            this.itmUpperCase.Text = "Upper Case";
            this.itmUpperCase.Click += new System.EventHandler(this.itmUpperCase_Click);
            // 
            // itmLowerCase
            // 
            this.itmLowerCase.Name = "itmLowerCase";
            this.itmLowerCase.Size = new System.Drawing.Size(134, 22);
            this.itmLowerCase.Text = "Lower Case";
            this.itmLowerCase.Click += new System.EventHandler(this.itmLowerCase_Click);
            // 
            // itmCopy
            // 
            this.itmCopy.Name = "itmCopy";
            this.itmCopy.Size = new System.Drawing.Size(134, 22);
            this.itmCopy.Text = "Copy";
            this.itmCopy.Click += new System.EventHandler(this.itmCopy_Click);
            // 
            // itmCut
            // 
            this.itmCut.Name = "itmCut";
            this.itmCut.Size = new System.Drawing.Size(134, 22);
            this.itmCut.Text = "Cut";
            this.itmCut.Click += new System.EventHandler(this.itmCut_Click);
            // 
            // itmPaste
            // 
            this.itmPaste.Name = "itmPaste";
            this.itmPaste.Size = new System.Drawing.Size(134, 22);
            this.itmPaste.Text = "Paste";
            this.itmPaste.Click += new System.EventHandler(this.itmPaste_Click);
            // 
            // itmUndo
            // 
            this.itmUndo.Name = "itmUndo";
            this.itmUndo.Size = new System.Drawing.Size(134, 22);
            this.itmUndo.Text = "Undo";
            this.itmUndo.Click += new System.EventHandler(this.itmUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(536, 115);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(75, 23);
            this.btnRedo.TabIndex = 3;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // EditorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 147);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCut);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnLowerCase);
            this.Controls.Add(this.btnUpperCase);
            this.Controls.Add(this.textField);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditorUI";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textField;
        private System.Windows.Forms.Button btnUpperCase;
        private System.Windows.Forms.Button btnLowerCase;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem itmUpperCase;
        private System.Windows.Forms.ToolStripMenuItem itmLowerCase;
        private System.Windows.Forms.ToolStripMenuItem itmCopy;
        private System.Windows.Forms.ToolStripMenuItem itmCut;
        private System.Windows.Forms.ToolStripMenuItem itmPaste;
        private System.Windows.Forms.ToolStripMenuItem itmUndo;
        private System.Windows.Forms.Button btnRedo;
    }
}

