using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeployLib;
using System.Diagnostics;
using System.IO;

namespace DeployManager
{
    public partial class UCDeployFileList : UserControl
    {
        public UCDeployFileList()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var deployProjectBind = this.GetProject() != null;
            var selected = this.lstSourceList.SelectedIndex >= 0;
            this.添加ToolStripMenuItem.Enabled = deployProjectBind && true;
            this.修改ToolStripMenuItem.Enabled = deployProjectBind && selected;
            this.删除ToolStripMenuItem.Enabled = deployProjectBind && selected;
            this.打开源路径SToolStripMenuItem.Enabled = deployProjectBind && selected;
            this.打开目标路径DToolStripMenuItem.Enabled = deployProjectBind && selected;
            this.部署ToolStripMenuItem.Enabled = deployProjectBind;
            this.强制部署ToolStripMenuItem.Enabled = deployProjectBind;
            this.部署选中项ToolStripMenuItem.Enabled = deployProjectBind && selected;
            this.强制部署选中项ToolStripMenuItem.Enabled = deployProjectBind && selected;
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = this.GetProject();
            if (project == null) { return; }
            var frmCreateDeployEntry = new FormCreateDeployEntry();
            if (frmCreateDeployEntry.ShowDialog() == DialogResult.OK)
            {
                project.Add(frmCreateDeployEntry.NewDeployEntry);
                this.lstSourceList.Items.Add(frmCreateDeployEntry.NewDeployEntry);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = this.lstSourceList.SelectedIndex;
            this.ModifyDeployEntry(selected);
        }

