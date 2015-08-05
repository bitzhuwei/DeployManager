using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    public class DeployReport : List<DeployEntryReport>
    {
        public override string ToString()
        {
            var builder = new StringBuilder();
            var errorCount = 0;
            foreach (var entryReport in this)
            {
                var entryErrors = entryReport.Count;
                if (entryErrors > 0)
                {
                    errorCount += entryErrors;
                    builder.AppendLine(string.Format("{0} Error(s) during: {1}",
                        entryErrors, entryReport));
                    foreach (var singleFileReport in entryReport)
                    {
                        builder.AppendLine(string.Format("{0}", singleFileReport));
                    }
                    builder.AppendLine();
                }
            }
            if (errorCount == 0)
            {
                builder.AppendLine(string.Format(
                    "All of {0} files are deployed successfully!",
                    this.DeployedFileCount));
            }
            else
            {
                builder.Insert(0, string.Format("{0} error(s) occured!{1}", errorCount, Environment.NewLine));
            }

            var result = builder.ToString();
            return result;
        }

        public int DeployedFileCount { get; set; }
    }
}
