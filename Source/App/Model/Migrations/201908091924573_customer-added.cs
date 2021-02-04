namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customeradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
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
            
            AddColumn("dbo.Sale", "Due", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Sale", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sale", "CustomerId");
            AddForeignKey("dbo.Sale", "CustomerId", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Sale", new[] { "CustomerId" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropColumn("dbo.Sale", "CustomerId");
            DropColumn("dbo.Sale", "Due");
            DropTable("dbo.Customer");
        }
    }
}
