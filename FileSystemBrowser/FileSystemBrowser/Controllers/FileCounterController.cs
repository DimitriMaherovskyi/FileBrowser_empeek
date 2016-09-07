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
using FileSystemBrowser.Helpers;
using System.Web;

namespace FileSystemBrowser.Controllers
{
    public class FileCounterController : ApiController
    {
        [HttpGet]
        public JsonResult<FileCounterContainer> CountFilesFromDirectory(string root)
        {
            // If position is over logical drives.
            if (root != null)
            {
                return Json(new FileCounter().CountFiles(root));
            }
            else
            {
                return Json(new FileCounter().CountAllFiles());
            }
        }
    }
}
