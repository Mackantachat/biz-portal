namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCompanyToJuristic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompanyTranslation", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.CompanyApplicationStatus", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.CompanyApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.CompanyApplicationStatus", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyApplicationStatus", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyApplicationStatus", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Company", new[] { "CompanyTaxID" });
            DropIndex("dbo.CompanyTranslation", new[] { "CompanyID" });
            DropIndex("dbo.CompanyTranslation", new[] { "LanguageID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "ApplicationID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "CompanyID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "ApplicationStatusID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "CreatedUserID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "UpdatedUserID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "DeletedUserID" });
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
                .PrimaryKey(t => t.JuristicApplicationStatusID)
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
            
            DropTable("dbo.Company");
            DropTable("dbo.CompanyTranslation");
            DropTable("dbo.CompanyApplicationStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CompanyApplicationStatus",
                c => new
                    {
                        CompanyApplicationStatusID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
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
                .PrimaryKey(t => t.CompanyApplicationStatusID);
            
            CreateTable(
                "dbo.CompanyTranslation",
                c => new
                    {
                        CompanyTranslationID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 450),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyTranslationID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanySysName = c.String(nullable: false, maxLength: 450),
                        CompanyTaxID = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            DropForeignKey("dbo.JuristicApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatus", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatus", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JuristicApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.JuristicApplicationStatus", "ApplicationID", "dbo.Application");
            DropIndex("dbo.JuristicApplicationStatus", new[] { "DeletedUserID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "UpdatedUserID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "CreatedUserID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "ApplicationStatusID" });
            DropIndex("dbo.JuristicApplicationStatus", new[] { "ApplicationID" });
            DropTable("dbo.JuristicApplicationStatus");
            CreateIndex("dbo.CompanyApplicationStatus", "DeletedUserID");
            CreateIndex("dbo.CompanyApplicationStatus", "UpdatedUserID");
            CreateIndex("dbo.CompanyApplicationStatus", "CreatedUserID");
            CreateIndex("dbo.CompanyApplicationStatus", "ApplicationStatusID");
            CreateIndex("dbo.CompanyApplicationStatus", "CompanyID");
            CreateIndex("dbo.CompanyApplicationStatus", "ApplicationID");
            CreateIndex("dbo.CompanyTranslation", "LanguageID");
            CreateIndex("dbo.CompanyTranslation", "CompanyID");
            CreateIndex("dbo.Company", "CompanyTaxID", unique: true);
            AddForeignKey("dbo.CompanyApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CompanyApplicationStatus", "DeletedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CompanyApplicationStatus", "CreatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CompanyApplicationStatus", "CompanyID", "dbo.Company", "CompanyID");
            AddForeignKey("dbo.CompanyApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus", "ApplicationStatusID");
            AddForeignKey("dbo.CompanyApplicationStatus", "ApplicationID", "dbo.Application", "ApplicationID");
            AddForeignKey("dbo.CompanyTranslation", "LanguageID", "dbo.Language", "LanguageID");
            AddForeignKey("dbo.CompanyTranslation", "CompanyID", "dbo.Company", "CompanyID");
        }
    }
}
