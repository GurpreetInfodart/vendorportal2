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
    
    public partial class tblInvoiceRejecttion
    {
        public long ID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string HsnSac { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public string RejectedQty { get; set; }
        public string BasicPrice { get; set; }
        public string AccessiblePrice { get; set; }
        public string TotalBasicPrice { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string TotalTax { get; set; }
        public string TotalValue { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
