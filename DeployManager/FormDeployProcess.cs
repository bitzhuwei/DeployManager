using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeployLib;
using System.IO;

namespace DeployManager
{
    public partial class FormDeployProcess : Form
    {
        private DeployLib.DeployProject project;
        private Func<int, object, bool> funcAfterProcessedOneEntry;
        private DeployCmd deployCmd;

        public FormDeployProcess(DeployLib.DeployProject project, DeployCmd cmd)
        {
            InitializeComponent();
            this.project = project;
            this.deployCmd = cmd;
        }
        private void bckDeployer_DoWork(object sender, DoWorkEventArgs e)
        {
            //var document = e.Argument as Document;
            //if (document == null)
            //{
            //    e.Result = string.Format("{0}", "No deploy file opened or created.");
            //    return;
            //}
            //var project = document.Project;
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
            bckDeployer.ReportProgress(0, string.Format("Preparing for deployment..."));
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
            var value = e.ProgressPercentage;
            if (value < pgbDeployProcess.Minimum) value = pgbDeployProcess.Minimum;
            else if (value > pgbDeployProcess.Maximum) value = pgbDeployProcess.Maximum;
            this.pgbDeployProcess.Value = value;
            this.txtDeployReport.AppendText(e.UserState.ToString());
            this.txtDeployReport.AppendText(Environment.NewLine);
        }

        private void bckDeployer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error during deploying",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("The deploying is cancelled.", "Tip",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.txtDeployReport.AppendText(string.Format("{0}", e.Result));
                this.pgbDeployProcess.Value = this.pgbDeployProcess.Minimum;
            }
        }

        private void FormDeployProcess_Load(object sender, EventArgs e)
        {
            var document = this.project;
            if (document == null) { return; }
            this.txtDeployReport.Clear();
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
    }
}
