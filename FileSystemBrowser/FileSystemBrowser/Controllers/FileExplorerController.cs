using FileSystemBrowser.Helpers;
using FileSystemBrowser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace FileSystemBrowser.Controllers
{
    public class FileExplorerController : ApiController
    {
        [HttpGet]
        public JsonResult<DirectoryContainer> GrabDirectoryContents(string root)
        {
            DirectoryContainer dc = new DirectoryContainer();
            FolderContentsGrabber fcg = new FolderContentsGrabber();

            if (root == "back")
            {
                dc.DirectoryList = fcg.GetSubdirectories(new DirectoryInfo(root).Parent);
                dc.FileList = fcg.GetFilesFromDirectory(new DirectoryInfo(root).Parent);

                return Json(dc);
            }

            
            dc.DirectoryList = fcg.GetSubdirectories(new DirectoryInfo(root));
            dc.FileList = fcg.GetFilesFromDirectory(new DirectoryInfo(root));

            return Json(dc);
        }
        
    }
}
