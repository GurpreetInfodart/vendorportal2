using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class UploadInvoiceViewModel
    {
        public string PO_Num { get; set; }
        public string Mat_Code { get; set; }
        public string Del_Date { get; set; }
        public string Invoice_No { get; set; }
        public string Invoice_Date { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public HttpPostedFileBase ExcelFile { get; set; }
    }
}