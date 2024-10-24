using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class SupplierScheduleViewModel
    {
        public string UploadDate { get; set; }
        public string SUPP_CODE { get; set; }
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
}