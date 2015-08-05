using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    public class DeployEntryReport : List<SingleFileReport>
    {
        private DeployEntry entry;
        public DeployEntryReport(DeployEntry entry)
        {
            this.entry = entry;
        }
        public override string ToString()
        {
            var result = string.Format("entry: {0}", entry);
            return result;
        }
    }
}
