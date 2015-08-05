namespace DeployManager
{
    partial class FormDeployProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeployProcess));
            this.pgbDeployProcess = new System.Windows.Forms.ProgressBar();
            this.bckDeployer = new System.ComponentModel.BackgroundWorker();
            this.txtDeployReport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pgbDeployProcess
            // 
            this.pgbDeployProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbDeployProcess.Location = new System.Drawing.Point(12, 12);
            this.pgbDeployProcess.Name = "pgbDeployProcess";
            this.pgbDeployProcess.Size = new System.Drawing.Size(1092, 23);
            this.pgbDeployProcess.TabIndex = 0;
            // 
            // bckDeployer
            // 
            this.bckDeployer.WorkerReportsProgress = true;
            this.bckDeployer.WorkerSupportsCancellation = true;
            this.bckDeployer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckDeployer_DoWork);
            this.bckDeployer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bckDeployer_ProgressChanged);
            this.bckDeployer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckDeployer_RunWorkerCompleted);
            // 
            // txtDeployReport
            // 
            this.txtDeployReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeployReport.Location = new System.Drawing.Point(12, 41);
            this.txtDeployReport.Multiline = true;
            this.txtDeployReport.Name = "txtDeployReport";
            this.txtDeployReport.ReadOnly = true;
            this.txtDeployReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDeployReport.Size = new System.Drawing.Size(1092, 358);
            this.txtDeployReport.TabIndex = 2;
            // 
            // FormDeployProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 411);
            this.Controls.Add(this.txtDeployReport);
            this.Controls.Add(this.pgbDeployProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeployProcess";
            this.Text = "Deploy process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDeployProcess_FormClosing);
            this.Load += new System.EventHandler(this.FormDeployProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbDeployProcess;
        private System.ComponentModel.BackgroundWorker bckDeployer;
        private System.Windows.Forms.TextBox txtDeployReport;
    }
}