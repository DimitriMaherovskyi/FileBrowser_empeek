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
        public JsonResult<DirectoryContainer> GrabDirectoryContents(string root, string token)
        {
            return Json(new DirectoryNavigator().GrabContents(root, token));
        }   
    }
}
