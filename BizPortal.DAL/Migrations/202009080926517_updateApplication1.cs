namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplication1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "IsEnableELicense", c => c.Boolean(nullable: false));
            AddColumn("dbo.Application", "ELicenseConsumerKey", c => c.String());
            AddColumn("dbo.Application", "ELicenseSecret", c => c.String());
            DropColumn("dbo.Application", "IsEnableSigning");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "IsEnableSigning", c => c.Boolean(nullable: false));
            DropColumn("dbo.Application", "ELicenseSecret");
            DropColumn("dbo.Application", "ELicenseConsumerKey");
            DropColumn("dbo.Application", "IsEnableELicense");
        }
    }
}
