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
    
    public partial class tblECN
    {
        public long Id { get; set; }
        public string RowType { get; set; }
        public System.DateTime InsDate { get; set; }
        public string Model { get; set; }
        public string ECRECNNo { get; set; }
        public string ChangeDes { get; set; }
        public string SUPP_NAME { get; set; }
        public string SUPP_CODE { get; set; }
        public Nullable<System.DateTime> ECNDate { get; set; }
        public string ECNFile { get; set; }
        public string ECNFileLocation { get; set; }
        public Nullable<int> SuppECNRes { get; set; }
        public Nullable<System.DateTime> SuppECNResDate { get; set; }
        public string SuppECNResFile { get; set; }
        public string SuppECNResFileLocation { get; set; }
        public Nullable<int> SuppFeaRes { get; set; }
        public Nullable<System.DateTime> SuppFeaResDate { get; set; }
        public string SuppFeaResFile { get; set; }
        public string SuppFeaResFileLocation { get; set; }
        public Nullable<int> BSLGo1 { get; set; }
        public Nullable<System.DateTime> BSLGoResDate1 { get; set; }
        public Nullable<int> SuppDimRes { get; set; }
        public Nullable<System.DateTime> SuppDimResDate { get; set; }
        public string SuppDimResFile { get; set; }
        public string SuppDimResFileLocation { get; set; }
        public Nullable<int> BSLGoRes2 { get; set; }
        public Nullable<System.DateTime> BSLGoResDate2 { get; set; }
        public string UnderProCom { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
    }
}