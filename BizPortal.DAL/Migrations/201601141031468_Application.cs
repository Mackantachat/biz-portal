namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Application : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false, identity: true),
                        ApplicationSysName = c.String(nullable: false, maxLength: 450),
                        OrgCode = c.String(nullable: false, maxLength: 450),
                        ApplicationUrl = c.String(maxLength: 1000),
                        ConsumerKey = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ApplicationID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.Organization", t => t.OrgCode)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.OrgCode)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.ApplicationTranslation",
                c => new
                    {
                        ApplicationTranslationID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        ApplicationName = c.String(nullable: false, maxLength: 450),
                        ApplicationDetail = c.String(maxLength: 1000),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationTranslationID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .Index(t => t.ApplicationID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        OrgCode = c.String(nullable: false, maxLength: 450),
                        OrgSysName = c.String(nullable: false, maxLength: 450),
                        Address = c.String(maxLength: 1000),
                        Url = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.OrgCode);
            
            CreateTable(
                "dbo.OrganizationTranslation",
                c => new
                    {
                        OrganizationTranslationID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        OrgName = c.String(nullable: false, maxLength: 450),
                        Abbreviation = c.String(maxLength: 450),
                        LanguageID = c.Int(nullable: false),
                        Organization_OrgCode = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.OrganizationTranslationID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .ForeignKey("dbo.Organization", t => t.Organization_OrgCode)
                .Index(t => t.LanguageID)
                .Index(t => t.Organization_OrgCode);
            
            CreateTable(
                "dbo.ApplicationStatus",
                c => new
                    {
                        ApplicationStatusID = c.Int(nullable: false, identity: true),
                        ApplicationSysStatusName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.ApplicationStatusID);
            
            CreateTable(
                "dbo.ApplicationStatusTranslation",
                c => new
                    {
                        ApplicationStatusTranslationID = c.Int(nullable: false, identity: true),
                        ApplicationStatusID = c.Int(nullable: false),
                        ApplicationStatusName = c.String(nullable: false, maxLength: 450),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationStatusTranslationID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanySysName = c.String(nullable: false, maxLength: 450),
                        CompanyTaxID = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => t.CompanyID)
                .Index(t => t.CompanyTaxID, unique: true);
            
            CreateTable(
                "dbo.CompanyTranslation",
                c => new
                    {
                        CompanyTranslationID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 450),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyTranslationID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .Index(t => t.CompanyID)
                .Index(t => t.LanguageID);
            
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
                .PrimaryKey(t => t.CompanyApplicationStatusID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.ApplicationStatus", t => t.ApplicationStatusID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.ApplicationID)
                .Index(t => t.CompanyID)
                .Index(t => t.ApplicationStatusID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyApplicationStatus", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyApplicationStatus", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyApplicationStatus", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyApplicationStatus", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyApplicationStatus", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.CompanyApplicationStatus", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.CompanyTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.CompanyTranslation", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.ApplicationStatusTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.ApplicationStatusTranslation", "ApplicationStatusID", "dbo.ApplicationStatus");
            DropForeignKey("dbo.Application", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Application", "OrgCode", "dbo.Organization");
            DropForeignKey("dbo.OrganizationTranslation", "Organization_OrgCode", "dbo.Organization");
            DropForeignKey("dbo.OrganizationTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.Application", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Application", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.ApplicationTranslation", "ApplicationID", "dbo.Application");
            DropIndex("dbo.CompanyApplicationStatus", new[] { "DeletedUserID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "UpdatedUserID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "CreatedUserID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "ApplicationStatusID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "CompanyID" });
            DropIndex("dbo.CompanyApplicationStatus", new[] { "ApplicationID" });
            DropIndex("dbo.CompanyTranslation", new[] { "LanguageID" });
            DropIndex("dbo.CompanyTranslation", new[] { "CompanyID" });
            DropIndex("dbo.Company", new[] { "CompanyTaxID" });
            DropIndex("dbo.ApplicationStatusTranslation", new[] { "LanguageID" });
            DropIndex("dbo.ApplicationStatusTranslation", new[] { "ApplicationStatusID" });
            DropIndex("dbo.OrganizationTranslation", new[] { "Organization_OrgCode" });
            DropIndex("dbo.OrganizationTranslation", new[] { "LanguageID" });
            DropIndex("dbo.ApplicationTranslation", new[] { "LanguageID" });
            DropIndex("dbo.ApplicationTranslation", new[] { "ApplicationID" });
            DropIndex("dbo.Application", new[] { "DeletedUserID" });
            DropIndex("dbo.Application", new[] { "UpdatedUserID" });
            DropIndex("dbo.Application", new[] { "CreatedUserID" });
            DropIndex("dbo.Application", new[] { "OrgCode" });
            DropTable("dbo.CompanyApplicationStatus");
            DropTable("dbo.CompanyTranslation");
            DropTable("dbo.Company");
            DropTable("dbo.ApplicationStatusTranslation");
            DropTable("dbo.ApplicationStatus");
            DropTable("dbo.OrganizationTranslation");
            DropTable("dbo.Organization");
            DropTable("dbo.ApplicationTranslation");
            DropTable("dbo.Application");
        }
    }
}
