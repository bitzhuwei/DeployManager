namespace DeployManager
{
    partial class FormCreateDeployEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateDeployEntry));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.btnBrowseSourceFromNetwork = new System.Windows.Forms.Button();
            this.btnbrowseDestFromNetwork = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.selectSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseDest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(492, 72);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(573, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSource.Location = new System.Drawing.Point(492, 11);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSource.TabIndex = 15;
            this.btnBrowseSource.Text = "Local...";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // btnBrowseSourceFromNetwork
            // 
            this.btnBrowseSourceFromNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSourceFromNetwork.Location = new System.Drawing.Point(573, 11);
            this.btnBrowseSourceFromNetwork.Name = "btnBrowseSourceFromNetwork";
            this.btnBrowseSourceFromNetwork.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSourceFromNetwork.TabIndex = 16;
            this.btnBrowseSourceFromNetwork.Text = "Network...";
            this.btnBrowseSourceFromNetwork.UseVisualStyleBackColor = true;
            this.btnBrowseSourceFromNetwork.Click += new System.EventHandler(this.btnBrowseSourceFromNetwork_Click);
            // 
            // btnbrowseDestFromNetwork
            // 
            this.btnbrowseDestFromNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnbrowseDestFromNetwork.Location = new System.Drawing.Point(573, 38);
            this.btnbrowseDestFromNetwork.Name = "btnbrowseDestFromNetwork";
            this.btnbrowseDestFromNetwork.Size = new System.Drawing.Size(75, 23);
            this.btnbrowseDestFromNetwork.TabIndex = 19;
            this.btnbrowseDestFromNetwork.Text = "Network...";
            this.btnbrowseDestFromNetwork.UseVisualStyleBackColor = true;
            this.btnbrowseDestFromNetwork.Click += new System.EventHandler(this.btnbrowseDestFromNetwork_Click);
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Location = new System.Drawing.Point(65, 40);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new System.Drawing.Size(421, 21);
            this.txtDest.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "Dest:";
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(65, 13);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(421, 21);
            this.txtSource.TabIndex = 14;
            // 
            // selectSourceFile
            // 
            this.selectSourceFile.RestoreDirectory = true;
            this.selectSourceFile.Title = "选择源";
            // 
            // btnBrowseDest
            // 
            this.btnBrowseDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDest.Location = new System.Drawing.Point(492, 38);
            this.btnBrowseDest.Name = "btnBrowseDest";
            this.btnBrowseDest.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDest.TabIndex = 18;
            this.btnBrowseDest.Text = "Local...";
            this.btnBrowseDest.UseVisualStyleBackColor = true;
            this.btnBrowseDest.Click += new System.EventHandler(this.btnBrowseDest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "Source:";
            // 
            // FormCreateDeployEntry
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(660, 106);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowseSource);
            this.Controls.Add(this.btnBrowseSourceFromNetwork);
            this.Controls.Add(this.btnbrowseDestFromNetwork);
            this.Controls.Add(this.txtDest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnBrowseDest);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCreateDeployEntry";
            this.Text = "Create a deploy entry.";
            this.Load += new System.EventHandler(this.FormCreateDeployEntry_Load);
            this.Click += new System.EventHandler(this.FormCreateDeployEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog selectFolder;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Button btnBrowseSourceFromNetwork;
        private System.Windows.Forms.Button btnbrowseDestFromNetwork;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.OpenFileDialog selectSourceFile;
        private System.Windows.Forms.Button btnBrowseDest;
        private System.Windows.Forms.Label label1;

    }
}