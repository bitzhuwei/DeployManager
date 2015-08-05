using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Management;
using DeployLib;

namespace DeployManager
{
    public partial class FormNetworkPath : Form
    {
        private static string lastIp = "127.0.0.1";
        private static string lastUsername = "";
        private static string lastPassword = "";
        public DeployLib.Transporter SelectedPath { get; set; }

        public FormNetworkPath(NodeType selectingNodeType)
        {
            InitializeComponent();
            this.txtIP.Text = lastIp;
            this.txtUsername.Text = lastUsername;
            this.txtPassword.Text = lastPassword;
            this.selectingNodeType = selectingNodeType;
        }
        public FormNetworkPath(string ip, string username, string password, NodeType selectingNodeType)
        {
            InitializeComponent();
            this.txtIP.Text = ip;
            this.txtUsername.Text = username;
            this.txtPassword.Text = password;
            this.selectingNodeType = selectingNodeType;
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress ip = null;
            if (!IPAddress.TryParse(txtIP.Text, out ip))
            {
                MessageBox.Show("IP地址非法，请重新输入！");
                return;
            }
            this.btnConnect.Text = "请稍候...";
            this.btnConnect.Enabled = false;
            this.treeView1.Nodes.Clear();

            try
            {
                WmiShareFunction.RemoveShareNetConnect(ip.ToString(), connectionName, txtUsername.Text, txtPassword.Text);
                WmiShareFunction.CreateShareNetConnect(ip.ToString(), connectionName, txtUsername.Text, txtPassword.Text);
                foreach (var diskChar in alphabet)
                {
                    var remoteDir = string.Format(@"\\{0}\{1}$", ip, diskChar);
                    try
                    {
                        var directories = Directory.GetDirectories(remoteDir);
                        var content = new NetworkNode(remoteDir, NodeType.Directory);
                        var diskNode = new TreeNode(remoteDir)
                        {
                            ToolTipText = content.ToString(),
                            Tag = content,
                        };
                        diskNode.BackColor = Color.Yellow;
                        this.treeView1.Nodes.Add(diskNode);
                    }
                    catch (Exception)
                    {
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (this.treeView1.Nodes.Count == 0)
            {
                MessageBox.Show(string.Format("获取{0}的磁盘信息失败，请重试！", ip), "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.btnConnect.Text = "连接";
            this.btnConnect.Enabled = true;
            this.treeView1.Enabled = true;
            //this.btnOK.Enabled = true;
        }
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string connectionName = "C$";
        private NodeType selectingNodeType;

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count > 0) { return; }

            var nodeContent = e.Node.Tag as NetworkNode;
            if (nodeContent.NodeType == NodeType.File) { return; }

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    var directories = Directory.GetDirectories(nodeContent.Fullname);
                    var files = Directory.GetFiles(nodeContent.Fullname);

                    e.Node.Nodes.AddRange(
                        (from item in directories
                         select GetDirectoryNode(item)
                         ).ToArray());
                    e.Node.Nodes.AddRange(
                        (from item in files
                         select GetFileNode(item)
                         ).ToArray());
                    e.Node.ExpandAll();
                    break;
                }
                catch (Exception)
                {
                    if (i == 0)
                    {
                        IPAddress ip = null;
                        if (!IPAddress.TryParse(txtIP.Text, out ip))
                        {
                            MessageBox.Show("IP地址非法，请重新输入！");
                            return;
                        }
                        WmiShareFunction.RemoveShareNetConnect(ip.ToString(), connectionName, txtUsername.Text, txtPassword.Text);
                        WmiShareFunction.CreateShareNetConnect(ip.ToString(), connectionName, txtUsername.Text, txtPassword.Text);
                    }
                }
            }
        }

        private TreeNode GetDirectoryNode(string text)
        {
            var leaveDir = text.Split(Path.DirectorySeparatorChar).Last();
            var result = new TreeNode(leaveDir);
            var content = new NetworkNode(text, NodeType.Directory);
            result.ToolTipText = content.ToString();
            result.Tag = content;
            result.BackColor = Color.Yellow;
            return result;
        }

        private TreeNode GetFileNode(string text)
        {
            var filename = Path.GetFileName(text);
            var result = new TreeNode(filename);
            var content = new NetworkNode(text, NodeType.File);
            result.ToolTipText = content.ToString();
            result.Tag = content;
            return result;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
            { MessageBox.Show("请选择一个结点！"); return; }
            var content = node.Tag as NetworkNode;
            if (content == null)
            { MessageBox.Show("没有绑定 NetworkNode 对象！"); return; }
            IPAddress ip = null;
            if (!IPAddress.TryParse(txtIP.Text, out ip))
            { MessageBox.Show("IP地址非法，请重新输入！"); return; }
            var result = new DeployLib.Transporter();
            var dir = new DirectoryInfo(content.Fullname);
            result.Goods = dir.FullName.Substring(dir.Root.FullName.Length - 2);// content.ShareName;// dir.FullName.Substring(dir.Root.FullName.Length);
            result.Ip = txtIP.Text; lastIp = txtIP.Text;
            result.Username = txtUsername.Text; lastUsername = txtUsername.Text;
            result.Password = txtPassword.Text; lastPassword = txtPassword.Text;
            this.SelectedPath = result;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var content = e.Node.Tag as NetworkNode;
            if (content == null) { this.btnOK.Enabled = false; return; }

            this.btnOK.Enabled = (content.NodeType == this.selectingNodeType) ;
        }
    }
}
