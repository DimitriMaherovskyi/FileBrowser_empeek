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
        public JsonResult<FileCounter> GetInformation()
        {
            FileCounter fi = CountFiles();
            return Json(fi);
        }

        private FileCounter CountFiles()
        {
            FileCounter fc = new FileCounter();

            // To search in the entire computer.
            string[] drives = Environment.GetLogicalDrives();

            Parallel.ForEach(drives, d =>
            {
                DriveInfo di = new DriveInfo(d);
                DirectoryInfo rootDir = di.RootDirectory;
                WalkDirectoryTree(rootDir, fc);
            });

            return fc;
        }

        // CountFiles() method.
        private static void WalkDirectoryTree(DirectoryInfo root, FileCounter fileCounter)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            
            files = GetFilesFromDirectory(root);
            DetermineFilesLength(files, fileCounter);
            subDirs = GetSubdirectories(root);
            
            // Recursive call.
            if (subDirs != null)
            {
                Parallel.ForEach(subDirs, sd =>
                {
                    WalkDirectoryTree(sd, fileCounter);
                });
            }
        }

        // WalkDirectoryTree() methods.
        private static FileInfo[] GetFilesFromDirectory(DirectoryInfo root)
        {
            // Getting files from directory.
            try
            {
                return root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                return null;
            }
        }

        private static DirectoryInfo[] GetSubdirectories(DirectoryInfo root)
        {
            // Get all subdirectories under current directory.
            try
            {
                return root.GetDirectories();
            }
            catch (UnauthorizedAccessException e)
            {
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                return null;
            }
        }

        private static void DetermineFilesLength(FileInfo[] files, FileCounter fileCounter)
        {
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
                            fileCounter.FileUnder10MbCounter++;
                        }
                        if (fi.Length > Mb10 && fi.Length <= Mb50)
                        {
                            fileCounter.File10To50MbCounter++;
                        }
                        if (fi.Length >= Mb100)
                        {
                            fileCounter.FileOver100MbCounter++;
                        }
                    }
                }
            }
        }
        // End of WalkDirectoryTree() methods.
    }
}
