﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemBrowser.Models
{
    public class FilesInfo
    {
        // File counters.
        public long FileUnder10MbCounter { get; set; }
        public long File10To50MbCounter { get; set; }
        public long FileOver100MbCounter { get; set; }

        // Files name container.
        public List<string> FileDirectory { get; set; }
    }
}