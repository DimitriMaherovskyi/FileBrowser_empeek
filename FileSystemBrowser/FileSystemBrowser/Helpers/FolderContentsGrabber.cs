using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemBrowser.Helpers
{
    public class FolderContentsGrabber
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FileInfo[] GetFilesFromDirectory(DirectoryInfo root)
        {
            // Getting files from directory.
            try
            {
                return root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                log.Info(e);
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                log.Info(e);
                return null;
            }
        }

        public DirectoryInfo[] GetSubdirectories(DirectoryInfo root)
        {
            // Get all subdirectories under current directory.
            try
            {
                return root.GetDirectories();
            }
            catch (UnauthorizedAccessException e)
            {
                log.Info(e);
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                log.Info(e);
                return null;
            }
        }
    }
}