//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GspDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exporter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exporter()
        {
            this.FormAs = new HashSet<FormA>();
            this.FormAIssues = new HashSet<FormAIssue>();
        }
    
        public System.Guid Id { get; set; }
        public string ExporterNo { get; set; }
        public string CompanyOrFactoryName { get; set; }
        public Nullable<int> FactoryTypeId { get; set; }
        public string CorporateAddress { get; set; }
        public Nullable<int> CorporateAddressPsId { get; set; }
        public Nullable<int> CorporateAddressDistrictId { get; set; }
        public Nullable<int> CorporateAddressCountryId { get; set; }
        public string FactoryAddress { get; set; }
        public Nullable<int> FactoryAddressPsId { get; set; }
        public Nullable<int> FactoryAddressDistrictId { get; set; }
        public Nullable<int> FactoryAddressCountryId { get; set; }
        public Nullable<System.Guid> PrimaryPersonId { get; set; }
        public Nullable<System.Guid> SecondaryPersonId { get; set; }
        public string AssociationMembershipNo { get; set; }
        public string AssociationBinNo { get; set; }
        public string AssociationTradeLicense { get; set; }
        public string AssociationTinNo { get; set; }
        public Nullable<System.DateTime> PeriodofValidation { get; set; }
        public string ActivityDescription { get; set; }
        public string IndustrialProcessDescription { get; set; }
        public string GoodsDescription { get; set; }
        public string HsCode { get; set; }
        public int ActiveStateId { get; set; }
        public string EpbRegistrationNo { get; set; }
        public Nullable<System.DateTime> RegistrationValidSince { get; set; }
        public int IdentityId { get; set; }
        public bool IsUndertakingByExporter { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string Bonded { get; set; }
        public string ErcNo { get; set; }
        public string BolRegNo { get; set; }
        public string FireLicNo { get; set; }
        public string BondLicNo { get; set; }
        public System.DateTime Created { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public System.Guid ChangedBy { get; set; }
        public string ExporterType { get; set; }
    
        public virtual ActiveState ActiveState { get; set; }
        public virtual Country Country { get; set; }
        public virtual Country Country1 { get; set; }
        public virtual District District { get; set; }
        public virtual District District1 { get; set; }
        public virtual FactoryType FactoryType { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person Person1 { get; set; }
        public virtual Thana Thana { get; set; }
        public virtual Thana Thana1 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormA> FormAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormAIssue> FormAIssues { get; set; }
    }
}