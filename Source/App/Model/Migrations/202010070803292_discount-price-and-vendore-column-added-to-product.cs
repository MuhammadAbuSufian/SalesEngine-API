namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discountpriceandvendorecolumnaddedtoproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Product", "VendorBarcodeNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "VendorBarcodeNo");
            DropColumn("dbo.Product", "DiscountPrice");
        }
    }
}
