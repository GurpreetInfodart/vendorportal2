using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Supplier,Visitor")]
    public class SupplierPOController : Controller
    {
        
        VendorPortalEntities db = new VendorPortalEntities();
        // GET: SupplierPO
        List<SupplierPOViewModel> objSupplierPOList = null;
        BasicPaging objBasicPaging = null;

         

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SupplierPOPending(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string SupplierCode = User.Identity.Name;
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            objSupplierPOList = db.Database.SqlQuery<SupplierPOViewModel>("exec usp_GetSupplierPOData @SUPP_CODE={0}, @CurrentPage={1}, @TYPE={2} ,@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SupplierCode, CurrentPage, 0,fromDate,toDate,SearchBy,SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSupplierPOData_TotalCount @SUPP_CODE={0}, @TYPE={1} ,@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", SupplierCode,  0, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            

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

            // var pglst = objSupplierPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierPOPending", objSupplierPOList);
        }
        public PartialViewResult SupplierPOAcknoledge(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string SupplierCode = User.Identity.Name;
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            objSupplierPOList = db.Database.SqlQuery<SupplierPOViewModel>("exec usp_GetSupplierPOData @SUPP_CODE={0}, @CurrentPage={1}, @TYPE={2} ,@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SupplierCode, CurrentPage, 1,fromDate,toDate, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSupplierPOData_TotalCount @SUPP_CODE={0}, @TYPE={1} ,@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", SupplierCode, 1, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            var data = (from i in db.POes
                        join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
                        where i.STATUS == 1 && i.SUPP_CODE == SupplierCode
                        && ((fromDate != "" && toDate != "" && i.PO_DATE >= dtFrmDt && i.PO_DATE <= dtToDt)
                        || (fromDate == "" || toDate == "" && i.PO_DATE <= DateTime.Now))
                        group i by new { i.PO_NUM, i.PO_DATE, i.STATUS } into gss

                        select new { }
                     ).ToList();

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

            // var pglst = objSupplierPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierPOAcknoledge", objSupplierPOList);
        }
        public PartialViewResult SupplierPODecline(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string SupplierCode = User.Identity.Name;
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }
            objSupplierPOList = db.Database.SqlQuery<SupplierPOViewModel>("exec usp_GetSupplierPOData @SUPP_CODE={0} ,@CurrentPage={1}, @TYPE={2} ,@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SupplierCode, CurrentPage, 2, fromDate, toDate,SearchBy,SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSupplierPOData_TotalCount @SUPP_CODE={0}, @TYPE={1} ,@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", SupplierCode, 2, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            var data = (from i in db.POes
                        join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
                        where i.STATUS == 2 && i.SUPP_CODE ==SupplierCode
                        && ((fromDate != "" && toDate != "" && i.PO_DATE >= dtFrmDt && i.PO_DATE <= dtToDt)
                        || (fromDate == "" || toDate == "" && i.PO_DATE <= DateTime.Now))
                        group i by new { i.PO_NUM, i.PO_DATE, i.STATUS } into gss

                        select new { }
                     ).ToList();


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

            //  var pglst = objSupplierPOList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierPODecline", objSupplierPOList);
        }

        [HttpGet]
        public ActionResult ViewSupplierPO()
        {
            string SupplierCode = "A10080";


            List<SupplierPOViewModel> listPending = db.Database.SqlQuery<SupplierPOViewModel>("exec usp_GetSupplierPOData @SUPP_CODE={0}", SupplierCode).ToList();

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

            TempData["PendingData"] = getPO;
            TempData["AcknolodgeData"] = getAcknolodge;
            TempData["DeclineData"] = getDecline;

            ViewBag.pending_Data = getPO;
            ViewBag.Acknolodge_Data = getAcknolodge;
            ViewBag.Decline_Data = getDecline;

            return View("ViewSupplierPO");
        }

        public ActionResult SupplierPODetail(int flg = 0)
        {
            bool statusType = false;
            if (flg == 0)
            {
                statusType = true;
            }
            ViewBag.SetStatus = statusType;
            return View();
        }
        public PartialViewResult PODetail(string PO_Number, int CurrentPage = 1, int flg = 0, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            TempData["PO_Number"] = PO_Number;
            string SupplierCode = User.Identity.Name;
            bool statusType = false;
            List<SupplierPODetailViewModel> list = null;
            
                list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", PO_Number, CurrentPage, flg, fromDate, toDate, SearchBy, SearchValue).ToList();
            //if (flg == 0)
            //{
            //    list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0} ,@CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}
            //else if (flg == 1)
            //{
            //    list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0} ,@CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}
            //else
            //{
            //    list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0} ,@CurrentPage={1}, @TYPE={2}", PO_Number, CurrentPage, flg).ToList();
            //}

            if (flg == 0)
            {
                statusType = true;
            }

            var totalData = db.Database.SqlQuery<int>("exec usp_GetBuyerPODetail_TotalCount @PO_NUM={0}, @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", PO_Number, flg, fromDate, toDate, SearchBy, SearchValue).ToList();

            //var totalData = (from p in db.POes
            //                 join pd in db.PO_DETAILS on p.PO_NUM equals pd.PO_NUM
            //                 join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //                 join pm in db.Plant_Master on pd.PLANT_CODE equals pm.Plant_Code
            //                 join tm in db.tblMaterials on pd.MaterialCode equals tm.MaterialCode
            //                 where p.PO_NUM == PO_Number && p.STATUS == flg
            //                 select new { }
            //   ).ToList();

            //var data = list.Where(x => x.Status == 0).ToList();
            //if (data != null && data.Count > 0)
            //{
            //    statusType = true;
            //}
            ViewBag.SetStatus = statusType;

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

            //var pglst = list.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);

            return PartialView("_SupplierPODetail", list);
        }


        //public ActionResult SupplierPODetail(string PO_Number)
        //{
        //    TempData["PO_Number"] = PO_Number;

        //    bool statusType = false;
        //    List<SupplierPODetailViewModel> list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}", PO_Number).ToList();

        //    var data= list.Where(x => x.Status == 0).ToList();
        //    if (data != null && data.Count>0)
        //    {
        //        statusType = true;
        //    }
        //    ViewBag.SetStatus = statusType;
        //    return View("SupplierPODetail", data);
        //}

        public ActionResult SupplierAcknoledgePODetail(string PO_Number)
        { 
            List<SupplierPODetailViewModel> list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}", PO_Number).ToList();
            var data = list.Where(x => x.Status == 1).ToList();
            ViewBag.SetStatus = false;
            return View("SupplierPODetail", data);
        }

        public ActionResult SupplierDeclinePODetail(string PO_Number)
        {
            List<SupplierPODetailViewModel> list = db.Database.SqlQuery<SupplierPODetailViewModel>("exec usp_GetBuyerPODetail @PO_NUM={0}", PO_Number).ToList();
            var data = list.Where(x => x.Status == 2).ToList();
            ViewBag.SetStatus = false;
            return View("SupplierPODetail", data);
        }

        [HttpGet]
        public JsonResult ApprovedPOUpdateStatus(string Remark)
        {
            TempData.Keep();
            string PO_Num = (string)TempData["PO_Number"];

            using (VendorPortalEntities obj = new VendorPortalEntities())
            {
                //PO poes = obj.POes.Where(x => x.PO_NUM == PO_Num).FirstOrDefault();
                //poes.ACC_REJ_DateTime = DateTime.Now;
                //poes.STATUS = 1;
                //poes.ACC_REJ_Remark = Remark;
                //obj.SaveChanges();

                


                List<PO_DETAILS> poes = obj.PO_DETAILS.Where(x => x.PO_NUM == PO_Num).ToList();
                poes.ForEach(a =>
                {
                    a.ACC_REJ_ON = DateTime.Now;
                    a.STATUS = 1;
                    a.ACC_REJ_REMARKS = Remark;
                });

                obj.SaveChanges();
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RejectPOUpdateStatus(string Remark)
        {

            string PO_Num = (string)TempData.Peek("PO_Number");

            using (VendorPortalEntities obj = new VendorPortalEntities())
            {
              List<PO_DETAILS> poes = obj.PO_DETAILS.Where(x => x.PO_NUM == PO_Num).ToList();
                poes.ForEach(a =>
                {
                    a.ACC_REJ_ON = DateTime.Now;
                    a.STATUS = 2;
                    a.ACC_REJ_REMARKS = Remark;
                });
                
                obj.SaveChanges();
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}