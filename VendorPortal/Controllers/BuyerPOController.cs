
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Buyer,Admin,Administrator")]
    //[Authorize(Roles = "Supplier")]
    public class BuyerPOController : Controller
    {
        VendorPortalEntities db = new VendorPortalEntities();
        List<BuyerPOViewModel> objBuyerPOList = null;
        BasicPaging objBasicPaging = null;
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult BuyerPOPending(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
        
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            objBuyerPOList = db.Database.SqlQuery<BuyerPOViewModel>("exec usp_GetBuyerPOData @CurrentPage={0}, @TYPE={1} ,@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", CurrentPage, 0, fromDate, toDate, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetBuyerPOData_TotalCount @TYPE={0} ,@FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", 0, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var data = (from i in db.POes
            //            join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
            //            where i.STATUS == 0
            //            && ((fromDate != "" && toDate != "" && i.PO_DATE >= dtFrmDt && i.PO_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && i.PO_DATE <= DateTime.Now))
            //            group i by new { i.PO_NUM, i.PO_DATE, i.STATUS } into gss

            //            select new { }
            //          ).ToList();

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
            ViewBag.pending_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            // var pglst = objBuyerPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerPOPending", objBuyerPOList);
        }
        public PartialViewResult BuyerPOAcknoledge(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
        
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            objBuyerPOList = db.Database.SqlQuery<BuyerPOViewModel>("exec usp_GetBuyerPOData @CurrentPage={0}, @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", CurrentPage, 1, fromDate, toDate, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetBuyerPOData_TotalCount @TYPE={0} ,@FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", 1, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var data = (from i in db.POes
            //            join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
            //            where i.STATUS == 1
            //            && ((fromDate != "" && toDate != "" && i.PO_DATE >= dtFrmDt && i.PO_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && i.PO_DATE <= DateTime.Now))
            //            group i by new { i.PO_NUM, i.PO_DATE, i.STATUS } into gss
            //            select new { }
            //          ).ToList();

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
            ViewBag.Acknoledge_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objBuyerPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerPOAcknoledge", objBuyerPOList);
        }
        public PartialViewResult BuyerPODecline(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            //BasicPaging objBasicPaging = new BasicPaging();
     

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            objBuyerPOList = db.Database.SqlQuery<BuyerPOViewModel>("exec usp_GetBuyerPOData @CurrentPage={0}, @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", CurrentPage, 2, fromDate, toDate, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetBuyerPOData_TotalCount @TYPE={0} ,@FromDate={1}, @ToDate={2},@SearchBy={3},@SearchValue={4}", 2, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var data = (from i in db.POes
            //            join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
            //            where i.STATUS == 2
            //            && ((fromDate != "" && toDate != "" && i.PO_DATE >= dtFrmDt && i.PO_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && i.PO_DATE <= DateTime.Now))
            //            group i by new { i.PO_NUM, i.PO_DATE, i.STATUS } into gss
            //            select new { }
            //          ).ToList();

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
            ViewBag.decline_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objBuyerPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerPODecline", objBuyerPOList);
        }

        [HttpGet]
        public ActionResult ViewBuyerPO()
        {
            int pageSize = 3;
            //int pageIndex = 1;


            // pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            //IPagedList<POViewModel> _poPending = null;
            //IPagedList<POViewModel> _poAcknowledge = null;
            //IPagedList<POViewModel> _poDeclined = null;


            List<BuyerPOViewModel> listPending = db.Database.SqlQuery<BuyerPOViewModel>("exec usp_GetBuyerPOData").ToList();

            var getPO = (from po in listPending
                         where po.Status == 0
                         select new BuyerPOViewModel
                         {
                             PO_Number = po.PO_Number,
                             PO_Date = po.PO_Date,
                             Quantity = po.Quantity
                         }).ToList();

            var getAcknolodge = (from po in listPending
                                 where po.Status == 1
                                 select new BuyerPOViewModel
                                 {
                                     PO_Number = po.PO_Number,
                                     PO_Date = po.PO_Date,
                                     Quantity = po.Quantity
                                 }).ToList();

            var getDecline = (from po in listPending
                              where po.Status == 2
                              select new BuyerPOViewModel
                              {
                                  PO_Number = po.PO_Number,
                                  PO_Date = po.PO_Date,
                                  Quantity = po.Quantity
                              }).ToList();

            //_poPending = getPO.ToPagedList(pageIndex, pageSize);
            // _poPending = getPO.ToPagedList(pageIndex, pageSize);
            //   }


            TempData["PendingData"] = getPO;
            TempData["AcknolodgeData"] = getAcknolodge;
            TempData["DeclineData"] = getDecline;

            ViewBag.pending_Data = getPO;
            ViewBag.Acknolodge_Data = getAcknolodge;
            ViewBag.Decline_Data = getDecline;

            return View("ViewBuyerPO");
        }

        public ActionResult BuyerPODetail()
        {
            return View();
        }
        public PartialViewResult PODetail(string PO_Number, int CurrentPage = 1, int flg = 0, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            List<BuyerPODetailViewModel> list = null;
            list = db.Database.SqlQuery<BuyerPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}, @CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", PO_Number, CurrentPage, flg, fromDate, toDate, SearchBy, SearchValue).ToList();
            //if (flg == 0)
            //{
            //    list = db.Database.SqlQuery<BuyerPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}, @CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}
            //if (flg == 1)
            //{
            //    list = db.Database.SqlQuery<BuyerPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}, @CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}
            //else
            //{
            //    list = db.Database.SqlQuery<BuyerPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}, @CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}

            //var totalData = (from p in db.POes
            //                 join pd in db.PO_DETAILS on p.PO_NUM equals pd.PO_NUM
            //                 join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //                 join pm in db.Plant_Master on pd.PLANT_CODE equals pm.Plant_Code
            //                 join tm in db.tblMaterials on pd.MaterialCode equals tm.MaterialCode
            //                 where p.PO_NUM == PO_Number && p.STATUS == flg
            //                 select new { }
            //    ).ToList();

            var totalData = db.Database.SqlQuery<int>("exec usp_GetBuyerPODetail_TotalCount @PO_NUM={0}, @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", PO_Number, flg, fromDate, toDate, SearchBy, SearchValue).ToList();


            objBasicPaging = new BasicPaging()
            {
                TotalItem = totalData.FirstOrDefault(),
                RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
                CurrentPage = CurrentPage
            };
            if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
            {
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
            }
            else
                objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
            ViewBag.detail_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //   var pglst = list.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);

            return PartialView("_BuyerPODetail", list);
        }

        public ActionResult FilterPendingDataByDate(string FromDate, string ToDate)
        {
            List<BuyerPOViewModel> lstPo = null;
            lstPo = (List<BuyerPOViewModel>)TempData.Peek("PendingData");

            var dd = lstPo.AsEnumerable().Where(x => Convert.ToDateTime(x.PO_Date) >= Convert.ToDateTime(FromDate) && Convert.ToDateTime(x.PO_Date) <= Convert.ToDateTime(ToDate)).ToList();
            ViewBag.pending_Data = dd;
            return View("ViewPO");
        }

    }


}