namespace DeployManager
{
    partial class UCFileSystemBroswer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trvFileSystem = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFileSystemInfo = new System.Windows.Forms.ComboBox();
            this.bckFileSystemLoader = new System.ComponentModel.BackgroundWorker();
            this.ucProgressInfo1 = new DeployManager.UCProgressInfo();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvFileSystem
            // 
            this.trvFileSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvFileSystem.ContextMenuStrip = this.contextMenuStrip1;
            this.trvFileSystem.Location = new System.Drawing.Point(3, 30);
            this.trvFileSystem.Name = "trvFileSystem";
            this.trvFileSystem.Size = new System.Drawing.Size(629, 416);
            this.trvFileSystem.TabIndex = 2;
            this.trvFileSystem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFileSystem_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新RToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 26);
            // 
            // 刷新RToolStripMenuItem
            // 
            this.刷新RToolStripMenuItem.Name = "刷新RToolStripMenuItem";
            this.刷新RToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新RToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.刷新RToolStripMenuItem.Text = "刷新(&R)";
            this.刷新RToolStripMenuItem.Click += new System.EventHandler(this.刷新RToolStripMenuItem_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.Location = new System.Drawing.Point(479, 1);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(153, 23);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "添加网络上的计算机...";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "文件来自：";
            // 
            // cmbFileSystemInfo
            // 
            this.cmbFileSystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFileSystemInfo.FormattingEnabled = true;
            this.cmbFileSystemInfo.Location = new System.Drawing.Point(74, 3);
            this.cmbFileSystemInfo.Name = "cmbFileSystemInfo";
            this.cmbFileSystemInfo.Size = new System.Drawing.Size(399, 20);
            this.cmbFileSystemInfo.TabIndex = 0;
            this.cmbFileSystemInfo.SelectedIndexChanged += new System.EventHandler(this.cmbFileSystemInfo_SelectedIndexChanged);
            // 
            // bckFileSystemLoader
            // 
            this.bckFileSystemLoader.WorkerReportsProgress = true;
            this.bckFileSystemLoader.WorkerSupportsCancellation = true;
            this.bckFileSystemLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckFileSystemLoader_DoWork);
            this.bckFileSystemLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bckFileSystemLoader_ProgressChanged);
            this.bckFileSystemLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckFileSystemLoader_RunWorkerCompleted);
            // 
            // ucProgressInfo1
            // 
            this.ucProgressInfo1.Location = new System.Drawing.Point(117, 196);
            this.ucProgressInfo1.Name = "ucProgressInfo1";
            this.ucProgressInfo1.Size = new System.Drawing.Size(401, 100);
            this.ucProgressInfo1.TabIndex = 8;
            this.ucProgressInfo1.Visible = false;
            // 
            // UCFileSystemBroswer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbFileSystemInfo);
            this.Controls.Add(this.ucProgressInfo1);
            this.Controls.Add(this.trvFileSystem);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.label1);
            this.Name = "UCFileSystemBroswer";
            this.Size = new System.Drawing.Size(635, 449);
            this.Load += new System.EventHandler(this.UCFileSystemBroswer_Load);
            this.Resize += new System.EventHandler(this.UCFileSystemBroswer_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TreeView trvFileSystem;
        private UCProgressInfo ucProgressInfo1;
        private System.Windows.Forms.ComboBox cmbFileSystemInfo;
        private System.ComponentModel.BackgroundWorker bckFileSystemLoader;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新RToolStripMenuItem;
    }
}
