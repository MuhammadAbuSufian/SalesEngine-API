//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayslipDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class SyncSetting
    {
        public string Id { get; set; }
        public int SyncTable { get; set; }
        public System.DateTime LastSyncDate { get; set; }
        public int NewSyncEntry { get; set; }
        public int ModifiedSyncEntry { get; set; }
        public bool IsOneWaySync { get; set; }
        public bool IsCycleComplete { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
        public bool IsInitialSync { get; set; }
    }
}
