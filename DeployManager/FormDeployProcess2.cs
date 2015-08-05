using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeployLib;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace DeployManager
{
    public partial class FormDeployProcess2 : Form
    {
        private DeployLib.DeployProject project;
        private Func<int, object, bool> funcAfterProcessedOneEntry;
        private DeployCmd deployCmd;
        private TaskbarManager taskbar = TaskbarManager.Instance;
        private DateTime? lastStartCopying = null;
        public FormDeployProcess2(DeployLib.DeployProject project, DeployCmd cmd)
        {
            InitializeComponent();
            this.project = project;
            this.deployCmd = cmd;
        }
        private void bckDeployer_DoWork(object sender, DoWorkEventArgs e)
        {
            var project = e.Argument as DeployProject;
            if (project == null)
            {
                e.Result = string.Format("{0}", "Deploy project is empty!");
                return;
            }

            if (funcAfterProcessedOneEntry == null)
            {
                funcAfterProcessedOneEntry = new Func<int, object, bool>(AfterProcessedOneEntry);
            }
            var report = project.Deploy(deployCmd, funcAfterProcessedOneEntry);
            e.Result = report;
        }

        private bool AfterProcessedOneEntry(int percentProgress, object userState)
        {
            bckDeployer.ReportProgress(percentProgress, userState);
            return (!bckDeployer.CancellationPending);
        }

        private void bckDeployer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (pgbDeployProcess.IsDisposed) { return; }

            UpdateProgressBar(e);
            UpdateTaskbar(e);
            UpdateDatagridView(e);
        }


        private void UpdateDatagridView(ProgressChangedEventArgs e)
        {
            var userState = e.UserState as UserState;
            if (userState == null)
            {
                userState = new UserState(false, "userState:", e.UserState.ToString(), -1, DateTime.Now, null);
            }
            var now = userState.StartCopying;
            var gridView = this.gvDeployReport;
            if (gridView.Rows.Count > 0)
            {
                gridView.Rows[gridView.Rows.Count - 1].Cells["Cost"].Value = now.Subtract(this.lastStartCopying.Value);
            }
            this.lastStartCopying = now;
            gridView.Rows.Add(userState.Updated, userState.SourceFullname, userState.DestinationFullname, userState.Length, null, userState.Entry);
            gridView.FirstDisplayedScrollingRowIndex = gridView.Rows.Count - 1;
            this.Text = string.Format("{0} [{1}]({2})",
                userState.Updated ? "Coppying" : "Skipped",
                userState.SourceFullname, userState.Length);
        }

        private void UpdateTaskbar(ProgressChangedEventArgs e)
        {
            var pgbDeployProcess = this.pgbDeployProcess;
            if (pgbDeployProcess.Style == ProgressBarStyle.Marquee)
            {
                if (TaskbarManager.IsPlatformSupported)
                { taskbar.SetProgressState(TaskbarProgressBarState.Indeterminate); }
            }
            var value = e.ProgressPercentage;
            if (TaskbarManager.IsPlatformSupported)
            { taskbar.SetProgressValue(value, pgbDeployProcess.Maximum, this.Handle); }
        }

        private void UpdateProgressBar(ProgressChangedEventArgs e)
        {
            var pgbDeployProcess = this.pgbDeployProcess;
            if (pgbDeployProcess.Style == ProgressBarStyle.Marquee)
            {
                pgbDeployProcess.Style = ProgressBarStyle.Blocks;
            }
            var value = e.ProgressPercentage;
            if (value < pgbDeployProcess.Minimum) value = pgbDeployProcess.Minimum;
            else if (value > pgbDeployProcess.Maximum) value = pgbDeployProcess.Maximum;
            pgbDeployProcess.Value = value;
        }
        private void bckDeployer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var pgbDeployProcess = this.pgbDeployProcess;
            if (!pgbDeployProcess.IsDisposed)
            {
                pgbDeployProcess.Style = ProgressBarStyle.Blocks;
            }

            if (e.Error != null)
            {
                this.Text = e.Error.Message;
                var frmDeployResult = new FormDeployResult(e.Error.Message, "Error during deployment", TaskbarProgressBarState.Error);
                frmDeployResult.ShowDialog(this);
            }
            else if (e.Cancelled || this.deployCmd.CancelDeploy)
            {
                this.Text = "The deploying is cancelled.";
                var frmDeployResult = new FormDeployResult("The deployment is cancelled.", "Tip", TaskbarProgressBarState.Paused);
                frmDeployResult.ShowDialog(this);
            }
            else
            {
                UpdateDatagridView_lastOne();

                var result = string.Format("{0}", e.Result);
                this.Text = result;
                var frmDeployResult = new FormDeployResult(result, "Deploy completed", TaskbarProgressBarState.Normal);
                frmDeployResult.ShowDialog(this);
                if(!pgbDeployProcess.IsDisposed)
                { pgbDeployProcess.Value = pgbDeployProcess.Minimum; }
            }
        }

        private void UpdateDatagridView_lastOne()
        {
            var lastStartCopying = this.lastStartCopying;
            var gvDeployReport = this.gvDeployReport;
            if (lastStartCopying.HasValue && gvDeployReport.Rows.Count > 0)
            {
                gvDeployReport.Rows[gvDeployReport.Rows.Count - 1].Cells["Cost"].Value = DateTime.Now.Subtract(lastStartCopying.Value);
            }
        }

        private void FormDeployProcess_Load(object sender, EventArgs e)
        {
            var document = this.project;
            if (document == null) { return; }
            var gvDeployReport = this.gvDeployReport;
            var pgbDeployProcess = this.pgbDeployProcess;
            pgbDeployProcess.Value = pgbDeployProcess.Minimum;
            gvDeployReport.Rows.Clear();
            this.Text = "Preparing for deployment...";
            this.lblInfo.Text = string.Empty;
            pgbDeployProcess.Style = ProgressBarStyle.Marquee;
            this.taskbar.SetProgressState(TaskbarProgressBarState.Indeterminate);
            this.bckDeployer.RunWorkerAsync(document);
        }

        private void FormDeployProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bckDeployer.IsBusy)
            {
                if (MessageBox.Show("Abort deploying and close this window?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    this.deployCmd.CancelDeploy = true;
                    this.bckDeployer.CancelAsync();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvDeployReport_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name != "Size")
            {
                e.Handled = false;
                return;
            }

            var item1 = e.CellValue1 as FileLength;
            var item2 = e.CellValue2 as FileLength;
            if (item1 == null || item2 == null)
            {
                e.Handled = false;
                return;
            }

            var compare = item1.Length - item2.Length;
            if (compare > 0) { e.SortResult = 1; }
            else if (compare == 0) { e.SortResult = 0; }
            else { e.SortResult = -1; }
            e.Handled = true;
        }



        private void 打开源路径SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = this.gvDeployReport.SelectedRows;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (selected.Count > 16)
            {
                if (MessageBox.Show(string.Format("Open {0} items?", selected.Count), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                { return; }
            }
            foreach (var item in selected)
            {
                var row = item as DataGridViewRow;
                var entryCell = row.Cells["DeployEntry"];
                if (entryCell == null) { continue; }
                var entry = entryCell.Value as DeployEntry;
                if (entry == null) { continue; }
                var sourceCell = row.Cells["Source"];
                if (sourceCell == null) { continue; }
                var source = sourceCell.Value as string;
                if (source == null) { continue; }

                if (!string.IsNullOrEmpty(entry.Source.Ip))
                {
                    WmiShareFunction.CreateShareNetConnect(
                        entry.Source.Ip, "C$",
                        entry.Source.Username,
                        entry.Source.Password);
                }
                Process.Start("explorer", string.Format(" /select, {0}", source));
            }
        }

        private void 打开目标路径DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = this.gvDeployReport.SelectedRows;
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show("Please choose an item first.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (selected.Count > 16)
            {
                if (MessageBox.Show(string.Format("Open {0} items?", selected.Count), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                { return; }
            }
            foreach (var item in selected)
            {
                var row = item as DataGridViewRow;
                var cell = row.Cells["DeployEntry"];
                if (cell == null) { continue; }
                var entry = cell.Value as DeployEntry;
                if (entry == null) { continue; }
                var destinationCell = row.Cells["Destination"];
                if (destinationCell == null) { continue; }
                var destination = destinationCell.Value as string;
                if (destination == null) { continue; }
                if (!string.IsNullOrEmpty(entry.DestinationDir.Ip))
                {
                    WmiShareFunction.CreateShareNetConnect(
                        entry.DestinationDir.Ip, entry.DestinationDir.Goods,
                        entry.DestinationDir.Username, entry.DestinationDir.Password);
                }

                Process.Start("explorer", string.Format(" /select, {0}", destination));
            }
        }

        private void gvDeployReport_SelectionChanged(object sender, EventArgs e)
        {
            var rowIndex = 0;
            foreach (var item in gvDeployReport.SelectedRows)
            {
                var row = item as DataGridViewRow;
                if (row != null)
                {
                    if (row.Index > rowIndex)
                    { rowIndex = row.Index; }
                }
            }
            this.lblInfo.Text = string.Format("Ln {0}", rowIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var mousePosition = Control.MousePosition;
            Point absLocation = this.gvDeployReport.PointToScreen(Point.Empty);
            var hitTestInfo = this.gvDeployReport.HitTest(mousePosition.X - absLocation.X, mousePosition.Y - absLocation.Y);
            var selected = hitTestInfo.RowIndex >= 0;
            if (selected)
            {
                foreach (var item in this.gvDeployReport.SelectedRows)
                {
                    var row = item as DataGridViewRow;
                    if (row != null && row.Index != hitTestInfo.RowIndex)
                    {
                        row.Selected = false;
                    }
                }
                this.gvDeployReport.Rows[hitTestInfo.RowIndex].Selected = true;
            }

            this.打开源路径SToolStripMenuItem.Enabled = selected;
            this.打开目标路径DToolStripMenuItem.Enabled = selected;
        }
    }
}

