using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using GspDataAccess;
using MigrateExporteFromGsp.Service;
using PayslipDataAccess;
using PayslipDataAccess.Enums;
using Exporter = PayslipDataAccess.Exporter;

namespace MigrateExporteFromGsp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gspDb = new GspDBEntities();
            var payslipDb = new EpbPayslipDbEntities();

            var syncSettings = payslipDb.SyncSettings.FirstOrDefault(x => x.SyncTable == (int)SyncTable.Exporter);
            if (syncSettings == null)
            {
                CreateFirstExporterSyncStatusEntry(payslipDb, SyncTable.Exporter);
                syncSettings = payslipDb.SyncSettings.FirstOrDefault(x => x.SyncTable == (int)SyncTable.Exporter);
            }
            if (syncSettings == null) return;

            if (syncSettings.IsCycleComplete)
            {
                syncSettings.NewSyncEntry = 0;
                syncSettings.ModifiedSyncEntry = 0;

                syncSettings.Skip = 0;
                //syncSettings.Take = 0;

                syncSettings.IsCycleComplete = false;
            }
            syncSettings.Skip = 0;

            var count = syncSettings.IsInitialSync
                ? gspDb.Exporters.Count()
                : gspDb.Exporters.Count(x => x.Changed >= syncSettings.LastSyncDate);
            if (count == 0) return;

            Console.WriteLine("Total Syncable " + SyncTable.Exporter + " Found: " + count +
                              "\n\n***********Sync Staus***********\n\n" +
                              "Last Sync Date:" + syncSettings.LastSyncDate + "\n" +
                              "Is One Way Sync? : " + syncSettings.IsOneWaySync + " \n" +
                              "Is First Sync? : " + syncSettings.IsInitialSync + " \n" +
                              "Query Helper Take Value: " + syncSettings.Take + "\n" +
                              "Query Helper Skip Value: " + syncSettings.Skip + "\n");

            Console.WriteLine(
                "System is  giving you 30 seconds to validate and cancel the sync operation if anything found wrong!");
            Thread.Sleep(30000);

            //for (var i = 0; i < count/syncSettings.Take + 1; i++)
            //{
            //    //Read Gsp Exporters            
            //    var gspExporters = LoadGspExporters(gspDb, syncSettings.Skip, syncSettings.Take);

            //    //Convert to Payslip Exporter
            //    var payslipExporters = ConvertToPayslipExporter(gspExporters);

            //    //save to payslip db
            //    var save = SyncExportersToPayslipDb(payslipDb, payslipExporters, syncSettings);
            //}

            int currentPageSize = 10, currentPageNumber = 1, currentTakeNumber = 10;

            for (currentPageNumber = 1; (currentPageNumber - 1) * currentPageSize < count; currentPageNumber++)
            {
                var currentSkipNumber = (currentPageNumber - 1) * currentPageSize;
                if ((currentPageNumber * currentPageSize) >= count)
                {
                    currentTakeNumber = count - ((currentPageNumber - 1) * currentPageSize);
                }

                //Read Gsp Exporters            
                var gspExporters = LoadGspExportersNewPaging(gspDb, currentSkipNumber, currentTakeNumber);

                //Convert to Payslip Exporter
                var payslipExporters = ConvertToPayslipExporter(gspExporters);

                //save to payslip db
                var save = SyncExportersToPayslipDb(payslipDb, payslipExporters, syncSettings);
            }

            syncSettings.IsCycleComplete = true;
            syncSettings.LastSyncDate = DateTime.Now.AddMinutes(-30);
            syncSettings.Modified = DateTime.Now;
            if (syncSettings.IsInitialSync) syncSettings.IsInitialSync = false;


            payslipDb.SaveChanges();

            Console.WriteLine("Wow, the sync has been completed successfully!");
            Console.ReadLine();
        }

        private static void CreateFirstExporterSyncStatusEntry(EpbPayslipDbEntities payslipDb, SyncTable table)
        {
            payslipDb.SyncSettings.Add(new SyncSetting
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                CreatedBy = "System",
                Modified = DateTime.Now,
                ModifiedBy = "System",
                Active = true,
                SyncTable = (int)table,
                LastSyncDate = new DateTime(2017, 01, 01),
                NewSyncEntry = 0,
                ModifiedSyncEntry = 0,
                IsOneWaySync = true,
                IsInitialSync = true,
                IsCycleComplete = true,
                Take = 10,
                Skip = 0
            });

            payslipDb.SaveChanges();
        }

        private static bool SyncExportersToPayslipDb(EpbPayslipDbEntities payslipDb, List<Exporter> payslipExporters,
            SyncSetting syncSettings)
        {
            foreach (var payslipExporter in payslipExporters)
            {
                var exporter = payslipDb.Exporters.Find(payslipExporter.Id);

                Console.WriteLine(payslipExporter.CompanyOrFactoryName);

                if (exporter == null)
                {
                    payslipDb.Exporters.Add(payslipExporter);
                    syncSettings.NewSyncEntry += 1;
                }
                else
                {
                    var modifiedExporterData = ConvertionService.ModifiedExporterData(exporter, payslipExporter);
                    payslipDb.Entry(modifiedExporterData).State = EntityState.Modified;
                    syncSettings.ModifiedSyncEntry += 1;
                }

                try
                {
                    syncSettings.Skip += 1;
                    payslipDb.SaveChanges();
                }
                catch (Exception exception)
                {
                    WriteExceptionLog(exception);
                    Console.WriteLine(exception.Message);
                }
            }
            try
            {
                return payslipDb.SaveChanges() > 0;
            }
            catch (Exception exception)
            {
                WriteExceptionLog(exception);
                return false;
            }
        }

        private static void WriteExceptionLog(Exception exception)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
            var logDirectory = baseDirectory + DateTime.Today.Year + @"\" + DateTime.Today.Month + @"\" +
                               DateTime.Today.Day + @"\";
            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

            var logFile = logDirectory + DateTime.Today.Year + DateTime.Today.Month + DateTime.Today.Day + ".log";
            File.AppendAllText(logFile, "Date: " + DateTime.Now + Environment.NewLine);
            File.AppendAllText(logFile, exception + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }

        private static List<Exporter> ConvertToPayslipExporter(List<GspDataAccess.Exporter> gspExporters)
        {
            var exporters = new List<Exporter>();

            foreach (var x in gspExporters)
            {
                var exporter = new Exporter
                {
                    Id = x.Id.ToString(),
                    Created = x.Created,
                    CreatedBy = "System",
                    Modified = x.Changed,
                    ModifiedBy = "System",
                    Active = x.ActiveStateId == 1 ? true : false,
                    ActiveState = x.ActiveStateId,
                    ExporterCategory = (int)ExporterCategory.Textile,
                    ExporterType = x.ExporterType,
                    ExporterNumber = x.ExporterNo,
                    IdentityId = x.IdentityId,
                    EpbRegistrationNo = x.EpbRegistrationNo,
                    //RegDate = Convert.ToDateTime(x.RegDate),
                    //RegistrationValidSince = Convert.ToDateTime(x.RegistrationValidSince),
                    //PeriodofValidation = Convert.ToDateTime(x.PeriodofValidation),
                    IsUndertakingByExporter = x.IsUndertakingByExporter,
                    CompanyOrFactoryName = x.CompanyOrFactoryName,
                    CorporateAddress = x.CorporateAddress,
                    FactoryAddress = x.FactoryAddress,
                    AssociationMembershipNo = x.AssociationMembershipNo,
                    AssociationBinNo = x.AssociationBinNo,
                    AssociationTradeLicense = x.AssociationTradeLicense,
                    AssociationTinNo = x.AssociationTinNo,
                    ActivityDescription = x.ActivityDescription,
                    IndustrialProcessDescription = x.IndustrialProcessDescription,
                    GoodsDescription = x.GoodsDescription,
                    HsCode = x.HsCode,
                    Bonded = x.Bonded,
                    ErcNo = x.ErcNo,
                    BolRegNo = x.BolRegNo,
                    FireLicNo = x.FireLicNo,
                    BondLicNo = x.BondLicNo,
                    ServiceOffice = (int)ServiceOffice.Dhaka,
                    EpbReferenceNo = (int)EpbReferenceNo.DH
                };


                exporter.FactoryType = x.FactoryType != null
                    ? (x.FactoryType.NameResourceKey ?? x.FactoryType.NameResourceKey)
                    : "";

                exporter.RegDate = x.RegDate ?? DateTime.Today.AddYears(-50);
                exporter.RegistrationValidSince = x.RegistrationValidSince ?? DateTime.Today.AddYears(-50);
                exporter.PeriodofValidation = x.PeriodofValidation ?? DateTime.Today.AddYears(-50);

                exporter.PrimaryPersonName = x.Person == null ? "" : x.Person.FirstName + " " + x.Person.LastName;
                exporter.PrimaryPersonAddress = x.Person == null ? "" : x.Person.UserProfiles.FirstOrDefault()?.Address;
                exporter.PrimaryPersonPhone = x.Person == null ? "" : x.Person.MobilePhoneNo;

                exporter.SecondaryPersonName = x.Person1 == null ? "" : x.Person1.FirstName + " " + x.Person1.LastName;
                exporter.SecondaryPersonAddress = x.Person1 == null
                    ? ""
                    : x.Person1.UserProfiles.FirstOrDefault()?.Address;
                exporter.SecondaryPersonPhone = x.Person1 == null ? "" : x.Person1.MobilePhoneNo;

                exporter.CommercialManagerName = x.Person1 == null ? "" : x.Person1.FirstName + " " + x.Person1.LastName;
                exporter.CommercialManagerAddress = x.Person1 == null
                    ? ""
                    : x.Person1.UserProfiles.FirstOrDefault()?.Address;
                exporter.CommercialManagerPhone = x.Person1 == null ? "" : x.Person1.MobilePhoneNo;

                exporters.Add(exporter);
            }

            return exporters;
        }


        private static List<GspDataAccess.Exporter> LoadGspExporters(GspDBEntities gspDb, int skip, int take)
        {
            var exporters = gspDb.Exporters.OrderByDescending(x => x.ExporterNo).Skip(skip).Take(take).ToList();
            return exporters;
        }

        private static List<GspDataAccess.Exporter> LoadGspExportersNewPaging(GspDBEntities gspDb, int skip, int take)
        {
            var exporters = gspDb.Exporters.OrderByDescending(x => x.Changed).Skip(skip).Take(take).ToList();
            return exporters;
        }
    }
}