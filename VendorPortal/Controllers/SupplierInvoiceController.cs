using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.UI;
using System.Threading;
using System.Resources;
using VendorPortal.Models;
using ZXing;
using ZXing.Common;
using System.Drawing.Imaging;
using System.Data.Entity;
using VendorPortal.Common;

namespace VendorPortal.Controllers
{
    public class SupplierInvoiceController : Controller
    {

        VendorPortalEntities db = new VendorPortalEntities();
        List<InvoiceViewModel> objInvoiceList = null;
        BasicPaging objBasicPaging = null;
        // GET: SupplierInvoice
        public ActionResult Index()
        {
            ViewBag.InvalidLink = false;
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = (Messages)TempData["Message"];
                TempData["Message"] = null;
                ViewBag.InvalidLink = true;
            }
            return View();
        }
        //public ActionResult Search(string search)
        //{
        //    string SupplierCode = User.Identity.Name;
        //    objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}, @CurrentPage={1},@FromDate={2}, @ToDate={3}", SupplierCode, 1, "", "").ToList();

        //    var _data = objInvoiceList.Where(x => x.INVOICE_NUMBER.StartsWith(search) || x.Qty == Convert.ToDecimal(search) || x.TotalAmt == Convert.ToDecimal(search));



        //    return View("_SupplierInvoice",_data);

        //}
        public PartialViewResult SupplierInvoice(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string SupplierCode = User.Identity.Name;

            if (SearchBy == "" || SearchValue=="")
            {
                SearchValue = "";
                SearchBy = "";
            }
           

            objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}, @CurrentPage={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", SupplierCode, CurrentPage, fromDate, toDate, SearchBy, SearchValue).ToList(); 

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }


            var totalCount = db.Database.SqlQuery<int>("exec GetInvoiceForBuyer_Supplier_TotalCount @UserType={0}, @FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", SupplierCode, fromDate, toDate, SearchBy, SearchValue).ToList();
             

            objBasicPaging = new BasicPaging()
            {
                TotalItem = totalCount.FirstOrDefault(),
                RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                CurrentPage = CurrentPage
            };

