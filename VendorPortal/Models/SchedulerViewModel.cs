using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class SchedulerViewModel
    {
        private string scheduleType = "15 DAYS SCHEDULE";
        private string uploadedBy = "admin";

        public string UploadDate { get; set; }
        public string SUPP_CODE { get; set; }
        public string ScheduleType
        {
            get { return scheduleType; }
            set { scheduleType = value; }
        }
        public string UploadedBy
        {
            get { return uploadedBy; }
            set { uploadedBy = value; }
        }
        public int Status { get; set; }

        public string Plant_Name { get; set; }
        public string SUPP_NAME { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public string Position { get; set; }
        public string Schedule_NO { get; set; }
        public decimal Qty { get; set; }
        public string Delv_Date { get; set; }
        public string Remark { get; set; }
        public Int64 ScheduleId { get; set; }
        
        public string Acc_Rej_Remark { get; set; }



    }

    public class Master_Schedule_Dtls
    {
        public Int64 ScheduleId { get; set; }
        public string Remark { get; set; }
       // public List<SupplierScheduleViewModel> Schedule_Dtls { get; set; }
    }
    //public class Master_Schedule_Dtls
    //{
    //    public List<SupplierScheduleViewModel> SupplierSchedule { get; set; }

    //}
}