using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VendorPortal.Models;
using PagedList;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Data.Common;
using System.Data.Entity;

namespace VendorPortal.Controllers
{
    [Authorize(Roles = "Buyer,Supplier,Admin,Administrator,BSLUser,Quality,R&D,Visitor")]
    public class BuyerScheduleController : Controller
    {
        List<SchedulerViewModel> list = null;
        VendorPortalEntities db = new VendorPortalEntities();
        // GET: Schedule

        List<SchedulerViewModel> objSchedulerViewModelList = null;
        BasicPaging objBasicPaging = null;

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult BuyerPendingSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            if (SearchBy == "" || SearchValue == "")
            {
                SearchValue = "";
                SearchBy = "";
            }

            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", "Buyer", CurrentPage, 0,fromDate,toDate, SearchBy, SearchValue).ToList();
            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", "Buyer",0, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 0
            //                  && ((fromDate != "" && toDate != "" && sh.DEL_DATE >= dtFrmDt && sh.DEL_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.DEL_DATE <= DateTime.Now))
            //                 group sh by new { sh.UploadDate, p.SUPP_CODE, sh.Status } into gdd
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
            ViewBag.pending_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objSchedulerViewModelList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerPendingSchedule", objSchedulerViewModelList);
        }
        public PartialViewResult BuyerAcknowledgeSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", "Buyer", CurrentPage, 1, fromDate, toDate, SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", "Buyer", 1,fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 1
            //                   && ((fromDate != "" && toDate != "" && sh.DEL_DATE >= dtFrmDt && sh.DEL_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.DEL_DATE <= DateTime.Now))
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
            ViewBag.Acknoledge_paging = objBasicPaging;

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = objSchedulerViewModelList.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);
            return PartialView("_BuyerAcknowledgeSchedule", objSchedulerViewModelList);
        }
        public PartialViewResult BuyerDeclineSchedule(int CurrentPage = 1, string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
            //BasicPaging objBasicPaging = new BasicPaging();
            //binary@321#
            objSchedulerViewModelList = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0} ,@CurrentPage={1}, @TYPE={2},@FromDate={3}, @ToDate={4},@SearchBy={5},@SearchValue={6}", "Buyer", CurrentPage, 2, fromDate, toDate,SearchBy, SearchValue).ToList();

            var totalCount = db.Database.SqlQuery<int>("exec usp_GetSchedulerData_TotalCount @UserType={0} , @TYPE={1},@FromDate={2}, @ToDate={3},@SearchBy={4},@SearchValue={5}", "Buyer", 2, fromDate, toDate, SearchBy, SearchValue).ToList();

            DateTime dtFrmDt = new DateTime();
            DateTime dtToDt = new DateTime();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                dtFrmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                dtToDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            }

            //var totalData = (from sh in db.Schedules
            //                 join p in db.POes on sh.POID equals p.ID
            //                 where sh.Status == 2
            //                   && ((fromDate != "" && toDate != "" && sh.DEL_DATE >= dtFrmDt && sh.DEL_DATE <= dtToDt)
            //            || (fromDate == "" || toDate == "" && sh.DEL_DATE <= DateTime.Now))
            //                 group sh by new { sh.UploadDate, p.SUPP_CODE, sh.Status} into gdd
            //                 select new { }
            // ).ToList();

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
            return PartialView("_BuyerDeclineSchedule", objSchedulerViewModelList);
        }

        public ActionResult ViewBuyerSchedule(int? page)
        {
            int pageSize = 3;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0}", "Buyer").ToList();

            var getPendingData = list.Where(x => x.Status == 0).ToList().ToPagedList(pageIndex, pageSize);
            var getAcknowledgeData = list.Where(x => x.Status == 1).ToList().ToPagedList(pageIndex, pageSize);
            var getDeclineData = list.Where(x => x.Status == 2).ToList().ToPagedList(pageIndex, pageSize);



            //var getAcknowledgeData = list.Where(x => x.Status == 1).Select(a => new SchedulerViewModel
            //{
            //    UploadDate = a.UploadDate,
            //    SUPP_CODE = a.SUPP_CODE,
            //    ScheduleType = a.ScheduleType,
            //    UploadedBy = a.UploadedBy

            //}).ToList().ToPagedList(pageIndex, pageSize); 



            TempData["PendingData"] = list.Where(x => x.Status == 0).ToList();
            TempData["AcknowledgeData"] = list.Where(x => x.Status == 1).ToList();
            TempData["DeclineData"] = list.Where(x => x.Status == 2).ToList();

            ViewBag.pending_Data = getPendingData;
            ViewBag.Acknolodge_Data = getAcknowledgeData;
            ViewBag.Decline_Data = getDeclineData;

            return View("ViewBuyerSchedule");
        }

        public ActionResult BuyerScheduleDetail()
        {
            return View();
        }
        public PartialViewResult ScheduleDetail(string SUPP_CODE, string upload_date, int CurrentPage = 1, int flg = 0,  string fromDate = "", string toDate = "", string SearchBy = "", string SearchValue = "")
        {
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

            //list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0} ,@CurrentPage={1}", SUPP_CODE, CurrentPage).ToList();


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

            Int64 _scheduleStatus;
            if (list.Count > 0 && list != null)
            {
                _scheduleStatus = list.Select(i => i.Status).FirstOrDefault();
                ViewBag.Status = _scheduleStatus;
            }

            //CurrentPage = (CurrentPage - objBasicPaging.RowParPage) < 0 ? (CurrentPage - 1) : 0;
            //CurrentPage = (objInvoiceList.Count() / objBasicPaging.RowParPage) - 1;

            //var pglst = list.Skip((CurrentPage - 1) * objBasicPaging.RowParPage).Take(objBasicPaging.RowParPage);

            return PartialView("_BuyerScheduleDetail", list);
        }
        public ActionResult AcknowledgePaging(int? page)
        {
            int pageSize = 3;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var _acknowledge = (List<SchedulerViewModel>)TempData.Peek("AcknowledgeData");
            ViewBag.Acknolodge_Data = _acknowledge.ToList().ToPagedList(pageIndex, pageSize);

            var _pending_Data = (List<SchedulerViewModel>)TempData.Peek("PendingData");
            var _decline_Data = (List<SchedulerViewModel>)TempData.Peek("DeclineData");

            ViewBag.pending_Data = _pending_Data.ToPagedList(pageIndex, pageSize);
            ViewBag.Decline_Data = _decline_Data.ToPagedList(pageIndex, pageSize);

            return View("ViewBuyerSchedule");
        }

        public ActionResult BuyerSchedulerDetail(string SUPP_CODE, string UploadDate)
        {
            //var schData = (from sh in db.Schedule1
            //          join p in db.POes on sh.POID equals p.ID
            //          join pd in db.PO_DETAILS on p.PO_NUM equals pd.PO_NUM
            //          join sm in db.SUPPLIER_MASTER on p.SUPP_CODE equals sm.SUPP_CODE
            //          join pm in db.Plant_Master on pd.PLANT_CODE equals pm.Plant_Code
            //          join tm in db.tblMaterials on sh.MAT_CODE equals tm.MaterialCode
            //          where p.SUPP_CODE == SUPP_CODE// && sh.UploadDate == Convert.ToDateTime(UploadDate)
            //          select new SchedulerViewModel
            //          {
            //              //UploadDate = Convert.ToString(sh.UploadDate),//.ToString("dd/MM/yyyy")
            //              SUPP_CODE = p.SUPP_CODE,
            //              Plant_Name = pm.Plant_Name,
            //              SUPP_NAME = sm.SUPP_NAME,
            //              MaterialCode = tm.MaterialCode,
            //              //MaterialDescription = tm.MaterialDescription,
            //              //UOM = pd.UOM,
            //              //Position = sh.Position,
            //              //Schedule_NO = sh.SCHEDULE_NO,
            //              //Qty = Convert.ToDecimal(sh.Qty),
            //              Remark = sh.REMARKS,
            //              //Delv_Date = Convert.ToString(sh.DEL_DATE)

            //          }
            //    ).ToList();

            List<SchedulerViewModel> list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0}", SUPP_CODE).ToList();
            return View("BuyerSchedulerDetail", list);
        }

        public ActionResult ViewSupplierSchedule()
        {
            string Supp_code = "A10001";
            List<SchedulerViewModel> list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerData @UserType={0}", Supp_code).ToList();

            var getPendingData = list.Where(x => x.Status == 0).Select(a => new SchedulerViewModel
            {
                UploadDate = a.UploadDate,
                SUPP_CODE = a.SUPP_CODE,
                ScheduleType = a.ScheduleType,
                UploadedBy = a.UploadedBy

            }).ToList();

            var getAcknowledgeData = list.Where(x => x.Status == 1).Select(a => new SchedulerViewModel
            {
                UploadDate = a.UploadDate,
                SUPP_CODE = a.SUPP_CODE,
                ScheduleType = a.ScheduleType,
                UploadedBy = a.UploadedBy

            }).ToList();

            var getDeclineData = list.Where(x => x.Status == 2).Select(a => new SchedulerViewModel
            {
                UploadDate = a.UploadDate,
                SUPP_CODE = a.SUPP_CODE,
                ScheduleType = a.ScheduleType,
                UploadedBy = a.UploadedBy

            }).ToList();

            ViewBag.Supp_pending_Data = getPendingData;
            ViewBag.Supp_Acknolodge_Data = getAcknowledgeData;
            ViewBag.Supp_Decline_Data = getDeclineData;

            return View("ViewSupplierSchedule");

        }
        public ActionResult SupplierSchedulerDetail(string SUPP_CODE)
        {
            bool statusType = false;
            List<SchedulerViewModel> list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0}", SUPP_CODE).ToList();
            var getPendingData = list.Where(x => x.Status == 0).ToList();
            if (getPendingData != null && getPendingData.Count > 0)
            {
                statusType = true;
            }
            ViewBag.SetStatus = statusType;
            return View("SupplierSchedulerDetail", getPendingData);
        }

        public ActionResult SupplierSchedulerAknoledgeDetail(string SUPP_CODE)
        {
            List<SchedulerViewModel> list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0}", SUPP_CODE).ToList();
            var getPendingData = list.Where(x => x.Status == 1).ToList();
            ViewBag.SetStatus = false;
            return View("SupplierSchedulerDetail", getPendingData);
        }
        public ActionResult SupplierSchedulerDeclineDetail(string SUPP_CODE)
        {
            List<SchedulerViewModel> list = db.Database.SqlQuery<SchedulerViewModel>("exec usp_GetSchedulerDataBySuppCde @SUPP_CODE={0}", SUPP_CODE).ToList();
            var getPendingData = list.Where(x => x.Status == 2).ToList();
            ViewBag.SetStatus = false;
            return View("SupplierSchedulerDetail", getPendingData);
        }
        public ActionResult ScheduleDel(string SubActId)
        {
            ViewBag.DDLVM = (from vm in db.SUPPLIER_MASTER
                             where vm.ISACTIVE == "Y"
                             select new { vm.SUPP_CODE });
            return View();
        }
        public System.Data.DataTable QueryToTable(string queryText, SqlParameter[] parametes)
        {
            using (DbDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = db.Database.Connection.CreateCommand();
                adapter.SelectCommand.CommandText = queryText;
                if (parametes != null)
                    adapter.SelectCommand.Parameters.AddRange(parametes);
                System.Data.DataTable table = new System.Data.DataTable();
                adapter.Fill(table);
                return table;
            }
        }
        public ActionResult ExportData(string ddlvm, FormCollection fomr)
        {
            //frmDate = "2018/01/01";
            //toDate = "2018/01/30";
            System.Data.DataTable dt = new System.Data.DataTable();
            bool message = false;
            try
            {
                SqlParameter[] parametes = new[]
                {
                    new SqlParameter("vendorcode", fomr["DDLVM"].ToString()),
                    new SqlParameter("fromDate", Convert.ToDateTime(fomr["fromdate"].ToString())),
                    new SqlParameter("todate", Convert.ToDateTime(fomr["Todate"].ToString()))
                };

                dt = QueryToTable("SP_SchedulevsDelivery @vendorcode, @fromDate, @todate", parametes);
                // var list = db.Database.SqlQuery<string>("exec SP_SchedulevsDelivery @vendorcode={0},@fromDate={1},@todate={2}", ddlvm, Convert.ToDateTime(frmDate), Convert.ToDateTime(toDate)).ToList();
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Remove("stid");

                    //ExportToExcel(dt);
                    //int UID = Convert.ToInt16(Session["User_ID"].ToString().Trim());
                    //var UserName = (from User in db.USER_MASTER
                    //                where User.SNO == UID
                    //                select new { User.USERNAME }).FirstOrDefault();
                    //string fileName = "SchedularExcel_" + UserName.USERNAME + ".xls";
                    string fileName = "SchedularExcel_" + fomr["DDLVM"].ToString().Trim() + ".xls";
                    var grid = new System.Web.UI.WebControls.GridView();
                    grid.DataSource = dt;
                    grid.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter objStringWriter = new StringWriter();
                    HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                    grid.RenderControl(objHtmlTextWriter);
                    Response.Output.Write(objStringWriter.ToString());
                    Response.Flush();
                    Response.End();
                    message = true;

                    //return View("ScheduleDel");

                    //  return RedirectToAction("ScheduleDel", "Schedule", new { SubActId = "0000000018" });

                }
                else
                {
                    //dt =
                    DataColumn dc = new DataColumn("Message", typeof(String));
                    dt.Columns.Add(dc);

                    DataRow dr = dt.NewRow();
                    dr[0] = "DATA NOT FOUND";
                    dt.Rows.Add(dr);


                    string fileName = "SchedularExcel_" + fomr["DDLVM"].ToString().Trim() + ".xls";
                    var grid = new System.Web.UI.WebControls.GridView();
                    grid.DataSource = dt;
                    grid.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter objStringWriter = new StringWriter();
                    HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                    grid.RenderControl(objHtmlTextWriter);
                    Response.Output.Write(objStringWriter.ToString());
                    Response.Flush();
                    Response.End();
                    message = true;

                    //return RedirectToAction("ScheduleDel","Schedule", new { SubActId="0000000018", message= "Record_not_Found"}); 
                }
                //  return RedirectToAction("ScheduleDel", "Schedule", new { SubActId = "0000000018" });
                return Json(JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public ActionResult Reschedule(int id)
        {
            var _data = db.Schedules.Where(x => x.ID == id).FirstOrDefault();
            if (_data != null)
            {
                _data.Status = 0;
                db.Entry(_data).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}