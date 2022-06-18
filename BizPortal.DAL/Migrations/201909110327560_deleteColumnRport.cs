namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteColumnRport : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PermitSummaryReport", "BusinessCode");
            DropColumn("dbo.PermitSummaryReport", "BusinessName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PermitSummaryReport", "BusinessName", c => c.String());
            AddColumn("dbo.PermitSummaryReport", "BusinessCode", c => c.String());
        }
    }
}
