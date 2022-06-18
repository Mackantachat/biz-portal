namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgInformation1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "GovernmentAgentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "GovernmentAgentID", c => c.String(maxLength: 450));
        }
    }
}
