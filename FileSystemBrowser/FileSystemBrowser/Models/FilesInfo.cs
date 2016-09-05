using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemBrowser.Models
{
    public class FilesInfo
    {
        // File counters.
        public int FileUnder10MbCounter { get; set; }
        public int File10To50MbCounter { get; set; }
        public int FileOver100MbCounter { get; set; }

        // Files name container.
        public List<string> FileDirectory { get; set; }
    }
}