        private void ModifyDeployEntry(int selected)
        {
            var project = this.GetProject();
            if (project == null) { return; }
            if (selected < 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            var entry = project[selected];
            var frmModifyDeployEntry = new FormModifyDeployEntry(entry);
            if (frmModifyDeployEntry.ShowDialog() == DialogResult.OK)
            {
                this.lstSourceList.Items[selected] = project[selected];
                project.IsDirty = true;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = this.GetProject();
            if (project == null) { return; }
            var selected = this.lstSourceList.SelectedIndex;
            if (selected < 0)
            {
                MessageBox.Show("请选中一条记录后再删除！", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (MessageBox.Show(
                string.Format("您确定要删除这条记录吗？[{0}]", project[selected]), "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                project.RemoveAt(selected);
                this.lstSourceList.Items.RemoveAt(selected);
            }
        }

        private DeployProject _project;// = new DeployProject();

        public DeployProject GetProject()
        {
            return _project;
        }

        public void SetProject(DeployProject project)
        {
            _project = project;
            this.lstSourceList.Items.Clear();
            if (project != null)
            {
                foreach (var item in project)
                {
                    this.lstSourceList.Items.Add(item);
                }
            }
        }

        public bool IsDirty()
        {
            var project = this.GetProject();
            if (project == null) { return false; }
            return project.IsDirty;
        }

        private void 打开源路径SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = this.lstSourceList.SelectedItems;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            foreach (var item in selected)
            {
                var entry = item as DeployEntry;
                if (entry != null)
                {
                    var sourceFilenames = Path.GetFileName(entry.Source.Goods);
                    var sourceRoot = string.Empty;
                    if (!string.IsNullOrEmpty(entry.Source.Ip))
                    {
                        WmiShareFunction.CreateShareNetConnect(
                            entry.Source.Ip, entry.Source.Goods,
                            entry.Source.Username, entry.Source.Password);
                        sourceRoot = string.Format(@"\\{0}\{1}",
                            entry.Source.Ip, Path.GetDirectoryName(entry.Source.Goods));
                        WmiShareFunction.CreateShareNetConnect(
                            entry.Source.Ip, "C$",
                            entry.Source.Username,
                            entry.Source.Password);
                    }
                    else
                    { sourceRoot = Path.GetDirectoryName(entry.Source.Goods); }
                    var sourceFullnames = Directory.GetFiles(sourceRoot, sourceFilenames, SearchOption.TopDirectoryOnly);
                    foreach (var sourceFullname in sourceFullnames)
                    {
                        try
                        {
                            Process.Start("explorer", string.Format(" /select, {0}", sourceFullname));
                            break;
                        }
                        catch (System.IO.DirectoryNotFoundException dnfe)
                        {
                            MessageBox.Show(dnfe.Message, "Exception");
                        }
                    }
                }
            }
        }

        private void 打开目标路径DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = this.lstSourceList.SelectedItems;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            foreach (var item in selected)
            {
                var entry = item as DeployEntry;
                if (entry != null)
                {
                    var destRoot = string.Empty;
                    if (!string.IsNullOrEmpty(entry.DestinationDir.Ip))
                    {
                        WmiShareFunction.CreateShareNetConnect(
                            entry.DestinationDir.Ip, entry.DestinationDir.Goods,
                            entry.DestinationDir.Username, entry.DestinationDir.Password);
                        destRoot = string.Format(@"\\{0}\{1}",
                            entry.DestinationDir.Ip, entry.DestinationDir.Goods);
                    }
                    else
                    { destRoot = new DirectoryInfo(entry.DestinationDir.Goods).FullName; }

                    try
                    {
                        Process.Start("explorer", string.Format("{0}", destRoot));
                    }
                    catch (System.IO.DirectoryNotFoundException dnfe)
                    {
                        MessageBox.Show(dnfe.Message, "Exception");
                    }
                }
            }
        }

        private void lstSourceList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var selected = this.lstSourceList.IndexFromPoint(e.Location);
                this.ModifyDeployEntry(selected);
            }
        }

        public void Deploy(object sender = null, EventArgs e = null)
        {
            var frmDeploy = new FormDeployProcess2(this._project,
                new DeployCmd(false));
            frmDeploy.ShowDialog(this);
        }

        public void DeploySelectedItems(object sender = null, EventArgs e = null)
        {
            var selected = this.lstSourceList.SelectedItems;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            DeploySelectedItems(selected, false);
        }

        private void DeploySelectedItems(ListBox.SelectedObjectCollection selected,
            bool forceDeploy)
        {
            var project = new DeployProject();
            foreach (var item in selected)
            {
                var entry = item as DeployEntry;
                if (entry != null)
                {
                    project.Add(entry);
                }
            }
            var frmDeploy = new FormDeployProcess2(project, new DeployCmd(forceDeploy)); 
            frmDeploy.ShowDialog(this);
        }

        public void ForceDeploy(object sender = null, EventArgs e = null)
        {
            var frmDeploy = new FormDeployProcess2(this._project, new DeployCmd(true));
            frmDeploy.ShowDialog(this);
        }

        public void ForceDeploySelectedItems(object sender = null, EventArgs e = null)
        {
            var selected = this.lstSourceList.SelectedItems;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            DeploySelectedItems(selected, true);
        }

        private void lstSourceList_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    var index = this.lstSourceList.IndexFromPoint(e.Location);
            //    if (index != ListBox.NoMatches)
            //    {
            //        this.lstSourceList.SelectedIndex = index;
            //    }
            //    this.contextMenuStrip1.Show(this, e.Location);
            //}
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            var mousePosition = Control.MousePosition;
            Point absLocation = this.lstSourceList.PointToScreen(this.lstSourceList.Location);
            var index = this.lstSourceList.IndexFromPoint(mousePosition. X - absLocation.X, mousePosition.Y - absLocation.Y);
            //var index = this.lstSourceList.IndexFromPoint(this.contextMenuStrip1.Left - absLocation.X, this.contextMenuStrip1.Top - absLocation.Y);
            if (index != ListBox.NoMatches && index != this.lstSourceList.SelectedIndex)
            {
                this.lstSourceList.SelectedIndex = index;
            }
        }

    }

}
