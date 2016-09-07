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
        public JsonResult<FileCounterContainer> CountFilesFromDirectory(string root, string token)
        {
            return Json(Count(root, token));
        }

        public FileCounterContainer Count(string root, string token)
        {
            //DirectoryNavigator dn = new DirectoryNavigator();
            if (token == "root")
            {
                return new FileCounter().CountAllFiles();
            }

            if (token == "back")
            {
                DirectoryInfo di = new DirectoryInfo(root);
                di = di.Parent;
                if (di != null)
                {
                    return new FileCounter().CountFiles(di.FullName);
                }

                return new FileCounter().CountAllFiles();
            }

            return new FileCounter().CountFiles(root);
        }
    }
}
