//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PurchaseOrder
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUnit
    {
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitDescription { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> UpdatedOn { get; set; }
        public bool Active { get; set; }
    }
}
