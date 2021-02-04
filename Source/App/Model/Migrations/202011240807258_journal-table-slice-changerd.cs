namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class journaltableslicechangerd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Journal", "CreatedBy", c => c.String(maxLength: 128));
            CreateIndex("dbo.Journal", "CreatedBy");
            AddForeignKey("dbo.Journal", "CreatedBy", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journal", "CreatedBy", "dbo.User");
            DropIndex("dbo.Journal", new[] { "CreatedBy" });
            AlterColumn("dbo.Journal", "CreatedBy", c => c.String());
        }
    }
}
