using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GspDataAccess;
using PayslipDataAccess.Enums;

namespace MigrateExporteFromGsp.Service
{
    public static class ConvertionService
    {
        public static PayslipDataAccess.Exporter ModifiedExporterData(PayslipDataAccess.Exporter model, PayslipDataAccess.Exporter exporter)
        {
            model.Id = exporter.Id;
            model.Created = exporter.Created;
            model.CreatedBy = exporter.CreatedBy;
            model.Modified = exporter.Modified;
            model.ModifiedBy = exporter.ModifiedBy;
            model.Active = exporter.Active;

            model.ActiveState = exporter.ActiveState;
            model.ExporterCategory = exporter.ExporterCategory;
            model.ExporterType = exporter.ExporterType;
            model.ExporterNumber = exporter.ExporterNumber;
            model.IdentityId = exporter.IdentityId;
            model.EpbRegistrationNo = exporter.EpbRegistrationNo;
            //RegDate = Convert.ToDateTime(exporter.RegDate),
            //RegistrationValidSince = Convert.ToDateTime(exporter.RegistrationValidSince),
            //PeriodofValidation = Convert.ToDateTime(exporter.PeriodofValidation),
            model.IsUndertakingByExporter = exporter.IsUndertakingByExporter;

            model.CompanyOrFactoryName = exporter.CompanyOrFactoryName;
            model.CorporateAddress = exporter.CorporateAddress;

            model.FactoryAddress = exporter.FactoryAddress;

            model.AssociationMembershipNo = exporter.AssociationMembershipNo;
            model.AssociationBinNo = exporter.AssociationBinNo;
            model.AssociationTradeLicense = exporter.AssociationTradeLicense;
            model.AssociationTinNo = exporter.AssociationTinNo;
            model.ActivityDescription = exporter.ActivityDescription;
            model.IndustrialProcessDescription = exporter.IndustrialProcessDescription;
            model.GoodsDescription = exporter.GoodsDescription;
            model.HsCode = exporter.HsCode;
            model.Bonded = exporter.Bonded;
            model.ErcNo = exporter.ErcNo;
            model.BolRegNo = exporter.BolRegNo;
            model.FireLicNo = exporter.FireLicNo;
            model.BondLicNo = exporter.BondLicNo;

            //model.ServiceOffice = (int) ServiceOffice.Dhaka;
            //model.EpbReferenceNo = (int) EpbReferenceNo.DH;


            model.FactoryType = exporter.FactoryType;

            model.RegDate = exporter.RegDate;
            model.RegistrationValidSince = exporter.RegistrationValidSince;
            model.PeriodofValidation = exporter.PeriodofValidation;

            model.PrimaryPersonName = exporter.PrimaryPersonName;
            model.PrimaryPersonAddress = exporter.PrimaryPersonAddress;
            model.PrimaryPersonPhone = exporter.PrimaryPersonPhone;

            model.SecondaryPersonName = exporter.SecondaryPersonName;
            model.SecondaryPersonAddress = exporter.SecondaryPersonAddress;
            model.SecondaryPersonPhone = exporter.SecondaryPersonPhone;

            model.CommercialManagerName = exporter.CommercialManagerName;
            model.CommercialManagerAddress = exporter.CommercialManagerAddress;
            model.CommercialManagerPhone = exporter.CommercialManagerPhone;

            return model;
        }
    }
}
