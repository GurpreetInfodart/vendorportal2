using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class GetVendor_AssociationViewModel
    {
        public Int64 Id { get; set; }
        public string SUPP_NAME { get; set; }
        public string VENDOR_CODE { get; set; }
        public string USER_CODE { get; set; }

    }
}