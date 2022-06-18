namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplicationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "ApplicationType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "ApplicationType");
        }
    }
}
