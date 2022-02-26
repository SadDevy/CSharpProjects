
namespace ADONet01
{
    partial class FormUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblProviderType = new System.Windows.Forms.Label();
            this.cbProviderType = new System.Windows.Forms.ComboBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.tbDbmsVersion = new System.Windows.Forms.TextBox();
            this.lblDbmsVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProviderType
            // 
            this.lblProviderType.AutoSize = true;
            this.lblProviderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProviderType.Location = new System.Drawing.Point(15, 12);
            this.lblProviderType.Name = "lblProviderType";
            this.lblProviderType.Size = new System.Drawing.Size(104, 15);
            this.lblProviderType.TabIndex = 0;
            this.lblProviderType.Text = "Тип провайдера:";
            // 
            // cbProviderType
            // 
            this.cbProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProviderType.FormattingEnabled = true;
            this.cbProviderType.Location = new System.Drawing.Point(18, 30);
            this.cbProviderType.Name = "cbProviderType";
            this.cbProviderType.Size = new System.Drawing.Size(172, 21);
            this.cbProviderType.TabIndex = 1;
            this.cbProviderType.SelectedIndexChanged += new System.EventHandler(this.cbProviderType_SelectedIndexChanged);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConnectionString.Location = new System.Drawing.Point(15, 69);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(132, 15);
            this.lblConnectionString.TabIndex = 2;
            this.lblConnectionString.Text = "Строка подключения:";
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Location = new System.Drawing.Point(19, 87);
            this.tbConnectionString.Multiline = true;
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Size = new System.Drawing.Size(354, 78);
            this.tbConnectionString.TabIndex = 3;
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.Location = new System.Drawing.Point(19, 185);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(149, 23);
            this.btnGetVersion.TabIndex = 4;
            this.btnGetVersion.Text = "Получить версию";
            this.btnGetVersion.UseVisualStyleBackColor = true;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // tbDbmsVersion
            // 
            this.tbDbmsVersion.Location = new System.Drawing.Point(19, 246);
            this.tbDbmsVersion.Multiline = true;
            this.tbDbmsVersion.Name = "tbDbmsVersion";
            this.tbDbmsVersion.ReadOnly = true;
            this.tbDbmsVersion.Size = new System.Drawing.Size(354, 52);
            this.tbDbmsVersion.TabIndex = 6;
            // 
            // lblDbmsVersion
            // 
            this.lblDbmsVersion.AutoSize = true;
            this.lblDbmsVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDbmsVersion.Location = new System.Drawing.Point(15, 228);
            this.lblDbmsVersion.Name = "lblDbmsVersion";
            this.lblDbmsVersion.Size = new System.Drawing.Size(88, 15);
            this.lblDbmsVersion.TabIndex = 5;
            this.lblDbmsVersion.Text = "Версия СУБД:";
            // 
            // FormUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 307);
            this.Controls.Add(this.tbDbmsVersion);
            this.Controls.Add(this.lblDbmsVersion);
            this.Controls.Add(this.btnGetVersion);
            this.Controls.Add(this.tbConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.cbProviderType);
            this.Controls.Add(this.lblProviderType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Версия СУБД";
            this.Load += new System.EventHandler(this.FormUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProviderType;
        private System.Windows.Forms.ComboBox cbProviderType;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.TextBox tbDbmsVersion;
        private System.Windows.Forms.Label lblDbmsVersion;
    }
}

