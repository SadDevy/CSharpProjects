namespace CurrencyConverter.WinView
{
    partial class FormView
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
            this.lblValue = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.tbCourse = new System.Windows.Forms.TextBox();
            this.btnInDollars = new System.Windows.Forms.Button();
            this.btnInRubles = new System.Windows.Forms.Button();
            this.tbDollars = new System.Windows.Forms.TextBox();
            this.lblDollars = new System.Windows.Forms.Label();
            this.tbRubles = new System.Windows.Forms.TextBox();
            this.lblRubles = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(16, 59);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(48, 20);
            this.lblValue.TabIndex = 0;
            this.lblValue.Text = "Value:";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(81, 56);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(149, 27);
            this.tbValue.TabIndex = 1;
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(16, 22);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(57, 20);
            this.lblCourse.TabIndex = 0;
            this.lblCourse.Text = "Course:";
            // 
            // tbCourse
            // 
            this.tbCourse.Location = new System.Drawing.Point(81, 19);
            this.tbCourse.Name = "tbCourse";
            this.tbCourse.Size = new System.Drawing.Size(149, 27);
            this.tbCourse.TabIndex = 1;
            this.tbCourse.TextChanged += new System.EventHandler(this.tbCourse_TextChanged);
            this.tbCourse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCourse_KeyPress);
            // 
            // btnInDollars
            // 
            this.btnInDollars.Location = new System.Drawing.Point(16, 90);
            this.btnInDollars.Name = "btnInDollars";
            this.btnInDollars.Size = new System.Drawing.Size(104, 29);
            this.btnInDollars.TabIndex = 2;
            this.btnInDollars.Text = "In Dollars";
            this.btnInDollars.UseVisualStyleBackColor = true;
            this.btnInDollars.Click += new System.EventHandler(this.btnInDollars_Click);
            // 
            // btnInRubles
            // 
            this.btnInRubles.Location = new System.Drawing.Point(126, 90);
            this.btnInRubles.Name = "btnInRubles";
            this.btnInRubles.Size = new System.Drawing.Size(104, 29);
            this.btnInRubles.TabIndex = 2;
            this.btnInRubles.Text = "In Rubles";
            this.btnInRubles.UseVisualStyleBackColor = true;
            this.btnInRubles.Click += new System.EventHandler(this.btnInRubles_Click);
            // 
            // tbDollars
            // 
            this.tbDollars.Location = new System.Drawing.Point(340, 16);
            this.tbDollars.Name = "tbDollars";
            this.tbDollars.ReadOnly = true;
            this.tbDollars.Size = new System.Drawing.Size(86, 27);
            this.tbDollars.TabIndex = 1;
            // 
            // lblDollars
            // 
            this.lblDollars.AutoSize = true;
            this.lblDollars.Location = new System.Drawing.Point(275, 19);
            this.lblDollars.Name = "lblDollars";
            this.lblDollars.Size = new System.Drawing.Size(59, 20);
            this.lblDollars.TabIndex = 0;
            this.lblDollars.Text = "Dollars:";
            // 
            // tbRubles
            // 
            this.tbRubles.Location = new System.Drawing.Point(340, 53);
            this.tbRubles.Name = "tbRubles";
            this.tbRubles.ReadOnly = true;
            this.tbRubles.Size = new System.Drawing.Size(86, 27);
            this.tbRubles.TabIndex = 1;
            // 
            // lblRubles
            // 
            this.lblRubles.AutoSize = true;
            this.lblRubles.Location = new System.Drawing.Point(275, 56);
            this.lblRubles.Name = "lblRubles";
            this.lblRubles.Size = new System.Drawing.Size(56, 20);
            this.lblRubles.TabIndex = 0;
            this.lblRubles.Text = "Rubles:";
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 134);
            this.Controls.Add(this.lblRubles);
            this.Controls.Add(this.tbRubles);
            this.Controls.Add(this.lblDollars);
            this.Controls.Add(this.tbDollars);
            this.Controls.Add(this.btnInRubles);
            this.Controls.Add(this.btnInDollars);
            this.Controls.Add(this.tbCourse);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lblValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormView";
            this.Text = "Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.TextBox tbCourse;
        private System.Windows.Forms.Button btnInDollars;
        private System.Windows.Forms.Button btnInRubles;
        private System.Windows.Forms.TextBox tbDollars;
        private System.Windows.Forms.Label lblDollars;
        private System.Windows.Forms.TextBox tbRubles;
        private System.Windows.Forms.Label lblRubles;
    }
}

