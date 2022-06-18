namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInformation3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "ShowRemark", c => c.Boolean(nullable: false));
            AddColumn("dbo.Application", "Remark", c => c.String());
            AddColumn("dbo.Application", "CitizenShowRemark", c => c.Boolean(nullable: false));
            AddColumn("dbo.Application", "CitizenRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "CitizenRemark");
            DropColumn("dbo.Application", "CitizenShowRemark");
            DropColumn("dbo.Application", "Remark");
            DropColumn("dbo.Application", "ShowRemark");
        }
    }
}
