﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EpbPayslipDbEntities : DbContext
    {
        public EpbPayslipDbEntities()
            : base("name=EpbPayslipDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AspNetPermission> AspNetPermissions { get; set; }
        public virtual DbSet<AspNetResource> AspNetResources { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<EpbService> EpbServices { get; set; }
        public virtual DbSet<EpbServiceLog> EpbServiceLogs { get; set; }
        public virtual DbSet<Exporter> Exporters { get; set; }
        public virtual DbSet<ExporterEpbService> ExporterEpbServices { get; set; }
        public virtual DbSet<ExporterEpbServicePrintLog> ExporterEpbServicePrintLogs { get; set; }
        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<ServiceIssueType> ServiceIssueTypes { get; set; }
        public virtual DbSet<ServiceIssueTypeHourlyCost> ServiceIssueTypeHourlyCosts { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<SyncSetting> SyncSettings { get; set; }
    }
}
