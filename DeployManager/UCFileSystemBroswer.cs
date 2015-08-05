using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DeployManager
{
    public partial class UCFileSystemBroswer : UserControl
    {
        public UCFileSystemBroswer()
        {
            InitializeComponent();
        }

        private void UCFileSystemBroswer_Resize(object sender, EventArgs e)
        {
            var x = (this.Width - this.ucProgressInfo1.Width) / 2;
            var y = (this.Height - this.ucProgressInfo1.Height) / 2;
            this.ucProgressInfo1.Location = new Point(x, y);
        }

        private void UCFileSystemBroswer_Load(object sender, EventArgs e)
        {
            foreach (var item in fileSystemInfoHistory)
            {
                this.cmbFileSystemInfo.Items.Add(item);
            }
            this.UCFileSystemBroswer_Resize(sender, e);
        }

        static UCFileSystemBroswer()
        {
            var localMachine = new FileSystemInfo()
            {
                Ip = new IPAddress(new byte[] { 127, 0, 0, 1 }),
                Name = "Local Machine",
                Username = "",
                Password = "",
            };
            fileSystemInfoHistory.Add(localMachine);
        }

        private static List<FileSystemInfo> fileSystemInfoHistory = new List<FileSystemInfo>();

        private void cmbFileSystemInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = this.cmbFileSystemInfo.SelectedItem as FileSystemInfo;
            if (selected == null) { return; }
            if (this.bckFileSystemLoader.IsBusy)
            {
                MessageBox.Show("Busy, please try again later.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this.bckFileSystemLoader.RunWorkerAsync(selected);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var frmInsertFileSystemInfo = new FormInsertFileSystemInfo();
            if (frmInsertFileSystemInfo.ShowDialog() == DialogResult.OK)
            {
                var newFileSystemInfo = frmInsertFileSystemInfo.NewFileSystemInfo;
                UCFileSystemBroswer.fileSystemInfoHistory.Add(newFileSystemInfo);
                this.cmbFileSystemInfo.Items.Add(newFileSystemInfo);
                this.cmbFileSystemInfo.SelectedItem = newFileSystemInfo;
            }
        }

        private void bckFileSystemLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            var fileSystemInfo = e.Argument as FileSystemInfo;
            if (fileSystemInfo != null)
            {
                bckFileSystemLoader.ReportProgress(10,
                    string.Format("Pull disk info. from {0}", fileSystemInfo));
                // TODO: string.Format("Pull disk info. from {0}", fileSystemInfo)


            }
        }

        private void bckFileSystemLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bckFileSystemLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void 刷新RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbFileSystemInfo_SelectedIndexChanged(sender, e);
        }

        private void trvFileSystem_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }
    }

    public class FileSystemInfo
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private IPAddress ip;

        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", name, ip);
            //return base.ToString();
        }
    }
}
