using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class SupplierPOViewModel
    {
        public string PO_Number { get; set; }
        public string PO_Date { get; set; }
        public decimal? Quantity { get; set; }
        public int Status { get; set; }
    }
}