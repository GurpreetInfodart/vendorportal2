using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class Invalid_Invoice_Bulk
    {
        public string PoNumber { get; set; }
        public string MaterialCode { get; set; }
        public string DelDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceQty { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string Remark { get; set; }

    }
}