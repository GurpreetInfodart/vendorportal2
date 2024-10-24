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
using System.Data;
using System.Reflection;
using System.Text;
using VendorPortal.Common;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Supplier,Admin,Buyer,Administrator,BSLUser,Quality,R&D,Visitor")]
    public class SupplierController : Controller
    {
        BasicPaging objBasicPaging = null;
        VendorPortalEntities db = new VendorPortalEntities();
        public ActionResult PackingStandardSupp()
        {
            return View();
        }
        public ActionResult SourcingSupp()
        {
            ViewBag.DDLSN = (from po in db.POLists
                             select new { po.SUPP_CODE }).Distinct();

            return View();
        }
        public ActionResult CapacitySupp()
        {
            return View();
        }
        public ActionResult QualitySupp()
        {
            return View();
        }
        public ActionResult FortnightSupp()
        {
            return View();
        }
        public ActionResult KITSupp()
        {
            return View();
        }
        public ActionResult PreDispatchSupp()
        {
            return View();
        }
        public ActionResult FourMSupp()
        {
            return View();
        }
        public ActionResult SMIRSupp()
        {
            return View();
        }
        public ActionResult QMSupp()
        {
            return View();
        }
        public ActionResult NotificationsSupp()
        {
            return View();
        }
        public ActionResult CertificatefileUpload(string DocumentType, string FileName, tblCertificate tblApp)
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


                Int64 p = -100;
                List<int> Id = new List<int>();
                Id = db.Database.SqlQuery<Int32>("exec getid @SUPP_CODE={0}", (Supp_code)).ToList();
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.IATFTS = Session["FileName"].ToString();
                            tblApp.IATFTSLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.ISO = Session["FileName"].ToString();
                            tblApp.ISOLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.OHSAS = Session["FileName"].ToString();
                            tblApp.OHSASLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.Environment = Session["FileName"].ToString();
                            tblApp.EnvironmentLOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.Other1 = Session["FileName"].ToString();
                            tblApp.Other1LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.Other2 = Session["FileName"].ToString();
                            tblApp.Other2LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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
                            tblApp.SupplierName = Supp_code;
                            tblApp.Other3 = Session["FileName"].ToString();
                            tblApp.Other3LOC = DestinationPath;
                            tblApp.UploadDate = DateTime.Now;
                            tblApp.Status = false;
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

        public PartialViewResult QualitySysList(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<tblCertificate> list = db.Database.SqlQuery<tblCertificate>("exec GetSuplierQualitycer @SupplierName={0}, @CurrentPage={1}", Supp_code, CurrentPage).ToList();

            int total = 0;


            total = (from i in db.tblCertificates
                     where i.Active == 1 && i.SupplierName == Supp_code
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

            return PartialView("_QualitySupp", list);
        }
        public PartialViewResult Fornight(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFornightTOP6 @CurrentPage={0},@SupplierName={1}", CurrentPage, "All").ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierFornightTOP6 @SupplierName={0}", Supp_code).ToList();
            //var TotalCount = 6;
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

            return PartialView("_FortnightSupp", list);
        }
        public PartialViewResult KIT(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierKITTOP3 @CurrentPage={0},@SupplierName={1}", CurrentPage, "All").ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierKITTOP3 @SupplierName={0}", Supp_code).ToList();

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

            return PartialView("_KITSupp", list);
        }
        public PartialViewResult Pre(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierPreDispatch @CurrentPage={0}, @SupplierName={1}", CurrentPage, Supp_code).ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierPreDispatch @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_PreDispatchSupp", list);
        }
        public PartialViewResult FourM(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierFourMTop3 @CurrentPage={0}, @SupplierName={1}", CurrentPage, Supp_code).ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierFourMTop3 @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_FourMSupp", list);
        }
        public PartialViewResult SMIR(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSMIRTop3  @CurrentPage={0}, @SupplierName={1}", CurrentPage, Supp_code).ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierSMIRTop3 @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_SMIRSupp", list);
        }
        public PartialViewResult QM(int CurrentPage = 1)
        {
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierQM @CurrentPage={0},@SupplierName={1}", CurrentPage, "All").ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierQM @SupplierName={0}", "All").ToList();
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

            return PartialView("_QMSupp", list);
        }
        public void csv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
                            , "SUPPLIER_CODE", "MAT_CODE", "QUANTITY", "DEL_DATE", "D1", "D2", "D3", "D4", "D5\n");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("{0},", dr["SUPPLIER_CODE"]);
                sb.AppendFormat("{0},", dr["MAT_CODE"]);
                sb.AppendFormat("{0},", dr["QUANTITY"]);
                sb.AppendFormat("{0},", dr["DEL_DATE"]);
                sb.AppendFormat("{0}", dr["D1"]);
                sb.AppendFormat("{0},", dr["D2"]);
                sb.AppendFormat("{0},", dr["D3"]);
                sb.AppendFormat("{0},", dr["D4"]);
                sb.AppendFormat("{0},", dr["D5"]);
                sb.AppendFormat("\n");
            }

            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=Employee.CSV ");
            response.ContentType = "text/plain";
            response.Write(sb.ToString());
            response.End();
        }
        public FileContentResult CSVread(System.Data.DataTable table)
        {

            string csv = string.Empty;
            foreach (DataColumn column in table.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }
            csv = csv.Remove(csv.Length - 1, 1);
            //Add new line.
            csv += "\r\n";
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv = csv.Remove(csv.Length - 1, 1);
                //Add new line.
                csv += "\r\n";
            }
            //Download the CSV file.


            //string path = System.Web.Configuration.WebConfigurationManager.AppSettings["myFilePath"].ToString();


            Response.Clear();
            Response.Buffer = true;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Area.csv",
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv");
            // Response.AddHeader("content-disposition", "attachment;filename=StockDeclaration.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            //Response.Output.Write(csv);

            // // Adding Header Or Column in the First Row of CSV
            //string csvPath = path + "StockDeclaration_" + Session["User_ID"].ToString().Trim() + ".csv";
            // Save or upload CSV format File (.csv)
            //System.IO.File.WriteAllText(csvPath, csv.ToString());
            //System.IO.File.AppendAllText(csvPath, csv.ToString());


        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
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
        public partial class Sch
        {
            public string SUPP_CODE { get; set; }
            public string MAT_CODE { get; set; }
            public string MaterialDescription { get; set; }
            public Nullable<decimal> Qty { get; set; }
            public DateTime? DEL_DATE { get; set; }
            public string D1 { get; set; }
            public string D2 { get; set; }
            public string D3 { get; set; }
            public string D4 { get; set; }
            public string D5 { get; set; }

        }
        public PartialViewResult SuplierSourcing(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierSourcing  @CurrentPage={0}, @SupplierName={1}", CurrentPage, Supp_code).ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierSourcing @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_SourcingSupp", list);
        }
        public PartialViewResult Quality(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierQuality @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();
            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierQuality @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_QualitySupp", list);
        }

        public PartialViewResult Capacity(int CurrentPage = 1)
        {
            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierCapacity  @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();

            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierCapacity @SupplierName={0}", Supp_code).ToList();
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

            return PartialView("_CapacitySupp", list);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult DownloadFourM()
        {
            string loc = Server.MapPath("~/Template");
            string s = loc + "\\" + "4M_Change_Mgmnt_FORMAT-UPDATED.xls";
            s = GetVirtualPath(s);
            byte[] fileBytes = System.IO.File.ReadAllBytes(@s);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = "4M_Change_Mgmnt_FORMAT-UPDATED.xls";
            return response;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult DownloadSMIR()
        {
            string loc = Server.MapPath("~/Template");
            string s = loc + "\\" + "SMIR_FORMAT.xls";
            s = GetVirtualPath(s);
            byte[] fileBytes = System.IO.File.ReadAllBytes(@s);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = "SMIR_FORMAT.xls";
            return response;
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
        [AcceptVerbs(HttpVerbs.Get)]
        public void DownloadT()
        {
            string Supp_code = User.Identity.Name;
            DataTable dt = new DataTable();
            List<Sch> list = db.Database.SqlQuery<Sch>("exec GetPreDispatchTemplate @SupplierName={0}", Supp_code).ToList();
            dt = ToDataTable(list);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
                            , "SUPPLIER_CODE", "MAT_CODE", "QUANTITY", "DEL_DATE", "D1", "D2", "D3", "D4", "D5\n");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("{0},", dr["SUPPLIER_CODE"]);
                sb.AppendFormat("{0},", dr["MAT_CODE"]);
                sb.AppendFormat("{0},", dr["QUANTITY"]);
                sb.AppendFormat("{0},", dr["DEL_DATE"]);
                sb.AppendFormat("{0}", dr["D1"]);
                sb.AppendFormat("{0},", dr["D2"]);
                sb.AppendFormat("{0},", dr["D3"]);
                sb.AppendFormat("{0},", dr["D4"]);
                sb.AppendFormat("{0},", dr["D5"]);
                sb.AppendFormat("\n");
            }

            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=PreDispatch.CSV");
            response.ContentType = "text/plain";
            response.Write(sb.ToString());
            response.End();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public void DownloadTPre(String Date)
        {
            string Supp_code = User.Identity.Name;
            DataTable dt = new DataTable();
            List<Sch> list = db.Database.SqlQuery<Sch>("exec GetPreDispatchTemplate @SupplierName={0},@Date={1}", Supp_code, Date).ToList();
            dt = ToDataTable(list);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}"
                            , "SUPPLIER_CODE", "MAT_CODE", "MaterialDescription", "QUANTITY", "DEL_DATE", "D1", "D2", "D3", "D4", "D5\n");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("{0},", dr["SUPP_CODE"]);
                sb.AppendFormat("{0},", dr["MAT_CODE"]);
                sb.AppendFormat("{0},", dr["MaterialDescription"]);
                sb.AppendFormat("{0},", dr["Qty"]);
                sb.AppendFormat("{0},", DateTime.Parse(dr["DEL_DATE"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                sb.AppendFormat("{0}", dr["D1"]);
                sb.AppendFormat("{0},", dr["D2"]);
                sb.AppendFormat("{0},", dr["D3"]);
                sb.AppendFormat("{0},", dr["D4"]);
                sb.AppendFormat("{0},", dr["D5"]);
                sb.AppendFormat("\n");
            }

            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=PreDispatch.CSV");
            response.ContentType = "text/plain";
            response.Write(sb.ToString());
            response.End();
        }
        private string GetVirtualPath(string physicalPath)
        {
            //   string rootpath = Server.MapPath("~/");
            // physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");
            return physicalPath;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult ApprovedPackingList(int CurrentPage = 1)
        {

            string Supp_code = User.Identity.Name;
            List<ApprovedPacking> list = db.Database.SqlQuery<ApprovedPacking>("exec GetSuplierForUploadPackingBySupplier  @CurrentPage={0},@SupplierName={1}", CurrentPage, Supp_code).ToList();

            var TotalCount = db.Database.SqlQuery<int>("exec Total_Count_GetSuplierForUploadPackingBySupplier @SupplierName={0}", Supp_code).ToList();



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

            return PartialView("_PackingStandardSupp", list);
        }
        public ActionResult SMIRUploadExlOrPdf(string DocumentType, string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            ViewBag.SN = SupplierName;
            bool flag = false;
            int total = 0;
            try
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.SupplierName == Supp_code && i.DocumentType.Trim().ToUpper() == "SMIR" && i.UploadDate.Month == DateTime.Now.Month
                         select new
                         {
                             i.Id
                         }).ToList().Count();
                if (total == 0)
                {
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
                                tblApp.SupplierName = Supp_code;
                                tblApp.MaterialCode = MaterialCode;
                                tblApp.FileName = Session["FileName"].ToString();
                                tblApp.FileType = Path.GetExtension(FileName);
                                tblApp.DocumentType = DocumentType;
                                tblApp.Location = DestinationPath;
                                tblApp.UploadDate = DateTime.Now;
                                tblApp.Status = false;
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
                else
                {
                    return Json("File already uploaded for this month.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult FourMUploadExlOrPdf(string DocumentType, string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            ViewBag.SN = SupplierName;
            bool flag = false;
            int total = 0;
            try
            {
                total = (from i in db.tblFileMetaDatas
                         where i.Active == 1 && i.SupplierName == Supp_code && i.DocumentType.Trim().ToUpper() == "FourM" && i.UploadDate.Month == DateTime.Now.Month
                         select new
                         {
                             i.Id
                         }).ToList().Count();
                if (total == 0)
                {
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
                                tblApp.SupplierName = Supp_code;
                                tblApp.MaterialCode = MaterialCode;
                                tblApp.FileName = Session["FileName"].ToString();
                                tblApp.FileType = Path.GetExtension(FileName);
                                tblApp.DocumentType = DocumentType;
                                tblApp.Location = DestinationPath;
                                tblApp.UploadDate = DateTime.Now;
                                tblApp.Status = false;
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
                else
                {
                    return Json("File already uploaded for this month.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Failed to data submit.", JsonRequestBehavior.AllowGet);
            }

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
                        tblApp.SupplierName = Supp_code;
                        tblApp.MaterialCode = MaterialCode;
                        tblApp.FileName = Session["FileName"].ToString();
                        tblApp.FileType = Path.GetExtension(FileName);
                        tblApp.DocumentType = DocumentType;
                        tblApp.Location = DestinationPath;
                        tblApp.UploadDate = DateTime.Now;
                        tblApp.Status = false;
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
        public ActionResult UploadFiles(HttpPostedFileBase UploadFile1)
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
        public ActionResult UploadExlOrPdf(string SupplierName, string MaterialCode, string FileName, Int64 Id, tblFileMetaData tblApp)
        {
            string Supp_code = User.Identity.Name;
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
                        tblApp.Status = false;
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
        public ActionResult DeleteExlOrPdf(string Id, tblFileMetaData tblApp)
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
        [AcceptVerbs(HttpVerbs.Get)]
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VendorInfoSupp()
        {
            return View();
        }





        public ActionResult InsVendorInfo(string Street, string City, string PostCode, string District, string Country, string VATRegNo, string Tin, string Telephone1, string Telephone2, string Telephone3, string FaxNo, string Emailid, string BankName, string BankAddress, string BankAccountNo
                    , string BankSwiftCode, string BankCurrency, string PaymentTerm, string PaymentMethod, string OrderCurrency, string Incoterms, string SchemaGroupVendor, string ContactPerson, string Telephone, string GRbased, string ERSVendor, string OtherInformation1, string OtherInformation2,
                    string OtherInformation3, string OtherInformation4, string RequestedName, string RequestedDepartment, string RequestedDate, string ApprovedName, string ApprovedDepartment, string ApprovedDate, tblVendorInfo tblApp)
        {
            string Supp_code = User.Identity.Name;
            ViewBag.Result = "";
            ViewBag.ErrorMsg = "";
            try
            {
                using (VendorPortalEntities Obj = new VendorPortalEntities())
                {
                    Obj.tblVendorInfoes.Add(tblApp);
                    tblApp.SUPP_CODE = Supp_code;
                    tblApp.SUPP_NAME = Supp_code;
                    tblApp.Street = Street;
                    tblApp.City = City;
                    tblApp.PostCode = PostCode;
                    tblApp.District = District;
                    tblApp.Country = Country;
                    tblApp.VATRegNo = VATRegNo;
                    tblApp.Tin = Tin;
                    tblApp.Telephone1 = Telephone1;
                    tblApp.Telephone2 = Telephone2;
                    tblApp.Telephone3 = Telephone3;
                    tblApp.FaxNo = FaxNo;
                    tblApp.Emailid = Emailid;
                    tblApp.BankName = BankName;
                    tblApp.BankAddress = BankAddress;
                    tblApp.BankAccountNo = BankAccountNo;
                    tblApp.BankSwiftCode = BankSwiftCode;
                    tblApp.BankCurrency = BankCurrency;
                    tblApp.PaymentTerm = PaymentTerm;
                    tblApp.PaymentMethod = PaymentMethod;
                    tblApp.OrderCurrency = OrderCurrency;
                    tblApp.Incoterms = Incoterms;
                    tblApp.SchemaGroupVendor = SchemaGroupVendor;
                    tblApp.ContactPerson = ContactPerson;
                    tblApp.Telephone = Telephone;
                    tblApp.GRbased = GRbased;
                    tblApp.ERSVendor = ERSVendor;
                    tblApp.OtherInformation1 = OtherInformation1;
                    tblApp.OtherInformation2 = OtherInformation2;
                    tblApp.OtherInformation3 = OtherInformation3;
                    tblApp.OtherInformation4 = OtherInformation4;
                    tblApp.RequestedName = RequestedName;
                    tblApp.RequestedDepartment = RequestedDepartment;
                    //tblApp.RequestedDate = RequestedDate;
                    tblApp.ApprovedName = ApprovedName;
                    tblApp.ApprovedDepartment = ApprovedDepartment;
                    //tblApp.ApprovedDate = ApprovedDate;

                    tblApp.Active = 1;
                    tblApp.CreatedBy = Supp_code;
                    tblApp.CreatedOn = DateTime.Now;
                    Obj.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = "Error while file uploading.";
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }





        public JsonResult GetListSupplier()
        {
            string Supp_code = User.Identity.Name;
            List<tblVendorInfo> list = db.Database.SqlQuery<tblVendorInfo>("exec GetSupplier @SUPP_CODE={0}", Supp_code).ToList();
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public ActionResult SaveVendorQue1()
        {
            return View("VendorQueSupp");
        }
        [HttpGet]
        public ActionResult SaveVendorQue2()
        {
            return View("VendorQueSupp");
        }
        [HttpGet]
        public ActionResult SaveVendorQue3()
        {
            return View("VendorQueSupp");
        }
        [HttpGet]
        public ActionResult SaveVendorQue4()
        {
            return View("VendorQueSupp");
        }
        [HttpPost]
        public ActionResult SaveVendorQue1(tblVendorQue objVendorQue, HttpPostedFileBase planfile)
        {
            try
            {
                string fileName = "";
                string filePath = "NA";
                string f = "NA";
                bool flag = false;
                string Supp_code = User.Identity.Name;
                string SUPP_NAME = Supp_code;
                if (planfile != null)
                {
                    fileName = System.IO.Path.GetFileName(planfile.FileName);
                    fileName = Path.GetFileNameWithoutExtension(fileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(fileName);
                    filePath = Server.MapPath("~/data/Financial_File");
                    f = Server.MapPath("~/data/Financial_File/" + fileName);
                }
                List<Int64> Id = new List<Int64>();
                Id = db.Database.SqlQuery<Int64>("exec CheckApprovedId @SUPP_CODE={0}", Supp_code).ToList();
                if (Id.Count > 0)
                {
                    Messages msg = new Messages();
                    msg.Message_Id = 2;
                    msg.Message = "Approved data already exist.";
                    ViewBag.msg = msg;
                }
                else
                {
                    Int64 p = -100;
                    List<Int64> IdP = new List<Int64>();
                    IdP = db.Database.SqlQuery<Int64>("exec CheckPendingId @SUPP_CODE={0}", Supp_code).ToList();
                    if (IdP.Count > 0)
                    {
                        p = IdP[0];
                    }
                    if (p == -100)
                    {
                        List<string> NAME = new List<string>();
                        NAME = db.Database.SqlQuery<string>("exec GetSupplierName @SUPP_CODE={0}", Supp_code).ToList();
                        if (NAME.Count > 0)
                        {
                            SUPP_NAME = NAME[0];
                        }
                        objVendorQue.FormType = "CompanyProfile";
                        objVendorQue.SUPP_CODE = Supp_code;
                        objVendorQue.SUPP_NAME = SUPP_NAME;
                        objVendorQue.PlanFileName = fileName;
                        objVendorQue.PlanFileNameLoc = filePath;
                        objVendorQue.Active = 1;
                        objVendorQue.Status = false;
                        db.tblVendorQues.Add(objVendorQue);
                        db.SaveChanges();
                        Messages msg = new Messages();
                        msg.Message_Id = 1;
                        msg.Message = "Successfully saved. next...please fill financial details.";
                        ViewBag.msg = msg;
                        flag = true;
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == p).FirstOrDefault();
                            if (objVendorQue.VenIntroName != null)
                            {
                                tblApps.VenIntroName = objVendorQue.VenIntroName;
                            }
                            if (objVendorQue.VenIntroDesignation != null)
                            {
                                tblApps.VenIntroDesignation = objVendorQue.VenIntroDesignation;
                            }
                            if (objVendorQue.VenIntroDate != null)
                            {
                                tblApps.VenIntroDate = objVendorQue.VenIntroDate;
                            }
                            if (objVendorQue.NameCompany != null)
                            {
                                tblApps.NameCompany = objVendorQue.NameCompany;
                            }
                            if (objVendorQue.Regoffice != null)
                            {
                                tblApps.Regoffice = objVendorQue.Regoffice;
                            }
                            if (objVendorQue.RegTelphone != null)
                            {
                                tblApps.RegTelphone = objVendorQue.RegTelphone;
                            }
                            if (objVendorQue.RegFax != null)
                            {
                                tblApps.RegFax = objVendorQue.RegFax;
                            }
                            if (objVendorQue.RegEmail != null)
                            {
                                tblApps.RegEmail = objVendorQue.RegEmail;
                            }
                            if (objVendorQue.Headoffice != null)
                            {
                                tblApps.Headoffice = objVendorQue.Headoffice;
                            }
                            if (objVendorQue.HeadTelphone != null)
                            {
                                tblApps.HeadTelphone = objVendorQue.HeadTelphone;
                            }
                            if (objVendorQue.HeadFax != null)
                            {
                                tblApps.HeadFax = objVendorQue.HeadFax;
                            }
                            if (objVendorQue.HeadEmail != null)
                            {
                                tblApps.HeadEmail = objVendorQue.HeadEmail;
                            }
                            if (objVendorQue.Plant != null)
                            {
                                tblApps.Plant = objVendorQue.Plant;
                            }
                            if (objVendorQue.PlantTelephone != null)
                            {
                                tblApps.PlantTelephone = objVendorQue.PlantTelephone;
                            }
                            if (objVendorQue.PlantFax != null)
                            {
                                tblApps.PlantFax = objVendorQue.PlantFax;
                            }
                            if (objVendorQue.PlantEmail != null)
                            {
                                tblApps.PlantEmail = objVendorQue.PlantEmail;
                            }
                            if (objVendorQue.ConPrName != null)
                            {
                                tblApps.ConPrName = objVendorQue.ConPrName;
                            }
                            if (objVendorQue.ConPrDesignation != null)
                            {
                                tblApps.ConPrDesignation = objVendorQue.ConPrDesignation;
                            }
                            if (objVendorQue.ConPrOCN != null)
                            {
                                tblApps.ConPrOCN = objVendorQue.ConPrOCN;
                            }
                            if (objVendorQue.ConPrRCN != null)
                            {
                                tblApps.ConPrRCN = objVendorQue.ConPrRCN;
                            }
                            if (objVendorQue.ConPrName1 != null)
                            {
                                tblApps.ConPrName1 = objVendorQue.ConPrName1;
                            }
                            if (objVendorQue.ConPrDesignation1 != null)
                            {
                                tblApps.ConPrDesignation1 = objVendorQue.ConPrDesignation1;
                            }
                            if (objVendorQue.ConPrOCN1 != null)
                            {
                                tblApps.ConPrOCN1 = objVendorQue.ConPrOCN1;
                            }
                            if (objVendorQue.ConPrRCN1 != null)
                            {
                                tblApps.ConPrRCN1 = objVendorQue.ConPrRCN1;
                            }
                            if (objVendorQue.ConPrName2 != null)
                            {
                                tblApps.ConPrName2 = objVendorQue.ConPrName2;
                            }
                            if (objVendorQue.ConPrDesignation2 != null)
                            {
                                tblApps.ConPrDesignation2 = objVendorQue.ConPrDesignation2;
                            }
                            if (objVendorQue.ConPrOCN2 != null)
                            {
                                tblApps.ConPrOCN2 = objVendorQue.ConPrOCN2;
                            }
                            if (objVendorQue.ConPrRCN2 != null)
                            {
                                tblApps.ConPrRCN2 = objVendorQue.ConPrRCN2;
                            }
                            if (objVendorQue.ConPrName3 != null)
                            {
                                tblApps.ConPrName3 = objVendorQue.ConPrName3;
                            }
                            if (objVendorQue.ConPrDesignation3 != null)
                            {
                                tblApps.ConPrDesignation3 = objVendorQue.ConPrDesignation3;
                            }
                            if (objVendorQue.ConPrOCN3 != null)
                            {
                                tblApps.ConPrOCN3 = objVendorQue.ConPrOCN3;
                            }
                            if (objVendorQue.ConPrRCN3 != null)
                            {
                                tblApps.ConPrRCN3 = objVendorQue.ConPrRCN3;
                            }
                            if (objVendorQue.ProductName != null)
                            {
                                tblApps.ProductName = objVendorQue.ProductName;
                            }
                            if (objVendorQue.MainCustomer != null)
                            {
                                tblApps.MainCustomer = objVendorQue.MainCustomer;
                            }
                            if (objVendorQue.ComOfSupply != null)
                            {
                                tblApps.ComOfSupply = objVendorQue.ComOfSupply;
                            }
                            if (objVendorQue.AnnualTurnOver != null)
                            {
                                tblApps.AnnualTurnOver = objVendorQue.AnnualTurnOver;
                            }
                            if (objVendorQue.ProductName1 != null)
                            {
                                tblApps.ProductName1 = objVendorQue.ProductName1;
                            }
                            if (objVendorQue.MainCustomer1 != null)
                            {
                                tblApps.MainCustomer1 = objVendorQue.MainCustomer1;
                            }
                            if (objVendorQue.ComOfSupply1 != null)
                            {
                                tblApps.ComOfSupply1 = objVendorQue.ComOfSupply1;
                            }
                            if (objVendorQue.AnnualTurnOver1 != null)
                            {
                                tblApps.AnnualTurnOver1 = objVendorQue.AnnualTurnOver1;
                            }
                            if (objVendorQue.ProductName2 != null)
                            {
                                tblApps.ProductName2 = objVendorQue.ProductName2;
                            }
                            if (objVendorQue.MainCustomer2 != null)
                            {
                                tblApps.MainCustomer2 = objVendorQue.MainCustomer2;
                            }
                            if (objVendorQue.ComOfSupply2 != null)
                            {
                                tblApps.ComOfSupply2 = objVendorQue.ComOfSupply2;
                            }
                            if (objVendorQue.AnnualTurnOver2 != null)
                            {
                                tblApps.AnnualTurnOver2 = objVendorQue.AnnualTurnOver2;
                            }
                            if (objVendorQue.ProductName3 != null)
                            {
                                tblApps.ProductName3 = objVendorQue.ProductName3;
                            }
                            if (objVendorQue.MainCustomer3 != null)
                            {
                                tblApps.MainCustomer3 = objVendorQue.MainCustomer3;
                            }
                            if (objVendorQue.ComOfSupply3 != null)
                            {
                                tblApps.ComOfSupply3 = objVendorQue.ComOfSupply3;
                            }
                            if (objVendorQue.AnnualTurnOver3 != null)
                            {
                                tblApps.AnnualTurnOver3 = objVendorQue.AnnualTurnOver3;
                            }
                            if (objVendorQue.ComCertificate != null)
                            {
                                tblApps.ComCertificate = objVendorQue.ComCertificate;
                            }
                            if (objVendorQue.NameOfQuality != null)
                            {
                                tblApps.NameOfQuality = objVendorQue.NameOfQuality;
                            }
                            if (objVendorQue.CertifyingAgency != null)
                            {
                                tblApps.CertifyingAgency = objVendorQue.CertifyingAgency;
                            }
                            if (objVendorQue.YearOfCertification != null)
                            {
                                tblApps.YearOfCertification = objVendorQue.YearOfCertification;
                            }
                            if (objVendorQue.NameOfQuality1 != null)
                            {
                                tblApps.NameOfQuality1 = objVendorQue.NameOfQuality1;
                            }
                            if (objVendorQue.CertifyingAgency1 != null)
                            {
                                tblApps.CertifyingAgency1 = objVendorQue.CertifyingAgency1;
                            }
                            if (objVendorQue.YearOfCertification1 != null)
                            {
                                tblApps.YearOfCertification1 = objVendorQue.YearOfCertification1;
                            }
                            if (objVendorQue.NameOfQuality2 != null)
                            {
                                tblApps.NameOfQuality2 = objVendorQue.NameOfQuality2;
                            }
                            if (objVendorQue.CertifyingAgency2 != null)
                            {
                                tblApps.CertifyingAgency2 = objVendorQue.CertifyingAgency2;
                            }
                            if (objVendorQue.YearOfCertification2 != null)
                            {
                                tblApps.YearOfCertification2 = objVendorQue.YearOfCertification2;
                            }
                            if (objVendorQue.NameOfQuality3 != null)
                            {
                                tblApps.NameOfQuality3 = objVendorQue.NameOfQuality3;
                            }
                            if (objVendorQue.CertifyingAgency3 != null)
                            {
                                tblApps.CertifyingAgency3 = objVendorQue.CertifyingAgency3;
                            }
                            if (objVendorQue.YearOfCertification3 != null)
                            {
                                tblApps.YearOfCertification3 = objVendorQue.YearOfCertification3;
                            }
                            if (objVendorQue.BarCode != null)
                            {
                                tblApps.BarCode = objVendorQue.BarCode;
                            }
                            if (objVendorQue.BarCodeYear != null)
                            {
                                tblApps.BarCodeYear = objVendorQue.BarCodeYear;
                            }
                            if (objVendorQue.TotalArea != null)
                            {
                                tblApps.TotalArea = objVendorQue.TotalArea;
                            }
                            if (objVendorQue.TotalCoveredArea != null)
                            {
                                tblApps.TotalCoveredArea = objVendorQue.TotalCoveredArea;
                            }
                            if (objVendorQue.ConnectedElectrical != null)
                            {
                                tblApps.ConnectedElectrical = objVendorQue.ConnectedElectrical;
                            }
                            if (objVendorQue.AOASOEC != null)
                            {
                                tblApps.AOASOEC = objVendorQue.AOASOEC;
                            }
                            if (objVendorQue.DescriptionName != null)
                            {
                                tblApps.DescriptionName = objVendorQue.DescriptionName;
                            }
                            if (objVendorQue.DateOfAssociation != null)
                            {
                                tblApps.DateOfAssociation = objVendorQue.DateOfAssociation;
                            }
                            if (objVendorQue.Partnership != null)
                            {
                                tblApps.Partnership = objVendorQue.Partnership;
                            }
                            if (objVendorQue.Remarks != null)
                            {
                                tblApps.Remarks = objVendorQue.Remarks;
                            }
                            if (objVendorQue.DescriptionName1 != null)
                            {
                                tblApps.DescriptionName1 = objVendorQue.DescriptionName1;
                            }
                            if (objVendorQue.DateOfAssociation1 != null)
                            {
                                tblApps.DateOfAssociation1 = objVendorQue.DateOfAssociation1;
                            }
                            if (objVendorQue.Partnership1 != null)
                            {
                                tblApps.Partnership1 = objVendorQue.Partnership1;
                            }
                            if (objVendorQue.Remarks1 != null)
                            {
                                tblApps.Remarks1 = objVendorQue.Remarks1;
                            }
                            if (objVendorQue.DescriptionName2 != null)
                            {
                                tblApps.DescriptionName2 = objVendorQue.DescriptionName2;
                            }
                            if (objVendorQue.DateOfAssociation2 != null)
                            {
                                tblApps.DateOfAssociation2 = objVendorQue.DateOfAssociation2;
                            }
                            if (objVendorQue.Partnership2 != null)
                            {
                                tblApps.Partnership2 = objVendorQue.Partnership2;
                            }
                            if (objVendorQue.Remarks2 != null)
                            {
                                tblApps.Remarks2 = objVendorQue.Remarks2;
                            }
                            if (objVendorQue.DescriptionName3 != null)
                            {
                                tblApps.DescriptionName3 = objVendorQue.DescriptionName3;
                            }
                            if (objVendorQue.DateOfAssociation3 != null)
                            {
                                tblApps.DateOfAssociation3 = objVendorQue.DateOfAssociation3;
                            }
                            if (objVendorQue.Partnership3 != null)
                            {
                                tblApps.Partnership3 = objVendorQue.Partnership3;
                            }
                            if (objVendorQue.Remarks3 != null)
                            {
                                tblApps.Remarks3 = objVendorQue.Remarks3;
                            }
                            if (objVendorQue.ICPA != null)
                            {
                                tblApps.ICPA = objVendorQue.ICPA;
                            }
                            if (objVendorQue.MOMT != null)
                            {
                                tblApps.MOMT = objVendorQue.MOMT;
                            }
                            if (objVendorQue.FEPlans != null)
                            {
                                tblApps.FEPlans = objVendorQue.FEPlans;
                            }
                            if (planfile != null)
                            {

                                tblApps.PlanFileName = fileName;
                                tblApps.PlanFileNameLoc = filePath;
                            }
                            Obj.SaveChanges();
                        }
                        Messages msg = new Messages();
                        msg.Message_Id = 1;
                        msg.Message = "Successfully saved. next...please fill financial details if not already filled.";
                        ViewBag.msg = msg;
                        flag = true;
                    }

                }
                if (flag)
                {
                    if (planfile != null)
                    {
                        planfile.SaveAs(f);
                    }
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Message_Id = 2;
                msg.Message = "Failed ! Please try again.";
                ViewBag.msg = msg;
            }
            return View("VendorQueSupp");
            //return RedirectToAction("VendorQueBSLUser");
        }
        [HttpPost]
        public ActionResult SaveVendorQue2(tblVendorQue objVendorQue, HttpPostedFileBase postedFile)
        {

            try
            {
                bool flag = false;
                string Supp_code = User.Identity.Name;
                string SUPP_NAME = Supp_code;
                string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileName = Path.GetFileNameWithoutExtension(fileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(fileName);
                string filePath = Server.MapPath("~/data/Financial_File");
                string f = Server.MapPath("~/data/Financial_File/" + fileName);
                List<Int64> Id = new List<Int64>();
                Id = db.Database.SqlQuery<Int64>("exec CheckApprovedId @SUPP_CODE={0}", Supp_code).ToList();
                if (Id.Count > 0)
                {
                    Messages msg = new Messages();
                    msg.Message_Id = 2;
                    msg.Message = "Approved data already exist.";
                    ViewBag.msg = msg;
                }
                else
                {
                    Int64 p = -100;
                    List<Int64> IdP = new List<Int64>();
                    IdP = db.Database.SqlQuery<Int64>("exec CheckPendingId @SUPP_CODE={0}", Supp_code).ToList();
                    if (IdP.Count > 0)
                    {
                        p = IdP[0];
                    }
                    if (p == -100)
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 2;
                        msg.Message = "First submit Company's Profile.";
                        ViewBag.msg = msg;
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == p).FirstOrDefault();
                            if (objVendorQue.BankName != null)
                            {
                                tblApps.BankName = objVendorQue.BankName;
                            }
                            if (objVendorQue.BankAddress != null)
                            {
                                tblApps.BankAddress = objVendorQue.BankAddress;
                            }
                            if (objVendorQue.BankTelephone != null)
                            {
                                tblApps.BankTelephone = objVendorQue.BankTelephone;
                            }
                            if (objVendorQue.BankName1 != null)
                            {
                                tblApps.BankName1 = objVendorQue.BankName1;
                            }
                            if (objVendorQue.BankAddress1 != null)
                            {
                                tblApps.BankAddress1 = objVendorQue.BankAddress1;
                            }
                            if (objVendorQue.BankTelephone1 != null)
                            {
                                tblApps.BankTelephone1 = objVendorQue.BankTelephone1;
                            }
                            if (objVendorQue.BankName2 != null)
                            {
                                tblApps.BankName2 = objVendorQue.BankName2;
                            }
                            if (objVendorQue.BankAddress2 != null)
                            {
                                tblApps.BankAddress2 = objVendorQue.BankAddress2;
                            }
                            if (objVendorQue.BankTelephone2 != null)
                            {
                                tblApps.BankTelephone2 = objVendorQue.BankTelephone2;
                            }
                            if (objVendorQue.BankName3 != null)
                            {
                                tblApps.BankName3 = objVendorQue.BankName3;
                            }
                            if (objVendorQue.BankAddress3 != null)
                            {
                                tblApps.BankAddress3 = objVendorQue.BankAddress3;
                            }
                            if (objVendorQue.BankTelephone3 != null)
                            {
                                tblApps.BankTelephone3 = objVendorQue.BankTelephone3;
                            }
                            if (objVendorQue.NameOfCustomer != null)
                            {
                                tblApps.NameOfCustomer = objVendorQue.NameOfCustomer;
                            }
                            if (objVendorQue.CustomerTurnover != null)
                            {
                                tblApps.CustomerTurnover = objVendorQue.CustomerTurnover;
                            }
                            if (objVendorQue.CustomerRemarks != null)
                            {
                                tblApps.CustomerRemarks = objVendorQue.CustomerRemarks;
                            }
                            if (objVendorQue.NameOfCustomer1 != null)
                            {
                                tblApps.NameOfCustomer1 = objVendorQue.NameOfCustomer1;
                            }
                            if (objVendorQue.CustomerTurnover1 != null)
                            {
                                tblApps.CustomerTurnover1 = objVendorQue.CustomerTurnover1;
                            }
                            if (objVendorQue.CustomerRemarks1 != null)
                            {
                                tblApps.CustomerRemarks1 = objVendorQue.CustomerRemarks1;
                            }
                            if (objVendorQue.NameOfCustomer2 != null)
                            {
                                tblApps.NameOfCustomer2 = objVendorQue.NameOfCustomer2;
                            }
                            if (objVendorQue.CustomerTurnover2 != null)
                            {
                                tblApps.CustomerTurnover2 = objVendorQue.CustomerTurnover2;
                            }
                            if (objVendorQue.CustomerRemarks2 != null)
                            {
                                tblApps.CustomerRemarks2 = objVendorQue.CustomerRemarks2;
                            }
                            if (objVendorQue.NameOfCustomer3 != null)
                            {
                                tblApps.NameOfCustomer3 = objVendorQue.NameOfCustomer3;
                            }
                            if (objVendorQue.CustomerTurnover3 != null)
                            {
                                tblApps.CustomerTurnover3 = objVendorQue.CustomerTurnover3;
                            }
                            if (objVendorQue.CustomerRemarks3 != null)
                            {
                                tblApps.CustomerRemarks3 = objVendorQue.CustomerRemarks3;
                            }
                            if (objVendorQue.SalesTaxNo != null)
                            {
                                tblApps.SalesTaxNo = objVendorQue.SalesTaxNo;
                            }
                            if (objVendorQue.ExciseRegNo != null)
                            {
                                tblApps.ExciseRegNo = objVendorQue.ExciseRegNo;
                            }
                            if (objVendorQue.PAN != null)
                            {
                                tblApps.PAN = objVendorQue.PAN;
                            }
                            if (objVendorQue.TDS != null)
                            {
                                tblApps.TDS = objVendorQue.TDS;
                            }
                            tblApps.Financial_FileName = fileName;
                            tblApps.Financial_FilePath = filePath;
                            if(objVendorQue.GSTNo !=null)
                            {
                                tblApps.GSTNo = objVendorQue.GSTNo;
                            }
                            if (objVendorQue.TINNo != null)
                            {
                                tblApps.TINNo = objVendorQue.TINNo;
                            }
                            Obj.SaveChanges();
                        }
                        Messages msg = new Messages();
                        msg.Message_Id = 1;
                        msg.Message = "Successfully saved. next...please fill human resource details if not already filled.";
                        ViewBag.msg = msg;
                        flag = true;
                    }

                }
                if (flag)
                {
                    postedFile.SaveAs(f);
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Message_Id = 2;
                msg.Message = "Failed ! Please try again.";
                ViewBag.msg = msg;
            }
            return View("VendorQueSupp");
        }
        [HttpPost]
        public ActionResult SaveVendorQue3(tblVendorQue objVendorQue)
        {
            try
            {
                string Supp_code = User.Identity.Name;
                string SUPP_NAME = Supp_code;
                List<Int64> Id = new List<Int64>();
                Id = db.Database.SqlQuery<Int64>("exec CheckApprovedId @SUPP_CODE={0}", Supp_code).ToList();
                if (Id.Count > 0)
                {
                    Messages msg = new Messages();
                    msg.Message_Id = 2;
                    msg.Message = "Approved data already exist.";
                    ViewBag.msg = msg;
                }
                else
                {
                    Int64 p = -100;
                    List<Int64> IdP = new List<Int64>();
                    IdP = db.Database.SqlQuery<Int64>("exec CheckPendingId @SUPP_CODE={0}", Supp_code).ToList();
                    if (IdP.Count > 0)
                    {
                        p = IdP[0];
                    }
                    if (p == -100)
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 2;
                        msg.Message = "First submit Company's Profile.";
                        ViewBag.msg = msg;
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == p).FirstOrDefault();
                            if (objVendorQue.PerEmp != null)
                            {
                                tblApps.PerEmp = objVendorQue.PerEmp;
                            }
                            if (objVendorQue.UnionisedEmp != null)
                            {
                                tblApps.UnionisedEmp = objVendorQue.UnionisedEmp;
                            }
                            if (objVendorQue.NonUnionisedEmp != null)
                            {
                                tblApps.NonUnionisedEmp = objVendorQue.NonUnionisedEmp;
                            }
                            if (objVendorQue.CasualEmp != null)
                            {
                                tblApps.CasualEmp = objVendorQue.CasualEmp;
                            }
                            if (objVendorQue.Managers != null)
                            {
                                tblApps.Managers = objVendorQue.Managers;
                            }
                            if (objVendorQue.Supervisiors != null)
                            {
                                tblApps.Supervisiors = objVendorQue.Supervisiors;
                            }
                            if (objVendorQue.Production != null)
                            {
                                tblApps.Production = objVendorQue.Production;
                            }
                            if (objVendorQue.QA != null)
                            {
                                tblApps.QA = objVendorQue.QA;
                            }
                            if (objVendorQue.Administration != null)
                            {
                                tblApps.Administration = objVendorQue.Administration;
                            }
                            if (objVendorQue.DegreeHolders != null)
                            {
                                tblApps.DegreeHolders = objVendorQue.DegreeHolders;
                            }
                            if (objVendorQue.DiplomaHolders != null)
                            {
                                tblApps.DiplomaHolders = objVendorQue.DiplomaHolders;
                            }
                            if (objVendorQue.Others != null)
                            {
                                tblApps.Others = objVendorQue.Others;
                            }
                            if (objVendorQue.Direct != null)
                            {
                                tblApps.Direct = objVendorQue.Direct;
                            }
                            if (objVendorQue.Indirect != null)
                            {
                                tblApps.Indirect = objVendorQue.Indirect;
                            }
                            if (objVendorQue.DirectITI != null)
                            {
                                tblApps.DirectITI = objVendorQue.DirectITI;
                            }
                            if (objVendorQue.DirectNonITI != null)
                            {
                                tblApps.DirectNonITI = objVendorQue.DirectNonITI;
                            }
                            if (objVendorQue.IndirectITI != null)
                            {
                                tblApps.IndirectITI = objVendorQue.IndirectITI;
                            }
                            if (objVendorQue.IndirectNonITI != null)
                            {
                                tblApps.IndirectNonITI = objVendorQue.IndirectNonITI;
                            }
                            if (objVendorQue.WorkersUnion != null)
                            {
                                tblApps.WorkersUnion = objVendorQue.WorkersUnion;
                            }
                            if (objVendorQue.LongTermSettleUnion != null)
                            {
                                tblApps.LongTermSettleUnion = objVendorQue.LongTermSettleUnion;
                            }
                            if (objVendorQue.Strike != null)
                            {
                                tblApps.Strike = objVendorQue.Strike;
                            }
                            if (objVendorQue.IRSituation != null)
                            {
                                tblApps.IRSituation = objVendorQue.IRSituation;
                            }
                            if (objVendorQue.ShiftTimings != null)
                            {
                                tblApps.ShiftTimings = objVendorQue.ShiftTimings;
                            }
                            if (objVendorQue.AnnualTrainingPlan != null)
                            {
                                tblApps.AnnualTrainingPlan = objVendorQue.AnnualTrainingPlan;
                            }
                            if (objVendorQue.TrainingProgBeingPlan != null)
                            {
                                tblApps.TrainingProgBeingPlan = objVendorQue.TrainingProgBeingPlan;
                            }
                            if (objVendorQue.ForeignSub != null)
                            {
                                tblApps.ForeignSub = objVendorQue.ForeignSub;
                            }
                            if (objVendorQue.ForeignSubManager != null)
                            {
                                tblApps.ForeignSubManager = objVendorQue.ForeignSubManager;
                            }
                            if (objVendorQue.ForeignSubStaff != null)
                            {
                                tblApps.ForeignSubStaff = objVendorQue.ForeignSubStaff;
                            }
                            if (objVendorQue.ForeignSubWorkmen != null)
                            {
                                tblApps.ForeignSubWorkmen = objVendorQue.ForeignSubWorkmen;
                            }
                            if(objVendorQue.TrainingConductedInYear !=null)
                            {
                                tblApps.TrainingConductedInYear = objVendorQue.TrainingConductedInYear;
                            }
                            Obj.SaveChanges();
                        }
                        Messages msg = new Messages();
                        msg.Message_Id = 1;
                        msg.Message = "Successfully saved. next... please fill technical details  if not already filled.";
                        ViewBag.msg = msg;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Message_Id = 2;
                msg.Message = "Failed ! Please try again.";
                ViewBag.msg = msg;
            }
            return View("VendorQueSupp");
        }
        [HttpPost]
        public ActionResult SaveVendorQue4(tblVendorQue objVendorQue, HttpPostedFileBase fluDescirbe, HttpPostedFileBase fluGiveDetail, HttpPostedFileBase MachineFile, HttpPostedFileBase InhouseFile, HttpPostedFileBase RDFile, HttpPostedFileBase RawMaterialFile)
        {
            try
            {
                bool flag = false;
                string Supp_code = User.Identity.Name;
                string SUPP_NAME = Supp_code;
                string Non_Conforming_FileName = string.Empty;
                string Inspection_FileName = string.Empty;
                string Non_Conforming_FilePath = string.Empty;
                string Inspection_FilePath = string.Empty;
                string MachineFileName = "";
                string MachineFilePath = "";
                string MachineFilePath1 = "";
                string InhouseFileName = "";
                string InhouseFilePath = "";
                string InhouseFilePath1 = "";
                string RDFileName = "";
                string RDFilePath = "";
                string RDFilePath1 = "";
                string RawMaterialFileName = "";
                string RawMaterialFilePath = "";
                string RawMaterialFilePath1 = "";
                if (MachineFile != null)
                {
                    MachineFileName = System.IO.Path.GetFileName(MachineFile.FileName);
                    MachineFileName = Path.GetFileNameWithoutExtension(MachineFileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(MachineFileName);
                    MachineFilePath = Server.MapPath("~/data/Technical_File");
                    MachineFilePath1 = Server.MapPath("~/data/Technical_File/" + MachineFileName);
                }
                if (InhouseFile != null)
                {
                    InhouseFileName = System.IO.Path.GetFileName(InhouseFile.FileName);
                    InhouseFileName = Path.GetFileNameWithoutExtension(InhouseFileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(InhouseFileName);
                    InhouseFilePath = Server.MapPath("~/data/Technical_File");
                    InhouseFilePath1 = Server.MapPath("~/data/Technical_File/" + InhouseFileName);
                }
                if (RDFile != null)
                {
                    RDFileName = System.IO.Path.GetFileName(RDFile.FileName);
                    RDFileName = Path.GetFileNameWithoutExtension(RDFileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(RDFileName);
                    RDFilePath = Server.MapPath("~/data/Technical_File");
                    RDFilePath1 = Server.MapPath("~/data/Technical_File/" + RDFileName);
                }
                if (RawMaterialFile != null)
                {
                    RawMaterialFileName = System.IO.Path.GetFileName(RawMaterialFile.FileName);
                    RawMaterialFileName = Path.GetFileNameWithoutExtension(RawMaterialFileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(RawMaterialFileName);
                    RawMaterialFilePath = Server.MapPath("~/data/Technical_File");
                    RawMaterialFilePath1 = Server.MapPath("~/data/Technical_File/" + RawMaterialFileName);
                }
                Non_Conforming_FileName = System.IO.Path.GetFileName(fluDescirbe.FileName);
                Non_Conforming_FileName = Path.GetFileNameWithoutExtension(Non_Conforming_FileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(Non_Conforming_FileName);
                Non_Conforming_FilePath = Server.MapPath("~/data/Technical_File");
                string Non_Conforming_FilePath1 = Server.MapPath("~/data/Technical_File/" + Non_Conforming_FileName);
                Inspection_FileName = System.IO.Path.GetFileName(fluGiveDetail.FileName);
                Inspection_FileName = Path.GetFileNameWithoutExtension(Inspection_FileName) + " " + RemoveSpecialChars(Supp_code) + " " + " " + RemoveSpecialChars(DateTime.Now.ToString()) + Path.GetExtension(Inspection_FileName);
                Inspection_FilePath = Server.MapPath("~/data/Technical_File");
                string Inspection_FilePath1 = Server.MapPath("~/data/Technical_File/" + Inspection_FileName);
                List<Int64> Id = new List<Int64>();
                Id = db.Database.SqlQuery<Int64>("exec CheckApprovedId @SUPP_CODE={0}", Supp_code).ToList();
                if (Id.Count > 0)
                {
                    Messages msg = new Messages();
                    msg.Message_Id = 2;
                    msg.Message = "Approved data already exist.";
                    ViewBag.msg = msg;
                    return View("VendorQueSupp");
                }
                else
                {
                    Int64 p = -100;
                    List<Int64> IdP = new List<Int64>();
                    IdP = db.Database.SqlQuery<Int64>("exec CheckPendingId @SUPP_CODE={0}", Supp_code).ToList();
                    if (IdP.Count > 0)
                    {
                        p = IdP[0];
                    }
                    if (p == -100)
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 2;
                        msg.Message = "First submit Company's Profile.";
                        ViewBag.msg = msg;
                    }
                    else
                    {
                        using (VendorPortalEntities Obj = new VendorPortalEntities())
                        {
                            tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == p).FirstOrDefault();
                            if (objVendorQue.MachineEquipSNo != null)
                            {
                                tblApps.MachineEquipSNo = objVendorQue.MachineEquipSNo;
                            }
                            if (objVendorQue.MachineEquipDescrip != null)
                            {
                                tblApps.MachineEquipDescrip = objVendorQue.MachineEquipDescrip;
                            }
                            if (objVendorQue.MachineEquipQty != null)
                            {
                                tblApps.MachineEquipQty = objVendorQue.MachineEquipQty;
                            }
                            if (objVendorQue.MachineEquipType != null)
                            {
                                tblApps.MachineEquipType = objVendorQue.MachineEquipType;
                            }
                            if (objVendorQue.MachineEquipRemarks != null)
                            {
                                tblApps.MachineEquipRemarks = objVendorQue.MachineEquipRemarks;
                            }
                            if (objVendorQue.InhouseSNo != null)
                            {
                                tblApps.InhouseSNo = objVendorQue.InhouseSNo;
                            }
                            if (objVendorQue.InhouseDescrip != null)
                            {
                                tblApps.InhouseDescrip = objVendorQue.InhouseDescrip;
                            }
                            if (objVendorQue.InhouseQty != null)
                            {
                                tblApps.InhouseQty = objVendorQue.InhouseQty;
                            }
                           
                            if (objVendorQue.InhouseRemarks != null)
                            {
                                tblApps.InhouseRemarks = objVendorQue.InhouseRemarks;
                            }
                            //tblApps.ReaxnPlanNonConfirm = objVendorQue.ReaxnPlanNonConfirm;
                            if (objVendorQue.RDFacility != null)
                            {
                                tblApps.RDFacility = objVendorQue.RDFacility;
                            }
                            if (objVendorQue.RawMaterialSNo != null)
                            {
                                tblApps.RawMaterialSNo = objVendorQue.RawMaterialSNo;
                            }
                            if (objVendorQue.RawMaterialDescrip != null)
                            {
                                tblApps.RawMaterialDescrip = objVendorQue.RawMaterialDescrip;
                            }
                            if (objVendorQue.RawMaterialSource != null)
                            {
                                tblApps.RawMaterialSource = objVendorQue.RawMaterialSource;
                            }
                            if (objVendorQue.RawMaterialRemarks != null)
                            {
                                tblApps.RawMaterialRemarks = objVendorQue.RawMaterialRemarks;
                            }
                            if (objVendorQue.Tracebility != null)
                            {
                                tblApps.Tracebility = objVendorQue.Tracebility;
                            }
                            if (objVendorQue.SpecialFacility != null)
                            {
                                tblApps.SpecialFacility = objVendorQue.SpecialFacility;
                            }
                            if (objVendorQue.RejectionMonitoringSys != null)
                            {
                                tblApps.RejectionMonitoringSys = objVendorQue.RejectionMonitoringSys;
                            }
                            if (objVendorQue.CusSatisfyIndex != null)
                            {
                                tblApps.CusSatisfyIndex = objVendorQue.CusSatisfyIndex;
                            }
                            //tblApps.InspectProc = objVendorQue.InspectProc;
                            if (objVendorQue.ISISO != null)
                            {
                                tblApps.ISISO = objVendorQue.ISISO;
                            }
                            if (objVendorQue.ENBS != null)
                            {
                                tblApps.ENBS = objVendorQue.ENBS;
                            }
                            if (objVendorQue.JISJASO != null)
                            {
                                tblApps.JISJASO = objVendorQue.JISJASO;
                            }
                            if (objVendorQue.DIN != null)
                            {
                                tblApps.DIN = objVendorQue.DIN;
                            }
                            if (objVendorQue.AnyOthers != null)
                            {
                                tblApps.AnyOthers = objVendorQue.AnyOthers;
                            }
                            if (objVendorQue.IS_ISO_Remark != null)
                            {
                                tblApps.IS_ISO_Remark = objVendorQue.IS_ISO_Remark;
                            }
                            if (objVendorQue.EN_BS_Remark != null)
                            {
                                tblApps.EN_BS_Remark = objVendorQue.EN_BS_Remark;
                            }
                            if (objVendorQue.JIS_JASO_Remark != null)
                            {
                                tblApps.JIS_JASO_Remark = objVendorQue.JIS_JASO_Remark;
                            }
                            if (objVendorQue.DIN_Remark != null)
                            {
                                tblApps.DIN_Remark = objVendorQue.DIN_Remark;
                            }
                            if (objVendorQue.AnyOthers_Remark != null)
                            {
                                tblApps.AnyOthers_Remark = objVendorQue.AnyOthers_Remark;
                            }
                            tblApps.Non_Conforming_FileName = Non_Conforming_FileName;
                            tblApps.Non_Conforming_FilePath = Non_Conforming_FilePath;
                            tblApps.Inspection_FileName = Inspection_FileName;
                            tblApps.Inspection_FilePath = Inspection_FilePath;
                            if (MachineFile != null)
                            {
                                tblApps.MachineFileName = MachineFileName;
                                tblApps.MachineFilePath = MachineFilePath;
                            }
                            if (InhouseFile != null)
                            {
                                tblApps.InHouseFileName = InhouseFileName;
                                tblApps.InHouseFilePath = InhouseFilePath;
                            }
                            if (RDFile != null)
                            {
                                tblApps.RDFileName = RDFileName;
                                tblApps.RDFilePath = RDFilePath;
                            }
                            if (RawMaterialFile != null)
                            {
                                tblApps.RawMaterialFileName = RawMaterialFileName;
                                tblApps.RawMaterialFilePath = RawMaterialFilePath;
                            }
                            Obj.SaveChanges();
                        }
                        Messages msg = new Messages();
                        msg.Message_Id = 1;
                        msg.Message = "Successfully saved.";
                        ViewBag.msg = msg;
                        flag = true;
                    }

                }
                if (flag)
                {
                    fluDescirbe.SaveAs(Non_Conforming_FilePath1);
                    fluGiveDetail.SaveAs(Inspection_FilePath1);
                    if (MachineFile != null)
                    {
                        MachineFile.SaveAs(MachineFilePath1);
                    }
                    if (InhouseFile != null)
                    {
                        InhouseFile.SaveAs(InhouseFilePath1);
                    }
                    if (RDFile != null)
                    {
                        RDFile.SaveAs(RDFilePath1);
                    }
                    if (RawMaterialFile != null)
                    {
                        RawMaterialFile.SaveAs(RawMaterialFilePath1);
                    }
                }
                //return RedirectToAction("VendorQueBSLUser");
                return View("VendorQueSupp");
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.Message_Id = 2;
                msg.Message = "Failed ! Please try again.";
                ViewBag.msg = msg;
                return View("VendorQueSupp");
            }
          
        }
        #region Vendor Que pdf Download
        iTextSharp.text.Font objfontHeading = FontFactory.GetFont("Verdana", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        iTextSharp.text.Font objfontValue = FontFactory.GetFont("Verdana", 7, iTextSharp.text.BaseColor.DARK_GRAY);
        iTextSharp.text.Font objfontSubHeading = FontFactory.GetFont("Verdana", 8, iTextSharp.text.BaseColor.DARK_GRAY);
        iTextSharp.text.Font objfontBold = FontFactory.GetFont("Verdana", 8, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLACK);
        DataSet ds = null;
        PdfPTable PdfTable = null;
        PdfPCell PdfPCell = null;
        Chunk objChunkUnderline = null;
        Chunk objChunkUnderlineSpace = null;
        float tableWidth = 550f;
        float tablePadding = 2f;
        Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
        PdfPTable PdfTblEmpInfo = null;
        Paragraph objParagraphTemp = null;
        public FileResult CreatePdf(int Id)
        {
            String LocalUrlFin = ConfigurationManager.AppSettings["LocalUrlFin"];
            String LocalUrlTech = ConfigurationManager.AppSettings["LocalUrlTech"];
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == Id).FirstOrDefault();
                string s = "VendorQue" + " " + RemoveSpecialChars(tblApps.SUPP_CODE) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + ".pdf";
                string DestinationPath = Server.MapPath("~/data/VendorQue/" + s);
                PdfWriter.GetInstance(document, new FileStream(DestinationPath, FileMode.Create));
                document.Open();
                objParagraphTemp = new Paragraph("BHARAT SEATS LIMITED", objfontHeading);
                objParagraphTemp.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                objParagraphTemp.SpacingAfter = 4f;
                document.Add(objParagraphTemp);
                #region Vendor Introduced by
                document.Add(new Paragraph("Vendor Introduced by:", objfontHeading));
                PdfPTable table = new PdfPTable(6);
                table.TotalWidth = tableWidth;
                //fix the absolute width of the table
                table.LockedWidth = true;
                //relative col widths in proportions - 1/3 and 2/3
                float[] widths = new float[] { 2f, 3f, 2f, 3f, 2f, 3f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 1;
                //leave a gap before and after the table
                table.DefaultCell.Padding = tablePadding;
                table.SpacingBefore = 3f;
                table.SpacingAfter = 4f;
                PdfPCell PdfPCell = new PdfPCell(new Phrase("Name:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(PdfPCell);
                table.AddCell(new Phrase(tblApps.VenIntroName == null ? " ": tblApps.VenIntroName, objfontValue));
                PdfPCell = new PdfPCell(new Phrase("Designation:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(PdfPCell);
                table.AddCell(new Phrase(tblApps.VenIntroDesignation == null ? " " : tblApps.VenIntroDesignation, objfontValue));
                PdfPCell = new PdfPCell(new Phrase("Date:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(PdfPCell);
                table.AddCell(new Phrase(tblApps.VenIntroDate == null ? " " :tblApps.VenIntroDate.ToString().Replace("00:00:00", ""), objfontValue));
                document.Add(table);
                #endregion
                objParagraphTemp = new Paragraph("VENDOR QUESTIONNAIRE", objfontHeading);
                objParagraphTemp.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                objParagraphTemp.SpacingAfter = 4f;
                document.Add(objParagraphTemp);
                document.Add(new Paragraph("1. Company's Profile:", objfontHeading));
                #region 1.1 Name Of Company
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("1.1 Name Of Company:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.NameCompany == null ? " " : tblApps.NameCompany, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 1.2 Address Of Company
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.2 Address Of Company:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                 widths = new float[] { 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                  PdfPCell = null;
                string[] val = new string[] { " Registered Office:", "Head Office:", "Plant: " };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.Regoffice, tblApps.Headoffice, tblApps.Plant };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { "Telephone:", "Telephone:", "Telephone: " };
                for (int i = 0; i < 3; i++)
                {
                        PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                        PdfPCell.Padding = tablePadding;
                        PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.RegTelphone, tblApps.HeadTelphone, tblApps.PlantTelephone };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { "FAX:", "FAX:", "FAX: " };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.RegFax, tblApps.HeadFax, tblApps.PlantFax };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { "e-mail:", "e-mail:", "e-mail: " };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.RegEmail, tblApps.HeadEmail, tblApps.PlantEmail };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.3 Contact Persons
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.3 Contact Persons:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Name:", "Designation:", "Contact No. Office:", "Contact No. Residence:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.ConPrName, tblApps.ConPrDesignation, tblApps.ConPrOCN,tblApps.ConPrRCN, tblApps.ConPrName1, tblApps.ConPrDesignation1, tblApps.ConPrOCN1, tblApps.ConPrRCN1, tblApps.ConPrName2, tblApps.ConPrDesignation2, tblApps.ConPrOCN2, tblApps.ConPrRCN2, tblApps.ConPrName3, tblApps.ConPrDesignation3, tblApps.ConPrOCN3, tblApps.ConPrRCN3 };
                for (int i = 0; i < 16; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.4 Product Manufactured By Company
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.4 Product Manufactured By Company:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Name Of Product:", "Main Customer:", "Supply(year):", "%of annual Turn over:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
               
                val = new string[] { tblApps.ProductName, tblApps.MainCustomer, tblApps.ComOfSupply, tblApps.AnnualTurnOver.ToString(), tblApps.ProductName1, tblApps.MainCustomer1, tblApps.ComOfSupply1, tblApps.AnnualTurnOver1.ToString(), tblApps.ProductName2, tblApps.MainCustomer2, tblApps.ComOfSupply2, tblApps.AnnualTurnOver2.ToString(), tblApps.ProductName3, tblApps.MainCustomer3, tblApps.ComOfSupply3, tblApps.AnnualTurnOver3.ToString() };
                for (int i = 0; i < 16; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.5 Certification Details
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.5 Certification Details:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Is your company certified for ISO/ QS/ TS/ TPM/ Any other quality system (If yes then furnish the below details):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.ComCertificate == null ? " " : tblApps.ComCertificate, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Name Of Quality System:", "Certifying Agency:", "Year of Certification:"};
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.NameOfQuality, tblApps.CertifyingAgency, tblApps.YearOfCertification, tblApps.NameOfQuality1, tblApps.CertifyingAgency1, tblApps.YearOfCertification1, tblApps.NameOfQuality2, tblApps.CertifyingAgency2, tblApps.YearOfCertification2, tblApps.NameOfQuality3, tblApps.CertifyingAgency3, tblApps.YearOfCertification3};
                for (int i = 0; i < 12; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.6 Do you have Bar-Coding
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.6 Do you have Bar-Coding System for Material Supply? If yes since which year:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.BarCode == null ? " " : tblApps.BarCode + "," + tblApps.BarCodeYear == null ? " " : tblApps.BarCodeYear, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 1.7 Details Of Plant
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.7 Details Of Plant:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "a) Total Area(sq-mts):", "b) Total Covered Area(sq-mts):", "c) Connected Electrical Road(kva):", "d) Availability of alternate source of Electricity(In-House):" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.TotalArea , tblApps.TotalCoveredArea , tblApps.ConnectedElectrical, tblApps.AOASOEC };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.8 Partnership / Joint-venture
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.8 Partnership / Joint-venture foreign collaboration technical Tie-up:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Description & Name:", "Date  Of Association:", "Partnership:", "Remarks:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.DescriptionName, tblApps.DateOfAssociation.ToString().Replace("00:00:00", ""), tblApps.Partnership, tblApps.Remarks, tblApps.DescriptionName1, tblApps.DateOfAssociation1.ToString().Replace("00:00:00", ""), tblApps.Partnership1, tblApps.Remarks1, tblApps.DescriptionName2, tblApps.DateOfAssociation2.ToString().Replace("00:00:00", ""), tblApps.Partnership2, tblApps.Remarks2, tblApps.DescriptionName3, tblApps.DateOfAssociation3.ToString().Replace("00:00:00", ""), tblApps.Partnership3, tblApps.Remarks3 };
                for (int i = 0; i < 16; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.9 Other Details
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.9 Other Details:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(2);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "a) Installed Capacity per annum (w.r.t BSL's Parts):", " b) Spare Capacity available for BSL (in %):" };
                for (int i = 0; i < 2; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.ICPA , tblApps.SCAFBSL.ToString() };
                for (int i = 0; i < 2; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 1.10  Mode of Material Transport
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.10  Mode of Material Transport:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.MOMT == null ? " " : tblApps.MOMT, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 1.11 Future Expansion Plans
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("1.11 Future Expansion Plans , Details of Plan with Target Date:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.FEPlans == null ? " " : tblApps.FEPlans, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.PlanFileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfTable.SpacingAfter = 4f;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlFin + tblApps.PlanFileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfTable.SpacingAfter = 4f;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                document.Add(new Paragraph("2. Financial's Details:", objfontHeading));
                #region 2.1 Attach Balance Sheet 
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("2.1 Attach Balance Sheet for last Years(as annexure):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.Financial_FileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlFin + tblApps.Financial_FileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 2.2 Company's Bankers Details
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.2 Company's Bankers Details:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Name Of Bank:", "Address:", "Telephone Nos:" };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.BankName, tblApps.BankAddress,tblApps.BankTelephone, tblApps.BankName1, tblApps.BankAddress1, tblApps.BankTelephone1, tblApps.BankName2, tblApps.BankAddress2, tblApps.BankTelephone2, tblApps.BankName3, tblApps.BankAddress3, tblApps.BankTelephone3 };
                for (int i = 0; i < 12; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 2.3 Turnover Details of Previous Year
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.3 Turnover Details of Previous Year:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Name Of Customer:", "Turn-Over(in Rs Lacs):", "Remarks:" };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.NameOfCustomer, tblApps.CustomerTurnover, tblApps.CustomerRemarks, tblApps.NameOfCustomer1, tblApps.CustomerTurnover1, tblApps.CustomerRemarks1, tblApps.NameOfCustomer2, tblApps.CustomerTurnover2, tblApps.CustomerRemarks2, tblApps.NameOfCustomer3, tblApps.CustomerTurnover3, tblApps.CustomerRemarks3 };
                for (int i = 0; i < 12; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region 2.4  GST No
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.4  GST No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.GSTNo == null ? " " : tblApps.GSTNo, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 2.5  TIN No.
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.5  TIN No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.TINNo == null ? " " : tblApps.TINNo, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 2.6  Sales Tax No
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.6  Sales Tax No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.SalesTaxNo == null ? " " : tblApps.SalesTaxNo, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 2.7  Excise Registration No
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.7  Excise Registration No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.ExciseRegNo == null ? " " : tblApps.ExciseRegNo, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 2.8  Permanent A/C No
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.8  Permanent A/C No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.PAN == null ? " " : tblApps.PAN, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 2.9  TDS A/C No
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("2.9  TDS A/C No.:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingAfter = 4f;
                PdfPCell = new PdfPCell(new Phrase(tblApps.TDS == null ? " " : tblApps.TDS, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                document.Add(new Paragraph("3. Financial's Details:", objfontHeading));
                #region 3.1 Total Employees
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("3.1 Total Employees:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Permanent:", "Unionised:", "Non-Unionised:", "Casuals:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.PerEmp, tblApps.UnionisedEmp, tblApps.NonUnionisedEmp, tblApps.CasualEmp };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("i) Managers:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.Managers == null ? " " : tblApps.Managers, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "ii) Supervisors:", "Degree Holder:", "Diploma Holders:", "Others:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.Supervisiors, tblApps.DegreeHolders, tblApps.DiplomaHolders, tblApps.Others };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "iii) Production:", "a) Direct:", "b) Indirect:" };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.Production, tblApps.Direct, tblApps.Indirect };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Direct ITI:", "Direct NonITI:", "Indirect ITI:", "Indirect NonITI" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.DirectITI, tblApps.DirectNonITI, tblApps.IndirectITI, tblApps.IndirectNonITI };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("iv) QA:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.QA == null ? " " : tblApps.QA, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("v) Administration:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.Administration == null ? " " : tblApps.Administration, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.2 Do you have worker's Union
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.2 Do you have worker's Union:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.WorkersUnion == null ? " " : tblApps.WorkersUnion, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.3 Is There any Long Term
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.3 Is There any Long Term Settlement with the Union?:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.LongTermSettleUnion == null ? " " : tblApps.LongTermSettleUnion, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.4 Give details of Strike Lock
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.4 Give details of Strike Lock out go slow etc during the last 3 years if any:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.Strike == null ? " " : tblApps.Strike, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.5 How is the IR situation?
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.5 How is the IR situation?:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.IRSituation == null ? " " : tblApps.IRSituation, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.6 Number of shifts timings
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.6 Number of shifts timings:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.ShiftTimings == null ? " " : tblApps.ShiftTimings.ToString(), objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 3.7 Details Of Training Programs
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("3.7 Details Of Training Programs:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "a) Annual Training Plan?:", "b) Training Programmes being planned (yearly Target):", "c) Training Conducted Last year (in NOS):"};
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.AnnualTrainingPlan, tblApps.TrainingProgBeingPlan, tblApps.TrainingConductedInYear.ToString() };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("d) Foreign Training If any:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Subject Covered:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase(tblApps.ForeignSub == null ? " " : tblApps.ForeignSub, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingAfter = 4f;
                widths = new float[] { 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Managers:", "Staff:", "Workmen:" };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.ForeignSubManager, tblApps.ForeignSubStaff, tblApps.ForeignSubWorkmen };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                #endregion
                document.Add(new Paragraph("4. Technical Details:", objfontHeading));
                #region 4.1 Details of Machinery & Equipment
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("4.1 Details of Machinery & Equipment:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(5);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "S.No:", "Description and Make:", "Qty:", "Type (GPM/SPM/CNC etc.):", "Remarks/Purpose:" };
                for (int i = 0; i < 5; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.MachineEquipSNo, tblApps.MachineEquipDescrip, tblApps.MachineEquipQty, tblApps.MachineEquipType, tblApps.MachineEquipRemarks };
                for (int i = 0; i < 5; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
               
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Details of other Machinary & Equipment (Attach separate sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);

                if (tblApps.MachineFileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.MachineFileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.2 Details of In-house Testing
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.2 Details of In-house Testing & Inspection Facilities:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "S.No:", "Description and Make:", "Qty:", "Purpose/Remarks:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.InhouseSNo, tblApps.InhouseDescrip, tblApps.InhouseQty, tblApps.InhouseRemarks };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Details of other In-house Testing & Inspection Facilities(Attach separate sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.InHouseFileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.InHouseFileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.3 R & D Facilities
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.3 R & D Facilities (If yes attach separate sheet giving details e.g.CAD / CAM / CAE facilities trained Man-power etc.Also furnish details of products designed & developed during last two years):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                PdfPCell = new PdfPCell(new Phrase(tblApps.RDFacility == null ? " " : tblApps.RDFacility, objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Details of other R&D Facilities (Attach separate sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.RDFileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.RDFileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.4 Source of Raw Material
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.4 Source of Raw Material (BSL Related Product):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "S.No:", "Description:", "Source:", "Remarks:" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { tblApps.RawMaterialSNo, tblApps.RawMaterialDescrip, tblApps.RawMaterialSource, tblApps.RawMaterialRemarks };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("Details of other Raw Materials Facilities (Attach separate sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.RawMaterialFileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.RawMaterialFileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.5 Tracebility
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.5 Tracebility (Kindly define your product Traceability System in separat sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                PdfPCell = new PdfPCell(new Phrase(tblApps.Tracebility == null ? " " : tblApps.Tracebility, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 4.6 Describe the Reaction Plan
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.6 Describe the Reaction Plan for Non-conforming products (Attach separate sheet):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.Non_Conforming_FileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.Non_Conforming_FileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.7 Any Special Process/Facility
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.7 Any Special Process/Facility:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                PdfPCell = new PdfPCell(new Phrase(tblApps.SpecialFacility == null ? " " : tblApps.SpecialFacility, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 4.8 Rejection Monitoring System
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.8 Rejection Monitoring System:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                PdfPCell = new PdfPCell(new Phrase(tblApps.RejectionMonitoringSys == null ? " " : tblApps.RejectionMonitoringSys, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 4.9 Customer Satisfaction Index/Trend
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.9 Customer Satisfaction Index/Trend:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                PdfPCell = new PdfPCell(new Phrase(tblApps.CusSatisfyIndex == null ? " " : tblApps.CusSatisfyIndex, objfontValue));
                PdfPCell.Padding = tablePadding;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                #endregion
                #region 4.10 Give details of Inspection
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.10 Give details of Inspection Procedures(Sampling Self-certification etc.):", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                if (tblApps.Inspection_FileName != null)
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase(LocalUrlTech + tblApps.Inspection_FileName + "   (*Copy and paste this link on web browser to download file)", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                else
                {
                    PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = tableWidth;
                    PdfTable.LockedWidth = true;
                    PdfPCell = new PdfPCell(new Phrase("Not Uploaded", objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                    document.Add(PdfTable);
                }
                #endregion
                #region 4.11  Exposure to National / International Standards
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfPCell = new PdfPCell(new Phrase("4.11  Exposure to National/International Standards:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(3);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                widths = new float[] { 2f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "        ", "Tick:", "Specify Nos. (which you are currently using):" };
                for (int i = 0; i < 3; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { "a) IS / ISO*",tblApps.ISISO,tblApps.IS_ISO_Remark, "b) EN/BS*",tblApps.ENBS,tblApps.EN_BS_Remark, "c) JIS/JASO*",tblApps.JISJASO,tblApps.JIS_JASO_Remark,"d) DIN*",tblApps.DIN,tblApps.DIN_Remark,"e) Any Others*",tblApps.AnyOthers,tblApps.AnyOthers_Remark};
                for (int i = 0; i < 15; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region Authorized
                PdfTable = new PdfPTable(2);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 15f;
                PdfTable.SpacingAfter = 4f;
                widths = new float[] { 1f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "Date:", "Authorized Signatory" };
                for (int i = 0; i < 2; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] {"         ", "         ", "         ","Name:                             ", "         ", "Designation:                             " };
                for (int i = 0; i <6; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                document.Add(new Paragraph("(*Please send the duly filled Questionnaire to Bharat Seats Limited)", objfontHeading));
                document.Add(new Paragraph("(For BSL office use only)", objfontHeading));
                #region BSL's EVALUATION & REMARKS
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("BSL's EVALUATION & REMARKS:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(5);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingAfter = 4f;
                widths = new float[] { 1f, 2f, 4f, 2f, 2f };
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] {"S. No.","Name of Authority","Remarks","Signature","Date"};
                for (int i = 0; i < 5; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] {"1.","Deptt. Head (Materials)","                             ","                  ","                ", "2.", "Deptt. Head (QA)", "                             ", "                  ", "                ", "3.", "Deptt. Head (Engg)", "                             ", "                  ", "                "};
                for (int i = 0; i < 15; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region Initial Approval
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("Initial Approval:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(4);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingAfter = 4f;
                widths = new float[] { 1f, 2f, 4f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "S. No.", "Name of Authority","Signature", "Date" };
                for (int i = 0; i < 4; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] { "1.", "G. M. (Works)", "                  ", "                ", "2.", "V. P. (Fin.)", "                  ", "                ", "3.", "Sr. V. P.", "                  ", "                " };
                for (int i = 0; i < 12; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion
                #region Final Approval
                PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingBefore = 5f;
                PdfPCell = new PdfPCell(new Phrase("Final Approval:", objfontHeading));
                PdfPCell.Padding = tablePadding;
                PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                PdfTable.AddCell(PdfPCell);
                document.Add(PdfTable);
                PdfTable = new PdfPTable(2);
                PdfTable.TotalWidth = tableWidth;
                PdfTable.LockedWidth = true;
                PdfTable.SpacingAfter = 4f;
                widths = new float[] { 2f, 2f};
                PdfTable.SetWidths(widths);
                PdfTable.HorizontalAlignment = 1;
                PdfTable.DefaultCell.Padding = tablePadding;
                PdfPCell = null;
                val = new string[] { "M. D.", "Date"};
                for (int i = 0; i < 2; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i].ToString(), objfontHeading));
                    PdfPCell.Padding = tablePadding;
                    PdfPCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    PdfTable.AddCell(PdfPCell);
                }
                val = new string[] {"                          ", "                "};
                for (int i = 0; i < 2; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(val[i] == null ? " " : val[i].ToString(), objfontValue));
                    PdfPCell.Padding = tablePadding;
                    PdfTable.AddCell(PdfPCell);
                }
                //table.SpacingAfter = 4f;
                document.Add(PdfTable);
                #endregion

                document.Close();
                byte[] fileBytes = System.IO.File.ReadAllBytes(@DestinationPath);
                var response = new FileContentResult(fileBytes, "application/octet-stream");
                response.FileDownloadName = s;
                return response;
            }
        }

            #endregion
            public FileResult CreateCsv(int Id)
        {
            String LocalUrlFin = ConfigurationManager.AppSettings["LocalUrlFin"];
            String LocalUrlTech = ConfigurationManager.AppSettings["LocalUrlTech"];
            //String UatUrlFin = ConfigurationManager.AppSettings["UatUrlFin"];
            //String UatUrlTech = ConfigurationManager.AppSettings["UatUrlTech"];
            using (VendorPortalEntities Obj = new VendorPortalEntities())
            {
                tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == Id).FirstOrDefault();
                string s = "VendorQue" + " " + RemoveSpecialChars(tblApps.SUPP_CODE) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + ".CSV";
                string DestinationPath = Server.MapPath("~/data/VendorQue/" + s);
                string Col = "             " + "," + "BHARAT SEATS LIMITED" + "," + "             ";

            

              
               
       
                byte[] fileBytes = System.IO.File.ReadAllBytes(@DestinationPath);
                var response = new FileContentResult(fileBytes, "application/octet-stream");
                response.FileDownloadName = s;
                return response;
            }
        }

        //public FileResult CreateCsv(int Id)
        //{
        //    String LocalUrlFin = ConfigurationManager.AppSettings["LocalUrlFin"];
        //    String LocalUrlTech = ConfigurationManager.AppSettings["LocalUrlTech"];
        //    //String UatUrlFin = ConfigurationManager.AppSettings["UatUrlFin"];
        //    //String UatUrlTech = ConfigurationManager.AppSettings["UatUrlTech"];
        //    using (VendorPortalEntities Obj = new VendorPortalEntities())
        //    {
        //        tblVendorQue tblApps = Obj.tblVendorQues.Where(x => x.Id == Id).FirstOrDefault();
        //        string s = "VendorQue" + " " + RemoveSpecialChars(tblApps.SUPP_CODE) + " " + RemoveSpecialChars(DateTime.Now.ToString()) + ".CSV";
        //        string DestinationPath = Server.MapPath("~/data/VendorQue/" + s);
        //        string Col = "             " + "," + "             " + "," + "BHARAT SEATS LIMITED" + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "             " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "Vendor Introduced by:             " + "," + "Name:             " + "," + "Designation:             " + "," + "Date:             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "             " + "," + tblApps.VenIntroName + "," + tblApps.VenIntroDesignation + "," + tblApps.VenIntroDate + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "             " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "             " + "," + "             " + "," + "VENDOR QUESTIONNAIRE" + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "             " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1. Company's Profile:" + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.1 Name Of Company: " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = tblApps.NameCompany + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.2 Address Of Company: " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Registered Office: " + "," + " Head Office: " + "," + " Plant: " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.Regoffice.Replace(","," ") + "," + tblApps.Headoffice.Replace(",", " ") + "," + tblApps.Plant.Replace(",", " ") + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Telephone: " + "," + " Telephone: " + "," + " Telephone: " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.RegTelphone + "," + tblApps.HeadTelphone + "," + tblApps.PlantTelephone + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Fax: " + "," + " Fax: " + "," + " Fax: " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.RegFax + "," + tblApps.HeadFax + "," + tblApps.PlantFax + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " e-mail: " + "," + " e-mail: " + "," + " e-mail: " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.RegEmail + "," + tblApps.HeadEmail + "," + tblApps.PlantEmail + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.3 Contact Persons: " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Name: " + "," + " Designation: " + "," + "Contact No. Office: " + "," + " Contact No. Residence ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ConPrName + "," + tblApps.ConPrDesignation + "," + tblApps.ConPrOCN + "," + tblApps.ConPrRCN + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ConPrName1 + "," + tblApps.ConPrDesignation1 + "," + tblApps.ConPrOCN1 + "," + tblApps.ConPrRCN1 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ConPrName2 + "," + tblApps.ConPrDesignation2 + "," + tblApps.ConPrOCN2 + "," + tblApps.ConPrRCN2 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ConPrName3 + "," + tblApps.ConPrDesignation3 + "," + tblApps.ConPrOCN3 + "," + tblApps.ConPrRCN3 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.4 Product Manufactured By Company: " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Name Of Product: " + "," + " Main Customer: " + "," + "Supply(year):" + "," + "%of annual Turn over";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ProductName + "," + tblApps.MainCustomer + "," + tblApps.ComOfSupply + "," + tblApps.AnnualTurnOver + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ProductName1.Replace(",", " ") + "," + tblApps.MainCustomer1.Replace(",", " ") + "," + tblApps.ComOfSupply1.Replace(",", " ") + "," + tblApps.AnnualTurnOver1 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ProductName2.Replace(",", " ") + "," + tblApps.MainCustomer2.Replace(",", " ") + "," + tblApps.ComOfSupply2.Replace(",", " ") + "," + tblApps.AnnualTurnOver2 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.ProductName3.Replace(",", " ") + "," + tblApps.MainCustomer3.Replace(",", " ") + "," + tblApps.ComOfSupply3.Replace(",", " ") + "," + tblApps.AnnualTurnOver3 + ",";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.5 Certification Details: " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Is your company certified for" + Environment.NewLine + "," + "ISO/ QS/ TS/ TPM/ Any other quality system" + Environment.NewLine + "," + "(If yes then furnish the below details):" + "," + tblApps.ComCertificate ;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Name Of Quality System: " + "," + " Certifying Agency: " + "," + "Year of Certification: " + "," + "   ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfQuality.Replace(",", " ") + "," + tblApps.CertifyingAgency.Replace(",", " ") + "," + tblApps.YearOfCertification.Replace(",", " ") + "," + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfQuality1 + "," + tblApps.CertifyingAgency1 + "," + tblApps.YearOfCertification1 + "," + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfQuality2.Replace(",", " ") + "," + tblApps.CertifyingAgency2.Replace(",", " ") + "," + tblApps.YearOfCertification2.Replace(",", " ") + "," + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfQuality3.Replace(",", " ") + "," + tblApps.CertifyingAgency3.Replace(",", " ") + "," + tblApps.YearOfCertification3.Replace(",", " ") + "," + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.6 Do you have Bar-Coding"+ Environment.NewLine+"System for Material Supply?"+ Environment.NewLine+"If yes since which year: " + "," + tblApps.BarCode + "," + tblApps.BarCodeYear;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.7 Details Of Plant " + "," + "        " + "," + "     " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " a) Total Area(sq-mts):" + "," + tblApps.TotalArea.Replace(",", " ") + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " b) Total Covered Area(sq-mts):" + "," + tblApps.TotalCoveredArea.Replace(",", " ") + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " c) Connected Electrical Road(kva):" + "," + tblApps.ConnectedElectrical.Replace(",", " ") + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " d) Availability of alternate"+ Environment.NewLine+"   "+","+ "source of Electricity(In-House)" + Environment.NewLine + "   " + "," + "& Capacity (kva):" + "," + tblApps.AOASOEC.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.8 Partnership / Joint-venture" + Environment.NewLine+" / foreign collaboration / " + Environment.NewLine+"technical Tie -up ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + " Description & Name: " + "," + " Date  Of Association: " + "," + "Partnership: " + "," + " Remarks ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.DescriptionName + "," + tblApps.DateOfAssociation + "," + tblApps.Partnership + "," + tblApps.Remarks;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.DescriptionName1.Replace(",", " ") + "," + tblApps.DateOfAssociation1 + "," + tblApps.Partnership1.Replace(",", " ") + "," + tblApps.Remarks1.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.DescriptionName2.Replace(",", " ") + "," + tblApps.DateOfAssociation2 + "," + tblApps.Partnership2.Replace(",", " ") + "," + tblApps.Remarks2.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.DescriptionName3.Replace(",", " ") + "," + tblApps.DateOfAssociation3 + "," + tblApps.Partnership3.Replace(",", " ") + "," + tblApps.Remarks3.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.9 Other Details:" + "," + "        " + "," + "     " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " a) Installed Capacity per annum"+ Environment.NewLine +"  "+","+"(w.r.t BSL's Parts):" + "," + tblApps.ICPA.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + " b) Spare Capacity available" + Environment.NewLine + "  " + "," + " for BSL (in %):" + "," + tblApps.SCAFBSL;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "1.10  Mode of Material Transport:" + "," + tblApps.MOMT + "," + "     " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        if (tblApps.PlanFileName != null)
        //        {
        //            Col = "1.11 Future Expansion Plans:" + "," + "Details of Plan with Target Date:" + Environment.NewLine + LocalUrlFin + tblApps.PlanFileName ;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "1.11 Future Expansion Plans:" + "," + "Details of Plan with Target Date:"+"Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2. Financial's Details:" + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        if (tblApps.Financial_FileName != null)
        //        {
        //            Col = "2.1 Attach Balance Sheet for"+ Environment.NewLine + "last Years(as annexure):" + Environment.NewLine + LocalUrlFin + tblApps.Financial_FileName;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "2.1 Attach Balance Sheet for" + Environment.NewLine + "last Years(as annexure):" + "," + "Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = " 2.2 Company's Bankers Details:" + "," + "         " + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + " Name Of Bank :" + "," + "  Address " + "," + "   Telephone Nos " + "," + "      ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.BankName + "," + tblApps.BankAddress.Replace(",", " ") + "," + tblApps.BankTelephone + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.BankName1 + "," + tblApps.BankAddress1.Replace(",", " ") + "," + tblApps.BankTelephone1 + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.BankName2 + "," + tblApps.BankAddress2.Replace(",", " ") + "," + tblApps.BankTelephone2 + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.BankName3 + "," + tblApps.BankAddress3.Replace(",", " ") + "," + tblApps.BankTelephone3 + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2.3 Turnover Details of Previous Year:" + "," + "         " + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + " Name Of Customer :" + "," + "  Turn-Over(in Rs Lacs) " + "," + " Remarks " + "," + "      ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfCustomer + "," + tblApps.CustomerTurnover + "," + tblApps.CustomerRemarks.Replace(",", " ") + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfCustomer1 + "," + tblApps.CustomerTurnover1 + "," + tblApps.CustomerRemarks1.Replace(",", " ") + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfCustomer2 + "," + tblApps.CustomerTurnover2 + "," + tblApps.CustomerRemarks2.Replace(",", " ") + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.NameOfCustomer3 + "," + tblApps.CustomerTurnover3 + "," + tblApps.CustomerRemarks3.Replace(",", " ") + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2.4  Sales Tax No:" + "," + tblApps.SalesTaxNo + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2.5  Excise Registration No:" + "," + tblApps.ExciseRegNo + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2.6  Permanent A/C No:" + "," + tblApps.PAN + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "2.7  TDS A/C No:" + "," + tblApps.TDS + "," + "           " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3. Financial's Details:" + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " 3.1 Total Employees:" + "," + "     " + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  Permanent:   " + "," + " Unionised: " + "," + "  Non-Unionised:      " + "," + " Casuals  ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.PerEmp + "," + tblApps.UnionisedEmp + "," + tblApps.NonUnionisedEmp + "," + tblApps.CasualEmp;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  i) Managers  " + "," + tblApps.Managers + "," + "       " + "," + "  ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  ii) Supervisors:  " + "," + "Degree Holder" + "," + " Diploma Holders" + "," + " Others ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.Supervisiors + "," + tblApps.DegreeHolders + "," + tblApps.DiplomaHolders + "," + tblApps.Others;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  iii) Production:  " + "," + tblApps.Production + "    " + "," + "  ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "a) Direct " + "," + " ITI:   " + "," + "   Non-ITI  " + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.Direct + "," + tblApps.DirectITI + "," + tblApps.DirectNonITI + "," + "";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "b) Indirect " + "," + " ITI:   " + "," + "   Non-ITI  " + "," + "     ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + tblApps.Indirect + "," + tblApps.IndirectITI + "," + tblApps.IndirectNonITI + "," + "";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  iv) QA  " + "," + tblApps.QA + "," + "       " + "," + "  ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "    " + "," + "  v) Administration  " + "," + tblApps.Administration + "," + "       " + "," + "  ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.2 Do you have worker's Union" + "," + tblApps.WorkersUnion + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.3 Is There any Long Term" + Environment.NewLine+ "Settlement with the Union?" + "," + tblApps.LongTermSettleUnion;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.4 Give details of Strike Lock"+ Environment.NewLine+ "out go slow etc during the" + Environment.NewLine + "last 3 years if any" + "," + tblApps.Strike;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.5 How is the IR situation?" + "," + tblApps.IRSituation + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.6 Number of shifts timings" + "," + tblApps.ShiftTimings + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "3.7 Details Of Training Programs" + "," + "    " + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " " + "," + " a) Annual Training Plan? " + "," + tblApps.AnnualTrainingPlan.Replace(",", " ") + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " " + "," + " b)Training Programmes being" + Environment.NewLine +" "+","+ "planned (yearly Target) " + "," + tblApps.TrainingProgBeingPlan.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " " + "," + " c) Training Conducted Last year (in NOS) " + "," + tblApps.TrainingConductedInYear + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " " + "," + " d) Foreign Training, If any" + "," + "" + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = " " + "," + "Subject Covered:" + "," + "Managers:" + "," + " Staff" + "," + " Workmen ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "         " + "," + tblApps.ForeignSubManager + "," + tblApps.ForeignSubStaff + "," + tblApps.ForeignSubWorkmen;
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "4. Technical Details:" + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "            " + "," + "             " + "," + "               " + "," + "             " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "4.1 Details of Machinery & Equipment:" + "," + "     " + "," + "     " + "," + "        " + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "S.No:" + "," + "  Description and Make   " + "," + "   Qty  " + "," + " Type (GPM/SPM/CNC etc.) " + "," + "   Remarks / Purpose ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = tblApps.MachineEquipSNo + "," + tblApps.MachineEquipDescrip.Replace(",", " ") + "," + tblApps.MachineEquipQty + "," + tblApps.MachineEquipType.Replace(",", " ") + "," + tblApps.MachineEquipRemarks.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        if (tblApps.MachineFileName != null)
        //        {
        //            Col = "Details of other Machinary &" + Environment.NewLine+"Equipment (Attach separate sheet):" + Environment.NewLine + LocalUrlTech + tblApps.MachineFileName;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "Details of other Machinary &" + Environment.NewLine + "Equipment (Attach separate sheet):" + Environment.NewLine + "Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "4.2 Details of In-house Testing" + Environment.NewLine + "& Inspection Facilities:";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "S.No:" + "," + "  Description and Make   " + "," + "   Qty  " + "," + " Purpose/Remarks ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = tblApps.InhouseSNo + "," + tblApps.InhouseDescrip.Replace(",", " ") + "," + tblApps.InhouseQty + "," + tblApps.InhouseRemarks.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);

        //        if (tblApps.InHouseFileName != null)
        //        {
        //            Col = "Details of other In-house Testing &" + Environment.NewLine+"Inspection Facilities(Attach separate sheet):" + Environment.NewLine + LocalUrlTech + tblApps.InHouseFileName;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "Details of other In-house Testing &" + Environment.NewLine +"Inspection Facilities(Attach separate sheet):" + "," + "Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "4.3 R & D Facilities:" + "," + tblApps.RDFacility + "," + "            " + "," + "             " + "," + "             ";
        //        // (If yes attach separate sheet giving details e.g.CAD / CAM / CAE facilities trained Man-power etc.Also furnish details of products designed & developed during last two years) 
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);

        //        if (tblApps.RDFileName != null)
        //        {
        //            Col = "Details of other R&D" + Environment.NewLine+"Facilities (Attach separate sheet):" + Environment.NewLine + LocalUrlTech + tblApps.RDFileName ;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "Details of other R&D" + Environment.NewLine + "Facilities (Attach separate sheet):" + "," + "Not Uploaded" + "," + "         ";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }

        //        Col = "4.4 Source of Raw Material" + Environment.NewLine + "(BSL Related Product) ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "" + "," + "  S.No:   " + "," + "   Description  " + "," + " Source " + "," + "   Remarks ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "     " + "," + tblApps.RawMaterialSNo + "," + tblApps.RawMaterialDescrip.Replace(",", " ") + "," + tblApps.RawMaterialSource.Replace(",", " ") + "," + tblApps.RawMaterialRemarks.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);

        //        if (tblApps.RawMaterialFileName != null)
        //        {
        //            Col = "Details of other Raw Materials" + Environment.NewLine + "Facilities (Attach separate sheet):" + Environment.NewLine + LocalUrlTech + tblApps.RawMaterialFileName;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "Details of other Raw Materials Facilities" + Environment.NewLine + "(Attach separate sheet):"+ "," + "Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "4.5 Tracebility: (Kindly define" + Environment.NewLine + "your product Traceability System " + Environment.NewLine + "in separat sheet) " + "," + tblApps.Tracebility.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        if (tblApps.Non_Conforming_FileName != null)
        //        {
        //            Col = "4.6 Describe the Reaction Plan" + Environment.NewLine + "for Non-conforming products " + Environment.NewLine + "(Attach separate sheet):" + Environment.NewLine + LocalUrlTech + tblApps.Non_Conforming_FileName;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = " 4.6 Describe the Reaction Plan for" + Environment.NewLine + " Non-conforming products" + Environment.NewLine + " (Attach separate sheet):" + "," +"Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "4.7 Any Special Process/Facility:" + Environment.NewLine +  tblApps.SpecialFacility.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "4.8 Rejection Monitoring System:" + Environment.NewLine + tblApps.RejectionMonitoringSys.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "4.9 Customer Satisfaction Index/Trend:" + Environment.NewLine + tblApps.CusSatisfyIndex.Replace(",", " ");
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        if (tblApps.Inspection_FileName != null)
        //        {
        //            Col = "4.10 Give details of Inspection" + Environment.NewLine + "Procedures(Sampling Self-certification etc.):" + Environment.NewLine + LocalUrlTech + tblApps.Inspection_FileName ;
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Col = "4.10 Give details of Inspection" + Environment.NewLine + "Procedures(Sampling Self-certification etc.):"+ "," + "Not Uploaded";
        //            System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        }
        //        Col = "4.11 Exposure to National /" + Environment.NewLine + " International Standards ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "                  " + "," + "             " + "," + " Please Tick    " + "," + "Specify Nos." + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "         " + "," + "a)IS / ISO*" + "," + tblApps.ISISO.Replace(",", " ") + "," + tblApps.IS_ISO_Remark + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "         " + "," + "b)EN/BS*" + "," + tblApps.ENBS.Replace(",", " ") + "," + tblApps.EN_BS_Remark.Replace(",", " ") + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "         " + "," + "c)JIS/JASO*" + "," + tblApps.JISJASO.Replace(",", " ") + "," + tblApps.JIS_JASO_Remark.Replace(",", " ") + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "         " + "," + "d)DIN*" + "," + tblApps.DIN.Replace(",", " ") + "," + tblApps.DIN_Remark.Replace(",", " ") + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        Col = "         " + "," + "e)Any Others*" + "," + tblApps.AnyOthers.Replace(",", " ") + "," + tblApps.AnyOthers_Remark.Replace(",", " ") + "," + "             ";
        //        System.IO.File.AppendAllText(DestinationPath, Col + Environment.NewLine);
        //        byte[] fileBytes = System.IO.File.ReadAllBytes(@DestinationPath);
        //        var response = new FileContentResult(fileBytes, "application/octet-stream");
        //        response.FileDownloadName = s;
        //        return response;
        //    }
        //}
        public ActionResult VendorQueBSLUser()
        {
            string Supp_code = User.Identity.Name;
            if (User.IsInRole("Supplier"))
            {
                List<tblVendorQue> list = db.Database.SqlQuery<tblVendorQue>("exec GetAllVendorSupplier @SupplierName={0}", Supp_code).ToList();
                ViewBag.VendorQue = list;
            }
            else
            {
                List<tblVendorQue> list = db.Database.SqlQuery<tblVendorQue>("exec GetAllVendor").ToList();
                ViewBag.VendorQue = list;
            }
            //List<tblVendorQue> _comList = new List<tblVendorQue>();
            //List<tblVendorQue> _finList = new List<tblVendorQue>();
            //List<tblVendorQue> _hrList = new List<tblVendorQue>();
            //List<tblVendorQue> _tchList = new List<tblVendorQue>();


            //_comList = (from i in list
            //            where i.Active == 0 && i.FormType == "CompanyProfile"
            //            select new tblVendorQue
            //            {
            //                Id = i.Id,
            //                SUPP_CODE = i.SUPP_CODE,
            //                SUPP_NAME = i.SUPP_NAME,
            //                VenIntroName = i.VenIntroName,
            //                VenIntroDesignation = i.VenIntroDesignation,
            //                VenIntroDate = i.VenIntroDate,
            //                NameCompany = i.NameCompany,
            //                Regoffice = i.Regoffice,
            //                RegTelphone = i.RegTelphone,
            //                RegFax = i.RegFax,
            //                RegEmail = i.RegEmail,
            //                Headoffice = i.Headoffice,
            //                HeadTelphone = i.HeadTelphone,
            //                HeadFax = i.HeadFax,
            //                HeadEmail = i.HeadEmail,
            //                Plant = i.Plant,
            //                PlantTelephone = i.PlantTelephone,
            //                PlantFax = i.PlantFax,
            //                PlantEmail = i.PlantEmail,
            //                ConPrName = i.ConPrName,
            //                ConPrDesignation = i.ConPrDesignation,
            //                ConPrOCN = i.ConPrOCN,
            //                ConPrRCN = i.ConPrRCN,
            //                ProductName = i.ProductName,
            //                MainCustomer = i.MainCustomer,
            //                ComOfSupply = i.ComOfSupply,
            //                AnnualTurnOver = i.AnnualTurnOver,
            //                ComCertificate = i.ComCertificate,
            //                NameOfQuality = i.NameOfQuality,
            //                CertifyingAgency = i.CertifyingAgency,
            //                YearOfCertification = i.YearOfCertification,
            //                BarCode = i.BarCode,
            //                TotalArea = i.TotalArea,
            //                TotalCoveredArea = i.TotalCoveredArea,
            //                ConnectedElectrical = i.ConnectedElectrical,
            //                AOASOEC = i.AOASOEC,
            //                DescriptionName = i.DescriptionName,
            //                DateOfAssociation = i.DateOfAssociation,
            //                Partnership = i.Partnership,
            //                Remarks = i.Remarks,
            //                ICPA = i.ICPA,
            //                SCAFBSL = i.SCAFBSL,
            //                MOMT = i.MOMT,
            //                FEPlans = i.FEPlans,
            //                Active = i.Active,
            //                Status = i.Status
            //            }
            //            ).ToList();

            //_finList = (from i in list
            //            where i.Active == 0 && i.FormType == "Financial"
            //            select new tblVendorQue
            //            {
            //                Id = i.Id,
            //                VenIntroName = i.VenIntroName,
            //                VenIntroDesignation = i.VenIntroDesignation,
            //                VenIntroDate = i.VenIntroDate,
            //                BankName = i.BankName,
            //                BankAddress = i.BankAddress,
            //                BankTelephone = i.BankTelephone,
            //                NameOfCustomer = i.NameOfCustomer,
            //                CustomerTurnover = i.CustomerTurnover,
            //                CustomerRemarks = i.CustomerRemarks,
            //                SalesTaxNo = i.SalesTaxNo,
            //                ExciseRegNo = i.ExciseRegNo,
            //                PAN = i.PAN,
            //                TDS = i.TDS,
            //                Active = i.Active,
            //                Status = i.Status
            //            }
            //           ).ToList();

            //_hrList = (from i in list
            //           where i.Active == 0 && i.FormType == "HumanResource"
            //           select new tblVendorQue
            //           {
            //               Id = i.Id,
            //               VenIntroName = i.VenIntroName,
            //               VenIntroDesignation = i.VenIntroDesignation,
            //               VenIntroDate = i.VenIntroDate,
            //               PerEmp = i.PerEmp,
            //               UnionisedEmp = i.UnionisedEmp,
            //               NonUnionisedEmp = i.NonUnionisedEmp,
            //               CasualEmp = i.CasualEmp,
            //               Managers = i.Managers,
            //               Supervisiors = i.Supervisiors,
            //               Production = i.Production,
            //               QA = i.QA,
            //               Administration = i.Administration,
            //               DegreeHolders = i.DegreeHolders,
            //               DiplomaHolders = i.DiplomaHolders,
            //               Others = i.Others,
            //               Direct = i.Direct,
            //               Indirect = i.Indirect,
            //               DirectITI = i.DirectITI,
            //               DirectNonITI = i.DirectNonITI,
            //               IndirectITI = i.IndirectITI,
            //               IndirectNonITI = i.IndirectNonITI,
            //               WorkersUnion = i.WorkersUnion,
            //               LongTermSettleUnion = i.LongTermSettleUnion,
            //               Strike = i.Strike,
            //               IRSituation = i.IRSituation,
            //               ShiftTimings = i.ShiftTimings,
            //               AnnualTrainingPlan = i.AnnualTrainingPlan,
            //               TrainingProgBeingPlan = i.TrainingProgBeingPlan,
            //               ForeignSubManager = i.ForeignSubManager,
            //               ForeignSubStaff = i.ForeignSubStaff,
            //               ForeignSubWorkmen = i.ForeignSubWorkmen,
            //               Active = i.Active,
            //               Status = i.Status
            //           }
            //          ).ToList();

            //_tchList = (from i in list
            //            where i.Active == 0 && i.FormType == "Technical"
            //            select new tblVendorQue
            //            {
            //                Id = i.Id,
            //                VenIntroName = i.VenIntroName,
            //                VenIntroDesignation = i.VenIntroDesignation,
            //                VenIntroDate = i.VenIntroDate,
            //                MachineEquipSNo = i.MachineEquipSNo,
            //                MachineEquipDescrip = i.MachineEquipDescrip,
            //                MachineEquipQty = i.MachineEquipQty,
            //                MachineEquipType = i.MachineEquipType,
            //                MachineEquipRemarks = i.MachineEquipRemarks,
            //                InhouseSNo = i.InhouseSNo,
            //                InhouseDescrip = i.InhouseDescrip,
            //                InhouseQty = i.InhouseQty,
            //                InhouseType = i.InhouseType,
            //                InhouseRemarks = i.InhouseRemarks,
            //                RDFacility = i.RDFacility,
            //                RawMaterialSNo = i.RawMaterialSNo,
            //                RawMaterialDescrip = i.RawMaterialDescrip,
            //                RawMaterialSource = i.RawMaterialSource,
            //                RawMaterialRemarks = i.RawMaterialRemarks,
            //                Tracebility = i.Tracebility,
            //                Active = i.Active,
            //                Status = i.Status
            //            }
            //          ).ToList();

            ////List<tblVendorQue> financialList = db.Database.SqlQuery<tblVendorQue>("exec GetDataForFinancialDetail").ToList();            
            ////List<tblVendorQue> HRList = db.Database.SqlQuery<tblVendorQue>("exec GetDataForHumanResourceDetail").ToList();
            ////List<tblVendorQue> TechList = db.Database.SqlQuery<tblVendorQue>("exec GetDataForTechnicalDetail").ToList();


            //ViewBag.CompanyLst = _comList;
            //ViewBag.FinancialLst = _finList;
            //ViewBag.HRLst = _hrList;
            //ViewBag.TechLst = _tchList;

            return View();
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

        public ActionResult ECNSupp()
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