//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Material
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMaterial
    {
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string Unit { get; set; }
        public string MaterialGroup { get; set; }
        public Nullable<int> SNP { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}