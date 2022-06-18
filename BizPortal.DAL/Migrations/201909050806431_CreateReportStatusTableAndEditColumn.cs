namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReportStatusTableAndEditColumn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermitSummaryStatusTracking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationRequestID = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        StatusOther = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PermitSummaryReport", "ApplicationRequestID", c => c.String(nullable: false));
            AddColumn("dbo.PermitSummaryReport", "ApplicationRequestNumber", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.PermitSummaryReport", "RecordUpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PermitSummaryReport", "RecordUpdatedDate");
            DropColumn("dbo.PermitSummaryReport", "ApplicationRequestNumber");
            DropColumn("dbo.PermitSummaryReport", "ApplicationRequestID");
            DropTable("dbo.PermitSummaryStatusTracking");
        }
    }
}
