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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private Document _currentDocument;

        public Document CurrentDocument
        {
            get { return _currentDocument; }
            set
            {
                _currentDocument = value;
                if (value != null)
                { this.deployFileListBox.SetProject(value.Project); }
            }
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmCreateDocument = new FormCreateDocument();
            if (frmCreateDocument.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var document = frmCreateDocument.NewDeployDocument;
                document.Save();
                this.CurrentDocument = document;
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openDeployProject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (this.CurrentDocument != null && this.CurrentDocument.IsDirty)
                {
                    var result = MessageBox.Show(
                        "当前的部署项目尚未保存，要保存后关闭当前项目吗？", "确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.CurrentDocument.Save();
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        // nothing to do.
                    }
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                try
                {
                    this.CurrentDocument = Document.Load(this.openDeployProject.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            }
        }

        private void 部署ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.deployFileListBox.Deploy(sender, e);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CurrentDocument != null && this.CurrentDocument.IsDirty)
            {
                var result = MessageBox.Show(
                        "当前的部署项目尚未保存，要保存后关闭退出程序吗？", "确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.CurrentDocument.Save();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    // nothing to do.
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.CurrentDocument = new Document()
            {
                Fullname = Path.Combine(Path.GetTempPath(),
                  string.Format("DeployProject1.xml")),
                Project = new DeployProject()
            };
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            var document = this.CurrentDocument;
            if (document == null) { return; }
            var project = document.Project;
            if (project == null) { return; }
            this.lblInfo.Text = string.Format("{0} deploy entries", project.Count);
            this.Text = string.Format("{0}{1} @ {2} - Deploy Manager",
                Path.GetFileName(document.Fullname),
                document.IsDirty ? "*" : "",
                Path.GetDirectoryName(document.Fullname));
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = this.CurrentDocument;
            if (project == null) { return; }

            var fullnameValid = true;
            var path = Path.GetDirectoryName(project.Fullname);
            fullnameValid = fullnameValid && Directory.Exists(path);
            fullnameValid = fullnameValid && (!Path.GetTempPath().Contains(path));
            var fileName = Path.GetFileName(project.Fullname);
            fullnameValid = fullnameValid && DeployLib.Utility.IsValidFileName(fileName);

            if (fullnameValid)
            {
                project.Save();
            }
            else if (this.saveDeployProject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                project.Fullname = this.saveDeployProject.FileName;
                project.Save();
            }
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            var document = this.CurrentDocument;
            if (document != null)
            {
                document.Project.DeleteNet();
            }
        }

    }
}
