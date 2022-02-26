
namespace TasksUI
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
            this.components = new System.ComponentModel.Container();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsSTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSCompleted = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSInProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSInQueue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSError = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBackgroundTop = new System.Windows.Forms.Panel();
            this.pnlBackgroundLeft = new System.Windows.Forms.Panel();
            this.pnlBackgroundRight = new System.Windows.Forms.Panel();
            this.pnlBackgroundBottom = new System.Windows.Forms.Panel();
            this.pnlBackgrond = new System.Windows.Forms.Panel();
            this.pnlDownloadingGrid = new System.Windows.Forms.Panel();
            this.dgvDownloading = new System.Windows.Forms.DataGridView();
            this.fileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileSizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressDataGridViewTextBoxColumn = new TasksUI.DataGridViewProgressBarColumn();
            this.urlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsDownloading = new System.Windows.Forms.BindingSource(this.components);
            this.pnlDownloadingSeparation = new System.Windows.Forms.Panel();
            this.pnlDownloadingInfo = new System.Windows.Forms.Panel();
            this.gbDownloadingInfo = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.statusBar.SuspendLayout();
            this.pnlBackgrond.SuspendLayout();
            this.pnlDownloadingGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDownloading)).BeginInit();
            this.pnlDownloadingInfo.SuspendLayout();
            this.gbDownloadingInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSTotal,
            this.tsSCompleted,
            this.tsSSize,
            this.tsSInProgress,
            this.tsSInQueue,
            this.tsSError});
            this.statusBar.Location = new System.Drawing.Point(0, 429);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(610, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "statusStrip1";
            // 
            // tsSTotal
            // 
            this.tsSTotal.Name = "tsSTotal";
            this.tsSTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSCompleted
            // 
            this.tsSCompleted.Name = "tsSCompleted";
            this.tsSCompleted.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSSize
            // 
            this.tsSSize.Name = "tsSSize";
            this.tsSSize.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSInProgress
            // 
            this.tsSInProgress.Name = "tsSInProgress";
            this.tsSInProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSInQueue
            // 
            this.tsSInQueue.Name = "tsSInQueue";
            this.tsSInQueue.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSError
            // 
            this.tsSError.Name = "tsSError";
            this.tsSError.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlBackgroundTop
            // 
            this.pnlBackgroundTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBackgroundTop.Location = new System.Drawing.Point(0, 0);
            this.pnlBackgroundTop.Name = "pnlBackgroundTop";
            this.pnlBackgroundTop.Size = new System.Drawing.Size(610, 15);
            this.pnlBackgroundTop.TabIndex = 1;
            // 
            // pnlBackgroundLeft
            // 
            this.pnlBackgroundLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBackgroundLeft.Location = new System.Drawing.Point(0, 15);
            this.pnlBackgroundLeft.Name = "pnlBackgroundLeft";
            this.pnlBackgroundLeft.Size = new System.Drawing.Size(15, 414);
            this.pnlBackgroundLeft.TabIndex = 2;
            // 
            // pnlBackgroundRight
            // 
            this.pnlBackgroundRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBackgroundRight.Location = new System.Drawing.Point(595, 15);
            this.pnlBackgroundRight.Name = "pnlBackgroundRight";
            this.pnlBackgroundRight.Size = new System.Drawing.Size(15, 414);
            this.pnlBackgroundRight.TabIndex = 3;
            // 
            // pnlBackgroundBottom
            // 
            this.pnlBackgroundBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBackgroundBottom.Location = new System.Drawing.Point(15, 414);
            this.pnlBackgroundBottom.Name = "pnlBackgroundBottom";
            this.pnlBackgroundBottom.Size = new System.Drawing.Size(580, 15);
            this.pnlBackgroundBottom.TabIndex = 4;
            // 
            // pnlBackgrond
            // 
            this.pnlBackgrond.Controls.Add(this.pnlDownloadingGrid);
            this.pnlBackgrond.Controls.Add(this.pnlDownloadingSeparation);
            this.pnlBackgrond.Controls.Add(this.pnlDownloadingInfo);
            this.pnlBackgrond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackgrond.Location = new System.Drawing.Point(15, 15);
            this.pnlBackgrond.Name = "pnlBackgrond";
            this.pnlBackgrond.Size = new System.Drawing.Size(580, 399);
            this.pnlBackgrond.TabIndex = 5;
            // 
            // pnlDownloadingGrid
            // 
            this.pnlDownloadingGrid.Controls.Add(this.dgvDownloading);
            this.pnlDownloadingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDownloadingGrid.Location = new System.Drawing.Point(0, 97);
            this.pnlDownloadingGrid.Name = "pnlDownloadingGrid";
            this.pnlDownloadingGrid.Size = new System.Drawing.Size(580, 302);
            this.pnlDownloadingGrid.TabIndex = 2;
            // 
            // dgvDownloading
            // 
            this.dgvDownloading.AllowUserToAddRows = false;
            this.dgvDownloading.AllowUserToDeleteRows = false;
            this.dgvDownloading.AutoGenerateColumns = false;
            this.dgvDownloading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownloading.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.fileSizeDataGridViewTextBoxColumn,
            this.progressDataGridViewTextBoxColumn,
            this.urlDataGridViewTextBoxColumn});
            this.dgvDownloading.DataSource = this.bsDownloading;
            this.dgvDownloading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDownloading.Location = new System.Drawing.Point(0, 0);
            this.dgvDownloading.MultiSelect = false;
            this.dgvDownloading.Name = "dgvDownloading";
            this.dgvDownloading.ReadOnly = true;
            this.dgvDownloading.RowHeadersWidth = 51;
            this.dgvDownloading.Size = new System.Drawing.Size(580, 302);
            this.dgvDownloading.TabIndex = 0;
            // 
            // fileDataGridViewTextBoxColumn
            // 
            this.fileDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileDataGridViewTextBoxColumn.DataPropertyName = "File";
            this.fileDataGridViewTextBoxColumn.HeaderText = "File";
            this.fileDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fileDataGridViewTextBoxColumn.Name = "fileDataGridViewTextBoxColumn";
            this.fileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Width = 90;
            // 
            // fileSizeDataGridViewTextBoxColumn
            // 
            this.fileSizeDataGridViewTextBoxColumn.DataPropertyName = "FileSize";
            this.fileSizeDataGridViewTextBoxColumn.HeaderText = "FileSize";
            this.fileSizeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fileSizeDataGridViewTextBoxColumn.Name = "fileSizeDataGridViewTextBoxColumn";
            this.fileSizeDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileSizeDataGridViewTextBoxColumn.Width = 90;
            // 
            // progressDataGridViewTextBoxColumn
            // 
            this.progressDataGridViewTextBoxColumn.DataPropertyName = "Progress";
            this.progressDataGridViewTextBoxColumn.HeaderText = "Progress";
            this.progressDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.progressDataGridViewTextBoxColumn.Name = "progressDataGridViewTextBoxColumn";
            this.progressDataGridViewTextBoxColumn.ReadOnly = true;
            this.progressDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.progressDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.progressDataGridViewTextBoxColumn.Width = 130;
            // 
            // urlDataGridViewTextBoxColumn
            // 
            this.urlDataGridViewTextBoxColumn.DataPropertyName = "Url";
            this.urlDataGridViewTextBoxColumn.HeaderText = "Url";
            this.urlDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.urlDataGridViewTextBoxColumn.Name = "urlDataGridViewTextBoxColumn";
            this.urlDataGridViewTextBoxColumn.ReadOnly = true;
            this.urlDataGridViewTextBoxColumn.Width = 125;
            // 
            // bsDownloading
            // 
            this.bsDownloading.AllowNew = true;
            this.bsDownloading.DataSource = typeof(TasksUI.Downloading);
            // 
            // pnlDownloadingSeparation
            // 
            this.pnlDownloadingSeparation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDownloadingSeparation.Location = new System.Drawing.Point(0, 82);
            this.pnlDownloadingSeparation.Name = "pnlDownloadingSeparation";
            this.pnlDownloadingSeparation.Size = new System.Drawing.Size(580, 15);
            this.pnlDownloadingSeparation.TabIndex = 1;
            // 
            // pnlDownloadingInfo
            // 
            this.pnlDownloadingInfo.Controls.Add(this.gbDownloadingInfo);
            this.pnlDownloadingInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDownloadingInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlDownloadingInfo.Name = "pnlDownloadingInfo";
            this.pnlDownloadingInfo.Size = new System.Drawing.Size(580, 82);
            this.pnlDownloadingInfo.TabIndex = 0;
            // 
            // gbDownloadingInfo
            // 
            this.gbDownloadingInfo.Controls.Add(this.btnDownload);
            this.gbDownloadingInfo.Controls.Add(this.tbUrl);
            this.gbDownloadingInfo.Controls.Add(this.tbFileName);
            this.gbDownloadingInfo.Controls.Add(this.lblFileName);
            this.gbDownloadingInfo.Controls.Add(this.lblUrl);
            this.gbDownloadingInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDownloadingInfo.Location = new System.Drawing.Point(0, 0);
            this.gbDownloadingInfo.Name = "gbDownloadingInfo";
            this.gbDownloadingInfo.Size = new System.Drawing.Size(580, 82);
            this.gbDownloadingInfo.TabIndex = 0;
            this.gbDownloadingInfo.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(477, 21);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(84, 44);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Location = new System.Drawing.Point(84, 21);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(375, 20);
            this.tbUrl.TabIndex = 0;
            this.tbUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbUrl_MouseDown);
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(84, 45);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(375, 20);
            this.tbFileName.TabIndex = 2;
            this.tbFileName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbFileName_MouseDown);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(21, 48);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 13);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "File Name:";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(21, 24);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(23, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Url:";
            // 
            // FormUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 451);
            this.Controls.Add(this.pnlBackgrond);
            this.Controls.Add(this.pnlBackgroundBottom);
            this.Controls.Add(this.pnlBackgroundRight);
            this.Controls.Add(this.pnlBackgroundLeft);
            this.Controls.Add(this.pnlBackgroundTop);
            this.Controls.Add(this.statusBar);
            this.MinimumSize = new System.Drawing.Size(626, 487);
            this.Name = "FormUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloader";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.pnlBackgrond.ResumeLayout(false);
            this.pnlDownloadingGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDownloading)).EndInit();
            this.pnlDownloadingInfo.ResumeLayout(false);
            this.gbDownloadingInfo.ResumeLayout(false);
            this.gbDownloadingInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.Panel pnlBackgroundTop;
        private System.Windows.Forms.Panel pnlBackgroundLeft;
        private System.Windows.Forms.Panel pnlBackgroundRight;
        private System.Windows.Forms.Panel pnlBackgroundBottom;
        private System.Windows.Forms.Panel pnlBackgrond;
        private System.Windows.Forms.Panel pnlDownloadingGrid;
        private System.Windows.Forms.DataGridView dgvDownloading;
        private System.Windows.Forms.Panel pnlDownloadingSeparation;
        private System.Windows.Forms.Panel pnlDownloadingInfo;
        private System.Windows.Forms.GroupBox gbDownloadingInfo;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.BindingSource bsDownloading;
        private System.Windows.Forms.ToolStripStatusLabel tsSTotal;
        private System.Windows.Forms.ToolStripStatusLabel tsSCompleted;
        private System.Windows.Forms.ToolStripStatusLabel tsSSize;
        private System.Windows.Forms.ToolStripStatusLabel tsSInProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsSInQueue;
        private System.Windows.Forms.ToolStripStatusLabel tsSError;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileSizeDataGridViewTextBoxColumn;
        private DataGridViewProgressBarColumn progressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlDataGridViewTextBoxColumn;
    }
}

