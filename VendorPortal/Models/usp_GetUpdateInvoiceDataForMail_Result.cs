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
    
    public partial class usp_GetUpdateInvoiceDataForMail_Result
    {
        public string PO_NUM { get; set; }
        public string PO_Date { get; set; }
        public string DEL_Date { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public Nullable<int> SNP { get; set; }
        public Nullable<decimal> QtyOrder { get; set; }
        public Nullable<decimal> PendingQty { get; set; }
        public Nullable<decimal> InvoiceQty { get; set; }
        public Nullable<decimal> ScheduleQty { get; set; }
        public string BatchCode { get; set; }
        public string MODEL { get; set; }
        public Nullable<decimal> CGST_AMT { get; set; }
        public Nullable<decimal> IGST_AMT { get; set; }
        public Nullable<decimal> SGST_IMT { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
    }
}
