using FileSystemBrowser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemBrowser.Helpers
{
    public class DirectoryNavigator
    {
        // Token constants.
        public const string commandBack = "back";
        public const string root = "root";

        public DirectoryContainer GrabContents(string root, string token)
        {
            DirectoryContainer dc = new DirectoryContainer();

            if (token == commandBack)
            {
                getParentDirectory(root, dc);
                return dc;
            }

            if (token == DirectoryNavigator.root)
            {
                getLogicalDrives(dc);
                return dc;
            }

            getChildDirectory(root, dc);
            return (dc);
        }

        private DirectoryContainer getParentDirectory(string root, DirectoryContainer dc)
        {
            DirectoryInfo di = new DirectoryInfo(root);
            di = di.Parent;

            // If directory info is not a logical drive.
            if (di != null)
            {
                return getDirectory(root, dc, di);
            }
            else
            {
                return getLogicalDrives(dc);
            }
        }

        private DirectoryContainer getChildDirectory(string root, DirectoryContainer dc)
        {
            DirectoryInfo di = new DirectoryInfo(root);
            return getDirectory(root, dc, di);
        }

        private DirectoryContainer getDirectory(string root, DirectoryContainer dc, DirectoryInfo di)
        {
            FolderContentsGrabber fcg = new FolderContentsGrabber();

            dc.DirectoryList = fcg.GetSubdirectories(di);
            dc.FileList = fcg.GetFilesFromDirectory(di);
            dc.Path = di.FullName;

            return dc;
        }

        private DirectoryContainer getLogicalDrives(DirectoryContainer dc)
        {
            string[] drives = Directory.GetLogicalDrives();
            DirectoryInfo[] dir = new DirectoryInfo[drives.Length];
            for (int i = 0; i < dir.Length; i++)
            {
                dir[i] = new DirectoryInfo(drives[i]);
            }

            dc.DirectoryList = dir;
            // Parent to logical drives is where app hosted. To avoid collisions.
            dc.Path = root;

            return dc;
        }
    }
}