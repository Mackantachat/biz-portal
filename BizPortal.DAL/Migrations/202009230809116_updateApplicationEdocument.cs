namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplicationEdocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "ELicenseXMLStandard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "ELicenseXMLStandard");
        }
    }
}
