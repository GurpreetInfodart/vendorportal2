
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
    [Authorize(Roles = "Buyer,Admin,Administrator")]
    public class BuyerInvoiceController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
        List<InvoiceViewModel> objInvoiceList = null;
        BasicPaging objBasicPaging = null;
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult BuyerInvoice(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#

            objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}, @CurrentPage={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", "Buyer", CurrentPage, fromDate, toDate,SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            var totalCount = db.Database.SqlQuery<int>("exec GetInvoiceForBuyer_Supplier_TotalCount @UserType={0}, @FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", "Buyer", fromDate, toDate, SearchBy, SearchValue).ToList();

            //var totalCout = (from i in db.INVOICEs
            //                 join iv in db.INVOICE_DETAILS on i.ID equals iv.INV_ID
            //                 join sm in db.SUPPLIER_MASTER on i.SUPP_CODE equals sm.SUPP_CODE
            //                 where ((fromDate != "" && toDate != "" && i.INVOICE_DATE >= dtFrmDt && i.INVOICE_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && i.INVOICE_DATE <= DateTime.Now))
            //            && i.IsActive != 1
            //                 group new { i, iv, sm } by new { i.INVOICE_NUMBER, i.SUPP_CODE, sm.SUPP_NAME, i.INVOICE_DATE, iv.TOTAL_VAL_INC } into gs
            //                 select new { }
            //    ).ToList();

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
            ViewBag.paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerInvoice", objInvoiceList);
        }

        //public PartialViewResult SupplierInvoice(int CurrentPage = 1)
        //{
        //    //BasicPaging objBasicPagingNew = new BasicPaging();

        //     objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier_Test @UserType={0}", "Buyer").ToList();

        //    ViewBag.paging2 = objBasicPaging;
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
        //    ViewBag.paging2 = objBasicPaging;

        //    //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
        //    //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

        //    var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
        //    return PartialView("_SupplierInvoice", pglst);
        //}

        public ActionResult GenerateBuyerInvoice()
        {

            List<InvoiceViewModel> objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}", "Buyer").ToList();
            return View("GenerateBuyerInvoice", objInvoiceList);
        }
        //public ActionResult GenerateSupplierInvoice()
        //{
        //    string SupplierCode = "B10007";
        //    List<InvoiceViewModel> objInvoiceList = db.Database.SqlQuery<InvoiceViewModel>("exec GetInvoiceForBuyer_Supplier @UserType={0}", SupplierCode).ToList();
        //    return View("GenerateSupplierInvoice", objInvoiceList);
        //}
        public ActionResult BuyerInvoiceDetail()
        {
            return View();
        }

        public PartialViewResult InvoiceDetail(string InvoiceNo, string InvoiceDate, string Supp_Code, int CurrentPage = 1, string fromDate = "", string toDate = "")
        {
            //BasicPaging objBasicPagingNew = new BasicPaging();
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@CurrentPage={3},@FromDate={4}, @ToDate={5}", InvoiceNo, InvoiceDate, Supp_Code, CurrentPage,fromDate,toDate).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec uspGetInvoiceDetailFor_Buyer_Supplier_TotalCount @InvoiceNo={0},@InvoiceDate={1},@Supp_Code={2},@FromDate={3}, @ToDate={4}", InvoiceNo, InvoiceDate, Supp_Code, fromDate, toDate).ToList();

            //var totalCount = (from ivd in db.INVOICE_DETAILS
            //                  join iv in db.INVOICEs on ivd.INV_ID equals iv.ID
            //                  join tm in db.tblMaterials on ivd.MAT_CODE equals tm.MaterialCode
            //                  join p in db.POes on iv.POID equals p.ID
            //                  join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //                  where ivd.INV_ID == id
            //                  select new { }
            //                ).ToList();


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

            ViewBag.buyInvoice_paging = objBasicPaging;
            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objInvoiceList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerInvoiceDetail", objInvoiceList);
        }

        //public PartialViewResult InvoiceDetail(int id)
        //{
        //    List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @INV_ID={0}", id).ToList();
        //    return View("BuyerInvoiceDetail", objInvoiceList);
        //}
        public ActionResult SupplierInvoiceDetail(int id)
        {
            List<Buyer_Supplier_InvoiceDetail> objInvoiceList = db.Database.SqlQuery<Buyer_Supplier_InvoiceDetail>("exec uspGetInvoiceDetailFor_Buyer_Supplier @INV_ID={0}", id).ToList();
            return View("SupplierInvoiceDetail", objInvoiceList);
        }

    }
}