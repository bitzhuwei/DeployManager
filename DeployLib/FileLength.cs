using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    public class FileLength
    {
        public long Length { get; private set; }
        public FileLength(long length)
        {
            this.Length = length;
        }

        public override string ToString()
        {
            if (Length <= 0) { return string.Format("0KB"); }
            else if (Length < 1024) { return string.Format("1KB"); }
            else if (Length < 1024 * 1024) { return string.Format("{0:0.0}KB", (0.0 + Length) / 1024); }
            else if (Length < 1024 * 1024 * 1024) { return string.Format("{0:0.0}MB", (0.0 + Length) / 1024 / 1024); }
            else { return string.Format("{0:0.0}GB", (0.0 + Length) / 1024 / 1024 / 1024); }
        }
    }
}
