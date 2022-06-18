namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
            AddColumn("dbo.AspNetUsers", "OrgCode", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "CitizenID", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "JuristicID", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "PassportID", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "GovernmentAgentID", c => c.String(maxLength: 450));
            CreateIndex("dbo.AspNetUsers", "OrgCode");
            AddForeignKey("dbo.AspNetUsers", "OrgCode", "dbo.Organization", "OrgCode");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "OrgCode", "dbo.Organization");
            DropIndex("dbo.AspNetUsers", new[] { "OrgCode" });
            DropColumn("dbo.AspNetUsers", "GovernmentAgentID");
            DropColumn("dbo.AspNetUsers", "PassportID");
            DropColumn("dbo.AspNetUsers", "JuristicID");
            DropColumn("dbo.AspNetUsers", "CitizenID");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "OrgCode");
            DropColumn("dbo.AspNetUsers", "UserType");
        }
    }
}
