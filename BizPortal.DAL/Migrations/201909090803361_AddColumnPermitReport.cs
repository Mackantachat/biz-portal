namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPermitReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PermitSummaryReport", "AppSysName", c => c.String(nullable: false));
            AddColumn("dbo.PermitSummaryReport", "BusinessCode", c => c.String());
            AddColumn("dbo.PermitSummaryReport", "BusinessName", c => c.String());
            AddColumn("dbo.PermitSummaryReport", "JobIncompleteDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobWaitingDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobCheckDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobPendingDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobApprovedWaitingPayFeeDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobPaidFeeCreatingLicenseDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobRejectedDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobCompletedDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobFailedDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobCanceledDate", c => c.DateTime());
            AddColumn("dbo.PermitSummaryReport", "JobUpdatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PermitSummaryReport", "RecordUpdatedDate");
            DropTable("dbo.PermitSummaryStatusTracking");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.PermitSummaryReport", "RecordUpdatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PermitSummaryReport", "JobUpdatedDate");
            DropColumn("dbo.PermitSummaryReport", "JobCanceledDate");
            DropColumn("dbo.PermitSummaryReport", "JobFailedDate");
            DropColumn("dbo.PermitSummaryReport", "JobCompletedDate");
            DropColumn("dbo.PermitSummaryReport", "JobRejectedDate");
            DropColumn("dbo.PermitSummaryReport", "JobPaidFeeCreatingLicenseDate");
            DropColumn("dbo.PermitSummaryReport", "JobApprovedWaitingPayFeeDate");
            DropColumn("dbo.PermitSummaryReport", "JobPendingDate");
            DropColumn("dbo.PermitSummaryReport", "JobCheckDate");
            DropColumn("dbo.PermitSummaryReport", "JobWaitingDate");
            DropColumn("dbo.PermitSummaryReport", "JobIncompleteDate");
            DropColumn("dbo.PermitSummaryReport", "BusinessName");
            DropColumn("dbo.PermitSummaryReport", "BusinessCode");
            DropColumn("dbo.PermitSummaryReport", "AppSysName");
        }
    }
}
