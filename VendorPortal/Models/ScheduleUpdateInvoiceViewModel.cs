using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class ScheduleUpdateInvoiceViewModel
    {
        public string PO_NUM { get; set; }
        public string PODate { get; set; }
        public string ScheduleDate { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public int SNP { get; set; }
        public decimal QtyOrder { get; set; }
        public decimal QtyDelv { get; set; }
        public decimal PendingQty { get; set; }
        public decimal ScheduleQty { get; set; }
        public decimal InvoiceQty { get; set; }
        public decimal CGSTax { get; set; }
        public decimal SGSTax { get; set; }
        public decimal IGSTax { get; set; }
        public string Model { get; set; }
        public string MFG_Date { get; set; }
        public string BatchCode { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Unit { get; set; }
        public string POSITION_NO { get; set; }
        public string SCHEDULE_NO { get; set; }
        

    }
}