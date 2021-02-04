namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class journalandjournaltypeaddedandcompanytablemodified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        ApprovedBy = c.String(),
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
                "dbo.JournalType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
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
            
            AddColumn("dbo.Company", "Balence", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Company", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.JournalType", new[] { "Id" });
            DropIndex("dbo.Journal", new[] { "Id" });
            DropColumn("dbo.Company", "Note");
            DropColumn("dbo.Company", "Balence");
            DropTable("dbo.JournalType");
            DropTable("dbo.Journal");
        }
    }
}
