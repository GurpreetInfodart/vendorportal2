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
    
    public partial class uspGenerateInvoice_Barcode_Result
    {
        public long ID { get; set; }
        public long INV_ID { get; set; }
        public string MAT_CODE { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SGST { get; set; }
        public decimal CESS { get; set; }
        public decimal TOTAL_VAL_INC { get; set; }
        public decimal TOTAL_VAL_EXC { get; set; }
        public string Model { get; set; }
        public string MFG_DATE { get; set; }
        public string BATCHCODE { get; set; }
        public decimal CALC_VALUE_EXC { get; set; }
        public decimal CALC_VALUE_INC { get; set; }
        public string SCHEDULE_NO { get; set; }
        public string DEL_DATE { get; set; }
        public string INVOICE_DATE { get; set; }
        public string CONTRACT_NO { get; set; }
        public string POSITION_NO { get; set; }
        public decimal CGST_PER { get; set; }
        public decimal SGST_PER { get; set; }
        public decimal IGST_PER { get; set; }
        public string MaterialDescription { get; set; }
        public string Unit { get; set; }
        public string INVOICE_NUMBER { get; set; }
        public string Order_No { get; set; }
        public string HSN_SAC_NO { get; set; }
        public string SUPP_CODE { get; set; }
        public string SUPP_Name { get; set; }
        public int SNP { get; set; }
    }
}
