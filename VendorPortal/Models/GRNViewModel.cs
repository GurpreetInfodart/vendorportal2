using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class GRNViewModel
    {
        public Int64 ID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierNAme { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string PONO { get; set; }
        public string MRNNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string PstngDate { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
    }
}