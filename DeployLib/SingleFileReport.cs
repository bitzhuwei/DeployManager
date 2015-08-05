using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    public class SingleFileReport
    {
        public Exception Error { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Error.Message);
        }
    }
}
