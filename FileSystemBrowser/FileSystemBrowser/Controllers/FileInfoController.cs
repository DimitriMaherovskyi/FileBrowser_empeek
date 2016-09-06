using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using FileSystemBrowser.Models;
using System.Web.Http.Results;

namespace FileSystemBrowser.Controllers
{
    public class FileInfoController : ApiController
    {
        // File length values
        private const int Mb10 = 1;
        private const int Mb50 = 1;
        private const int Mb100 = 1;

        [HttpGet]
        public JsonResult<FilesInfo> GetInformation()
        {
            return Json(new FilesInfo());
        }
    }
}
