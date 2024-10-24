using ExcelDataReader;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VendorPortal.Common;
using VendorPortal.Models;


namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Supplier,Visitor")]
    public class SupplierScheduleController : Controller
    {


        List<SchedulerViewModel> list = null;
        VendorPortalEntities db = new VendorPortalEntities();
        // GET: Schedule

        List<SchedulerViewModel> objSchedulerViewModelList = null;
        BasicPaging objBasicPaging = null;

        public ActionResult Index()
        {



            if (User.Identity.IsAuthenticated)
            {
            }
            string ddd = User.Identity.Name;
            return View();
        }

        public PartialViewResult SupplierPendingSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string Supp_code = User.Identity.Name;

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", Supp_code, CurrentPage, 0, fromDate, toDate, SearchBy, SearchValue).ToList();
            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", Supp_code, 0, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }
            DateTime dtNow = DateTime.Now.AddMonths(-2);
            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 0 && p.SUPP_CODE == Supp_code
            //                  && ((fromDate != "" && toDate != "" && sh.UploadDate >= dtFrmDt && sh.UploadDate <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.UploadDate <= DateTime.Now))
            //            && sh.CreatedOn >= dtNow
            //                 group sh by new { sh.UploadDate, p.SUPP_CODE, sh.Status } into gdd
            //                 select new { }
            //   ).ToList();

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

            var pglst = objSchedulerViewModelList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierPendingSchedule", objSchedulerViewModelList);
        }
        public PartialViewResult SupplierAcknowledgeSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string Supp_code = User.Identity.Name;
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", Supp_code, CurrentPage, 1, fromDate, toDate, SearchBy, SearchValue).ToList();
            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", Supp_code, 1, fromDate, toDate, SearchBy, SearchValue).ToList();


            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }
            DateTime dtNow = DateTime.Now.AddMonths(-2);
            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 1 && p.SUPP_CODE == Supp_code
            //                  && ((fromDate != "" && toDate != "" && sh.UploadDate >= dtFrmDt && sh.UploadDate <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.UploadDate <= DateTime.Now))
            //             && sh.CreatedOn >= dtNow
            //                 group sh by new { sh.UploadDate, p.SUPP_CODE, sh.Status } into gdd
            //                 select new { }
            //  ).ToList();

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

            //var pglst = objSchedulerViewModelList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierAcknowledgeSchedule", objSchedulerViewModelList);
        }
        public PartialViewResult SupplierDeclineSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string Supp_code = User.Identity.Name;

            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", Supp_code, CurrentPage, 2, fromDate, toDate, SearchBy, SearchValue).ToList();
            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", Supp_code, 2, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }
            DateTime dtNow = DateTime.Now.AddMonths(-2);
            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 2 && p.SUPP_CODE == Supp_code
            //                  && ((fromDate != "" && toDate != "" && sh.UploadDate >= dtFrmDt && sh.UploadDate <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.UploadDate <= DateTime.Now))
            //             && sh.CreatedOn >= dtNow
            //                 group sh by new { sh.UploadDate, p.SUPP_CODE, sh.Status } into gdd
            //                 select new { }
            //).ToList();

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

            //var pglst = objSchedulerViewModelList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_SupplierDeclineSchedule", objSchedulerViewModelList);
        }
        public ActionResult SupplierScheduleDetail(int flg = 0)
        {
            bool statusType = false;
            if (flg == 0)
            {
                statusType = true;
            }
            ViewBag.SetStatus = statusType;
            return View();
        }
        public PartialViewResult ScheduleDetail(string SUPP_CODE, string upload_date, int CurrentPage = 1, int flg = 0, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            string Supp_code = User.Identity.Name;
            bool statusType = false;
            List<SchedulerViewModel> list = null;
            list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0} ,@CurrentPage={1}, @TYPE={2},@UploadDate={3},@FromDate={4}, @ToDate={5},@SearchBy={6},@SearchValue={7}", SUPP_CODE, CurrentPage, flg, upload_date, fromDate, toDate, SearchBy, SearchValue).ToList();
            //if (flg == 0)
            //{
            //    list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0} ,@CurrentPage={1}, @TYPE={2}", SUPP_CODE, CurrentPage, 0).ToList();
            //}
            //else if (flg == 1)
            //{
            //    list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0} ,@CurrentPage={1}, @TYPE={2}", SUPP_CODE, CurrentPage, 1).ToList();
            //}
            //else
            //{
            //    list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0} ,@CurrentPage={1}, @TYPE={2}", SUPP_CODE, CurrentPage, 2).ToList();
            //}
            var TotalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerDataBySuppCde_TotalCount @SUPP_CODE={0}, @TYPE={1},@UploadDate={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SUPP_CODE, flg, upload_date, fromDate, toDate, SearchBy, SearchValue).ToList();
            if (flg == 0)
            {
                statusType = true;
            }

            //var data = (from sh in db.Schedules

            //            join p in db.POes on sh.POID equals p.ID
            //            join pd in db.PO_DETAILS on p.PO_NUM equals pd.PO_NUM
            //            join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //            join pm in db.Plant_Master on pd.PLANT_CODE equals pm.Plant_Code
            //            join tm in db.tblMaterials on sh.MAT_CODE equals tm.MaterialCode
            //            where p.SUPP_CODE == SUPP_CODE && sh.Status == flg
            //            select new { }
            //     ).ToList();

            ViewBag.SetStatus = statusType;

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
            ViewBag.detail_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = list.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);

            return PartialView("_SupplierScheduleDetail", list);
        }
        public JsonResult ApprovedStatus(string values)
        {
            string Supp_code = User.Identity.Name;
            List<Master_Schedule_Dtls> msd = new List<Master_Schedule_Dtls>();
            // var dd= JObject
            var model = new JavaScriptSerializer().Deserialize<List<Master_Schedule_Dtls>>(values);


            var scheduleId = model.Select(x => x.ScheduleId);
            var rmk = model.Select(x => x.Remark);

            using (VendorPortalEntities obj = new VendorPortalEntities())
            {
                List<Schedule> sh = obj.Schedules.Where(x => scheduleId.Contains(x.ID)).ToList();

                int index = 0;
                foreach (Schedule objsh in sh)
                {
                    objsh.REMARKS = model[index].Remark;
                    objsh.Status = 1;
                    index++;
                }
                obj.SaveChanges();

            }

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeclineStatus(string values)
        {
            List<Master_Schedule_Dtls> msd = new List<Master_Schedule_Dtls>();
            // var dd= JObject
            var model = new JavaScriptSerializer().Deserialize<List<Master_Schedule_Dtls>>(values);


            var scheduleId = model.Select(x => x.ScheduleId);
            var rmk = model.Select(x => x.Remark);

            using (VendorPortalEntities obj = new VendorPortalEntities())
            {
                List<Schedule> sh = obj.Schedules.Where(x => scheduleId.Contains(x.ID)).ToList();

                int index = 0;
                foreach (Schedule objsh in sh)
                {
                    objsh.REMARKS = model[index].Remark;
                    objsh.Status = 2;
                    index++;
                }
                obj.SaveChanges();

            }

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

         
        public ActionResult ScheduleUpdateInvoice()
        {
            


            return View();
        }
        //public PartialViewResult UpdateInvoice(string SUPP_CODE, string upload_date, int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        //{            
        //    List<ScheduleUpdateInvoiceViewModel> objScheduleUpdateInvoiceList = null;
        //    objScheduleUpdateInvoiceList = db.Database.SqlQuery<ScheduleUpdateInvoiceViewModel>("exec usp_GetInvoiceUpdateData @SUPP_CODE={0}, @CurrentPage={1},@UploadDate={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SUPP_CODE, CurrentPage, upload_date, fromDate, toDate, SearchBy, SearchValue).ToList();


        //   // var TotalCount = db.Database.SqlQuery<int>("exec usp_GetInvoiceUpdateData_TotalCount @SUPP_CODE={0},@UploadDate={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", SUPP_CODE, upload_date, fromDate, toDate, SearchBy, SearchValue).ToList();

        //    //objBasicPaging = new BasicPaging()
        //    //{
        //    //    TotalItem = TotalCount.FirstOrDefault(),
        //    //    RowParPage = Convert.ToInt32(CommanResource.RecordsPerPage),
        //    //    CurrentPage = CurrentPage
        //    //};
        //    //if (objBasicPaging.TotalItem % objBasicPaging.RowParPage == 0)
        //    //{
        //    //    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage;
        //    //}
        //    //else
        //    //    objBasicPaging.TotalPage = objBasicPaging.TotalItem / objBasicPaging.RowParPage + 1;
        //    //ViewBag.paging = objBasicPaging;

        //    var CGSTLst = (new List<SelectListItem>
        //    {
        //        new SelectListItem{Text="Select",Value="-1",Selected=true },
        //        new SelectListItem{Text="0",Value="0" },
        //        new SelectListItem{Text="2.5",Value="2.5" },
        //        new SelectListItem{Text="6",Value="6" },
        //        new SelectListItem{Text="9",Value="9" },
        //        new SelectListItem{Text="14",Value="14" }
        //    }).ToList();

        //    ViewData["CGST"] = CGSTLst;

        //    var IGSTLst = (new List<SelectListItem>
        //    {
        //        new SelectListItem{Text="Select",Value="-1",Selected=true },
        //        new SelectListItem{Text="0",Value="0" },
        //        new SelectListItem{Text="5",Value="5" },
        //        new SelectListItem{Text="12",Value="12" },
        //        new SelectListItem{Text="18",Value="18" },
        //        new SelectListItem{Text="28",Value="28" }
        //    }).ToList();

        //    ViewData["IGST"] = IGSTLst;



        //    return PartialView("_UpdateInvoice", objScheduleUpdateInvoiceList);
        //}

        public JsonResult UpdateInvoice(string SUPP_CODE, string upload_date, int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            List<ScheduleUpdateInvoiceViewModel> objScheduleUpdateInvoiceList = null;
            objScheduleUpdateInvoiceList = db.Database.SqlQuery<ScheduleUpdateInvoiceViewModel>("exec usp_GetInvoiceUpdateData @SUPP_CODE={0}, @CurrentPage={1},@UploadDate={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", SUPP_CODE, CurrentPage, upload_date, fromDate, toDate, SearchBy, SearchValue).ToList();

             
            var CGSTLst = (new List<SelectListItem>
            {
                new SelectListItem{Text="Select",Value="-1",Selected=true },
                new SelectListItem{Text="0",Value="0" },
                new SelectListItem{Text="2.5",Value="2.5" },
                new SelectListItem{Text="6",Value="6" },
                new SelectListItem{Text="9",Value="9" },
                new SelectListItem{Text="14",Value="14" }
            }).ToList();

            ViewData["CGST"] = CGSTLst;

            var IGSTLst = (new List<SelectListItem>
            {
                new SelectListItem{Text="Select",Value="-1",Selected=true },
                new SelectListItem{Text="0",Value="0" },
                new SelectListItem{Text="5",Value="5" },
                new SelectListItem{Text="12",Value="12" },
                new SelectListItem{Text="18",Value="18" },
                new SelectListItem{Text="28",Value="28" }
            }).ToList();

            ViewData["IGST"] = IGSTLst;


            
            return Json(objScheduleUpdateInvoiceList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUpdateInvoice(string values)
        //public ActionResult GetUpdateInvoice(List<TestViewModel> mdl)
        {
           // string values="";
            string InvoiceNo = string.Empty;
            string Invoice_Date = string.Empty;
            string msg = string.Empty;

            string SUPP_Code = User.Identity.Name;

            Int64 invoiceId = 0;
            decimal Tot_CALC_VALUE_EXC = 0;
            decimal Tot_CALC_VALUE_INC = 0;

            var model = new JavaScriptSerializer().Deserialize<List<ScheduleUpdateInvoiceViewModel>>(values);

            List<INVOICE_DETAILS> objInvoiceDetList = new List<INVOICE_DETAILS>();


            var SuppGSTIN = (from i in db.SUPPLIER_MASTER
                             where i.SUPP_CODE == SUPP_Code
                             select new { i.GSTIN }

                ).ToList();

            var invNo = model.Select(e => e.InvoiceNo).FirstOrDefault();

            bool _validate = false;
            var _data = db.INVOICEs.Where(i => i.INVOICE_NUMBER == invNo && i.SUPP_CODE == SUPP_Code && (i.IsActive==0 || i.IsActive==null)).ToList();

            if (_data != null && _data.Count > 0)
            {
                DateTime _datPre = new DateTime();
                DateTime _datNxt = new DateTime();

                int CurrentYear = DateTime.Today.Year;
                int PreviousYear = DateTime.Today.Year - 1;
                int NextYear = DateTime.Today.Year + 1;
                string PreYear = PreviousYear.ToString();
                string NexYear = NextYear.ToString();
                string CurYear = CurrentYear.ToString();
                string FinYear = null;

                if (DateTime.Today.Month > 3)
                {
                    FinYear = CurYear + "-" + NexYear;
                    _datPre = Convert.ToDateTime(CurYear + "-04-01");
                    _datNxt = Convert.ToDateTime(NexYear + "-03-31");
                }

                else
                {
                    FinYear = PreYear + "-" + CurYear;
                    _datPre = Convert.ToDateTime(PreYear + "-04-01");
                    _datNxt = Convert.ToDateTime(CurYear + "-03-31");
                }

                bool validateInc = false;

                //1/

                foreach (var dat in _data)
                {
                    if (dat.INVOICE_DATE >= _datPre.Date && dat.INVOICE_DATE <= _datNxt.Date)
                    {
                        validateInc = true;
                    }
                      //validateInc =  i.INVOICE_DATE >= _datPre.Date && i.INVOICE_DATE <= _datNxt.Date);
                }

                if (validateInc)
                {
                    _validate = true;
                }

                //if (_validateInvoice != null)
                //{
                //    _validate = true;
                //}
            }


            if (!_validate)
            {
                int flag = 1;
                foreach (var item in model)
                {
                    //item.InvoiceNo


                    //InvoiceDetail objInvoiceDetail = new InvoiceDetail();


                    //var unitPrice = (from i in db.PO_DETAILS
                    //                 where i.MaterialCode == item.MaterialCode && i.PO_NUM == item.PO_NUM
                    //                 select new
                    //                 {
                    //                     i.UNIT_RATE
                    //                 }
                    //               ).ToList();


                    //if(_validate)
                    //{ 

                    DateTime delvDate = new DateTime();
                    delvDate = Convert.ToDateTime(item.ScheduleDate);
                    var PODetail = (from i in db.POes
                                    join a in db.PO_DETAILS on i.PO_NUM equals a.PO_NUM
                                    where i.PO_NUM == item.PO_NUM && a.MaterialCode == item.MaterialCode
                                    select new
                                    {
                                        a.UNIT_RATE,
                                        i.ID,
                                        a.HSN_SAC,
                                        a.CGST,
                                        a.IGST,
                                        a.SGST,
                                        a.CESS

                                    }).ToList();

                    var poId = PODetail[0].ID;

                    var scheduleDetail = (from i in db.Schedules
                                          where i.POID == poId && i.MAT_CODE == item.MaterialCode
                                          select new
                                          {
                                              i.ID,
                                              i.SCHEDULE_NO
                                          }).ToList();

                    var PositionNo = (from s in db.Schedules
                                      where s.POID == poId && s.MAT_CODE == item.MaterialCode
                                      && s.DEL_DATE == delvDate
                                      select new
                                      {
                                          s.Position
                                      }).ToList();

                    DateTime invoiceDate = new DateTime();
                    DateTime mfgDate = new DateTime();
                    // DateTime delvDate = new DateTime();

                    if (string.IsNullOrEmpty(item.MFG_Date))
                    {

                    }
                    else
                    {
                        mfgDate = Convert.ToDateTime(item.MFG_Date);
                    }
                    invoiceDate = Convert.ToDateTime(item.InvoiceDate);
                    delvDate = Convert.ToDateTime(item.ScheduleDate);

                    var _totalPrice = Convert.ToDecimal(item.InvoiceQty) * Convert.ToDecimal(PODetail[0].UNIT_RATE);
                    var _cgstAmt = _totalPrice * item.CGSTax * Convert.ToDecimal(0.01);
                    var _sgstAmt = _totalPrice * item.SGSTax * Convert.ToDecimal(0.01);
                    var _igstAmt = _totalPrice * item.IGSTax * Convert.ToDecimal(0.01);

                    var CALC_VALUE_INC = _totalPrice + _cgstAmt + _sgstAmt + _igstAmt;
                    var CALC_VALUE_EXC = _totalPrice;

                    string strCalcValueIng = Convert.ToString(CALC_VALUE_INC);
                    string strCalcValueExc = Convert.ToString(_totalPrice);

                    //objInvoiceMDL.INVOICE_NUMBER = item.InvoiceNo;
                    //objInvoiceMDL.SUPP_CODE = SUPP_Code;
                    //objInvoiceMDL.SUPPLIER_GSTIN = SuppGSTIN[0].GSTIN;
                    //objInvoiceMDL.SUPPLIER_CIN = "";
                    //objInvoiceMDL.INVOICE_DATE = invoiceDate;
                    //objInvoiceMDL.POID = PODetail[0].ID;
                    //objInvoiceMDL.HSN_SAC_NO = PODetail[0].HSN_SAC;
                    //objInvoiceMDL.CREATEDBY = 1;
                    //objInvoiceMDL.CREATEDON = DateTime.Now;


                    if (flag == 1)
                    {
                        InvoiceNo = item.InvoiceNo;
                        Invoice_Date = invoiceDate.ToString("dd/MM/yyyy");

                        INVOICE objInvoice = new INVOICE()
                        {
                            INVOICE_NUMBER = item.InvoiceNo,
                            SUPP_CODE = SUPP_Code,
                            SUPPLIER_GSTIN = SuppGSTIN[0].GSTIN,
                            SUPPLIER_CIN = "",
                            INVOICE_DATE = invoiceDate,
                            POID = PODetail[0].ID,
                            HSN_SAC_NO = PODetail[0].HSN_SAC,
                            CREATEDBY = 1,
                            CREATEDON = DateTime.Now
                        };
                        db.INVOICEs.Add(objInvoice);
                        db.SaveChanges();
                        invoiceId = objInvoice.ID;
                    }
                    INVOICE_DETAILS objInvoiceDet = new INVOICE_DETAILS()
                    {
                        INV_ID = invoiceId,
                        MAT_CODE = item.MaterialCode,
                        QTY = item.InvoiceQty,
                        RATE = PODetail[0].UNIT_RATE,
                        AMOUNT = _totalPrice,
                        CGST = PODetail[0].CGST,
                        CGST_AMT = _cgstAmt,
                        IGST = PODetail[0].IGST,
                        IGST_AMT = _igstAmt,
                        SGST = PODetail[0].SGST,
                        SGST_IMT = _sgstAmt,
                        CESS = PODetail[0].CESS,
                        CREATED_BY = 1,
                        CREATED_ON = DateTime.Now,
                        MODEL = item.Model,
                        MFG_DATE = mfgDate,
                        BATCHCODE = item.BatchCode,
                        CALC_VALUE_EXC = CALC_VALUE_EXC,
                        CALC_VALUE_INC = CALC_VALUE_INC,
                        SCHEDULE_NO = scheduleDetail[0].SCHEDULE_NO,
                        DEL_DATE = delvDate,
                        CONTRACT_NO = "",
                        POSITION_NO = PositionNo[0].Position,
                        CGST_PER = item.CGSTax,
                        SGST_PER = item.SGSTax,
                        IGST_PER = item.IGSTax,
                        SCH_ID = scheduleDetail[0].ID,
                        STATUS = 0,
                        TOTAL_VAL_EXC = 0,
                        TOTAL_VAL_INC = 0
                    };

                    //objInvoiceDetList.Add(objInvoiceDet);

                    Tot_CALC_VALUE_EXC += CALC_VALUE_EXC;
                    Tot_CALC_VALUE_INC += CALC_VALUE_INC;

                    flag++;
                    db.INVOICE_DETAILS.Add(objInvoiceDet);

                }

                using (VendorPortalEntities vb = new VendorPortalEntities())
                {
                    db.SaveChanges();
                    string dd = "";

                    (from user in db.INVOICE_DETAILS
                     where user.INV_ID == invoiceId
                     select user).ToList().ForEach((user) =>
                      {
                          user.TOTAL_VAL_EXC = Tot_CALC_VALUE_EXC;
                          user.TOTAL_VAL_INC = Tot_CALC_VALUE_INC;

                      });
                    db.SaveChanges();

                    bool result = SendmailInvoice(InvoiceNo, Invoice_Date);
                    if (!result)
                    {
                        msg = "Data Updated Successfully But Mail Sent Failed.";
                    }
                    else
                    {
                        msg = "Data Updated Successfully.";
                    }
                }
            }
            else
            {
                msg = "Invocice No already exists for this supplier or financial year.";
            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UploadInvoice()
        {
            ViewBag.InvalidLink = false;
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = (Messages)TempData["Message"];
                TempData["Message"] = null;

                List<Invalid_Invoice_Bulk> _objInvalid = (List<Invalid_Invoice_Bulk>)TempData.Peek("InValidExcel");
                if (_objInvalid != null && _objInvalid.Count > 0)
                {
                    ViewBag.InvalidLink = true;
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult UploadExcelData(UploadInvoiceViewModel _objUploadInvoiceViewModel)
        {
            string _strInvoiceNo = "";
            string _strInvoiceDate = "";
            bool sv = false;
            INVOICE objInvList = new INVOICE();
            List<INVOICE_DETAILS> objInvDetList = new List<INVOICE_DETAILS>();

            List<Invalid_Invoice_Bulk> InvalidBulk = new List<Invalid_Invoice_Bulk>();
            string Supp_code = User.Identity.Name;
            HttpPostedFileBase Upload = _objUploadInvoiceViewModel.ExcelFile;
            if (Upload != null && Upload.ContentLength > 0)
            {
                Stream stream = Upload.InputStream;
                StringBuilder sb = new StringBuilder();
                IExcelDataReader reader = null;

                if (Upload.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (Upload.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    ModelState.AddModelError("File", "This file format not supported.");

                    ViewBag.InvalidLink = false;
                    Messages msg = new Messages();
                    msg.Message_Id = 0;
                    msg.Message = "This file format not supported";
                    ViewBag.Msg = msg;
                    return View("UploadInvoice");
                }

                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                DataTable dtRosterData = result.Tables[0];
                if (dtRosterData != null && dtRosterData.Rows.Count > 0)
                {
                    dtRosterData = dtRosterData.Rows
                          .Cast<DataRow>()
                          .Where(row => !row.ItemArray.All(field => field is DBNull ||
                                   string.IsNullOrWhiteSpace(field as string)))
                          .CopyToDataTable();

                    DataSet result1 = reader.AsDataSet();
                    int ColCount = reader.FieldCount;

                    if (ColCount != 8)
                    {

                        ModelState.AddModelError("File", "This file format not supported.");

                        ViewBag.InvalidLink = false;
                        Messages msg = new Messages();
                        msg.Message_Id = 0;
                        msg.Message = "This file format not supported";
                        ViewBag.Msg = msg;
                        return View("UploadInvoice");
                    }

                    DataTable dtRoster = new DataTable();
                    dtRoster.Columns.Add("PONUMBER", typeof(string));
                    dtRoster.Columns.Add("MATERIALCODE", typeof(string));
                    dtRoster.Columns.Add("DelDATE", typeof(string));
                    dtRoster.Columns.Add("INVOICENUMBER", typeof(string));
                    dtRoster.Columns.Add("INVOICEDATE", typeof(string));
                    dtRoster.Columns.Add("INVOICEQTY", typeof(string));
                    dtRoster.Columns.Add("CGST", typeof(string));
                    dtRoster.Columns.Add("IGST", typeof(string));

                    var SuppGSTIN = (from i in db.SUPPLIER_MASTER
                                     where i.SUPP_CODE == Supp_code
                                     select new { i.GSTIN }

                   ).ToList();

                    foreach (DataRow dr in dtRosterData.Rows)
                    {
                        DataRow vdr = dtRoster.NewRow();
                        vdr["PONUMBER"] = dr["PONUMBER"];
                        vdr["MATERIALCODE"] = dr["MATERIALCODE"];
                        vdr["DelDATE"] = dr["DelDATE"];
                        vdr["INVOICENUMBER"] = dr["INVOICENUMBER"];
                        vdr["INVOICEDATE"] = dr["INVOICEDATE"];
                        vdr["INVOICEQTY"] = dr["INVOICEQTY"];
                        vdr["CGST"] = dr["CGST"];
                        vdr["IGST"] = dr["IGST"];
                        dtRoster.Rows.Add(vdr);
                    }


                    string material = ""; string DelDate = ""; string Quantity = ""; string PONum = ""; string SNP = ""; string ScheduleQty = "";
                    string InvNo = ""; string InvDate = ""; string CGST = "";
                    string CGSTPER = ""; string SGSTPER = ""; string SGST = ""; string IGSTPER = ""; string IGST = "";
                    string materialDesc1 = ""; string UOM1 = "";

                    PONum = dtRosterData.Rows[0][0].ToString();
                    material = dtRosterData.Rows[0][1].ToString();
                    DelDate = dtRosterData.Rows[0][2].ToString();
                    InvNo = dtRosterData.Rows[0][3].ToString();
                    InvDate = dtRosterData.Rows[0][4].ToString();
                    Quantity = dtRosterData.Rows[0][5].ToString();
                    CGSTPER = dtRosterData.Rows[0][6].ToString();
                    IGSTPER = dtRosterData.Rows[0][7].ToString();


                    //if (PONum != "PONUMBER" || material != "MATERIALCODE" || DelDate != "DelDATE" || InvNo != "INVOICENUMBER" || InvDate != "INVOICEDATE" || Quantity != "INVOICEQTY" || CGSTPER != "CGST" || IGSTPER != "IGST")
                    //{
                    //    ModelState.AddModelError("File", "Wrong file Uploaded");
                    //    return View("UploadInvoice");
                    //}

                    List<TableInvoice> InvoiceList = new List<TableInvoice>();
                    List<INVOICE_DETAILS> objInvoiceDetList = new List<INVOICE_DETAILS>();




                    DataRow EmptyMandatoryFieldCount = (from DataRow r in (dtRoster).Rows
                                                        where Convert.ToString(r["PONUMBER"]).Equals(string.Empty)
                                                        || Convert.ToString(r["MATERIALCODE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["DelDATE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICENUMBER"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICEDATE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICEQTY"]).Equals(string.Empty)
                                                        || Convert.ToString(r["CGST"]).Equals(string.Empty)
                                                        || Convert.ToString(r["IGST"]).Equals(string.Empty)
                                                        select r
                                                      ).FirstOrDefault();


                    InvalidBulk = dtRoster.AsEnumerable().Where(r => Convert.ToString(r["PONUMBER"]).Equals(string.Empty)
                                                        || Convert.ToString(r["MATERIALCODE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["DelDATE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICENUMBER"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICEDATE"]).Equals(string.Empty)
                                                        || Convert.ToString(r["INVOICEQTY"]).Equals(string.Empty)
                                                        || Convert.ToString(r["CGST"]).Equals(string.Empty)
                                                        || Convert.ToString(r["IGST"]).Equals(string.Empty)

                        )
                        .Select(dr => new Invalid_Invoice_Bulk
                        {

                            PoNumber = Convert.ToString(dr["PONUMBER"]),
                            MaterialCode = Convert.ToString(dr["MATERIALCODE"]),
                            DelDate = Convert.ToString(dr["DelDATE"]),
                            InvoiceNumber = Convert.ToString(dr["INVOICENUMBER"]),
                            InvoiceDate = Convert.ToString(dr["INVOICEDATE"]),
                            InvoiceQty = Convert.ToString(dr["INVOICEQTY"]),
                            CGST = Convert.ToString(dr["CGST"]),
                            IGST = Convert.ToString(dr["IGST"]),
                            Remark = "PO Number, MATERIAL CODE, Del DATE, INVOICE NUMBER, INVOICE DATE, INVOICE QTY, CGST, IGST Are Mandotry.",
                        })
                        .ToList();

                    if (EmptyMandatoryFieldCount == null)
                    {
                        if (dtRoster != null)
                        {
                            var _invoiceLst = (from myRow in dtRoster.AsEnumerable()
                                               select new
                                               {
                                                   INVOICENUMBER = myRow.Field<string>("INVOICENUMBER")

                                               }).Distinct().ToList();


                            var _invoiceDate = (from myRow in dtRoster.AsEnumerable()
                                                select new
                                                {
                                                    InvoiceDate = myRow.Field<string>("InvoiceDate")

                                                }).Distinct().ToList();

                            if (_invoiceLst.Count() == 1)
                            {
                                if (_invoiceDate.Count() == 1)
                                {

                                    string InvoiceNoumber = _invoiceLst[0].INVOICENUMBER;

                                    _strInvoiceNo = _invoiceLst[0].INVOICENUMBER;
                                    _strInvoiceDate = _invoiceDate[0].InvoiceDate;

                                    bool _validate = false;

                                    var _data = db.INVOICEs.Where(x => x.INVOICE_NUMBER == InvoiceNoumber && x.SUPP_CODE == Supp_code).ToList();

                                    if (_data != null && _data.Count > 0)
                                    {
                                        DateTime _datPre = new DateTime();
                                        DateTime _datNxt = new DateTime();

                                        int CurrentYear = DateTime.Today.Year;
                                        int PreviousYear = DateTime.Today.Year - 1;
                                        int NextYear = DateTime.Today.Year + 1;
                                        string PreYear = PreviousYear.ToString();
                                        string NexYear = NextYear.ToString();
                                        string CurYear = CurrentYear.ToString();
                                        string FinYear = null;

                                        if (DateTime.Today.Month > 3)
                                        {
                                            FinYear = CurYear + "-" + NexYear;
                                            _datPre = Convert.ToDateTime(CurYear + "-04-01");
                                            _datNxt = Convert.ToDateTime(NexYear + "-03-31");
                                        }

                                        else
                                        {
                                            FinYear = PreYear + "-" + CurYear;
                                            _datPre = Convert.ToDateTime(PreYear + "-04-01");
                                            _datNxt = Convert.ToDateTime(CurYear + "-03-31");
                                        }

                                        var _validateInvoice = _data.Where(x => x.INVOICE_DATE >= _datPre.Date && x.INVOICE_DATE <= _datNxt.Date);
                                        if (_validateInvoice != null)
                                        {
                                            _validate = true;
                                        }
                                    }
                                    if (!_validate)
                                    {
                                        //for (int a = 0; a < _invoiceLst.Count(); a++)
                                        //{
                                        Int64 _invoiceId = 0;
                                        bool status = false;
                                        //var var_List = from myRow in dtRoster.AsEnumerable()
                                        //               where myRow.Field<string>("INVOICENUMBER") == _invoiceLst[0].INVOICENUMBER
                                        //               select myRow;


                                        //DataTable dt = var_List.CopyToDataTable();
                                        DataTable dt = dtRoster.Copy();
                                        decimal Tot_CALC_VALUE_EXC = 0;
                                        decimal Tot_CALC_VALUE_INC = 0;
                                        int flg = 0;
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {


                                            PONum = dt.Rows[i][0].ToString().Trim();

                                            material = dt.Rows[i][1].ToString().Trim();
                                            //if (material != "")
                                            //{
                                            //    var materialDesc = (from mat in db.tblMaterials
                                            //                        where mat.MaterialCode == material
                                            //                        select new { mat.MaterialDescription }).FirstOrDefault();
                                            //    var UOM = (from mat in db.tblMaterials
                                            //               where mat.MaterialCode == material
                                            //               select new { mat.Unit }).FirstOrDefault();
                                            //    if (materialDesc != null)
                                            //    {
                                            //        materialDesc1 = materialDesc.MaterialDescription.ToString().Trim();
                                            //        UOM1 = UOM.Unit.ToString().Trim();
                                            //    }
                                            //    else
                                            //    {
                                            //        materialDesc1 = "";
                                            //        UOM1 = "";
                                            //    }
                                            //}

                                            DelDate = dt.Rows[i]["DelDATE"].ToString().Trim();
                                            InvNo = dt.Rows[i]["INVOICENUMBER"].ToString().Trim();
                                            InvDate = dt.Rows[i]["INVOICEDATE"].ToString().Trim();
                                            Quantity = dt.Rows[i]["INVOICEQTY"].ToString().Trim();
                                            CGSTPER = dt.Rows[i]["CGST"].ToString().Trim();
                                            IGSTPER = dt.Rows[i]["IGST"].ToString().Trim();


                                            DateTime delvDate = new DateTime();
                                            delvDate = Convert.ToDateTime(DelDate);
                                            DateTime dateInv1 = Convert.ToDateTime(InvDate);


                                            var _chkAckStatus = (from d in db.Schedules
                                                                 join p in db.POes on d.POID equals p.ID
                                                                 where p.PO_NUM == PONum && d.MAT_CODE == material && p.SUPP_CODE == Supp_code && d.DEL_DATE == delvDate
                                                                 select new { d.Status }).FirstOrDefault();
                                            if (_chkAckStatus != null && _chkAckStatus.Status == 1)
                                            {



                                                int j = 0;
                                                j = i + 1;


                                                //if (DelDate.Contains("/") || DelDate.Contains("-"))
                                                //{
                                                //    sb.Append("DelDate is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                //    ModelState.AddModelError("File", sb.ToString());
                                                //    return View();
                                                //}

                                                var chk_poNum = (from p in db.POes where p.PO_NUM == PONum select new { p.PO_NUM }).FirstOrDefault();
                                                if (chk_poNum == null)
                                                {
                                                    sb.Append("PO Number does not exists.");
                                                }
                                                else
                                                {

                                                     
                                                    if (DelDate.Contains("/") || DelDate.Contains("-"))
                                                    {
                                                        sb.Append("DelDate is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                    }


                                                    var regexItem = new Regex(@"(\d{2}).(\d{2}).(\d{4})");

                                                    if (!regexItem.IsMatch(DelDate))
                                                    {
                                                        sb.Append("Del Date is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                        ModelState.AddModelError("File", sb.ToString());
                                                        return View();
                                                    }

                                                     

                                                    if (InvDate.Contains("/") || InvDate.Contains("-"))
                                                    {
                                                        sb.Append("InvDate is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                    }

                                                    if (!regexItem.IsMatch(InvDate))
                                                    {
                                                        sb.Append("Invoice Date is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                        ModelState.AddModelError("File", sb.ToString());
                                                        return View();
                                                    }
                                                    //if (InvDate.Contains("/") || InvDate.Contains("-"))
                                                    //{
                                                    //    sb.Append("InvDate is not poper format of row no " + j + "in Uploaded Excel file. Please Use (dd.MM.yyyy)format." + "\r\n");
                                                    //    ModelState.AddModelError("File", sb.ToString());
                                                    //    return View();
                                                    //}


                                                    DateTime dateInv = DateTime.Now;

                                                    //string strDate = dateInv.ToString("dd.MM.yyyy");
                                                    if (dateInv1 > dateInv)
                                                    {
                                                        sb.Append("InvDate is greater than todays date. of row no " + j + "in Uploaded Excel file." + "\r\n");
                                                    }

                                                    var Quantity1 = new Regex(@"\d");
                                                    if (!Quantity1.IsMatch(Quantity))
                                                    {
                                                        sb.Append("Quantity is not proper of row no " + j + "in Uploaded Excel file." + "\r\n");
                                                    }


                                                    if ((CGSTPER == "0" || CGSTPER == "") && (IGSTPER == "0" || IGSTPER == ""))
                                                    {
                                                        sb.Append("CGST/IGST botn can not be blank or 0 " + "\r\n");
                                                    }

                                                    var CGSTPER1 = new Regex(@"\d");
                                                    if (!CGSTPER1.IsMatch(CGSTPER))
                                                    {
                                                        sb.Append("CGST is not proper format of row no " + j + "in Uploaded Excel file." + "\r\n");
                                                    }
                                                    var IGSTPER1 = new Regex(@"\d");
                                                    if (!IGSTPER1.IsMatch(IGSTPER))
                                                    {
                                                        sb.Append("IGST  is not proper format of row no " + j + "in Uploaded Excel file." + "\r\n");
                                                    }
                                                    if (CGSTPER != "0" && IGSTPER != "0")
                                                    {
                                                        sb.Append("Both tax are not allowed CGST /IGST or not empty. of row no " + j + "in Uploaded Excel file." + "\r\n");
                                                    }

                                                    int Dupcount = dt.Select("PONUMBER= '" + PONum + "' AND DelDATE = '" + DelDate + "' AND INVOICEDATE= '" + InvDate + "' and INVOICENUMBER = '" + InvNo + "' and MATERIALCODE= '" + material + "'").Count();
                                                    if (Dupcount > 1)
                                                    {
                                                        sb.Append("material duplicate found in File.MATCODE  '" + material + "' AND Del Date '" + DelDate + "' AND Invoice Number'" + InvNo + "' AND PoNumber '" + PONum + "'");
                                                        //ModelState.AddModelError("File", "material duplicate found in File. MATCODE  '" + material + "' AND Del Date '" + DelDate + "' AND Invoice Number'" + InvNo + "' AND PoNumber '" + PONum + "' ");
                                                        //return View();
                                                    }


                                                    var SNPQty = (from m in db.tblMaterials
                                                                  where m.MaterialCode == material
                                                                  select new { m.SNP }).ToList();

                                                    var QCount = (from iv in db.INVOICEs
                                                                  where iv.INVOICE_NUMBER == InvNo && iv.SUPP_CODE == Supp_code
                                                                  select new { iv.INVOICE_NUMBER }).FirstOrDefault();
                                                    if (QCount != null)
                                                    {
                                                        sb.Append(InvNo + " Invoice Number Already Exists.");
                                                    }
                                                    if (PONum != "" && Supp_code != "")
                                                    {
                                                        var poNum = (from p in db.POes where p.SUPP_CODE == Supp_code && p.PO_NUM == PONum select new { p.PO_NUM }).FirstOrDefault();
                                                        if (poNum == null)
                                                        {
                                                            sb.Append(PONum + " PO Number or Supplier Code are Invalid." + "\r\n");
                                                        }
                                                        else
                                                        {
                                                            if (PONum != "" && Supp_code != "" && DelDate != "")
                                                            {


                                                                var chkDelDate = (from d in db.Schedules
                                                                                  join p in db.POes on d.POID equals p.ID
                                                                                  where p.PO_NUM == PONum && d.MAT_CODE == material && p.SUPP_CODE == Supp_code && d.DEL_DATE == delvDate
                                                                                  select new { d.DEL_DATE }).FirstOrDefault();

                                                                if (chkDelDate == null)
                                                                {
                                                                    sb.Append(DelDate + " Del Date is Invalid" + "\r\n");
                                                                }

                                                            }

                                                            if (CGSTPER != "")
                                                            {
                                                                if (CGSTPER != "0" && CGSTPER != "2.5" && CGSTPER != "6" && CGSTPER != "9" && CGSTPER != "14")
                                                                {
                                                                    sb.Append(CGSTPER + " CGST is Invalid" + "\r\n");
                                                                }
                                                            }
                                                            if (IGSTPER != "")
                                                            {
                                                                if (IGSTPER != "0" && IGSTPER != "5" && IGSTPER != "12" && IGSTPER != "18" && IGSTPER != "28")
                                                                {
                                                                    sb.Append(CGSTPER + " IGST is Invalid" + "\r\n");
                                                                }
                                                            }
                                                            if (InvNo != "" && DelDate != "" && material != "")
                                                            {
                                                                var schQty = (from d in db.Schedules
                                                                              join p in db.POes on d.POID equals p.ID
                                                                              where p.PO_NUM == PONum && d.MAT_CODE == material && p.SUPP_CODE == Supp_code && d.DEL_DATE == delvDate
                                                                              select new { d.Qty }).FirstOrDefault();

                                                                if (schQty != null)
                                                                {
                                                                    if (Convert.ToDecimal(Quantity) > schQty.Qty)
                                                                    {
                                                                        sb.Append(material + " Material and Del Date " + DelDate + " Quantity is exceeds" + "\r\n");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    sb.Append(material + " Quantity not available." + "\r\n");
                                                                }
                                                            }
                                                            if (SNPQty.Count != 0)
                                                            {
                                                                var totalQty = Convert.ToDecimal(Quantity) / Convert.ToDecimal(SNPQty[0].SNP);
                                                                if (totalQty == 00)
                                                                {
                                                                    sb.Append(material + " Material and Del Date " + DelDate + " SNP NOT MATCH PLEASE CHANGE QUANTITY" + "\r\n");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                sb.Append(" SNP Not Available of This Material " + material + "\r\n");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                sb.Append("This PO " + PONum + " is not Acknowledged or Invalid. " + "\r\n");
                                            }

                                            //ModelState.AddModelError("File", sb.ToString());
                                            if (sb.Length > 0)
                                            {
                                                Invalid_Invoice_Bulk objInvalid = new Invalid_Invoice_Bulk();
                                                objInvalid.PoNumber = dt.Rows[i]["PONUMBER"].ToString().Trim();
                                                objInvalid.MaterialCode = dt.Rows[i]["MATERIALCODE"].ToString().Trim();
                                                objInvalid.DelDate = dt.Rows[i]["DelDATE"].ToString().Trim();
                                                objInvalid.InvoiceNumber = dt.Rows[i]["INVOICENUMBER"].ToString().Trim();
                                                objInvalid.InvoiceDate = dt.Rows[i]["INVOICEDATE"].ToString().Trim();
                                                objInvalid.InvoiceQty = dt.Rows[i]["INVOICEQTY"].ToString().Trim();
                                                objInvalid.CGST = dt.Rows[i]["CGST"].ToString().Trim();
                                                objInvalid.IGST = dt.Rows[i]["IGST"].ToString().Trim();
                                                objInvalid.Remark = Convert.ToString(sb);

                                                InvalidBulk.Add(objInvalid);
                                                sb.Clear();
                                                sv = true;
                                            }
                                            else
                                            {
                                                var InvLst = (from myRow in dtRoster.AsEnumerable()
                                                              where myRow.Field<string>("INVOICENUMBER") == InvNo
                                                              select new
                                                              {
                                                                  INVOICEDATE = myRow.Field<string>("INVOICEDATE"),
                                                                  PONUMBER = myRow.Field<string>("PONUMBER"),

                                                              }).ToList();

                                                DateTime dtInvoiceDt = new DateTime();
                                                dtInvoiceDt = Convert.ToDateTime(InvLst[0].INVOICEDATE);

                                                string _matCode = dt.Rows[i]["MATERIALCODE"].ToString().Trim();
                                                string _poNo = InvLst[0].PONUMBER;

                                                var PODetail = (from po in db.POes
                                                                join pd in db.PO_DETAILS on po.PO_NUM equals pd.PO_NUM
                                                                where po.PO_NUM == _poNo && pd.MaterialCode == _matCode
                                                                select new
                                                                {
                                                                    pd.UNIT_RATE,
                                                                    po.ID,
                                                                    pd.HSN_SAC,
                                                                    pd.CGST,
                                                                    pd.IGST,
                                                                    pd.SGST,
                                                                    pd.CESS

                                                                }).ToList();

                                                Int64 _poId = PODetail[0].ID;

                                                var scheduleDetail = (from s in db.Schedules
                                                                      where s.POID == _poId && s.MAT_CODE == _matCode
                                                                      select new
                                                                      {
                                                                          s.ID,
                                                                          s.SCHEDULE_NO
                                                                      }).ToList();


                                                var PositionNo = (from s in db.Schedules
                                                                  where s.POID == _poId && s.MAT_CODE == _matCode
                                                                  && s.DEL_DATE == delvDate
                                                                  select new
                                                                  {
                                                                      s.Position
                                                                  }).ToList();


                                                DateTime invoiceDate = new DateTime();
                                                DateTime mfgDate = new DateTime();


                                                //if (string.IsNullOrEmpty(item.MFG_Date))
                                                //{

                                                //}
                                                //else
                                                //{
                                                //    mfgDate = Convert.ToDateTime(item.MFG_Date);
                                                //}
                                                invoiceDate = Convert.ToDateTime(dateInv1);
                                                delvDate = Convert.ToDateTime(delvDate);

                                                var _totalPrice = Convert.ToDecimal(dt.Rows[i]["INVOICEQTY"].ToString().Trim()) * Convert.ToDecimal(PODetail[0].UNIT_RATE);
                                                var _cgstAmt = _totalPrice * Convert.ToDecimal(dt.Rows[i]["CGST"].ToString().Trim()) * Convert.ToDecimal(0.01);
                                                var _sgstAmt = _totalPrice * Convert.ToDecimal(dt.Rows[i]["CGST"].ToString().Trim()) * Convert.ToDecimal(0.01);
                                                var _igstAmt = _totalPrice * Convert.ToDecimal(dt.Rows[i]["IGST"].ToString().Trim()) * Convert.ToDecimal(0.01);

                                                var CALC_VALUE_INC = _totalPrice + _cgstAmt + _sgstAmt + _igstAmt;
                                                var CALC_VALUE_EXC = _totalPrice;

                                                string strCalcValueIng = Convert.ToString(CALC_VALUE_INC);
                                                string strCalcValueExc = Convert.ToString(_totalPrice);

                                                if (flg == 0)
                                                {
                                                    INVOICE objInv = new INVOICE()
                                                    {
                                                        INVOICE_NUMBER = _invoiceLst[0].INVOICENUMBER,
                                                        SUPP_CODE = Supp_code,
                                                        SUPPLIER_GSTIN = SuppGSTIN[0].GSTIN,
                                                        SUPPLIER_CIN = "",
                                                        INVOICE_DATE = dtInvoiceDt,
                                                        POID = PODetail[0].ID,
                                                        HSN_SAC_NO = PODetail[0].HSN_SAC,
                                                        CREATEDBY = 1,
                                                        CREATEDON = DateTime.Now
                                                    };
                                                    objInvList = objInv;
                                                    //db.INVOICEs.Add(objInvoice);
                                                    // db.SaveChanges();
                                                    // _invoiceId = objInvList.ID;
                                                    flg = 1;
                                                }
                                                //status = true;
                                                INVOICE_DETAILS objInvoiceDet = new INVOICE_DETAILS()
                                                {
                                                    // INV_ID = _invoiceId,
                                                    MAT_CODE = dt.Rows[i]["MATERIALCODE"].ToString().Trim(),
                                                    QTY = Convert.ToDecimal(dt.Rows[i]["INVOICEQTY"].ToString().Trim()),
                                                    RATE = PODetail[0].UNIT_RATE,
                                                    AMOUNT = _totalPrice,
                                                    CGST = PODetail[0].CGST,
                                                    CGST_AMT = _cgstAmt,
                                                    IGST = PODetail[0].IGST,
                                                    IGST_AMT = _igstAmt,
                                                    SGST = PODetail[0].SGST,
                                                    SGST_IMT = _sgstAmt,
                                                    CESS = PODetail[0].CESS,
                                                    CREATED_BY = 1,
                                                    CREATED_ON = DateTime.Now,
                                                    MODEL = "",
                                                    MFG_DATE = null,
                                                    BATCHCODE = "",
                                                    CALC_VALUE_EXC = CALC_VALUE_EXC,
                                                    CALC_VALUE_INC = CALC_VALUE_INC,
                                                    SCHEDULE_NO = scheduleDetail[0].SCHEDULE_NO,
                                                    DEL_DATE = delvDate,
                                                    CONTRACT_NO = "",
                                                    POSITION_NO = PositionNo[0].Position,
                                                    CGST_PER = Convert.ToDecimal(dt.Rows[i]["CGST"].ToString().Trim()),
                                                    SGST_PER = Convert.ToDecimal(dt.Rows[i]["CGST"].ToString().Trim()),
                                                    IGST_PER = Convert.ToDecimal(dt.Rows[i]["IGST"].ToString().Trim()),
                                                    SCH_ID = scheduleDetail[0].ID,
                                                    STATUS = 0,
                                                    TOTAL_VAL_EXC = 0,
                                                    TOTAL_VAL_INC = 0
                                                };
                                                objInvDetList.Add(objInvoiceDet);
                                                //  db.INVOICE_DETAILS.Add(objInvoiceDet);


                                                Tot_CALC_VALUE_EXC += CALC_VALUE_EXC;
                                                Tot_CALC_VALUE_INC += CALC_VALUE_INC;

                                                //db.SaveChanges();


                                                InvoiceList.Add(new TableInvoice(PONum, material, materialDesc1, UOM1, DelDate, InvNo, InvDate, Quantity, CGSTPER, IGSTPER));
                                            }
                                        }

                                        if (!sv)
                                        {

                                            db.INVOICEs.Add(objInvList);
                                            db.SaveChanges();
                                            _invoiceId = objInvList.ID;

                                            foreach (INVOICE_DETAILS item in objInvDetList)
                                            {
                                                item.INV_ID = _invoiceId;
                                                db.INVOICE_DETAILS.Add(item);
                                                db.SaveChanges();
                                                status = true;
                                            }


                                            if (status == true)
                                            {
                                                var getUpdate_Tol_Val_Exc_Inc = db.INVOICE_DETAILS.Where(x => x.INV_ID == _invoiceId).ToList();
                                                if (getUpdate_Tol_Val_Exc_Inc != null)
                                                {
                                                    getUpdate_Tol_Val_Exc_Inc.ForEach(m => m.TOTAL_VAL_EXC = Tot_CALC_VALUE_EXC);
                                                    //db.Entry(getUpdate_Tol_Val_Exc_Inc).State = System.Data.Entity.EntityState.Modified;
                                                    db.SaveChanges();

                                                    getUpdate_Tol_Val_Exc_Inc.ForEach(m => m.TOTAL_VAL_INC = Tot_CALC_VALUE_INC);
                                                    db.SaveChanges();


                                                }
                                                // db.INVOICE_DETAILS.Add(getUpdate_Tol_Val_Exc_Inc);

                                                //  db.SaveChanges();
                                            }
                                        }

                                    }
                                    else
                                    {
                                        sb.Append("Invoice No already exists for this supplier or financial year.");
                                    }
                                }
                                else
                                {
                                    sb.Append("Invoice Date can not be more than one.");
                                }
                            }
                            else
                            {
                                sb.Append("Invoice Number can not be more than one.");
                            }

                            //}
                        }
                        else
                        {
                            Messages msg = new Messages();
                            msg.Message_Id = 2;
                            msg.Message = "Required field error.";

                            TempData["Message"] = msg;
                        }
                    }
                    else
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 2;
                        msg.Message = "Required field error.";
                        TempData["Message"] = msg;
                    }
                    if (sb.Length > 0)
                    {
                        List<Invalid_Invoice_Bulk> Invalid = new List<Invalid_Invoice_Bulk>();

                        InvalidBulk = (from DataRow dr in dtRoster.Rows
                                       select new Invalid_Invoice_Bulk()
                                       {
                                           PoNumber = Convert.ToString(dr["PONUMBER"]),
                                           MaterialCode = Convert.ToString(dr["MATERIALCODE"]),
                                           DelDate = Convert.ToString(dr["DelDATE"]),
                                           InvoiceNumber = Convert.ToString(dr["INVOICENUMBER"]),
                                           InvoiceDate = Convert.ToString(dr["INVOICEDATE"]),
                                           InvoiceQty = Convert.ToString(dr["INVOICEQTY"]),
                                           CGST = Convert.ToString(dr["CGST"]),
                                           IGST = Convert.ToString(dr["IGST"]),
                                           Remark = sb.ToString(),

                                       }).ToList();


                        TempData["InValidExcel"] = InvalidBulk;
                    }
                    else
                    {

                        TempData["InValidExcel"] = InvalidBulk;
                    }

                    if (InvoiceList.Count > 0 && InvalidBulk.Count > 0)
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 0;
                        msg.Message = InvoiceList.Count + " record found valid and  " + InvalidBulk.Count + " record found invalid.";

                        TempData["Message"] = msg;
                        InValidExportToExcel();
                    }
                    if (InvoiceList.Count == 0 && InvalidBulk.Count > 0)
                    {
                        Messages msg = new Messages();
                        msg.Message_Id = 2;
                        msg.Message = "Upload Failed As All Records Are Invalid.";

                        TempData["Message"] = msg;

                        InValidExportToExcel();
                    }
                    if (InvoiceList.Count > 0 && InvalidBulk.Count == 0)
                    {
                        Messages msg = new Messages();
                        bool flg = SendmailInvoice(_strInvoiceNo, _strInvoiceDate);
                        if (flg)
                        {
                            msg.Message_Id = 1;
                            msg.Message = InvoiceList.Count + " record added successfully.";
                        }
                        else
                        {
                            msg.Message_Id = 1;
                            msg.Message = InvoiceList.Count + " record added successfully but due to some mail could not sent.";
                        }
                        TempData["Message"] = msg;
                    }
                }
                else
                {
                    Messages msg = new Messages();
                    msg.Message_Id = 0;
                    msg.Message = "No record are available in excel file";
                    TempData["Message"] = msg;
                }
            }
            return RedirectToAction("UploadInvoice");
        }

        public FileResult InValidExportToExcel()
        {
            TempData.Keep();
            List<Invalid_Invoice_Bulk> _Invalid = (List<Invalid_Invoice_Bulk>)TempData["InValidExcel"];
            string[] column = { "PONUMBER", "MATERIALCODE", "DelDATE", "INVOICENUMBER", "INVOICEDATE", "INVOICEQTY", "CGST", "IGST", "Remark" };
            string MDAttr = "PONUMBER,MATERIALCODE,DelDATE,INVOICENUMBER,INVOICEDATE,INVOICEQTY,CGST,IGST,Remark";
            ViewBag.InvalidLink = true;
            ExcelExportHelper obj = new ExcelExportHelper();
            return obj.ExportExcel(_Invalid, "Invoice Templete", ".xls", MDAttr, column);
        }
        public FileResult DownloadFile()
        {
            string fileName = @"~/App_Data/Invoice_Tempalate.xlsx";
            //string contentType = "application/vnd.ms-excel";
            return new FilePathResult(fileName, "application/vnd.ms-excel")
            {
                FileDownloadName = "Invoice_Tempalate.xlsx"
            };
        }

        public partial class TableInvoice
        {
            public string PONum { get; set; }
            public string Material { get; set; }
            public string MaterialDesc { get; set; }
            public string UOM { get; set; }
            public string DelDate { get; set; }
            public string InvNo { get; set; }
            public string InvDate { get; set; }
            public string Quantity { get; set; }
            public string CGSTPER { get; set; }
            public string IGSTPER { get; set; }

            public TableInvoice(string PONum, string Material, string MaterialDesc, string UOM, string DelDate, string InvNo, string InvDate, string Quantity, string CGSTPER, string IGSTPER)
            {
                this.PONum = PONum;
                this.Material = Material;
                this.MaterialDesc = MaterialDesc;
                this.UOM = UOM;
                this.DelDate = DelDate;
                this.InvNo = InvNo;
                this.InvDate = InvDate;
                this.Quantity = Quantity;
                this.CGSTPER = CGSTPER;
                this.IGSTPER = IGSTPER;

            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public bool SendmailInvoice(string INVOICE_NUMBER, string Invoice_Date)
        {
            List<MailViewModel> objMailViewModelList = null;
            bool result = false;
            string Supp_Code = User.Identity.Name;
            try
            {
                if (INVOICE_NUMBER != "")
                {
                    StringBuilder varHtml = new StringBuilder();
                    varHtml.Append("<p> Supplier :" + Supp_Code + " has issued an invoice with Invoice Number :" + INVOICE_NUMBER + " Date :" + Invoice_Date + ".</p>");

                    varHtml.Append("<table border='1'cellspacing='0' cellpadding='0' style='background: #fff;'>");
                    varHtml.Append("<thead>");
                    varHtml.Append("<tr class='Header'>");
                    varHtml.Append("<th class='text-center'>Sr. No.</th>");
                    varHtml.Append("<th class='text-center'>Invoice Date</th>");
                    varHtml.Append("<th class='text-center'>Invoice No.</th>");

                    varHtml.Append("<th class='text-center'>PO No.</th>");
                    //  varHtml.Append("<th class='text-center'>PO Date</th>");
                    varHtml.Append("<th class='text-center'>Material Code</th>");
                    varHtml.Append("<th class='text-center'>Material Desp.</th>");
                    varHtml.Append("<th class='text-center'>Material SNP</th>");
                    //varHtml.Append("<th class='text-center'>Qty Order</th>");
                    //varHtml.Append("<th class='text-center'>Qty Pending</th>");
                    varHtml.Append("<th class='text-center'>Schedule Qty.</th>");
                    varHtml.Append("<th class='text-center'>Invoice Qty</th>");
                    varHtml.Append("<th class='text-center'>CGS Tax</th>");
                    varHtml.Append("<th class='text-center'>SGS Tax</th>");
                    varHtml.Append("<th class='text-center'>IGS Tax</th>");
                    varHtml.Append("<th class='text-center'>Amount</th>");
                    // varHtml.Append("<th class='text-center'>Model Name</th>");
                    //varHtml.Append("<th class='text-center'>Batch Code</th>");
                    varHtml.Append("</tr>");
                    varHtml.Append("</thead>");
                    varHtml.Append("<tbody>");

                    objMailViewModelList = db.Database.SqlQuery<MailViewModel>("exec usp_GetUpdateInvoiceDataForMail @INVOICE_NUMBER={0}, @Invoice_Date={1}, @Supp_Code={2}", INVOICE_NUMBER, Invoice_Date, Supp_Code).ToList();

                    int srNo = 0;



                    foreach (var item in objMailViewModelList)
                    {
                        srNo++;
                        varHtml.Append("<tr>");

                        varHtml.Append("<td class='text-center' >" + srNo + "</td>");

                        varHtml.Append("<td class='text-center' >" + Invoice_Date + "</td>");
                        varHtml.Append("<td class='text-center' >" + INVOICE_NUMBER + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.PO_NUM + "</td>");

                        // varHtml.Append("<td class='text-center' >" + item.PO_Date + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.MaterialCode + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.MaterialDescription + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.SNP + "</td>");
                        //  varHtml.Append("<td class='text-center' >" + item.QtyOrder + "</td>");
                        // varHtml.Append("<td class='text-center' >" + item.PendingQty + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.ScheduleQty + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.InvoiceQty + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.CGST_AMT + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.SGST_IMT + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.IGST_AMT + "</td>");
                        varHtml.Append("<td class='text-center' >" + item.AMOUNT + "</td>");
                        // varHtml.Append("<td class='text-center' >" + item.MODEL + "</td>");
                        // varHtml.Append("<td class='text-center' >" + item.BatchCode + "</td>");

                        varHtml.Append("</tr>");
                    }
                    varHtml.Append("</tbody>");
                    varHtml.Append("</table>");


                    MailSend obj_Mail = new MailSend();
                    MailModel obj_MailModel = new MailModel();
                    obj_MailModel.From = ConfigurationManager.AppSettings["emailfrom"];



                    var buyerCode = db.VENDOR_ASSOC.Where(i => i.VENDOR_CODE == Supp_Code).ToList();


                    string strMailId = string.Empty;
                    foreach (var item in buyerCode)
                    {
                        string ss = item.USER_CODE.ToString();
                        var _buyerMail = (from buser in db.AspNetUsers
                                          where buser.UserName == item.USER_CODE
                                          select buser.Email).ToList();

                        if (string.IsNullOrEmpty(strMailId))
                        {
                            if (_buyerMail.Count != 0)
                            {
                                strMailId = _buyerMail[0];
                            }
                        }
                        else
                        {
                            if (_buyerMail.Count != 0)
                            {
                                strMailId += "," + _buyerMail[0];
                            }

                        }
                    }


                    //var buyerUser = (from buser in db.AspNetUsers
                    //                 where buser.UserName == buyerCode
                    //                 select buser).FirstOrDefault();

                    obj_MailModel.Subject = "New Invoice Issued-" + INVOICE_NUMBER;
                    obj_MailModel.To = strMailId;

                  //    obj_MailModel.To = "naresh.pal@infodartmail.com";


                    obj_MailModel.Body = HttpUtility.HtmlDecode(varHtml.ToString());

                    obj_Mail.sendMail(obj_MailModel);
                }

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

    }
}