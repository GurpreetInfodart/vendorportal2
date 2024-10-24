 
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

namespace VendorPortal.Controllers
{
    public class InvoiceController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
       // List<InvoiceViewModel> objInvoiceList = null;
       // BasicPaging objBasicPaging = null;
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        //public PartialViewResult BuyerInvoice(int CurrentPage = 1)
        //{
        //    BasicPaging objBasicPaging = new BasicPaging();
        //    //binary@321#
        //    List<InvoiceViewModel> objInvoiceList  = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}", "Buyer").ToList();

        //    ViewBag.paging = objBasicPaging;
        //    objBasicPaging = new BasicPaging()
        //    {
        //        TotalItem = objInvoiceList.Count(),
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

        //    //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
        //    //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

        //    var pglst = objInvoiceList.Skip((CurrentPage - 1)* objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
        //    return PartialView("_BuyerInvoice", pglst);
        //}

        //public PartialViewResult SupplierInvoice(int CurrentPage = 1)
        //{
        //    BasicPaging objBasicPagingNew = new BasicPaging();
        //    //binary@321#
        //    List<InvoiceViewModel> objInvoiceListN = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier_Test @UserType={0}", "Buyer").ToList();

        //    ViewBag.paging2 = objBasicPagingNew;
        //    objBasicPagingNew = new BasicPaging()
        //    {
        //        TotalItem = objInvoiceListN.Count(),
        //        RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
        //        CurrentPage = CurrentPage
        //    };
        //    if (objBasicPagingNew.TotalItem % objBasicPagingNew.RowParPage == 0)
        //    {
        //        objBasicPagingNew.TotalPage = objBasicPagingNew.TotalItem / objBasicPagingNew.RowParPage;
        //    }
        //    else
        //        objBasicPagingNew.TotalPage = objBasicPagingNew.TotalItem / objBasicPagingNew.RowParPage + 1;
        //    ViewBag.paging2 = objBasicPagingNew;

        //    //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
        //    //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

        //    var pglst = objInvoiceListN.Skip((CurrentPage - 1) * objBasicPagingNew.RowParPage).Take(objBasicPagingNew.RowParPage);
        //    return PartialView("_SupplierInvoice", pglst);
        //}

        public ActionResult GenerateBuyerInvoice()
        {
          
            List<InvoiceViewModel> objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}", "Buyer").ToList();

             

            return View("GenerateBuyerInvoice", objInvoiceList);
        }
        public ActionResult GenerateSupplierInvoice()
        {
            string SupplierCode = "B10007";
            List<InvoiceViewModel> objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}", SupplierCode).ToList();
            return View("GenerateSupplierInvoice", objInvoiceList);
        }
        public ActionResult BuyerInvoiceDetail(int id)
        {
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @INV_ID={0}", id).ToList();
            return View("BuyerInvoiceDetail", objInvoiceList);            
        }
        public ActionResult SupplierInvoiceDetail(int id)
        {
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @INV_ID={0}", id).ToList();
            return View("SupplierInvoiceDetail", objInvoiceList);
        }
        public ActionResult GenerateBarcode(int  id)
        {
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @INV_ID={0}", id).ToList();
            string barCode = "";
            var supplierDet = db.SUPPLIER_MASTER.Where(x => x.SUPP_CODE == "B10007").ToList();
            string InvoiceNo = objInvoiceList[0].INVOICE_NUMBER;
            string InvoiceDate = objInvoiceList[0].INVOICE_DATE;

            ViewBag.Sup_Name = supplierDet[0].SUPP_NAME;
            ViewBag.InvoiceNo = InvoiceNo;
            ViewBag.invoiceDate = InvoiceDate;
            ViewBag.totData = objInvoiceList.Count();

            barCode = supplierDet[0].SUPP_NAME;
            barCode = barCode + "," + InvoiceNo + "," + objInvoiceList.Count() + "," + InvoiceDate.Replace("-", "") + ";";
            foreach (var dr in objInvoiceList)
            {
                barCode = barCode + Convert.ToString(dr.MAT_CODE) + "," + Convert.ToString(dr.Unit) + "," + Convert.ToString(dr.Qty) + "," + Convert.ToString(dr.CONTRACT_NO) + "," + Convert.ToString(dr.SCHEDULE_NO) + "," + Convert.ToString(dr.POSITION_NO) + "," + "2" + "," + Convert.ToString(dr.DEL_DATE) + ";";
                //Convert.ToString(dr[7]).Replace(".", "") + ";";
            }
            ViewBag.barcode = barCode;
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
    }
}