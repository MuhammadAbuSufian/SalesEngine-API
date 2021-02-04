namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salesentitymodified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesDetail", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesDetail", "Amount", c => c.String());
        }
    }
}
