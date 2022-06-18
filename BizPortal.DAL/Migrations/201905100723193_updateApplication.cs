namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "FileOwner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "FileOwner");
        }
    }
}
