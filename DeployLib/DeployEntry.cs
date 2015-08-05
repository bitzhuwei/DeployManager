using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

namespace DeployLib
{
    public class DeployEntry : ICloneable
    {
        private DeployProject project;

        public DeployProject Project
        {
            get { return project; }
            set { project = value; }
        }

        private Transporter _source;

        public Transporter Source
        {
            get { return _source; }
            set
            {
                if (value != _source && this.project != null)
                {
                    this.project.IsDirty = true;
                }
                _source = value;
            }
        }

        private Transporter _destinationFile;

        public Transporter DestinationDir
        {
            get { return _destinationFile; }
            set
            {
                if (value != _destinationFile && this.project != null)
                {
                    this.project.IsDirty = true;
                }
                _destinationFile = value;
            }
        }

        public override string ToString()
        {
            var result = string.Format("[{0}] -> [{1}]", _source, _destinationFile);
            return result;
        }

        public XElement ToXElement()
        {
            var result = new XElement(strDeployEntry,
                new XElement(strSource, this.Source.ToXElement()),
                new XElement(strDestinationDir, this.DestinationDir.ToXElement()));

            return result;
        }

        public const string strDeployEntry = "DeployEntry";
        private const string strSource = "Source";
        private const string strDestinationDir = "DestinationDir";

        public static DeployEntry Parse(XElement item)
        {
            if (item == null || item.Name != strDeployEntry) { return null; }

            var result = new DeployEntry();
            var source = item.Element(strSource);
            result.Source = Transporter.Parse(source.Element(Transporter.strTransporter));
            var dest = item.Element(strDestinationDir);
            result.DestinationDir = Transporter.Parse(dest.Element(Transporter.strTransporter));

            return result;
        }

        public object Clone()
        {
            var result = this.MemberwiseClone();
            return result;
        }

        public int GetSourceFilesCount()
        {
            var sourceFilenames = Path.GetFileName(this.Source.Goods);
            var sourceRoot = string.Empty;
            if (!string.IsNullOrEmpty(this.Source.Ip))
            {
                WmiShareFunction.CreateShareNetConnect(
                    this.Source.Ip, this.Source.Goods,
                    this.Source.Username, this.Source.Password);
                sourceRoot = string.Format(@"\\{0}\{1}",
                    this.Source.Ip, Path.GetDirectoryName(this.Source.Goods));
            }
            else
            { sourceRoot = Path.GetDirectoryName(this.Source.Goods); }
            var sourceFullnames = Directory.GetFiles(sourceRoot, sourceFilenames, SearchOption.AllDirectories);
            return sourceFullnames.Length;
        }
        public Tuple<int, DeployEntryReport> Deploy(DeployCmd cmd,
            Func<int, object, bool> actReportProgress = null,
            int deployed = 0, int count = 0, bool forceDeploy = false)
        {
            var sourceFilenames = Path.GetFileName(this.Source.Goods);
            var sourceRoot = string.Empty;
            if (!string.IsNullOrEmpty(this.Source.Ip))
            {
                //WmiShareFunction.CreateShareNetConnect(
                //    this.Source.Ip, this.Source.Goods,
                //    this.Source.Username, this.Source.Password);
                sourceRoot = string.Format(@"\\{0}\{1}",
                    this.Source.Ip, Path.GetDirectoryName(this.Source.Goods));
            }
            else
            { sourceRoot = Path.GetDirectoryName(this.Source.Goods); }
            var sourceFullnames = Directory.GetFiles(sourceRoot, sourceFilenames, SearchOption.AllDirectories);

            var destRoot = string.Empty;
            if (!string.IsNullOrEmpty(this.DestinationDir.Ip))
            {
                //WmiShareFunction.CreateShareNetConnect(
                //    this.DestinationDir.Ip, this.DestinationDir.Goods,
                //    this.DestinationDir.Username, this.DestinationDir.Password);
                destRoot = string.Format(@"\\{0}\{1}",
                    this.DestinationDir.Ip, this.DestinationDir.Goods);
            }
            else
            { destRoot = new DirectoryInfo(this.DestinationDir.Goods).FullName; }

            int deployedIndex = 0;
            var report = new DeployEntryReport(this);
            foreach (var sourceFullname in sourceFullnames)
            {
                CopySingleFile(sourceRoot, sourceFullname, destRoot,
                    deployed, ref deployedIndex, count, ref report, actReportProgress,
                    cmd);
            }

            return new Tuple<int, DeployEntryReport>(deployedIndex, report);
        }

