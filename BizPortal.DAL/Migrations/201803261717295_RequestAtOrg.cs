namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestAtOrg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "RequestAtOrg", c => c.Boolean(nullable: false));
            AddColumn("dbo.Application", "CitizenRequestAtOrg", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "CitizenRequestAtOrg");
            DropColumn("dbo.Application", "RequestAtOrg");
        }
    }
}
