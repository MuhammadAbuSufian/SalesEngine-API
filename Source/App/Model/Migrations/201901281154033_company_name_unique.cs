namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class company_name_unique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Company", new[] { "Name" });
            AlterColumn("dbo.Company", "Name", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Company", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Company", new[] { "Name" });
            AlterColumn("dbo.Company", "Name", c => c.String(nullable: false, maxLength: 128, unicode: false));
            CreateIndex("dbo.Company", "Name", unique: true);
        }
    }
}
