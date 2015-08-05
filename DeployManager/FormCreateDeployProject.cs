using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using DeployLib;

namespace DeployManager
{
    public partial class FormCreateDeployProject : Form
    {
        public DeployProject NewDeployProject { get; set; }

        public FormCreateDeployProject()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.selectFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtPath.Text = this.selectFolder.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.RegexIsValidFileName(this.txtDeployItemListName.Text))
            {
                MessageBox.Show("文件名非法，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(this.txtPath.Text))
            {
                MessageBox.Show("请指定要保存的路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(this.txtPath.Text))
            {
                MessageBox.Show("您给定的路径不存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!this.txtDeployItemListName.Text.ToLower().EndsWith(".xml"))
            {
                this.txtDeployItemListName.Text = this.txtDeployItemListName.Text + ".xml";
            }

            this.NewDeployProject = new DeployProject() { Fullname = Path.Combine(this.txtPath.Text,this.txtDeployItemListName.Text) };

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        /// <summary>
        /// 正则式判断，是否为合法文件格式,是则返回Ture,否则返回False;
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool RegexIsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return false; }
            if (fileName.Length > 255) { return false; }
            else
            {
                Regex regex = new Regex(@"/|\\|<|>|\*|\?");
                return regex.IsMatch(fileName) ? false : true;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
