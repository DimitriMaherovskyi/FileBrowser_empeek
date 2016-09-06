using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using FileSystemBrowser.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;

namespace FileSystemBrowser.Controllers
{
    public class FileInfoController : ApiController
    {
        // File length values in bytes.
        private const int Mb10 = 10485760;
        private const int Mb50 = 52428800;
        private const int Mb100 = 104857600;

        // Locker.
        private static readonly object sync = new object();

        [HttpGet]
        public JsonResult<FilesInfo> GetInformation()
        {
            FilesInfo fi = CountFiles();
            return Json(fi);
        }

        private FilesInfo CountFiles()
        {
            FilesInfo fi = new FilesInfo();

            // To search in the entire computer.
            string[] drives = Environment.GetLogicalDrives();
            
            Parallel.ForEach(drives, d =>
            {
                DriveInfo di = new DriveInfo(d);
                DirectoryInfo rootDir = di.RootDirectory;
                WalkDirectoryTree(rootDir, fi);
            });

            return fi;
        }

        private static void WalkDirectoryTree(DirectoryInfo root, FilesInfo fInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            // Getting files from directory.
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {

            }
            catch (DirectoryNotFoundException e)
            {

            }

            // Check file length to count them by length.
            if (files != null)
            {
                // Synchronize threads.
                lock (sync)
                {
                    // Check files size, count them by category.
                    foreach (FileInfo fi in files)
                    {
                        if (fi.Length <= Mb10)
                        {
                            fInfo.FileUnder10MbCounter++;
                        }
                        if (fi.Length > Mb10 && fi.Length <= Mb50)
                        {
                            fInfo.File10To50MbCounter++;
                        }
                        if (fi.Length >= Mb100)
                        {
                            fInfo.FileOver100MbCounter++;
                        }
                    }
                }
            }

            // Get all subdirectories under current directory.
            try
            {
                subDirs = root.GetDirectories();
            }
            catch (UnauthorizedAccessException e)
            {

            }
            catch (DirectoryNotFoundException e)
            {

            }

            // Recursive call.
            if (subDirs != null)
            {
                Parallel.ForEach(subDirs, sd =>
                {
                    WalkDirectoryTree(sd, fInfo);
                });
            }
        }
    }
}
