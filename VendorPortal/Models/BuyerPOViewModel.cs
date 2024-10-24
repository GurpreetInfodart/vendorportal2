using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class BuyerPOViewModel
    {
        public string PO_Number { get; set; }
        public string PO_Date { get; set; }
        public decimal? Quantity { get; set; }
        public int Status { get; set; }
        public string SUPP_CODE { get; set; }
        public string SUPP_NAME { get; set; }

    }
}