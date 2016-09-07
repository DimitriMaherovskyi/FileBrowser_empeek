using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemBrowser.Models
{
    public class DirectoryContainer
    {
        public string Path { get; set; }
        public DirectoryInfo[] DirectoryList { get; set; }
        public FileInfo[] FileList { get; set; }
    }
}