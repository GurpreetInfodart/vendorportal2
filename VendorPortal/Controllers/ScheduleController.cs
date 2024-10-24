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

namespace VendorPortal.Controllers
{
    public class ScheduleController : Controller
    {
        List<SchedulerViewModel> list = null;
        VendorPortalEntities db = new VendorPortalEntities();
        // GET: Schedule

        public ActionResult Index()
        {
            return View();
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

        public ActionResult AcknowledgePaging(int?page)
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

        public JsonResult ApprovedStatus(string values)
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
    }
}