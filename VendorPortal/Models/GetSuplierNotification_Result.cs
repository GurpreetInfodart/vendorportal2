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
    
    public partial class GetSuplierNotification_Result
    {
        public long Id { get; set; }
        public string UserCode { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ValidTill { get; set; }
        public int Active { get; set; }
    }
}
