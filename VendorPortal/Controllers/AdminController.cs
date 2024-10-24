using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Admin,Administrator,Buyer,BSLUser,Quality,R&D,Visitor")]

    public class AdminController : Controller
    {
        BasicPaging objBasicPaging = null;
        VendorPortalEntities db = new VendorPortalEntities();
        public ActionResult ApprovedPackingStandardAdmin()
        {
            //dynamic model = new ExpandoObject();
            //model.data = (from pd in db.POLists
            //                join od in db.tblApprovedPackingStandards on pd.SUPP_CODE equals od.SupplierName
            //                 into t
            //                from rt in t.DefaultIfEmpty()
            //                orderby pd.SUPP_CODE
            //                select new
            //                {
            //                    pd.SUPP_CODE,
            //                    pd.MaterialCode
            //                    //rt.FileName,
            //                    //UploadDate = (DateTime?)rt.UploadDate,
            //                    //Status = (bool?)rt.Status,
            //                    //id = (int?)rt.Id,

            //                }).ToList();


            //List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingStandard @CurrentPage={0}", CurrentPage).ToList();

            ////List<ApprovedPacking> list =




            //objBasicPaging = new BasicPaging()
            //{
            //    TotalItem = 4910,
            //    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
            //    CurrentPage = CurrentPage
            //};
            //if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
            //{
            //    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
            //}
            //else
            //    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
            //ViewBag.paging = objBasicPaging;
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult SourcingDeclarationAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();

            return View();
        }

        public ActionResult QualitySysCerAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult FortnightAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult KITAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult ECNAdmin()
        {
            return View();
        }
        public ActionResult PreDispatchAdmin()
        {
            ViewBag.DDLSN =  (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult FourMAdmin()
        {
            ViewBag.DDLSN =  (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult SMIRAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        public ActionResult QualityManualAdmin()
        {
            ViewBag.DDLSN =  (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }

        public ActionResult Index()
        {
            // obj.getlist = db.proc_GEtsuplier().ToList();

            return View("Index");
        }
        #region Sourcing Declaration
       // [AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult SuplierSourcing(string suppcode)
        //{
        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSourcing @SupplierName={0}", suppcode).ToList();
        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
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
        
        public ActionResult KIT()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE }).Distinct();
            //List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierKIT @SupplierName={0}", suppcode).ToList();
            //var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Pre(string suppcode)
        {
            ViewBag.DDLSN =  (from po in db.SUPPLIER_MASTER
                                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();


            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FourM(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFourM @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult AllFourM()
        //{
        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierFourM ").ToList();
        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SMIR(string suppcode)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSMIR @SupplierName={0}", suppcode).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult AllSMIR()
        //{
        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierSMIR").ToList();
        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult QM(string suppcode)
        //{
        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierQM @SupplierName={0}", suppcode).ToList();
        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult AllQM()
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierQM").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult VendorInfoAdmin()
        {

            return View();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSupplierList()
        {
            List<tblVendorInfo> list = db.Database.SqlQuery<tblVendorInfo>("exec GetAllSupplierAdmin").ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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


        public ActionResult CommonDeleteExlOrPdf(string Id, tblFileMetaData tblApp)
        {
            bool flag = false;
            try
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
                        flag = true;
                    }

                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }


        }


        #endregion
        #region Suplier Packing Standard  


 
        public JsonResult UploadExlOrPdf(string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            bool flag = false;
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
                        if (User.IsInRole("BSLUser"))
                        {
                            tblApp.Status = false;
                        }
                        else
                        {
                            tblApp.Status = true;
                        }
                        tblApp.Active = 1;
                        tblApp.UploadedBy = Supp_code;
                        Obj.SaveChanges();
                        flag = true;
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

        public JsonResult DeleteExlOrPdf(int Id)
        {
            bool flag = false;
            try
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    //  var Unit_ = Obj.Entry(tblApp);
                    tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == Id).FirstOrDefault();
                    tblApps.Status = false;
                    tblApps.Active = 2;
                    tblApps.LastModified = DateTime.Now;
                    Obj.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }
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



        //public PartialViewResult ApprovedPacking_Grid(int CurrentPage = 1)
        //{
        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingStandard @CurrentPage={0}", CurrentPage).ToList();


        //    objBasicPaging = new BasicPaging()
        //    {
        //        TotalItem = 4910,
        //        RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
        //        CurrentPage = CurrentPage
        //    };
        //    if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
        //    {
        //        objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
        //    }
        //    else
        //        objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
        //    ViewBag.paging = objBasicPaging;


        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    //return jsonResult;

        //    return PartialView("_ApprovedPacking_Grid", list);
        //}
        public ActionResult SupplierCapacityDecAdmin()
        {
            ViewBag.DDLSN = (from po in db.SUPPLIER_MASTER
                             select new { po.SUPP_CODE, SUPP_NAME = po.SUPP_NAME + " - " + po.SUPP_CODE }).Distinct();
            return View();
        }
        

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetMaterial(string Suppcode)
        {
            return Json(db.POLists.Where<VendorPortal.Models.POList>(ob => ob.SUPP_CODE == Suppcode).OrderBy(ob => ob.SUPP_CODE).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Anoop Code Merge On 17 Oct 2018


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
                        tblApps.UnderProCom = p + "  "  + UserCode +" : "+txt;
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


        public partial class SUPP
        {
            public string SUPP_CODE { get; set; }
            public string SUPP_NAME { get; set; }
        }




        #endregion

        #region By Naresh


        public PartialViewResult SupplierCapacityDec_Grid( int CurrentPage = 1 , string SupplierName = "")
        {
            if (SupplierName == null)
            {
                SupplierName = "";
            }
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierCapacity @SupplierName={0}, @CurrentPage={1}", SupplierName, CurrentPage).ToList();

            int total = 0;

            if (SupplierName.ToUpper().Trim() == "")
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.DocumentType.Trim().ToUpper() == "CAPACITY"
                         select new
                         {
                             i.Id
                         }).ToList().Count();

            }
            else
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.SupplierName.ToUpper().Trim() == SupplierName.ToUpper().Trim() && i.DocumentType.Trim().ToUpper() == "CAPACITY"
                         select new
                         {
                             i
                         }).ToList().Count();
            }






            objBasicPaging = new BasicPaging()
            {
                TotalItem = total,
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

            return PartialView("_SupplierCapacityDec_Grid", list);
        }



        public ActionResult CommonUploadExlOrPdf(string DocumentType, string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            ViewBag.SN = SupplierName;
            bool flag = false;
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
                    Session["FileName"] = Path.GetFileNameWithoutExtension(FileName) + " " + RemoveSpecialChars(Supp_code) + " " + RemoveSpecialChars(MaterialCode) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(FileName);
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
                        if (User.IsInRole("BSLUser"))
                        {
                            tblApp.Status = false;
                        }
                        else
                        {
                            tblApp.Status = true;
                        }
                        tblApp.Active = 1;
                        tblApp.UploadedBy = Supp_code;
                        Obj.SaveChanges();
                        flag = true;

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
        public JsonResult UploadFiles(HttpPostedFileBase UploadFile1)
        {
            bool flag = false;
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    string path = Path.Combine(Server.MapPath("~/data/" + s), Session["FileName"].ToString());
                    file.SaveAs(path);
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

        public ActionResult CommonDeleteExlOrPdfActive3(string Id, tblFileMetaData tblApp)
        {
            bool flag = false;
            try
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
                        flag = true;
                    }

                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult Approve(string Id, tblFileMetaData tblApp)
        {
            bool flag = false;
            try
            {
                if (tblApp != null)
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblFileMetaData tblApps = Obj.tblFileMetaDatas.Where(x => x.Id == tblApp.Id).FirstOrDefault();
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
        public PartialViewResult FortnightList( int CurrentPage = 1,string SupplierName="All")
        {
          
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFornight @SupplierName={0}, @CurrentPage={1}", SupplierName, CurrentPage).ToList();

            int total = 0;

                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.DocumentType.Trim().ToUpper() == "Fortnight" && i.SupplierName.Trim().ToUpper() == "All"
                         select new
                         {
                             i.Id
                         }).ToList().Count();

             
            objBasicPaging = new BasicPaging()
            {
                TotalItem = total,
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

            return PartialView("_Fortnight_Grid", list);
        }

        public PartialViewResult KITDetailList(int CurrentPage = 1,string SupplierName="All")
        {
          
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierKIT @SupplierName={0}, @CurrentPage={1}", SupplierName, CurrentPage).ToList();

            int total = 0;

      
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 
                         && i.DocumentType.Trim().ToUpper() == "KIT" && i.SupplierName.Trim().ToUpper() == "All"
                         select new
                         {
                             i.Id
                         }).ToList().Count();

           

            objBasicPaging = new BasicPaging()
            {
                TotalItem = total,
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

            return PartialView("_KIT_Grid", list);
        }

        public PartialViewResult PreDispatchList(int CurrentPage = 1 , string SupplierName = "")
        {
            if (SupplierName == null)
            {
                SupplierName = "";
            }
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierPreDispatch @SupplierName={0}, @CurrentPage={1}", SupplierName, CurrentPage).ToList();

            int total = 0;

            if (SupplierName.ToUpper().Trim() == "")
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1
                         && i.DocumentType.Trim().ToUpper() == "PreDispatch"
                         select new
                         {
                             i.Id
                         }).ToList().Count();

            }
            else
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.SupplierName.ToUpper().Trim() == SupplierName.ToUpper().Trim()
                         && i.DocumentType.Trim().ToUpper() == "PreDispatch"
                         select new
                         {
                             i
                         }).ToList().Count();
            }

            objBasicPaging = new BasicPaging()
            {
                TotalItem = total,
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

            return PartialView("_PreDispatch_Grid", list);
        }

        public PartialViewResult QualitySysList(string SupplierName, int CurrentPage = 1)
        {
            if (SupplierName == null)
            {
                SupplierName = "";
            }
            List<tblCertificate> list = db.Database.SqlQuery<tblCertificate>("exec GetSuplierQualitycer @SupplierName={0}, @CurrentPage={1}", SupplierName, CurrentPage).ToList();

            int total = 0;

            if (SupplierName.ToUpper().Trim() == "")
            {
                total = (from i in db.tblCertificates
                         where i.Active == 1
                         select new
                         {
                             i.Id
                         }).ToList().Count();

            }
            else
            {
                total = (from i in db.tblCertificates
                         where i.Active == 1 && i.SupplierName.ToUpper().Trim() == SupplierName.ToUpper().Trim()
                         select new
                         {
                             i
                         }).ToList().Count();
            }

            objBasicPaging = new BasicPaging()
            {
                TotalItem = total,
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

            return PartialView("_QualitySysCre_Grid", list);
        }


        #endregion

        #region Anoop Paging
        public PartialViewResult AllSMIR(int CurrentPage = 1, string SUPP_CODE = "")
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierSMIR @CurrentPage={0}, @SupplierName ={1}", CurrentPage, SUPP_CODE).ToList();

            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetAllSuplierSMIR @SupplierName={0}", SUPP_CODE).ToList();

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

            return PartialView("_SMIRAdmin", list);

        }
        public PartialViewResult AllFourM(int CurrentPage = 1, string SUPP_CODE = "")
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetAllSuplierFourM @CurrentPage={0}, @SupplierName ={1}", CurrentPage, SUPP_CODE).ToList();

            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetAllSuplierFourM @SupplierName={0}", SUPP_CODE).ToList();

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

            return PartialView("_FourMAdmin", list);
        }
        public PartialViewResult ApprovedPacking_Grid(int CurrentPage = 1, string SUPP_CODE = "")
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingStandard @CurrentPage={0}, @SupplierName ={1}", CurrentPage, SUPP_CODE).ToList();


            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierForUploadPackingStandard @SupplierName={0}", SUPP_CODE).ToList();



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

            return PartialView("_ApprovedPacking_Grid", list);
        }
        //public PartialViewResult SuplierSourcing(int CurrentPage = 1, string SUPP_CODE = "")
        //{


        //    List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSourcing  @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();

        //    var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierSourcing @SupplierName={0}", SUPP_CODE).ToList();



        //    objBasicPaging = new BasicPaging()
        //    {
        //        TotalItem = TotalCount.FirstOrDefault(),
        //        RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
        //        CurrentPage = CurrentPage
        //    };
        //    if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
        //    {
        //        objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
        //    }
        //    else
        //        objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
        //    ViewBag.paging = objBasicPaging;


        //    var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    //return jsonResult;

        //    return PartialView("_SourcingDeclarationAdmin", list);

        //}
        public ActionResult NotificationsSupp()
        {
            return View();
        }
        public ActionResult CertificatefileUpload(string DocumentType, string SupplierName, string FileName, tblCertificate tblApp)
        {
            string Supp_code = User.Identity.Name;
            bool flag = false;
            try
            {
                string s = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                string DestinationPath = Server.MapPath("~/data/" + s);
                if (!Directory.Exists(DestinationPath))
                {
                    Directory.CreateDirectory(DestinationPath);
                }
                Session["FileName"] = Path.GetFileNameWithoutExtension(FileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(FileName);


                //var N = (from i in db.tblCertificates
                //         where i.Active == 1 && i.SupplierName == Session["UserName"].ToString()
                //         select new
                //         {
                //             i.Id
                //         }).Single();

                //Int64 Id = N.Id;
                Int64 p = -100;
                List<int> Id = new List<int>();
                Id = db.Database.SqlQuery<Int32>("exec getid @SUPP_CODE={0}", SupplierName).ToList();
                if (Id.Count > 0)
                {
                    p = Id[0];
                }
                if (DocumentType.Equals("IATFTS"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.IATFTS = Session["FileName"].ToString();
                            tblApp.IATFTSLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.IATFTS = Session["FileName"].ToString();
                            //tblApp.IATFTSLOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "IATFTS", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;

                    }
                }
                else if (DocumentType.Equals("ISO"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.ISO = Session["FileName"].ToString();
                            tblApp.ISOLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.ISO = Session["FileName"].ToString();
                            //tblApp.ISOLOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "ISO ", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;


                    }
                }
                else if (DocumentType.Equals("OHSAS"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.OHSAS = Session["FileName"].ToString();
                            tblApp.OHSASLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.OHSAS = Session["FileName"].ToString();
                            //tblApp.OHSASLOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "OHSAS", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;

                    }
                }
                else if (DocumentType.Equals("Environment"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.Environment = Session["FileName"].ToString();
                            tblApp.EnvironmentLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.Environment = Session["FileName"].ToString();
                            //tblApp.EnvironmentLOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "Environment", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;


                    }
                }
                else if (DocumentType.Equals("Other1"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.Other1 = Session["FileName"].ToString();
                            tblApp.Other1LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.Other1 = Session["FileName"].ToString();
                            //tblApp.Other1LOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "Other1", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;


                    }
                }
                else if (DocumentType.Equals("Other2"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.Other2 = Session["FileName"].ToString();
                            tblApp.Other2LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.Other2 = Session["FileName"].ToString();
                            //tblApp.Other2LOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "Other2", Session["FileName"].ToString(), DestinationPath).ToList();
                        flag = true;


                    }
                }
                else if (DocumentType.Equals("Other3"))
                {
                    if (p == -100)
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            Obj.tblCertificates.Add(tblApp);
                            tblApp.SupplierName = SupplierName;
                            tblApp.Other3 = Session["FileName"].ToString();
                            tblApp.Other3LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            if (User.IsInRole("BSLUser"))
                            {
                                tblApp.Status = false;
                            }
                            else
                            {
                                tblApp.Status = true;
                            }
                            tblApp.Active = 1;
                            tblApp.UploadedBy = Supp_code;
                            Obj.SaveChanges();
                            flag = true;

                        }
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == p).FirstOrDefault();
                            //tblApp.Other3 = Session["FileName"].ToString();
                            //tblApp.Other3LOC = DestinationPath;
                            tblApps.LastModified = DateTime.Now;
                            Obj.SaveChanges();

                        }
                        List<int> id = new List<int>();
                        id = db.Database.SqlQuery<Int32>("exec InsCertificate1File @Id={0},@Type={1},@File={2},@Loc={3}", Convert.ToInt64(p), "Other3", Session["FileName"].ToString(), DestinationPath).ToList();
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





     

        public JsonResult FileApprove(string Id, tblCertificate tblApp)
        {
            bool flag = false;
            try
            {
                if (tblApp != null)
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == tblApp.Id).FirstOrDefault();
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

        public ActionResult Filedelete(string Id, tblCertificate tblApp)

        {
            bool flag = false;
            try
            {
                if (tblApp != null)
                {
                    using (VendorPortalEntities Obj = new VendorPortalEntities())
                    {
                        var Unit_ = Obj.Entry(tblApp);
                        tblCertificate tblApps = Obj.tblCertificates.Where(x => x.Id == tblApp.Id).FirstOrDefault();
                        tblApps.Status = false;
                        tblApps.Active = 3;
                        tblApps.LastModified = DateTime.Now;
                        Obj.SaveChanges();
                        flag = true;
                    }

                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }
            if (flag)
            {
                return Json("Successfully deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed to data delete.", JsonRequestBehavior.AllowGet);
            }


        }

        public PartialViewResult SuplierSourcing(int CurrentPage = 1, string SUPP_CODE = "")
        {


            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSourcing  @CurrentPage={0},@SupplierName={1}", CurrentPage, SUPP_CODE).ToList();

            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierSourcing @SupplierName={0}", SUPP_CODE).ToList();



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

            return PartialView("_SourcingDeclarationAdmin", list);

        }
        public PartialViewResult QM(int CurrentPage = 1, string SUPP_CODE = "")
        {

                SUPP_CODE = "All";

            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierQM  @CurrentPage={0},@SupplierName={1}", CurrentPage, "All").ToList();

            int _totalCount = 0;

            if (SUPP_CODE == "")
            {
                _totalCount = (from i in db.tblFileMetaDatas
                               where i.Active == 1
                               && i.DocumentType == "QM"
                               select new { i }
                             ).ToList().Count();
            }
            else
            {
                _totalCount = (from i in db.tblFileMetaDatas
                               where i.Active == 1
                               && i.SupplierName == SUPP_CODE
                               && i.DocumentType == "QM"
                               select new { i }
                             ).ToList().Count();
            }

            objBasicPaging = new BasicPaging()
            {
                TotalItem = _totalCount,
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

            return PartialView("_QualityManualAdmin", list);
        }
        #endregion
    }
}