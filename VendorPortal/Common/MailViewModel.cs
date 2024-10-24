using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Common
{
    public class MailViewModel
    {
        public string PO_NUM { get; set; }
        public string PO_Date { get; set; }
        public string DEL_Date { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public int SNP { get; set; }
        public decimal QtyOrder { get; set; }
        public decimal PendingQty { get; set; }
        public decimal InvoiceQty { get; set; }
        public decimal ScheduleQty { get; set; }
        public string BatchCode { get; set; }
        public string MODEL { get; set; }
        public decimal CGST_AMT { get; set; }
        public decimal IGST_AMT { get; set; }
        public decimal SGST_IMT { get; set; }
        public decimal AMOUNT { get; set; }
    }
}