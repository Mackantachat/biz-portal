namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "MinistryCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Organization", "DepartmentCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Organization", "DivisionCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Organization", "LogoUrl", c => c.String(maxLength: 1000));
            AddColumn("dbo.OrganizationTranslation", "Address", c => c.String(maxLength: 1000));
            DropColumn("dbo.Organization", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organization", "Address", c => c.String(maxLength: 1000));
            DropColumn("dbo.OrganizationTranslation", "Address");
            DropColumn("dbo.Organization", "LogoUrl");
            DropColumn("dbo.Organization", "DivisionCode");
            DropColumn("dbo.Organization", "DepartmentCode");
            DropColumn("dbo.Organization", "MinistryCode");
        }
    }
}
