using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        // GET: Buyer
        public ActionResult Index()
        {           
            return View();
        }
        public ActionResult NotificationsSupp()
        {
            return View();
        }
    }
    
}