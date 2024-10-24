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
    [Authorize(Roles = "BSLUser,Buyer,Quality,R&D,Visitor")]
    public class BSLUserController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
        public ActionResult PackingStandardBSLUser()
        {
            return View();
        }
        public ActionResult SourcingBSLUser()
        {
            ViewBag.DDLSN = (from po in db.POLists
                             select new { po.SUPP_CODE }).Distinct();

            return View();
        }
        public ActionResult CapacityBSLUser()
        {
            ViewBag.DDLSN = (from po in db.POLists
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult QualityBSLUser()
        {
            ViewBag.DDLSN = (from po in db.POLists
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult FortnightBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult KITBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult PreDispatchBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult FourMBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult SMIRBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult QMBSLUser()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            return View();
        }
        public partial class ApprovedPacking
        {
            public string SUPP_CODE { get; set; }
            public string MaterialCode { get; set; }
            public string FileName { get; set; }
            public Int64? Id { get; set; }
            public DateTime? UploadDate { get; set; }
            public bool? Status { get; set; }
            public string Location { get; set; }
            public int? Active { get; set; }

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SuplierSourcing(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSourcing @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Quality(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierQuality @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Capacity(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierCapacity @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Fornight(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFornight @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult KIT(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierKIT @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Pre(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierPreDispatch @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FourM(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFourM @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult AllFourM()
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierFourM").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SMIR(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSMIR @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult AllSMIR()
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierSMIR").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult QM()
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierQM").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult VendorInfoBSLUser()
        {

            return View();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSupplierList()
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSupplierAdmin").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult QueApprove(int Id)
        {

            var _getUpdate = db.tblVendorQues.Find(Id);
            if (_getUpdate != null)
            {
                _getUpdate.Status = true;
                db.Entry(_getUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("VendorQueBSLUser");
        }
        public ActionResult QueDelete(int Id)
        {

            var _getUpdate = db.tblVendorQues.Find(Id);
            if (_getUpdate != null)
            {
                _getUpdate.Active = 3;
                db.Entry(_getUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("VendorQueBSLUser");
        }

        public ActionResult DeleteSupplier(string Id, tblVendorInfo tblApp)
        {
            if (tblApp != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblVendorInfo tblApps = Obj.tblVendorInfoes.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Status = false;
                    tblApps.Active = 2;
                    tblApps.ModifiedOn = DateTime.Now;
                    Obj.SaveChanges();
                }

            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult NotificationsSupp()
        {
            return View();
        }

        public ActionResult ApproveSupplier(string Id, tblVendorInfo tblApp)
        {
            if (tblApp != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblVendorInfo tblApps = Obj.tblVendorInfoes.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Status = true;
                    tblApps.ModifiedOn = DateTime.Now;
                    Obj.SaveChanges();
                }

            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
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
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedPackingList()
        {
            //List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingStandard @CurrentPage={0}, @SupplierName ={1}", CurrentPage, SUPP_CODE).ToList();
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingStandard @CurrentPage={0}, @SupplierName ={1}", 1, "").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult CommonUploadExlOrPdf(string DocumentType, string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            ViewBag.SN = SupplierName;
            //ViewBag.DDLSN = (from po in db.POLists
            //                 select new { po.SUPP_CODE }).Distinct();
            try
            {
                if (FileName != null)
                {
                    string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    string DestinationPath = Server.MapPath("~/data/" + s);
                    if (!Directory.Exists(DestinationPath))
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }
                    Session["FileName"] = Path.GetFileNameWithoutExtension(FileName) + " " + RemoveSpecialChars(SupplierName) + " " + RemoveSpecialChars(MaterialCode) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(FileName);
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        Obj.tblFileMetaDatas.Add(tblApp);
                        tblApp.SupplierName = SupplierName;
                        tblApp.MaterialCode = MaterialCode;
                        tblApp.FileName = Session["FileName"].ToString();
                        tblApp.FileType = Path.GetExtension(FileName);
                        tblApp.DocumentType = DocumentType;
                        tblApp.Location = DestinationPath;
                        tblApp.UploadDate = DateTime.Now;
                        tblApp.Status = false;
                        tblApp.Active = 1;
                        tblApp.UploadedBy = Session["UserName"].ToString();
                        Obj.SaveChanges();
                    }
                    if (Id != -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == Id).FirstOrDefault();
                            tblApps.Active = 3;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = "Error while file uploading.";
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);

        }
        public ActionResult CommonDeleteExlOrPdf(string Id, tblFileMetaData tblApp)
        {
            if (tblApp != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Status = false;
                    tblApps.Active = 2;
                    tblApps.LastModified = DateTime.Now;
                    Obj.SaveChanges();
                }

            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public ActionResult CommonDeleteExlOrPdfActive3(string Id, tblFileMetaData tblApp)
        {
            if (tblApp != null)
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Status = false;
                    tblApps.Active = 3;
                    tblApps.LastModified = DateTime.Now;
                    Obj.SaveChanges();
                }

            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase UploadFile1, FormCollection fomr)
        {
            try
            {
                if (UploadFile1 != null)
                {
                    string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    string path = Path.Combine(Server.MapPath("~/data/" + s), Session["FileName"].ToString());
                    UploadFile1.SaveAs(path);
                    ViewBag.Result = "File uploaded successfully.";
                }

            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = "Error while file uploading.";
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);// View("ApprovedPackingStandardAdmin");
        }
        public ActionResult UploadExlOrPdf(string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            try
            {
                if (FileName != null)
                {
                    string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    string DestinationPath = Server.MapPath("~/data/" + s);
                    if (!Directory.Exists(DestinationPath))
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }
                    Session["FileName"] = Path.GetFileNameWithoutExtension(FileName) + " " + RemoveSpecialChars(SupplierName) + " " + RemoveSpecialChars(MaterialCode) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(FileName);
                    //UploadFile.SaveAs(path + "BSL_Database_structure.docx");
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        Obj.tblFileMetaDatas.Add(tblApp);

                        tblApp.SupplierName = SupplierName;
                        tblApp.MaterialCode = MaterialCode;
                        tblApp.FileName = Session["FileName"].ToString();
                        tblApp.FileType = Path.GetExtension(FileName);
                        tblApp.DocumentType = "PackingStandard";
                        tblApp.Location = DestinationPath;
                        tblApp.UploadDate = DateTime.Now;
                        tblApp.Status = false;
                        tblApp.Active = 1;
                        tblApp.UploadedBy = Session["UserName"].ToString();
                        Obj.SaveChanges();
                    }
                    if (Id != -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == Id).FirstOrDefault();
                            tblApps.Active = 3;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = "Error while file uploading.";
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        // GET: BSLUser
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ECNBSLUser()
        {
            return View();
        }



        public partial class SUPP
        {
            public string SUPP_CODE { get; set; }
            public string SUPP_NAME { get; set; }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSupplier(string Type)
        {
            if (Type == "Supplier")
            {
                List<SUPP> list = db.Database.SqlQuery<SUPP>("exec SUPP").ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                List<SUPP> list = db.Database.SqlQuery<SUPP>("exec PLANT").ToList();
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }



        public JsonResult ECNUpload(string Model, string ECN, string ChangeDescription, string Supplier, string VendorCode, string File, tblECN tblApp)
        {
            bool flag = false;
            try
            {
                string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                string DestinationPath = Server.MapPath("~/data/" + s);
                if (!Directory.Exists(DestinationPath))
                {
                    Directory.CreateDirectory(DestinationPath);
                }
                Session["CheckECNFile"] = File;
                Session["ECNFile"] = Path.GetFileNameWithoutExtension(File) + " " + RemoveSpecialChars(VendorCode) + " " + RemoveSpecialChars(Model) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(File);
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    Obj.tblECNs.Add(tblApp);
                    tblApp.RowType = "P";
                    tblApp.InsDate = DateTime.Now;
                    tblApp.Model = Model;
                    tblApp.ECRECNNo = ECN;
                    tblApp.ChangeDes = ChangeDescription;
                    tblApp.SUPP_NAME = Supplier;
                    tblApp.SUPP_CODE = VendorCode;
                    tblApp.ECNDate = DateTime.Now;
                    tblApp.ECNFile = Session["ECNFile"].ToString();
                    tblApp.ECNFileLocation = DestinationPath;
                    tblApp.SuppECNRes = 0;
                    tblApp.Active = 1;
                    tblApp.CreatedOn = DateTime.Now;
                    tblApp.SuppECNRes = 0;
                    Obj.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully submitted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<JsonResult> UploadECNFile()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                int i = 0;
                string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0 && fileContent.FileName.Equals(Session["CheckECNFile"].ToString()))
                    {
                        HttpPostedFileBase f = files[i];
                        var path = Path.Combine(Server.MapPath("~/data/" + s), Session["ECNFile"].ToString());
                        f.SaveAs(path);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");

        }
        public JsonResult Entry(string Id, string Type, string txt, tblECN tblApp)
        {
            bool flag = false;
            try
            {
                string UserCode = User.Identity.Name;
                if (Type.Equals("UnderPro"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        string p = tblApps.UnderProCom;
                        tblApps.UnderProCom = p + "  " + UserCode + " : " + txt;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("Remarks"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        string r = tblApps.Remarks;
                        tblApps.Remarks = r + "  " + UserCode + " : " + txt;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully submitted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Yes(string Id, string Type, tblECN tblApp)
        {
            bool flag = false;
            try
            {
                if (Type.Equals("SuppECNRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppECNRes = 1;
                        tblApps.SuppECNResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("SuppFeaRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppFeaRes = 1;
                        tblApps.SuppFeaResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("SuppDimRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppDimRes = 1;
                        tblApps.SuppDimResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("BSLGo1"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.BSLGo1 = 1;
                        tblApps.BSLGoResDate1 = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        tblApps.SuppDimRes = 0;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("BSLGoRes2"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.BSLGoRes2 = 1;
                        tblApps.BSLGoResDate2 = DateTime.Now;
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
                return Json("Successfully accepted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult No(string Id, string Type, tblECN tblApp)
        {
            bool flag = false;
            try
            {
                if (Type.Equals("SuppECNRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppECNRes = 3;
                        tblApps.SuppECNResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("SuppFeaRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppFeaRes = 3;
                        tblApps.SuppFeaResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("SuppDimRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppDimRes = 3;
                        tblApps.SuppDimResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }
                }
                if (Type.Equals("BSLGo1"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.BSLGo1 = 3;
                        tblApps.BSLGoResDate1 = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                    }
                    List<int> id = new List<int>();
                    id = db.Database.SqlQuery<Int32>("exec InsBSLGoNo @Id={0},@Type={1}", Convert.ToInt64(Id), "BSLGo1").ToList();
                    flag = true;
                }
                if (Type.Equals("BSLGoRes2"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.BSLGoRes2 = 3;
                        tblApps.BSLGoResDate2 = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                    }
                    List<int> id = new List<int>();
                    id = db.Database.SqlQuery<Int32>("exec InsBSLGoNo @Id={0},@Type={1}", Convert.ToInt64(Id), "BSLGoRes2").ToList();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully rejected.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ECNDelete(string Id, tblECN tblApp)
        {
            bool flag = false;
            try
            {

                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    var Unit_ = Obj.Entry(tblApp);
                    tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                    tblApps.Active = 3;
                    tblApps.LastModified = DateTime.Now;
                    Obj.SaveChanges();
                    flag = true;
                }
            }


            catch (Exception ex)
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed ! Please try again.", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ECN(string Id, string Type, string File, tblECN tblApp)
        {
            bool flag = false;
            try
            {
                string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                string DestinationPath = Server.MapPath("~/data/" + s);
                if (!Directory.Exists(DestinationPath))
                {
                    Directory.CreateDirectory(DestinationPath);
                }
                Session["CheckECNFile"] = File;
                Session["ECNFile"] = Path.GetFileNameWithoutExtension(File) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(File);
                if (Type.Equals("SuppECNRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppECNRes = 2;
                        //tblApp.SuppECNResFile = Session["ECNFile"].ToString();
                        //tblApp.SuppECNResFileLocation = DestinationPath;
                        tblApps.SuppECNResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        tblApps.SuppFeaRes = 0;
                        Obj.SaveChanges();
                    }
                    List<int> id = new List<int>();
                    id = db.Database.SqlQuery<Int32>("exec InsECNFile @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(Id), "SuppECNRes", Session["ECNFile"].ToString(), DestinationPath).ToList();
                    flag = true;
                }
                if (Type.Equals("SuppFeaRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppFeaRes = 2;
                        //tblApp.SuppFeaResFile = Session["ECNFile"].ToString();
                        //tblApp.SuppFeaResFileLocation = DestinationPath;
                        tblApps.SuppFeaResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        tblApps.BSLGo1 = 0;
                        Obj.SaveChanges();
                    }
                    List<int> id = new List<int>();
                    id = db.Database.SqlQuery<Int32>("exec InsECNFile @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(Id), "SuppFeaRes", Session["ECNFile"].ToString(), DestinationPath).ToList();
                    flag = true;
                }
                if (Type.Equals("SuppDimRes"))
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblECN tblApps = Obj.tblECNs.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.SuppDimRes = 2;
                        //tblApp.SuppDimResFile = Session["ECNFile"].ToString();
                        //tblApp.SuppDimResFileLocation = DestinationPath;
                        tblApps.SuppDimResDate = DateTime.Now;
                        tblApps.LastModified = DateTime.Now;
                        tblApps.BSLGoRes2 = 0;
                        Obj.SaveChanges();
                    }
                    List<int> id = new List<int>();
                    id = db.Database.SqlQuery<Int32>("exec InsECNFile @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(Id), "SuppDimRes", Session["ECNFile"].ToString(), DestinationPath).ToList();
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully submitted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }
        }
    }
}