﻿using System;
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
        
        public FileCounterContainer Count(string root, string token)
        {
            if (token == DirectoryNavigator.root)
            {
                return countAllFiles();
            }
            if (token == DirectoryNavigator.commandBack)
            {
                // Get parent and check if it exists.
                DirectoryInfo di = new DirectoryInfo(root);
                di = di.Parent;
                if (di != null)
                {
                    return countFiles(di.FullName);
                }

                return countAllFiles();
            }

            return countFiles(root);
        }

        private FileCounterContainer countAllFiles()
        {
            FileCounterContainer fc = new FileCounterContainer();

            // To search in the entire host.
            string[] drives = Environment.GetLogicalDrives();

            Parallel.ForEach(drives, d =>
            {
                DriveInfo di = new DriveInfo(d);
                DirectoryInfo rootDir = di.RootDirectory;
                walkDirectoryTree(rootDir, fc);
            });

            return fc;
        }

        private FileCounterContainer countFiles(string root)
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
            FolderContentsGrabber fcg = new FolderContentsGrabber();

            files = fcg.GetFilesFromDirectory(root);
            determineFilesLength(files, fileCounter);
            subDirs = fcg.GetSubdirectories(root);

            // Recursive call.
            if (subDirs != null)
            {
                Parallel.ForEach(subDirs, sd =>
                {
                    walkDirectoryTree(sd, fileCounter);
                });
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
    }
}