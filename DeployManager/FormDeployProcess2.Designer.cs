namespace DeployManager
{
    partial class FormDeployProcess2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeployProcess2));
            this.bckDeployer = new System.ComponentModel.BackgroundWorker();
            this.pgbDeployProcess = new System.Windows.Forms.ProgressBar();
            this.gvDeployReport = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开源路径SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开目标路径DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Skipped = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeployEntry = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeployReport)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bckDeployer
            // 
            this.bckDeployer.WorkerReportsProgress = true;
            this.bckDeployer.WorkerSupportsCancellation = true;
            this.bckDeployer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckDeployer_DoWork);
            this.bckDeployer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bckDeployer_ProgressChanged);
            this.bckDeployer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckDeployer_RunWorkerCompleted);
            // 
            // pgbDeployProcess
            // 
            this.pgbDeployProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbDeployProcess.Location = new System.Drawing.Point(12, 12);
            this.pgbDeployProcess.Name = "pgbDeployProcess";
            this.pgbDeployProcess.Size = new System.Drawing.Size(1092, 23);
            this.pgbDeployProcess.TabIndex = 3;
            // 
            // gvDeployReport
            // 
            this.gvDeployReport.AllowUserToAddRows = false;
            this.gvDeployReport.AllowUserToDeleteRows = false;
            this.gvDeployReport.AllowUserToOrderColumns = true;
            this.gvDeployReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvDeployReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDeployReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDeployReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Skipped,
            this.Source,
            this.Destination,
            this.FileSize,
            this.Cost,
            this.DeployEntry});
            this.gvDeployReport.ContextMenuStrip = this.contextMenuStrip1;
            this.gvDeployReport.Location = new System.Drawing.Point(12, 41);
            this.gvDeployReport.Name = "gvDeployReport";
            this.gvDeployReport.ReadOnly = true;
            this.gvDeployReport.RowTemplate.Height = 23;
            this.gvDeployReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDeployReport.Size = new System.Drawing.Size(1092, 345);
            this.gvDeployReport.TabIndex = 4;
            this.gvDeployReport.SelectionChanged += new System.EventHandler(this.gvDeployReport_SelectionChanged);
            this.gvDeployReport.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.gvDeployReport_SortCompare);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开源路径SToolStripMenuItem,
            this.打开目标路径DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 打开源路径SToolStripMenuItem
            // 
            this.打开源路径SToolStripMenuItem.Name = "打开源路径SToolStripMenuItem";
            this.打开源路径SToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.打开源路径SToolStripMenuItem.Text = "打开源路径(&S)";
            this.打开源路径SToolStripMenuItem.Click += new System.EventHandler(this.打开源路径SToolStripMenuItem_Click);
            // 
            // 打开目标路径DToolStripMenuItem
            // 
            this.打开目标路径DToolStripMenuItem.Name = "打开目标路径DToolStripMenuItem";
            this.打开目标路径DToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.打开目标路径DToolStripMenuItem.Text = "打开目标路径(&D)";
            this.打开目标路径DToolStripMenuItem.Click += new System.EventHandler(this.打开目标路径DToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1116, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(52, 17);
            this.lblInfo.Text = "Row {0}";
            // 
            // Skipped
            // 
            this.Skipped.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Skipped.FillWeight = 30.18211F;
            this.Skipped.Frozen = true;
            this.Skipped.HeaderText = "Updated";
            this.Skipped.Name = "Skipped";
            this.Skipped.ReadOnly = true;
            this.Skipped.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Skipped.Width = 59;
            // 
            // Source
            // 
            this.Source.FillWeight = 176.6137F;
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            // 
            // Destination
            // 
            this.Destination.FillWeight = 176.6137F;
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            // 
            // FileSize
            // 
            this.FileSize.FillWeight = 24.91387F;
            this.FileSize.HeaderText = "Size";
            this.FileSize.Name = "FileSize";
            this.FileSize.ReadOnly = true;
            // 
            // Cost
            // 
            this.Cost.FillWeight = 29.54229F;
            this.Cost.HeaderText = "Cost";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            // 
            // DeployEntry
            // 
            this.DeployEntry.HeaderText = "DeployEntry";
            this.DeployEntry.Name = "DeployEntry";
            this.DeployEntry.ReadOnly = true;
            this.DeployEntry.Visible = false;
            // 
            // FormDeployProcess2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 411);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gvDeployReport);
            this.Controls.Add(this.pgbDeployProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeployProcess2";
            this.Text = "Deploy process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDeployProcess_FormClosing);
            this.Load += new System.EventHandler(this.FormDeployProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvDeployReport)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bckDeployer;
        private System.Windows.Forms.ProgressBar pgbDeployProcess;
        private System.Windows.Forms.DataGridView gvDeployReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开源路径SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开目标路径DToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Skipped;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewLinkColumn DeployEntry;
    }
}