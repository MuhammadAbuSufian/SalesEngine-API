namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class journaltablemodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journal", "JournalTypeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Journal", "JournalTypeId");
            AddForeignKey("dbo.Journal", "JournalTypeId", "dbo.JournalType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journal", "JournalTypeId", "dbo.JournalType");
            DropIndex("dbo.Journal", new[] { "JournalTypeId" });
            DropColumn("dbo.Journal", "JournalTypeId");
        }
    }
}
