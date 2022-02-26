
namespace AsyncAndAwaitUI
{
    partial class FormUI
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlDownlod = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.pnlFileInfo = new System.Windows.Forms.Panel();
            this.lblFileHeightValue = new System.Windows.Forms.Label();
            this.lblFileWidthValue = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.pnlImageInfo = new System.Windows.Forms.Panel();
            this.lblImageLengthValue = new System.Windows.Forms.Label();
            this.lblImageTypeValue = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblImageType = new System.Windows.Forms.Label();
            this.pnlUrl = new System.Windows.Forms.Panel();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.pnlClearButton = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlCancelButton = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlDownloadButton = new System.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.pnlPictureBox = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pnlBackgroundBot = new System.Windows.Forms.Panel();
            this.pnlBackgroundTop = new System.Windows.Forms.Panel();
            this.pnlBackgroundRight = new System.Windows.Forms.Panel();
            this.pnlBackgroundLeft = new System.Windows.Forms.Panel();
            this.pnlBackground.SuspendLayout();
            this.pnlDownlod.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlFileInfo.SuspendLayout();
            this.pnlImageInfo.SuspendLayout();
            this.pnlUrl.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlClearButton.SuspendLayout();
            this.pnlCancelButton.SuspendLayout();
            this.pnlDownloadButton.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.pnlPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            this.pnlBackground.Controls.Add(this.pnlDownlod);
            this.pnlBackground.Controls.Add(this.pnlProgress);
            this.pnlBackground.Controls.Add(this.pnlPictureBox);
            this.pnlBackground.Controls.Add(this.pnlBackgroundBot);
            this.pnlBackground.Controls.Add(this.pnlBackgroundTop);
            this.pnlBackground.Controls.Add(this.pnlBackgroundRight);
            this.pnlBackground.Controls.Add(this.pnlBackgroundLeft);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(619, 481);
            this.pnlBackground.TabIndex = 0;
            // 
            // pnlDownlod
            // 
            this.pnlDownlod.Controls.Add(this.pnlInfo);
            this.pnlDownlod.Controls.Add(this.pnlButtons);
            this.pnlDownlod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDownlod.Location = new System.Drawing.Point(19, 341);
            this.pnlDownlod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDownlod.Name = "pnlDownlod";
            this.pnlDownlod.Size = new System.Drawing.Size(581, 124);
            this.pnlDownlod.TabIndex = 8;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.pnlFileInfo);
            this.pnlInfo.Controls.Add(this.pnlImageInfo);
            this.pnlInfo.Controls.Add(this.pnlUrl);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(480, 124);
            this.pnlInfo.TabIndex = 1;
            // 
            // pnlFileInfo
            // 
            this.pnlFileInfo.Controls.Add(this.lblFileHeightValue);
            this.pnlFileInfo.Controls.Add(this.lblFileWidthValue);
            this.pnlFileInfo.Controls.Add(this.lblHeight);
            this.pnlFileInfo.Controls.Add(this.lblWidth);
            this.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileInfo.Location = new System.Drawing.Point(0, 88);
            this.pnlFileInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFileInfo.Name = "pnlFileInfo";
            this.pnlFileInfo.Size = new System.Drawing.Size(480, 44);
            this.pnlFileInfo.TabIndex = 3;
            // 
            // lblFileHeightValue
            // 
            this.lblFileHeightValue.AutoSize = true;
            this.lblFileHeightValue.Location = new System.Drawing.Point(196, 15);
            this.lblFileHeightValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileHeightValue.Name = "lblFileHeightValue";
            this.lblFileHeightValue.Size = new System.Drawing.Size(0, 17);
            this.lblFileHeightValue.TabIndex = 5;
            // 
            // lblFileWidthValue
            // 
            this.lblFileWidthValue.AutoSize = true;
            this.lblFileWidthValue.Location = new System.Drawing.Point(77, 15);
            this.lblFileWidthValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileWidthValue.Name = "lblFileWidthValue";
            this.lblFileWidthValue.Size = new System.Drawing.Size(0, 17);
            this.lblFileWidthValue.TabIndex = 4;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(139, 15);
            this.lblHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(57, 17);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "Height: ";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(-3, 15);
            this.lblWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(78, 17);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "File Width: ";
            // 
            // pnlImageInfo
            // 
            this.pnlImageInfo.Controls.Add(this.lblImageLengthValue);
            this.pnlImageInfo.Controls.Add(this.lblImageTypeValue);
            this.pnlImageInfo.Controls.Add(this.lblLength);
            this.pnlImageInfo.Controls.Add(this.lblImageType);
            this.pnlImageInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImageInfo.Location = new System.Drawing.Point(0, 44);
            this.pnlImageInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlImageInfo.Name = "pnlImageInfo";
            this.pnlImageInfo.Size = new System.Drawing.Size(480, 44);
            this.pnlImageInfo.TabIndex = 2;
            // 
            // lblImageLengthValue
            // 
            this.lblImageLengthValue.AutoSize = true;
            this.lblImageLengthValue.Location = new System.Drawing.Point(201, 15);
            this.lblImageLengthValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageLengthValue.Name = "lblImageLengthValue";
            this.lblImageLengthValue.Size = new System.Drawing.Size(0, 17);
            this.lblImageLengthValue.TabIndex = 4;
            // 
            // lblImageTypeValue
            // 
            this.lblImageTypeValue.AutoSize = true;
            this.lblImageTypeValue.Location = new System.Drawing.Point(89, 15);
            this.lblImageTypeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageTypeValue.Name = "lblImageTypeValue";
            this.lblImageTypeValue.Size = new System.Drawing.Size(0, 17);
            this.lblImageTypeValue.TabIndex = 3;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(139, 15);
            this.lblLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(60, 17);
            this.lblLength.TabIndex = 2;
            this.lblLength.Text = "Length: ";
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Location = new System.Drawing.Point(-3, 15);
            this.lblImageType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(90, 17);
            this.lblImageType.TabIndex = 1;
            this.lblImageType.Text = "Image Type: ";
            // 
            // pnlUrl
            // 
            this.pnlUrl.Controls.Add(this.tbUrl);
            this.pnlUrl.Controls.Add(this.lblUrl);
            this.pnlUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUrl.Location = new System.Drawing.Point(0, 0);
            this.pnlUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlUrl.Name = "pnlUrl";
            this.pnlUrl.Size = new System.Drawing.Size(480, 44);
            this.pnlUrl.TabIndex = 1;
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Location = new System.Drawing.Point(37, 10);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(421, 22);
            this.tbUrl.TabIndex = 1;
            this.tbUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbUrl_MouseDown);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(-4, 14);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(36, 17);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.pnlClearButton);
            this.pnlButtons.Controls.Add(this.pnlCancelButton);
            this.pnlButtons.Controls.Add(this.pnlDownloadButton);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButtons.Location = new System.Drawing.Point(480, 0);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(101, 124);
            this.pnlButtons.TabIndex = 0;
            // 
            // pnlClearButton
            // 
            this.pnlClearButton.Controls.Add(this.btnClear);
            this.pnlClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClearButton.Location = new System.Drawing.Point(0, 88);
            this.pnlClearButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlClearButton.Name = "pnlClearButton";
            this.pnlClearButton.Size = new System.Drawing.Size(101, 44);
            this.pnlClearButton.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(0, 9);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlCancelButton
            // 
            this.pnlCancelButton.Controls.Add(this.btnCancel);
            this.pnlCancelButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCancelButton.Location = new System.Drawing.Point(0, 44);
            this.pnlCancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCancelButton.Name = "pnlCancelButton";
            this.pnlCancelButton.Size = new System.Drawing.Size(101, 44);
            this.pnlCancelButton.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(0, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlDownloadButton
            // 
            this.pnlDownloadButton.Controls.Add(this.btnDownload);
            this.pnlDownloadButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDownloadButton.Location = new System.Drawing.Point(0, 0);
            this.pnlDownloadButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDownloadButton.Name = "pnlDownloadButton";
            this.pnlDownloadButton.Size = new System.Drawing.Size(101, 44);
            this.pnlDownloadButton.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(0, 7);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 28);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgress.Controls.Add(this.pbDownload);
            this.pnlProgress.Location = new System.Drawing.Point(19, 315);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(581, 27);
            this.pnlProgress.TabIndex = 7;
            // 
            // pbDownload
            // 
            this.pbDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDownload.Location = new System.Drawing.Point(0, 0);
            this.pbDownload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(581, 27);
            this.pbDownload.TabIndex = 0;
            // 
            // pnlPictureBox
            // 
            this.pnlPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPictureBox.Controls.Add(this.pbImage);
            this.pnlPictureBox.Location = new System.Drawing.Point(19, 16);
            this.pnlPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPictureBox.Name = "pnlPictureBox";
            this.pnlPictureBox.Size = new System.Drawing.Size(581, 299);
            this.pnlPictureBox.TabIndex = 4;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(581, 299);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            // 
            // pnlBackgroundBot
            // 
            this.pnlBackgroundBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBackgroundBot.Location = new System.Drawing.Point(19, 465);
            this.pnlBackgroundBot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackgroundBot.Name = "pnlBackgroundBot";
            this.pnlBackgroundBot.Size = new System.Drawing.Size(581, 16);
            this.pnlBackgroundBot.TabIndex = 3;
            // 
            // pnlBackgroundTop
            // 
            this.pnlBackgroundTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBackgroundTop.Location = new System.Drawing.Point(19, 0);
            this.pnlBackgroundTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackgroundTop.Name = "pnlBackgroundTop";
            this.pnlBackgroundTop.Size = new System.Drawing.Size(581, 16);
            this.pnlBackgroundTop.TabIndex = 2;
            // 
            // pnlBackgroundRight
            // 
            this.pnlBackgroundRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBackgroundRight.Location = new System.Drawing.Point(600, 0);
            this.pnlBackgroundRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackgroundRight.Name = "pnlBackgroundRight";
            this.pnlBackgroundRight.Size = new System.Drawing.Size(19, 481);
            this.pnlBackgroundRight.TabIndex = 1;
            // 
            // pnlBackgroundLeft
            // 
            this.pnlBackgroundLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBackgroundLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlBackgroundLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackgroundLeft.Name = "pnlBackgroundLeft";
            this.pnlBackgroundLeft.Size = new System.Drawing.Size(19, 481);
            this.pnlBackgroundLeft.TabIndex = 0;
            // 
            // FormUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 481);
            this.Controls.Add(this.pnlBackground);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(634, 518);
            this.Name = "FormUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Downloader";
            this.pnlBackground.ResumeLayout(false);
            this.pnlDownlod.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlFileInfo.ResumeLayout(false);
            this.pnlFileInfo.PerformLayout();
            this.pnlImageInfo.ResumeLayout(false);
            this.pnlImageInfo.PerformLayout();
            this.pnlUrl.ResumeLayout(false);
            this.pnlUrl.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlClearButton.ResumeLayout(false);
            this.pnlCancelButton.ResumeLayout(false);
            this.pnlDownloadButton.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlPictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlPictureBox;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnlBackgroundBot;
        private System.Windows.Forms.Panel pnlBackgroundTop;
        private System.Windows.Forms.Panel pnlBackgroundRight;
        private System.Windows.Forms.Panel pnlBackgroundLeft;
        private System.Windows.Forms.Panel pnlDownlod;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel pnlFileInfo;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Panel pnlImageInfo;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.Panel pnlUrl;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlClearButton;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlCancelButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlDownloadButton;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblFileHeightValue;
        private System.Windows.Forms.Label lblFileWidthValue;
        private System.Windows.Forms.Label lblImageLengthValue;
        private System.Windows.Forms.Label lblImageTypeValue;
    }
}

