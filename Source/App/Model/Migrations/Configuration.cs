using System.Web.Security;

namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Project.Model.BusinessDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;            
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Project.Model.BusinessDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //

            string UserGuid = "fcc3130d-8443-4b32-8946-571b4c9daf67";
            string RoleGuid = "gcc3130d-8c43-4b52-8986-571b4c9daf85";
            string CompanyGuid = "pccb130d-8443-1b32-8d46-571b4c9daf68";
   
            context.Companies.AddOrUpdate(
                new Company
                {
                    Id = CompanyGuid,
                    Name = "Codelys",
                    Address = "Padma Abasik, Rajshahi",
                    Email = "medi-pos-admin@codelys.com",
                    ValidTill = DateTime.Today.AddYears(100),
                    CreatedBy = "",
                    ModifiedBy = "",
                    CreatedCompany = "0",
                    Created = DateTime.Now,
                    Modified = null,
                    Active = true
                }
            );

            context.Roles.AddOrUpdate(
                new Role
                {
                    Id = RoleGuid,
                    Name = "SuperAdmin",
                    CreatedBy = "",
                    ModifiedBy = "",
                    CreatedCompany = CompanyGuid,
                    Created = DateTime.Now,
                    Modified = null,
                    Active = true
                }
            );

            context.Users.AddOrUpdate(
                  new User
                  {
                      Id = UserGuid,
                      Name = "Super Admin",
                      Email = "medi-pos-admin@codelys.com",
                      Address = "3444 73rd Street, Jackson Heights, NY 11372",
                      Phone = "00000000",
                      Password = CreatePasswordHash("codelyx"),
                      RoleId = RoleGuid,
                      CompanyId = CompanyGuid,
                      CreatedBy = "",
                      ModifiedBy = "",
                      CreatedCompany = CompanyGuid,
                      Created = DateTime.Now,
                      Modified = null,
                      Active = true
                  }
            );
            
        }

        public string CreatePasswordHash(string pwd)
        {
            string pwdAndSalt = String.Concat(pwd, "9F195724C95A7E56CBB5");
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdAndSalt, "sha1");

            return hashedPwd;
        }
    }
}
