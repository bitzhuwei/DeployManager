using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace DeployManager
{
    public partial class FormDeployResult : Form
    {
        public FormDeployResult(string message, string title, TaskbarProgressBarState taskbarProgressBarState)
        {
            InitializeComponent();
            this.txtMessage.Text = message;
            this.Text = title;
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(taskbarProgressBarState);
                TaskbarManager.Instance.SetProgressValue(100, 100);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FormDeployResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue(0, 100);
            }
        }

    }
}
