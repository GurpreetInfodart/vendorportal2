using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class MaterialRejectionViewModel
    {
        public Int64 ID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string HsnSac { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public string RejectedQty { get; set; }
        public string BasicPrice { get; set; }
        public string AccessiblePrice { get; set; }
        public string TotalBasicPrice { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string TotalTax { get; set; }
        public string TotalValue { get; set; }
    }
}