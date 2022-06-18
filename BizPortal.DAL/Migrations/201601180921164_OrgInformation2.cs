namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgInformation2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OrganizationTranslation", new[] { "Organization_OrgCode" });
            RenameColumn(table: "dbo.OrganizationTranslation", name: "Organization_OrgCode", newName: "OrgCode");
            AlterColumn("dbo.OrganizationTranslation", "OrgCode", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.OrganizationTranslation", "OrgCode");
            DropColumn("dbo.OrganizationTranslation", "OrganizationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationTranslation", "OrganizationID", c => c.Int(nullable: false));
            DropIndex("dbo.OrganizationTranslation", new[] { "OrgCode" });
            AlterColumn("dbo.OrganizationTranslation", "OrgCode", c => c.String(maxLength: 450));
            RenameColumn(table: "dbo.OrganizationTranslation", name: "OrgCode", newName: "Organization_OrgCode");
            CreateIndex("dbo.OrganizationTranslation", "Organization_OrgCode");
        }
    }
}
