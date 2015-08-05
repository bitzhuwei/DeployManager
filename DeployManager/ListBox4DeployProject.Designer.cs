namespace DeployManager
{
    partial class ListBox4DeployProject
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.打开源路径SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开目标路径DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.部署ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.强制部署ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部署选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.强制部署选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.修改ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.toolStripSeparator1,
            this.打开源路径SToolStripMenuItem,
            this.打开目标路径DToolStripMenuItem,
            this.toolStripSeparator2,
            this.部署ToolStripMenuItem,
            this.强制部署ToolStripMenuItem,
            this.部署选中项ToolStripMenuItem,
            this.强制部署选中项ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 236);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.添加ToolStripMenuItem.Text = "添加(&A)...";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.修改ToolStripMenuItem.Text = "修改(&F)...";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click); 
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.删除ToolStripMenuItem.Text = "删除(&R)...";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // 打开源路径SToolStripMenuItem
            // 
            this.打开源路径SToolStripMenuItem.Name = "打开源路径SToolStripMenuItem";
            this.打开源路径SToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.打开源路径SToolStripMenuItem.Text = "打开源路径(&S)";
            this.打开源路径SToolStripMenuItem.Click += new System.EventHandler(this.打开源路径SToolStripMenuItem_Click); 
            // 
            // 打开目标路径DToolStripMenuItem
            // 
            this.打开目标路径DToolStripMenuItem.Name = "打开目标路径DToolStripMenuItem";
            this.打开目标路径DToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.打开目标路径DToolStripMenuItem.Text = "打开目标路径(&D)";
            this.打开目标路径DToolStripMenuItem.Click += new System.EventHandler(this.打开目标路径DToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
            // 
            // 部署ToolStripMenuItem
            // 
            this.部署ToolStripMenuItem.Name = "部署ToolStripMenuItem";
            this.部署ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.部署ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.部署ToolStripMenuItem.Text = "部署";
            this.部署ToolStripMenuItem.Click += new System.EventHandler(this.Deploy);
            // 
            // 强制部署ToolStripMenuItem
            // 
            this.强制部署ToolStripMenuItem.Name = "强制部署ToolStripMenuItem";
            this.强制部署ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.强制部署ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.强制部署ToolStripMenuItem.Text = "强制部署";
            this.强制部署ToolStripMenuItem.Click += new System.EventHandler(ForceDeploy);
            // 
            // 部署选中项ToolStripMenuItem
            // 
            this.部署选中项ToolStripMenuItem.Name = "部署选中项ToolStripMenuItem";
            this.部署选中项ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.部署选中项ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.部署选中项ToolStripMenuItem.Text = "部署选中项";
            this.部署选中项ToolStripMenuItem.Click += new System.EventHandler(this.DeploySelectedItems);
            // 
            // 强制部署选中项ToolStripMenuItem
            // 
            this.强制部署选中项ToolStripMenuItem.Name = "强制部署选中项ToolStripMenuItem";
            this.强制部署选中项ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.强制部署选中项ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.强制部署选中项ToolStripMenuItem.Text = "强制部署选中项";
            this.强制部署选中项ToolStripMenuItem.Click += new System.EventHandler(this.ForceDeploySelectedItems);
            // 
            // ListBox4DeployProject
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSourceList_MouseDoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 打开源路径SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开目标路径DToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 部署ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 强制部署ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部署选中项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 强制部署选中项ToolStripMenuItem;
    }
}
