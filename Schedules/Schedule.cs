//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Schedules
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public long ID { get; set; }
        public Nullable<long> POID { get; set; }
        public string MAT_CODE { get; set; }
        public string Position { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<System.DateTime> DEL_DATE { get; set; }
        public string PLANT_CODE { get; set; }
        public string CONTRACT_NO { get; set; }
        public string SCHEDULE_NO { get; set; }
        public Nullable<int> Status { get; set; }
        public string REMARKS { get; set; }
        public string IS_BILL_GENERATED { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
    }
}
