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
    
    public partial class GetAllSuplierRating_Result
    {
        public long Id { get; set; }
        public string CummRank { get; set; }
        public string SuppCode { get; set; }
        public Nullable<decimal> MonthlyDeliveryRating { get; set; }
        public Nullable<decimal> MonthlyQualityRating { get; set; }
        public Nullable<decimal> MonthlyOverallRating { get; set; }
        public Nullable<decimal> CummDeliveryRating { get; set; }
        public Nullable<decimal> CummQualityRating { get; set; }
        public Nullable<decimal> CummOverallRating { get; set; }
        public string Grade { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<System.DateTime> ValidTill { get; set; }
        public int Active { get; set; }
    }
}
