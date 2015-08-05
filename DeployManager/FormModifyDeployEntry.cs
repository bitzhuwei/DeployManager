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
    public partial class FormModifyDeployEntry : Form
    {
        private DeployEntry entry;
        public FormModifyDeployEntry(DeployEntry entry)
        {
            InitializeComponent();
            this.entry = entry;
        }

        private void FormModifyDeployEntry_Load(object sender, EventArgs e)
        {
            var entry = this.entry;
            this.Text = string.Format("Modify: {0}", entry);
            if (entry == null) { return; }

            this.txtSource.Tag = entry.Source;
            this.txtSource.Text = string.Format("{0}", entry.Source.Goods);
            this.txtDest.Tag = entry.DestinationDir;
            this.txtDest.Text = string.Format("{0}", entry.DestinationDir.Goods);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.entry == null)
            {
                MessageBox.Show("No entry found, modification won't work.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var entry = this.entry.Clone() as DeployEntry;
            entry.Source = this.txtSource.Tag as Transporter;
            entry.Source.Goods = this.txtSource.Text;
            entry.DestinationDir = this.txtDest.Tag as Transporter;
            entry.DestinationDir.Goods = this.txtDest.Text;
            var message = string.Empty;
            if (entry.Conflicts(ref message))
            {
                MessageBox.Show(message, "Conflict!");
                return;
            }

            entry = this.entry;
            entry.Source = this.txtSource.Tag as Transporter;
            entry.Source.Goods = this.txtSource.Text;
            entry.DestinationDir = this.txtDest.Tag as Transporter;
            entry.DestinationDir.Goods = this.txtDest.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnBrowseSourceFromNetwork_Click(object sender, EventArgs e)
        {
            var source = this.txtSource.Tag as Transporter;
            var frmSelectFromNetwork = new FormNetworkPath(
                source.Ip, source.Username, source.Password, NodeType.File);
            if (frmSelectFromNetwork.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtSource.Tag = frmSelectFromNetwork.SelectedPath;
                this.txtSource.Text = frmSelectFromNetwork.SelectedPath.Goods;//.ToString();
            }
        }

        private void btnbrowseDestFromNetwork_Click(object sender, EventArgs e)
        {
            var dest = this.txtDest.Tag as Transporter;
            var frmSelectFromNetwork = new FormNetworkPath(
                dest.Ip, dest.Username, dest.Password, NodeType.Directory);
            if (frmSelectFromNetwork.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtDest.Tag = frmSelectFromNetwork.SelectedPath;
                this.txtDest.Text = frmSelectFromNetwork.SelectedPath.Goods;//.ToString();
            }
        }

    }
}
