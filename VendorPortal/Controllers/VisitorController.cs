using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VendorPortal.Models;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;

namespace VendorPortal.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ECNVisitor()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult Download(string file, string loc)
        {
            string s = loc + "\\" + file;
            s = GetVirtualPath(s);
            byte[] fileBytes = System.IO.File.ReadAllBytes(@s);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = file;
            return response;
        }
        private string GetVirtualPath(string physicalPath)
        {
            //   string rootpath = Server.MapPath("~/");
            // physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");
            return physicalPath;
        }
    }
}