namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Salary", newName: "Token");
            DropForeignKey("dbo.Attendance", "UserId", "dbo.User");
            DropForeignKey("dbo.Attendance", "SalaryId", "dbo.Salary");
            DropForeignKey("dbo.DeliveryInvoice", "DeliveryPlaceId", "dbo.DeliveryPlace");
            DropForeignKey("dbo.DeliveryInvoice", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.DeliveryPlace", "DeliveryLogId", "dbo.DeliveryLog");
            DropForeignKey("dbo.DeliveryLog", "DriverId", "dbo.User");
            DropForeignKey("dbo.PaymentReceivableDetail", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.PaymentReceivableDetail", "PaymentReceivableId", "dbo.PaymentReceivable");
            DropIndex("dbo.Attendance", new[] { "Id" });
            DropIndex("dbo.Attendance", new[] { "UserId" });
            DropIndex("dbo.Attendance", new[] { "SalaryId" });
            DropIndex("dbo.DeliveryLog", new[] { "Id" });
            DropIndex("dbo.DeliveryLog", new[] { "DriverId" });
            DropIndex("dbo.DeliveryPlace", new[] { "Id" });
            DropIndex("dbo.DeliveryPlace", new[] { "DeliveryLogId" });
            DropIndex("dbo.DeliveryInvoice", new[] { "Id" });
            DropIndex("dbo.DeliveryInvoice", new[] { "InvoiceId" });
            DropIndex("dbo.DeliveryInvoice", new[] { "DeliveryPlaceId" });
            DropIndex("dbo.Invoice", new[] { "Id" });
            DropIndex("dbo.PaymentReceivableDetail", new[] { "Id" });
            DropIndex("dbo.PaymentReceivableDetail", new[] { "PaymentReceivableId" });
            DropIndex("dbo.PaymentReceivableDetail", new[] { "InvoiceId" });
            DropIndex("dbo.PaymentReceivable", new[] { "Id" });
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        ValidTill = c.DateTime(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        CreatedCompany = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.PermissionMap",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(maxLength: 128),
                        PermissionId = c.String(maxLength: 128),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        CreatedCompany = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permission", t => t.PermissionId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.Id, unique: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Resource = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        CreatedCompany = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        CreatedCompany = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            AddColumn("dbo.Token", "ExpireAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Token", "Ticket", c => c.String());
            AddColumn("dbo.Token", "CreatedCompany", c => c.String(nullable: false));
            AddColumn("dbo.User", "RoleId", c => c.String(maxLength: 128));
            AddColumn("dbo.User", "CompanyId", c => c.String(maxLength: 128));
            AddColumn("dbo.User", "CreatedCompany", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
            CreateIndex("dbo.User", "RoleId");
            CreateIndex("dbo.User", "CompanyId");
            AddForeignKey("dbo.User", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.User", "RoleId", "dbo.Role", "Id");
            DropColumn("dbo.Token", "Rate");
            DropColumn("dbo.User", "UserName");
            DropColumn("dbo.User", "Vehicle");
            DropColumn("dbo.User", "LicenceNo");
            DropColumn("dbo.User", "Role");
            DropColumn("dbo.User", "LogedIn");
            DropColumn("dbo.User", "OverTime");
            DropColumn("dbo.User", "BreakTime");
            DropColumn("dbo.User", "Token");
            DropTable("dbo.Attendance");
            DropTable("dbo.DeliveryLog");
            DropTable("dbo.DeliveryPlace");
            DropTable("dbo.DeliveryInvoice");
            DropTable("dbo.Invoice");
            DropTable("dbo.PaymentReceivableDetail");
            DropTable("dbo.PaymentReceivable");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentReceivable",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentReceivableNo = c.String(nullable: false),
                        TotalInvoice = c.Int(nullable: false),
                        TotalPending = c.Decimal(precision: 18, scale: 2),
                        TotalCash = c.Decimal(precision: 18, scale: 2),
                        TotalCheck = c.Decimal(precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentReceivableDetail",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Cash = c.Decimal(precision: 18, scale: 2),
                        Check = c.Decimal(precision: 18, scale: 2),
                        PendingAmount = c.Decimal(precision: 18, scale: 2),
                        CheckNo = c.String(),
                        Status = c.Int(nullable: false),
                        PaymentReceivableId = c.String(maxLength: 128),
                        InvoiceId = c.String(maxLength: 128),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RecordId = c.String(nullable: false),
                        InvoiceNo = c.String(nullable: false),
                        Name = c.String(),
                        FileName = c.String(),
                        Path = c.String(),
                        FileType = c.String(nullable: false),
                        Category = c.String(),
                        CustomerName = c.String(),
                        SalesRepId = c.String(),
                        PaymentStatus = c.Int(nullable: false),
                        PendingAmount = c.Decimal(precision: 18, scale: 2),
                        InvoiceAmount = c.Decimal(precision: 18, scale: 2),
                        City = c.String(),
                        InvoiceDate = c.DateTime(),
                        Public = c.Boolean(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryInvoice",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Status = c.Boolean(nullable: false),
                        InvoiceId = c.String(maxLength: 128),
                        DeliveryPlaceId = c.String(maxLength: 128),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryPlace",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Place = c.String(nullable: false),
                        CityId = c.String(),
                        DeliveryLogId = c.String(maxLength: 128),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryLog",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DeliveryId = c.String(nullable: false),
                        DeliveryLogDate = c.DateTime(),
                        DriverId = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        In = c.DateTime(nullable: false),
                        Out = c.DateTime(),
                        AccuracyIn = c.Decimal(precision: 18, scale: 2),
                        LatitudeIn = c.Decimal(precision: 18, scale: 2),
                        LongitudeIn = c.Decimal(precision: 18, scale: 2),
                        AccuracyOut = c.Decimal(precision: 18, scale: 2),
                        LatitudeOut = c.Decimal(precision: 18, scale: 2),
                        LongitudeOut = c.Decimal(precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        SalaryId = c.String(maxLength: 128),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletionTime = c.DateTime(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "Token", c => c.String(nullable: false));
            AddColumn("dbo.User", "BreakTime", c => c.Int(nullable: false));
            AddColumn("dbo.User", "OverTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "LogedIn", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "Role", c => c.Int(nullable: false));
            AddColumn("dbo.User", "LicenceNo", c => c.String());
            AddColumn("dbo.User", "Vehicle", c => c.String());
            AddColumn("dbo.User", "UserName", c => c.String(nullable: false));
            AddColumn("dbo.Token", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.User", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.PermissionMap", "RoleId", "dbo.Role");
            DropForeignKey("dbo.PermissionMap", "PermissionId", "dbo.Permission");
            DropIndex("dbo.User", new[] { "CompanyId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Role", new[] { "Id" });
            DropIndex("dbo.Permission", new[] { "Id" });
            DropIndex("dbo.PermissionMap", new[] { "PermissionId" });
            DropIndex("dbo.PermissionMap", new[] { "RoleId" });
            DropIndex("dbo.PermissionMap", new[] { "Id" });
            DropIndex("dbo.Company", new[] { "Id" });
            AlterColumn("dbo.User", "Password", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String());
            DropColumn("dbo.User", "CreatedCompany");
            DropColumn("dbo.User", "CompanyId");
            DropColumn("dbo.User", "RoleId");
            DropColumn("dbo.Token", "CreatedCompany");
            DropColumn("dbo.Token", "Ticket");
            DropColumn("dbo.Token", "ExpireAt");
            DropTable("dbo.Role");
            DropTable("dbo.Permission");
            DropTable("dbo.PermissionMap");
            DropTable("dbo.Company");
            CreateIndex("dbo.PaymentReceivable", "Id", unique: true);
            CreateIndex("dbo.PaymentReceivableDetail", "InvoiceId");
            CreateIndex("dbo.PaymentReceivableDetail", "PaymentReceivableId");
            CreateIndex("dbo.PaymentReceivableDetail", "Id", unique: true);
            CreateIndex("dbo.Invoice", "Id", unique: true);
            CreateIndex("dbo.DeliveryInvoice", "DeliveryPlaceId");
            CreateIndex("dbo.DeliveryInvoice", "InvoiceId");
            CreateIndex("dbo.DeliveryInvoice", "Id", unique: true);
            CreateIndex("dbo.DeliveryPlace", "DeliveryLogId");
            CreateIndex("dbo.DeliveryPlace", "Id", unique: true);
            CreateIndex("dbo.DeliveryLog", "DriverId");
            CreateIndex("dbo.DeliveryLog", "Id", unique: true);
            CreateIndex("dbo.Attendance", "SalaryId");
            CreateIndex("dbo.Attendance", "UserId");
            CreateIndex("dbo.Attendance", "Id", unique: true);
            AddForeignKey("dbo.PaymentReceivableDetail", "PaymentReceivableId", "dbo.PaymentReceivable", "Id");
            AddForeignKey("dbo.PaymentReceivableDetail", "InvoiceId", "dbo.Invoice", "Id");
            AddForeignKey("dbo.DeliveryLog", "DriverId", "dbo.User", "Id");
            AddForeignKey("dbo.DeliveryPlace", "DeliveryLogId", "dbo.DeliveryLog", "Id");
            AddForeignKey("dbo.DeliveryInvoice", "InvoiceId", "dbo.Invoice", "Id");
            AddForeignKey("dbo.DeliveryInvoice", "DeliveryPlaceId", "dbo.DeliveryPlace", "Id");
            AddForeignKey("dbo.Attendance", "SalaryId", "dbo.Salary", "Id");
            AddForeignKey("dbo.Attendance", "UserId", "dbo.User", "Id");
            RenameTable(name: "dbo.Token", newName: "Salary");
        }
    }
}