            if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
            {
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
            }
            else
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
            ViewBag.paging2 = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierInvoice", objInvoiceList);
        }
        public ActionResult SupplierInvoiceDetail()
        {
            return View();
        }
        public PartialViewResult InvoiceDetail(string InvoiceNo, string InvoiceDate, string Supp_Code, int CurrentPage = 1, string fromDate = "", string toDate = "")
        {
            //BasicPaging objBasicPagingNew = new BasicPaging();
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@CurrentPage={3},@FromDate={4}, @ToDate={5}", InvoiceNo, InvoiceDate, Supp_Code, CurrentPage, fromDate, toDate).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec uspGetInvoiceDetailFor_Buyer_Supplier_TotalCount @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@FromDate={3}, @ToDate={4}", InvoiceNo, InvoiceDate, Supp_Code, fromDate, toDate).ToList();
            //var totalCount = (from ivd in db.INVOICE_DETAILS
            //                  join iv in db.INVOICEs on ivd.INV_ID equals iv.ID
            //                  join tm in db.tblMaterials on ivd.MAT_CODE equals tm.MaterialCode
            //                  join p in db.POes on iv.POID equals p.ID
            //                  join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //                  where ivd.INV_ID == id
            //                  select new { }
            //               ).ToList();

            objBasicPaging = new BasicPaging()
            {
                TotalItem = totalCount.FirstOrDefault(),
                RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                CurrentPage = CurrentPage
            };
            if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
            {
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
            }
            else
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;

            ViewBag.SupplierInvoice_paging = objBasicPaging;
            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            TempData["InvoiceData"] = objInvoiceList;
            return PartialView("_SupplierInvoiceDetail", objInvoiceList);
        }
        public ActionResult GenerateBarcode(string InvoiceNo, string InvoiceDate, string Supp_Code, int CurrentPage = 1)
        {
            string SupplierCode = User.Identity.Name;
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGenerateInvoice_Barcode @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@CurrentPage={3}", InvoiceNo, InvoiceDate, Supp_Code, CurrentPage).ToList();
            string barCode = "";
            var supplierDet = db.SUPPLIER_MASTER.Where(x => x.SUPP_CODE == SupplierCode).ToList();
            //string Invoice_No = objInvoiceList[0].INVOICE_NUMBER;
            //string Invoice_Date = objInvoiceList[0].INVOICE_DATE;

            ViewBag.Sup_Name = SupplierCode + " ("+ supplierDet[0].SUPP_NAME+")";
            ViewBag.InvoiceNo = InvoiceNo;
            ViewBag.invoiceDate = InvoiceDate;
            ViewBag.totData = objInvoiceList.Count();

            barCode = SupplierCode;// supplierDet[0].SUPP_NAME;
            barCode = barCode + "," + InvoiceNo + "," + objInvoiceList.Count() + "," + InvoiceDate.Replace("/", "") + ";";
            foreach (var dr in objInvoiceList)
            {
                barCode = barCode + Convert.ToString(dr.MAT_CODE) + "," + Convert.ToString(dr.Unit) + "," + Convert.ToString(dr.Qty) + "," + Convert.ToString(dr.CONTRACT_NO) + "," + Convert.ToString(dr.SCHEDULE_NO) + "," + Convert.ToString(dr.POSITION_NO) + "," + "2" + "," + Convert.ToString(dr.DEL_DATE).Replace("/", "") + ";";
                //Convert.ToString(dr[7]).Replace(".", "") + ";";
            }
            ViewBag.barcode = barCode;
            TempData["InvoiceData"] = objInvoiceList;
            return View("GenerateBarcode", objInvoiceList);

        }

        public static byte[] BarcodeImageGenerator(string data)
        {
            if (data.Length > 1850)
            {
                data = data.Substring(0, 1500);
            }
            //try
            //{
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.PDF_417,
                Options = new EncodingOptions { Width = 800, Height = 500 } //optional
            };
            var imgBitmap = writer.Write(data);
            using (var stream = new MemoryStream())
            {
                imgBitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
            //}
            //catch (Exception EX)
            //{
            //  //  return BarcodeImageGenerator;

            //}
        }

        public ActionResult Bincard()
        {
            return View();
        }
        public PartialViewResult BincardDetail(string InvoiceNo, string InvoiceDate, string Supp_Code, int CurrentPage = 1)
        {
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@CurrentPage={3}", InvoiceNo, InvoiceDate, Supp_Code, CurrentPage).ToList();


            objBasicPaging = new BasicPaging()
            {
                TotalItem = objInvoiceList.Count(),
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

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);

            return PartialView("_BincardDetail", pglst);

        }

        public ActionResult GenerateBinCard(int id)
        {

            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = null;
            objInvoiceList = (List<Buyer_Supplier_InvoiceDetail>)TempData.Peek("InvoiceData");
            var _data = objInvoiceList.Where(x => x.ID == id).ToList();
            ViewBag.InvoiceNo = _data[0].INVOICE_NUMBER;
            ViewBag.invoiceDate = _data[0].INVOICE_DATE;
            ViewBag.SupCode = _data[0].SUPP_CODE;
            ViewBag.SupName = _data[0].SUPP_Name;

            ViewBag.ItemDesc = _data[0].MaterialDescription;
            ViewBag.SNP = _data[0].SNP;
            ViewBag.ItemCode = _data[0].MAT_CODE;
            ViewBag.TotQty = _data[0].Qty;
            ViewBag.Model = _data[0].MODEL;
            ViewBag.MANDATE = _data[0].MFG_DATE;
            ViewBag.BATCH = _data[0].BATCHCODE;

            int val = Convert.ToInt32(_data[0].Qty / _data[0].SNP);
            ViewBag.Qtybin = val;
            return View();
        }

        public ActionResult DeleteInvoice(string InvoiceNo, string InvoiceDate, string Supp_Code)
        {
            DateTime InvoiceDt = new DateTime();
            InvoiceDt = Convert.ToDateTime(InvoiceDate);

            

            var _data = db.INVOICEs.Where(x => x.INVOICE_NUMBER == InvoiceNo && x.INVOICE_DATE == InvoiceDt && x.SUPP_CODE == Supp_Code).FirstOrDefault();
            if (_data != null)
            {
                var _dataDet = db.INVOICE_DETAILS.Where(x => x.INV_ID == _data.ID).FirstOrDefault();

                _data.IsActive = 1;                
                db.Entry(_data).State = EntityState.Modified;
                db.SaveChanges();

                //db.INVOICE_DETAILS.Remove(_dataDet);
                //db.SaveChanges();

                //db.INVOICEs.Remove(_data);                
                //db.SaveChanges();

                Messages msg = new Messages();
                msg.Message_Id = 1;
                msg.Message = "Record Deleted Successfully.";
                TempData["Message"] = msg;


            }

            return RedirectToAction("Index");
            //return redirecttoaction("viewinvoicesupplier", new { subactid = subactid });
        }
    }
}