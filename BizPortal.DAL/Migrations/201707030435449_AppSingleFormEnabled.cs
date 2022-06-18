namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppSingleFormEnabled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "SingleFormEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "SingleFormEnabled");
        }
    }
}
