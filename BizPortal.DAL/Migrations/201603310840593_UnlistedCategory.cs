namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnlistedCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Unlisted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Unlisted");
        }
    }
}
