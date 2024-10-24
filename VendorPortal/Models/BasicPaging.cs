using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorPortal.Models
{
    public class BasicPaging
    {
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int RowParPage { get; set; }
        public int TotalPage { get; set; }
            
    }
}