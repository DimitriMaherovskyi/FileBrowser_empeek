using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemBrowser.Models;
using System.IO;
using System.Threading.Tasks;

namespace FileSystemBrowser.Helpers
{
    public class FileCounter
    {
        // File length values in bytes.
        private const int Mb10 = 10485760;
        private const int Mb50 = 52428800;
        private const int Mb100 = 104857600;

        // Locker.
        private static readonly object sync = new object();

        public FileCounterContainer CountFiles(string root)
        {
            FileCounterContainer fc = new FileCounterContainer();
            DirectoryInfo rootDir = new DirectoryInfo(root);
            walkDirectoryTree(rootDir, fc);

            return fc;
        }

        private void walkDirectoryTree(DirectoryInfo root, FileCounterContainer fileCounter)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = getFilesFromDirectory(root);
            determineFilesLength(files, fileCounter);
            subDirs = getSubdirectories(root);

            // Recursive call.
            if (subDirs != null)
            {
                Parallel.ForEach(subDirs, sd =>
                {
                    walkDirectoryTree(sd, fileCounter);
                });
            }
        }

        // walkDirectoryTree() methods.
        private FileInfo[] getFilesFromDirectory(DirectoryInfo root)
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

        private DirectoryInfo[] getSubdirectories(DirectoryInfo root)
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

        private void determineFilesLength(FileInfo[] files, FileCounterContainer fileCounter)
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
        // End of walkDirectoryTree() methods.
    }
}