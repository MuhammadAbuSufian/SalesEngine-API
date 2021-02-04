namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class businesstypecolumnaddedtocompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "BusinessType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Company", "BusinessType");
        }
    }
}
