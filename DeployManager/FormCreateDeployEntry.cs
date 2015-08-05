using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeployLib;

namespace DeployManager
{
    public partial class FormCreateDeployEntry : Form
    {
        public FormCreateDeployEntry()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtSource.Text.Trim() == string.Empty
                || this.txtSource.Tag == null)
            {
                MessageBox.Show("Please select or type in the source.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (this.txtDest.Text.Trim() == string.Empty
                || this.txtDest.Tag == null)
            {
                MessageBox.Show("Please select or type in the destination directory.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            var source = this.txtSource.Tag as Transporter;
            var dest = this.txtDest.Tag as Transporter;
            source.Goods = this.txtSource.Text;
            dest.Goods = this.txtDest.Text;

            var entry = new DeployEntry()
            {
                Source = source,
                DestinationDir = dest,
            };
            var message = string.Empty;
            if (entry.Conflicts(ref message))
            {
                MessageBox.Show(message, "Conflict!");
                return;
            }
            this.NewDeployEntry = entry;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (this.selectSourceFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var source = new Transporter()
                {
                    Goods = this.selectSourceFile.FileName,
                    Ip = string.Empty,
                    Password = string.Empty,
                    Username = string.Empty
                };
                this.txtSource.Tag = source;
                this.txtSource.Text = source.Goods;//.ToString();
                this.txtSource.ReadOnly = false;
            }
        }

        private void btnBrowseDest_Click(object sender, EventArgs e)
        {
            if (this.selectFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var dest = new Transporter()
                {
                    Goods = this.selectFolder.SelectedPath,
                    Ip = string.Empty,
                    Password = string.Empty,
                    Username = string.Empty
                };
                this.txtDest.Tag = dest;
                this.txtDest.Text = dest.Goods;//.ToString();
            }
        }

        public DeployEntry NewDeployEntry { get; set; }

        private void btnbrowseDestFromNetwork_Click(object sender, EventArgs e)
        {
            var frmSelectFromNetwork = new FormNetworkPath(NodeType.Directory);
            if (frmSelectFromNetwork.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var selectedPath = frmSelectFromNetwork.SelectedPath;
                this.txtDest.Tag = selectedPath;
                this.txtDest.Text = selectedPath.Goods;//.ToString();
            }
        }

        private void FormCreateDeployEntry_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseSourceFromNetwork_Click(object sender, EventArgs e)
        {
            var frmSelectFromNetwork = new FormNetworkPath(NodeType.File);
            if (frmSelectFromNetwork.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var selectedPath = frmSelectFromNetwork.SelectedPath;
                this.txtSource.Tag = selectedPath;
                this.txtSource.Text = selectedPath.Goods;//.ToString();
                this.txtSource.ReadOnly = false;
            }
        }

    }
}
