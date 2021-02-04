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
    
    public partial class FormA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormA()
        {
            this.FormACategories = new HashSet<FormACategory>();
            this.FormADocuments = new HashSet<FormADocument>();
            this.FormALogs = new HashSet<FormALog>();
        }
    
        public System.Guid Id { get; set; }
        public string FormANo { get; set; }
        public Nullable<System.Guid> ExporterId { get; set; }
        public Nullable<System.Guid> DistributorId { get; set; }
        public int FormANoInt { get; set; }
        public int FormAStateId { get; set; }
        public string ImporterName { get; set; }
        public string ImporterAddress { get; set; }
        public string FreightBlNo { get; set; }
        public string FreightVessel { get; set; }
        public string FreightContainerNo { get; set; }
        public string FreightRoute { get; set; }
        public string InvoiceNo { get; set; }
        public string AssociationRegNo { get; set; }
        public string ExpNo { get; set; }
        public string EpbReferenceNo { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string NotesReceiving { get; set; }
        public string NotesVerification { get; set; }
        public string NotesDelivery { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<System.DateTime> BillOfLadingDate { get; set; }
        public string ItemNo { get; set; }
        public string MarksAndNoOfPackages { get; set; }
        public string NoAndKindOfPackages { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string LcNo { get; set; }
        public Nullable<System.DateTime> LcDate { get; set; }
        public string BbLcNo { get; set; }
        public Nullable<System.DateTime> BbLcDate { get; set; }
        public string BillOfExportNo { get; set; }
        public Nullable<System.DateTime> BillOfExportDate { get; set; }
        public string Origin { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> ProductCodeId { get; set; }
        public Nullable<int> Quantities { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string CollectorName { get; set; }
        public string CollectorTitle { get; set; }
        public string CollectorPhoneNo { get; set; }
        public string EpbOfficerName { get; set; }
        public string EpbOfficerTitle { get; set; }
        public string EpbOfficeName { get; set; }
        public string AuthorizePersonName { get; set; }
        public string AuthorizePersonTitle { get; set; }
        public string PaymentConfirmationNo { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> ServiceTime { get; set; }
        public Nullable<System.DateTime> DeclarationByExporterDate { get; set; }
        public System.DateTime Created { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public System.Guid ChangedBy { get; set; }
        public string NotesSubmissionByExporter { get; set; }
    
        public virtual Distributor Distributor { get; set; }
        public virtual District District { get; set; }
        public virtual Exporter Exporter { get; set; }
        public virtual FormAState FormAState { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormACategory> FormACategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormADocument> FormADocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormALog> FormALogs { get; set; }
    }
}