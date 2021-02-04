namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productentityupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "BarCodeNo", c => c.String());
            AlterColumn("dbo.Product", "Power", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Power", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Product", "BarCodeNo");
        }
    }
}
