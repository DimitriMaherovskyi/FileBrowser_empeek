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

namespace FileSystemBrowser.Controllers
{
    public class FileInfoController : ApiController
    {


        [HttpGet]
        public JsonResult<FileCounterContainer> GetInformation(string root)
        {
            // Using FileCounter helper.
            FileCounterContainer fc = new FileCounter().CountFiles(root);
            return Json(fc);
        }

        
    }
}