        private void CopySingleFile(string sourceRoot, string sourceFullname, string destRoot,
            int deployed, ref int deployedIndex, int count,
            ref DeployEntryReport report, Func<int, object, bool> actReportProgress,
            DeployCmd cmd)
        {
            try
            {
                if (cmd == null) { cmd = new DeployCmd(false); }
                if (!cmd.CancelDeploy)
                {
                    var sourceSubFullname = sourceFullname.Substring(sourceRoot.Length);
                    var destFullname = destRoot + sourceSubFullname;

                    var destDir = Path.GetDirectoryName(destFullname);
                    var destFileExists = File.Exists(destFullname);
                    var sourceFile = new FileInfo(sourceFullname);
                    var deploy = true;
                    if (!Directory.Exists(destDir))
                    { Directory.CreateDirectory(destDir); }
                    else if (destFileExists)
                    {
                        if (!(cmd.ForceDeploy))
                        {
                            var destFile = new FileInfo(destFullname);
                            if (sourceFile.LastWriteTime <= destFile.LastWriteTime)
                            { deploy = false; }
                        }
                    }

                    if (deploy)
                    {
                        DoCopySingleFile(sourceFullname, deployed, deployedIndex, count, actReportProgress, cmd, destFullname, destFileExists, sourceFile);
                    }
                    else
                    {
                        if (actReportProgress != null)
                        {
                            actReportProgress(
                                100 * (deployed + deployedIndex + 1) / count,
                                new UserState(false, sourceFullname, destFullname, sourceFile.Length, DateTime.Now, this));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var singleFileReport = new SingleFileReport() { Error = e, };
                report.Add(singleFileReport);
            }

            deployedIndex++;
        }

        private void DoCopySingleFile(string sourceFullname, int deployed, int deployedIndex, int count, Func<int, object, bool> actReportProgress, DeployCmd cmd, string destFullname, bool destFileExists, FileInfo sourceFile)
        {
            if (destFileExists)
            { File.SetAttributes(destFullname, System.IO.FileAttributes.Normal); }

            if (!string.IsNullOrEmpty(this.Source.Ip))
            {
                var sourcePath = Path.GetDirectoryName(this.Source.Goods);
                WmiShareFunction.CreateShareNetConnect(
                    this.Source.Ip, sourcePath, //"C$",
                    this.Source.Username,
                    this.Source.Password);
            }
            if (!string.IsNullOrEmpty(this.DestinationDir.Ip))
            {
                WmiShareFunction.CreateShareNetConnect(
                    this.DestinationDir.Ip, this.DestinationDir.Goods, //"C$",
                    this.DestinationDir.Username,
                    this.DestinationDir.Password);
            }

            if (actReportProgress != null)
            {
                actReportProgress(
                    100 * (deployed + deployedIndex + 1) / count,
                    new UserState(true, sourceFullname, destFullname, sourceFile.Length, DateTime.Now, this));
            }
            if (!cmd.CancelDeploy)
            { File.Copy(sourceFullname, destFullname, true); }
        }


        public void DeleteNet()
        {
            if (!string.IsNullOrEmpty(this.Source.Ip))
            {
                var sourcePath = Path.GetDirectoryName(this.Source.Goods);
                WmiShareFunction.RemoveShareNetConnect(
                    this.Source.Ip, sourcePath,
                    this.Source.Username, this.Source.Password);
            }
            if (!string.IsNullOrEmpty(this.DestinationDir.Ip))
            {
                WmiShareFunction.RemoveShareNetConnect(
                    this.DestinationDir.Ip, this.DestinationDir.Goods,
                    this.DestinationDir.Username, this.DestinationDir.Password);
            }
        }

        public bool Conflicts(ref string message)
        {
            //todo: please implement this method.
            var source = this.Source;
            if (source == null)
            {
                message = "source is null";
                return true;
            }
            var dest = this.DestinationDir;
            if (dest == null)
            {
                message = "destinaiton directory is null";
                return true;
            }
            var strSource = source.ToString();
            var strDest = dest.ToString();
            if (strSource.Contains(strDest))
            {
                message = string.Format("source[{0}] contains destination directory[{1}]!",
                    strSource, strDest);
                return true;
            }
            if (strDest.Contains(strSource))
            {
                message = string.Format("destination directory[{0}] contains source[{1}]!",
                    strDest, strSource);
                return true;
            }

            return false;
        }
    }
}
