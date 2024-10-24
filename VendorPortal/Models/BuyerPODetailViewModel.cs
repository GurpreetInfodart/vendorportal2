using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class BuyerPODetailViewModel
    {
        public string PlantName { get; set; }
        public string PO_NUM { get; set; }
        public string PODate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public string HSN_SAC { get; set; }
        public decimal QUANTITY { get; set; }
        public string UOM { get; set; }
        public decimal UNIT_RATE { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal NET_AMOUNT { get; set; }
        public decimal TotalValue_Without_Tax { get; set; }
        public decimal FREIGHT { get; set; }
        public decimal PF { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CESS { get; set; }
        public decimal TotalValue_With_Tax { get; set; }
    }
}