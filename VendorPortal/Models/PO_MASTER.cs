//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendorPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PO_MASTER
    {
        public int SLNO { get; set; }
        public string PO_NUM { get; set; }
        public string PO_DATE { get; set; }
        public string SUPP_CODE { get; set; }
        public string PLANT_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string MAT_CODE { get; set; }
        public string HSN_SAC { get; set; }
        public string GL_ACC { get; set; }
        public string QUANTITY { get; set; }
        public string UOM { get; set; }
        public string UNIT_RATE { get; set; }
        public string DISCOUNT { get; set; }
        public string NET_AMOUNT { get; set; }
        public string FREIGHT { get; set; }
        public string PF { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string CESS { get; set; }
        public string TOTAL_VALUE_INC { get; set; }
        public string TOTAL_VALUE_EXC { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public string STATUS { get; set; }
        public Nullable<int> ACC_REJ_BY { get; set; }
        public Nullable<System.DateTime> ACC_REJ_ON { get; set; }
        public string ACC_REJ_REMARKS { get; set; }
        public string AMENDED { get; set; }
        public Nullable<int> refslno { get; set; }
    }
}
