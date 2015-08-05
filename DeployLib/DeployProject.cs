using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace DeployLib
{
    public class DeployProject : ICloneable, IList<DeployEntry>
    {
        List<DeployEntry> itemList = new List<DeployEntry>();
        public const string strDeployProject = "DeployProject";

        public XElement ToXElement()
        {
            var result = new XElement(strDeployProject,
                from item in this
                select item.ToXElement());

            return result;
        }



        public static DeployProject Parse(XElement file)
        {
            DeployProject result = null;
            if (file.Name == strDeployProject)
            {
                result = new DeployProject();
                foreach (var item in file.Elements(DeployEntry.strDeployEntry))
                {
                    var entry = DeployEntry.Parse(item);
                    result.Add(entry);
                }
                result.IsDirty = false;
            }

            return result;
        }

        public void Save(string fullName)
        {
            var fullnameValid = true;
            var path = Path.GetDirectoryName(fullName);
            fullnameValid = fullnameValid && Directory.Exists(path);
            var fileName = Path.GetFileName(fullName);
            fullnameValid = fullnameValid && DeployLib.Utility.IsValidFileName(fileName);
            if (!fullnameValid)
            {
                throw new ArgumentException(
                    string.Format("fullname[{0}] is not a valid file name!", fullName));
            }

            this.ToXElement().Save(fullName);
            this.IsDirty = false;
        }


        public DeployReport Deploy(DeployCmd cmd,
            Func<int, object, bool> funcAfterProcessedOneEntry = null)
        {
            var count = 0;
            if (funcAfterProcessedOneEntry != null)
            { count = this.Sum(x => x.GetSourceFilesCount()); }
            var deployed = 0;
            var deployReport = new DeployReport();
            foreach (var item in this)
            {
                var report = item.Deploy(cmd, funcAfterProcessedOneEntry, deployed, count);
                deployed += report.Item1;
                deployReport.Add(report.Item2);
                deployReport.DeployedFileCount += deployed;
            }
            this.DeleteNet();
            return deployReport;
        }

        public object Clone()
        {
            var result = new DeployProject();
            foreach (var item in this)
            {
                var copy = item.Clone() as DeployEntry;
                result.Add(copy);
            }
            return result;
        }

        private bool _isDirty = false;

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }


        public int IndexOf(DeployEntry item)
        {
            return this.itemList.IndexOf(item);
        }

        public void Insert(int index, DeployEntry item)
        {
            this.itemList.Insert(index, item);
            this.IsDirty = true;
        }

        public void RemoveAt(int index)
        {
            this.itemList.RemoveAt(index);
            this.IsDirty = true;
        }

        public DeployEntry this[int index]
        {
            get
            {
                return this.itemList[index];
            }
            set
            {
                this.itemList[index] = value;
            }
        }

        public void Add(DeployEntry item)
        {
            this.itemList.Add(item);
            item.Project = this;
            this.IsDirty = true;
        }

        public void Clear()
        {
            this.itemList.Clear();
            this.IsDirty = true;
        }

        public bool Contains(DeployEntry item)
        {
            return this.itemList.Contains(item);
        }

        public void CopyTo(DeployEntry[] array, int arrayIndex)
        {
            this.itemList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.itemList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<DeployProject>)(this.itemList)).IsReadOnly; }
        }

        public bool Remove(DeployEntry item)
        {
            var result = this.itemList.Remove(item);
            if (result)
            {
                this.IsDirty = true;
            }
            return result;
        }

        public IEnumerator<DeployEntry> GetEnumerator()
        {
            return this.itemList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.itemList.GetEnumerator();
        }

        public void DeleteNet()
        {
            foreach (var item in this)
            {
                item.DeleteNet();
            }
        }

    }
}
