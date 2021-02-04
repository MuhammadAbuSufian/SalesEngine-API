namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posentityadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Representive = c.String(),
                        RepContact = c.String(),
                        Note = c.String(),
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
                "dbo.Group",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
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
                "dbo.Product",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ApplicationFor = c.String(),
                        ApplicationTo = c.String(),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Power = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Long(nullable: false),
                        GroupId = c.String(maxLength: 128),
                        BrandId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Brand", t => t.BrandId)
                .ForeignKey("dbo.Group", t => t.GroupId)
                .Index(t => t.Id, unique: true)
                .Index(t => t.GroupId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.PurchaseDetail",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        PurchaseId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Purchase", t => t.PurchaseId)
                .Index(t => t.Id, unique: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceNo = c.String(),
                        Comment = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                "dbo.SalesDetail",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.String(),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SalesId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Sale", t => t.SalesId)
                .Index(t => t.Id, unique: true)
                .Index(t => t.SalesId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceNo = c.String(),
                        Commint = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesDetail", "SalesId", "dbo.Sale");
            DropForeignKey("dbo.SalesDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PurchaseDetail", "PurchaseId", "dbo.Purchase");
            DropForeignKey("dbo.PurchaseDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "GroupId", "dbo.Group");
            DropForeignKey("dbo.Product", "BrandId", "dbo.Brand");
            DropIndex("dbo.Sale", new[] { "Id" });
            DropIndex("dbo.SalesDetail", new[] { "ProductId" });
            DropIndex("dbo.SalesDetail", new[] { "SalesId" });
            DropIndex("dbo.SalesDetail", new[] { "Id" });
            DropIndex("dbo.Purchase", new[] { "Id" });
            DropIndex("dbo.PurchaseDetail", new[] { "ProductId" });
            DropIndex("dbo.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("dbo.PurchaseDetail", new[] { "Id" });
            DropIndex("dbo.Product", new[] { "BrandId" });
            DropIndex("dbo.Product", new[] { "GroupId" });
            DropIndex("dbo.Product", new[] { "Id" });
            DropIndex("dbo.Group", new[] { "Id" });
            DropIndex("dbo.Brand", new[] { "Id" });
            DropTable("dbo.Sale");
            DropTable("dbo.SalesDetail");
            DropTable("dbo.Purchase");
            DropTable("dbo.PurchaseDetail");
            DropTable("dbo.Product");
            DropTable("dbo.Group");
            DropTable("dbo.Brand");
        }
    }
}
