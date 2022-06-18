namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationRoleInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Description", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Description");
        }
    }
}
