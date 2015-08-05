using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using DeployLib;

namespace DeployLib
{
    public class Document
    {
        private DeployLib.DeployProject _project;

        public DeployLib.DeployProject Project
        {
            get { return _project; }
            set { _project = value; }
        }

        private string _filename;

        public string Fullname
        {
            get { return _filename; }
            set
            {
                if (value != null)
                {
                    if (value.ToLower().EndsWith(".xml"))
                    { _filename = value; }
                    else
                    { _filename = value + ".xml"; }
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Fullname, Project);
            //return base.ToString();
        }

        public bool IsDirty
        {
            get
            {
                var project = this.Project;
                if (project == null) { return false; }
                else { return project.IsDirty; }
            }
        }

        public void Save()
        {
            this.Project.Save(this.Fullname);
        }

        public static Document Load(string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
            {
                throw new ArgumentNullException("fullname for Document is not set!");
            }
            var file = XElement.Load(fullname);
            var result = new Document();
            result.Fullname = fullname;
            result.Project = DeployProject.Parse(file);

            return result;
        }
    }
}
