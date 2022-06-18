namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJuristicRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JuristicApplicationStatus", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.JuristicApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.JuristicApplicationStatus", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatus", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.JuristicApplicationStatus", new[] { "ApplicationID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "ApplicationStatusID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "CreatedUserID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "UpdatedUserID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "DeletedUserID" });
            CreateTable(
                "dbo.JuristicApplicationStatusRequest",
                c => new
                    {
                        JuristicApplicationStatusRequestID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        JuristicID = c.Int(nullable: false),
                        Remark = c.String(maxLength: 1000),
                        ApplicationStatusID = c.Int(),
                        ApplicationStatusOther = c.String(maxLength: 450),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.JuristicApplicationStatusRequestID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.ApplicationID)
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.JuristicApplicationStatusRequestLog",
                c => new
                    {
                        JuristicApplicationStatusRequestLogID = c.Int(nullable: false, identity: true),
                        JuristicApplicationStatusRequestID = c.Int(nullable: false),
                        Remark = c.String(maxLength: 1000),
                        ApplicationStatusID = c.Int(nullable: false),
                        ApplicationStatusOther = c.String(maxLength: 450),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.JuristicApplicationStatusRequestLogID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.JuristicApplicationStatusRequest", t => t.JuristicApplicationStatusRequestID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.JuristicApplicationStatusRequestID)
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            DropTable("dbo.JuristicApplicationStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JuristicApplicationStatus",
                c => new
                    {
                        JuristicApplicationStatusID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        JuristicID = c.Int(nullable: false),
                        Remark = c.String(maxLength: 1000),
                        ApplicationStatusID = c.Int(),
                        ApplicationStatusOther = c.String(maxLength: 450),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.JuristicApplicationStatusID);
            
            DropForeignKey("dbo.JuristicApplicationStatusRequest", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequestLog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequestLog", "JuristicApplicationStatusRequestID", "dbo.JuristicApplicationStatusRequest");
            DropForeignKey("dbo.JuristicApplicationStatusRequestLog", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequestLog", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequestLog", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.JuristicApplicationStatusRequest", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequest", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatusRequest", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.JuristicApplicationStatusRequest", "ApplicationID", "dbo.Application");
            DropIndex("dbo.JuristicApplicationStatusRequestLog", new[] { "DeletedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequestLog", new[] { "UpdatedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequestLog", new[] { "CreatedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequestLog", new[] { "ApplicationStatusID" });
            DropIndex("dbo.JuristicApplicationStatusRequestLog", new[] { "JuristicApplicationStatusRequestID" });
            DropIndex("dbo.JuristicApplicationStatusRequest", new[] { "DeletedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequest", new[] { "UpdatedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequest", new[] { "CreatedUserID" });
            DropIndex("dbo.JuristicApplicationStatusRequest", new[] { "ApplicationStatusID" });
            DropIndex("dbo.JuristicApplicationStatusRequest", new[] { "ApplicationID" });
            DropTable("dbo.JuristicApplicationStatusRequestLog");
            DropTable("dbo.JuristicApplicationStatusRequest");
            CreateIndex("dbo.JuristicApplicationStatus", "DeletedUserID");
            CreateIndex("dbo.JuristicApplicationStatus", "UpdatedUserID");
            CreateIndex("dbo.JuristicApplicationStatus", "CreatedUserID");
            CreateIndex("dbo.JuristicApplicationStatus", "ApplicationStatusID");
            CreateIndex("dbo.JuristicApplicationStatus", "ApplicationID");
            AddForeignKey("dbo.JuristicApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JuristicApplicationStatus", "DeletedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JuristicApplicationStatus", "CreatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JuristicApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus", "ApplicationStatusID");
            AddForeignKey("dbo.JuristicApplicationStatus", "ApplicationID", "dbo.Application", "ApplicationID");
        }
    }
}
