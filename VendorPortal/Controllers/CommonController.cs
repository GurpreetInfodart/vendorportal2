using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;


namespace VendorPortal.Controllers
{
    public class CommonController : Controller
    {
        BasicPaging objBasicPaging = null;
        VendorPortalEntities db = new VendorPortalEntities();
        public ActionResult Rating()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult PPM()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult Warranty()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult LineStoppage()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult Notifications()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, po.SUPP_NAME }).Distinct();
            return View();
        }
        public ActionResult NotificationsSupp()
        {
            return View();
        }
        public ActionResult FOC()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult DownloadManual()
        {
            string loc = Server.MapPath("~/Template");
            string s = loc + "\\" + "Bharat_Seats_supplier_guide_Phase-1___2.pdf";
            s = GetVirtualPath(s);
            byte[] fileBytes = System.IO.File.ReadAllBytes(@s);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = "Bharat_Seats_supplier_guide_Phase-1___2.pdf";
            return response;
        }
        public JsonResult UpdateFOC(string SuppStockInHand, string SuppRejtedQty, string BSLClosingBalance, string Id,
         tblFOC tblApp)
        {
            bool flag = false;
            try
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblFOC tblApps = Obj.tblFOCs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.SuppStockInHand = Convert.ToDecimal(SuppStockInHand);
                    tblApps.SuppRejtedQty = Convert.ToDecimal(SuppRejtedQty);
                    tblApps.SuppClosingBalance = Convert.ToDecimal(SuppStockInHand) + Convert.ToDecimal(SuppRejtedQty);
                    Decimal d = Convert.ToDecimal(SuppStockInHand) + Convert.ToDecimal(SuppRejtedQty);
                    Decimal d1 = Convert.ToDecimal(d) - Convert.ToDecimal(BSLClosingBalance);
                    tblApps.Difference = Convert.ToDecimal(d1);
                    tblApps.LastModified = DateTime.Now;
                    Obj.SaveChanges();
                    flag = true;
                }
                //List<int> id = new List<int>();
                //id = db.Database.SqlQuery<Int32>("exec InsNextMonthRecordsFOC @Id={0}", Convert.ToInt64(Id)).ToList();
            }
            catch (Exception ex)
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Record updated successfully.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NotificationUpload(string UserName, string StartDate, string EndDate, string Message,
            string UC, tblNotification tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            try
            {
                if (Message != "" && UC.Equals("d"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        Obj.tblNotifications.Add(tblApp);

                        tblApp.UserCode = UserName;
                        tblApp.Message = Message;
                        tblApp.StartDate = Convert.ToDateTime(StartDate);
                        tblApp.ValidTill = Convert.ToDateTime(EndDate);
                        tblApp.UploadDate = DateTime.Now;
                        tblApp.Active = 1;
                        tblApp.UploadedBy = Supp_code;
                        Obj.SaveChanges();
                    }
                }
                else if (Message != "" && UC.Equals("t"))
                {
                    var query = from val in UserName.Split(';')
                                select val;
                    foreach (string nu in query)
                    {
                        if (nu != "")
                        {
                            using (VendorPortalEntities Obj = new VendorPortalEntities())
                            {
                                Obj.tblNotifications.Add(tblApp);

                                tblApp.UserCode = nu;
                                tblApp.Message = Message;
                                tblApp.StartDate = Convert.ToDateTime(StartDate);
                                tblApp.ValidTill = Convert.ToDateTime(EndDate);
                                tblApp.UploadDate = DateTime.Now;
                                tblApp.Active = 1;
                                tblApp.UploadedBy = Supp_code;
                                Obj.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = "Error while uploading.";
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public ActionResult DeleteFOC(string Id, tblFOC tblApp)
        {
            try
            {
                if (tblApp != null)
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblFOC tblApps = Obj.tblFOCs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.Active = 3;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                    }
                }
                ViewBag.Result = "Record deleted successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Record not deleted.Please try again!";
            }

            return View("FOC");
        }

        public JsonResult ApproveFOC(string Id, tblFOC tblApp)
        {
            bool flag = false;
            try
            {
                if (tblApp != null)
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblFOC tblApps = Obj.tblFOCs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.Status = true;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully Approved.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteNotification(string Id, tblNotification tblApp)
        {
            if (tblApp != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblNotification tblApps = Obj.tblNotifications.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Active = 3;
                    Obj.SaveChanges();
                }

            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public PartialViewResult Rtg(int CurrentPage = 1, string SUPP_CODE = "")
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblRating> list = db.Database.SqlQuery<tblRating>("exec GetSuplierRating  @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierRating  @SupplierName={0}", Supp_code).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount.FirstOrDefault(),
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
                return PartialView("_Rating", list);

            }
            else
            {
                List<tblRating> list = db.Database.SqlQuery<tblRating>("exec GetSuplierRating @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierRating @SupplierName={0}", SUPP_CODE).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount.FirstOrDefault(),
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else

                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

                return PartialView("_Rating", list);
            }
        }

        public JsonResult Notification()
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblNotification> list = db.Database.SqlQuery<tblNotification>("exec GetSuplierNotification @SupplierName={0}", Supp_code).ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                List<tblNotification> list = db.Database.SqlQuery<tblNotification>("exec GetAdminNotification").ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public JsonResult GetFOC()
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblFOC> list = db.Database.SqlQuery<tblFOC>("exec GetSuplierFOC @SupplierName={0}", Supp_code).ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                List<tblFOC> list = db.Database.SqlQuery<tblFOC>("exec GetALLSuplierFOC").ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
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
        public PartialViewResult PP(int CurrentPage = 1, string SUPP_CODE = "")
        {
            string Supp_code = User.Identity.Name;

            if (User.IsInRole("Supplier"))
            {
                List<tblPPM> list = db.Database.SqlQuery<tblPPM>("exec GetSuplierPPM  @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierPPM  @SupplierName={0}", Supp_code).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount.FirstOrDefault(),
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

                return PartialView("_PPM", list);
            }
            else
            {
                List<tblPPM> list = db.Database.SqlQuery<tblPPM>("exec GetSuplierPPM @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierPPM @SupplierName={0}", SUPP_CODE).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount.FirstOrDefault(),
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else

                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

                return PartialView("_PPM", list);
            }
        }


        public PartialViewResult Wrt(int CurrentPage = 1, String SUPP_CODE = "")
        {
            string Supp_code = User.Identity.Name;

            if (User.IsInRole("Supplier"))
            {
                List<tblWarrantyPartsRejected> list = db.Database.SqlQuery<tblWarrantyPartsRejected>("exec GetSuplierWarranty  @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierWarranty  @SupplierName={0}", Supp_code).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount[0],
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;


                return PartialView("_Warranty", list);
            }
            else
            {
                List<tblWarrantyPartsRejected> list = db.Database.SqlQuery<tblWarrantyPartsRejected>("exec GetSuplierWarranty  @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierWarranty  @SupplierName={0}", SUPP_CODE).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount[0],
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else

                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

                return PartialView("_Warranty", list);
            }
        }

        public PartialViewResult LS(int CurrentPage = 1, String SUPP_CODE = "")
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblLineStopage> list = db.Database.SqlQuery<tblLineStopage>("exec GetSuplierLineStoppage @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierLineStoppage  @SupplierName={0}", Supp_code).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount[0],
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;


                return PartialView("_LineStoppage", list);
            }
            else
            {
                List<tblLineStopage> list = db.Database.SqlQuery<tblLineStopage>("exec GetSuplierLineStoppage @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();
                var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierLineStoppage  @SupplierName={0}", SUPP_CODE).ToList();
                objBasicPaging = new BasicPaging()
                {
                    TotalItem = TotalCount[0],
                    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                    CurrentPage = CurrentPage
                };
                if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
                {
                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
                }
                else

                    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
                ViewBag.paging = objBasicPaging;


                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

                return PartialView("_LineStoppage", list);
            }
        }


        public JsonResult GetECNList()
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblECN> list = db.Database.SqlQuery<tblECN>("exec GetSupplierECN @SupplierName={0}", Supp_code).ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                List<tblECN> list = db.Database.SqlQuery<tblECN>("exec GetAllSupplierECN").ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

    }
}