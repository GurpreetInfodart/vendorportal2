using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class InvoiceViewModel
    {
        public Int64 ID { get; set; }
        public string INVOICE_NUMBER { get; set; }
        public string SUPP_CODE { get; set; }
        public string SUPP_Name { get; set; }
        public string SUPPLIER_GSTIN { get; set; }
        public string SUPPLIER_CIN { get; set; }
        public string INVOICE_DATE { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalAmt { get; set; }
        public Int64 POID { get; set; }
        public string HSN_SAC_NO { get; set; }
        public int TotalCount { get; set; }
    }
}