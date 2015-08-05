using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployManager
{
    class NetworkNode
    {
        private string fullname;
        /// <summary>
        /// Something like \\127.0.0.1\F$\bitzhuwei.cnblogs.com\DeployManager\
        /// </summary>
        public string Fullname
        {
            get { return fullname; }
            set
            {
                fullname = value;
                var dir = new System.IO.DirectoryInfo(fullname);
                this.ShareName = dir.FullName;
            }
        }
        private NodeType nodeType;

        internal NodeType NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        public NetworkNode(string fullname, NodeType nodeType)
        {
            this.Fullname = fullname;
            this.NodeType = nodeType;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", nodeType, fullname);
            //return base.ToString();
        }

        public string ShareName { get; private set; }
    }

    public enum NodeType
    {
        Directory,
        File,
    }
}
