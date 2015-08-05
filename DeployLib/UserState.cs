using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    /// <summary>
    /// Used in backgroundworker.ProgressChanged event as its second parameter 'userState'.
    /// </summary>
    public class UserState
    {
        public UserState(bool updated, string sourceFullname, string destinationFullname, long fileLength, DateTime startCopying, DeployEntry entry)
        {
            this.Updated = updated; this.SourceFullname = sourceFullname;
            this.DestinationFullname = destinationFullname; this.Length = new FileLength(fileLength);
            this.StartCopying = startCopying;
            this.Entry = entry;
        }
        public bool Updated { get; set; }
        public string SourceFullname { get; set; }
        public string DestinationFullname { get; set; }
        public FileLength Length { get; private set; }

        public override string ToString()
        {
            var result = string.Format("{0} [{1}] to [{2}]({3})",
                Updated ? "Coppying" : "Skipped coppying",
                SourceFullname, DestinationFullname, Length);
            return result;
        }


        public DeployEntry Entry { get; private set; }

        public DateTime StartCopying { get; set; }
    }
}
