namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitBizPortal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        Unlisted = c.Boolean(nullable: false),
                        SectionID = c.Int(),
                        CategoryID = c.Int(),
                        StartPublishing = c.DateTime(),
                        FinishPublishing = c.DateTime(),
                        Reads = c.Int(nullable: false),
                        Alias = c.String(nullable: false, maxLength: 450),
                        Published = c.Boolean(nullable: false),
                        Ordering = c.Int(),
                        ThumbnailUrl = c.String(maxLength: 2000),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.Section", t => t.SectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.SectionID)
                .Index(t => t.CategoryID)
                .Index(t => t.Alias, unique: true)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.ArticleTranslation",
                c => new
                    {
                        ArticleTranslationID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        ArticleName = c.String(nullable: false, maxLength: 450),
                        ArticleBody = c.String(storeType: "ntext"),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleTranslationID)
                .ForeignKey("dbo.Article", t => t.ArticleID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .Index(t => t.ArticleID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(nullable: false, maxLength: 450),
                        TwoLetterISOLanguageName = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.LanguageID)
                .Index(t => t.TwoLetterISOLanguageName, unique: true);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        SectionID = c.Int(nullable: false),
                        Alias = c.String(nullable: false, maxLength: 450),
                        Published = c.Boolean(nullable: false),
                        Ordering = c.Int(),
                        ThumbnailUrl = c.String(maxLength: 2000),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.Section", t => t.SectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.SectionID)
                .Index(t => t.Alias, unique: true)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.CategoryTranslation",
                c => new
                    {
                        CategoryTranslationID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 450),
                        Description = c.String(storeType: "ntext"),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryTranslationID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .Index(t => t.CategoryID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        SectionID = c.Int(nullable: false, identity: true),
                        Alias = c.String(nullable: false, maxLength: 450),
                        Published = c.Boolean(nullable: false),
                        Ordering = c.Int(),
                        ThumbnailUrl = c.String(maxLength: 2000),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.Alias, unique: true)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.SectionTranslation",
                c => new
                    {
                        SectionTranslationID = c.Int(nullable: false, identity: true),
                        SectionID = c.Int(nullable: false),
                        SectionName = c.String(nullable: false, maxLength: 450),
                        Description = c.String(storeType: "ntext"),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionTranslationID)
                .ForeignKey("dbo.Language", t => t.LanguageID)
                .ForeignKey("dbo.Section", t => t.SectionID)
                .Index(t => t.SectionID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Article", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Article", "SectionID", "dbo.Section");
            DropForeignKey("dbo.Article", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Article", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Article", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Category", "SectionID", "dbo.Section");
            DropForeignKey("dbo.Section", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SectionTranslation", "SectionID", "dbo.Section");
            DropForeignKey("dbo.SectionTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.Section", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Section", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Category", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Category", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CategoryTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.CategoryTranslation", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.ArticleTranslation", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.ArticleTranslation", "ArticleID", "dbo.Article");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SectionTranslation", new[] { "LanguageID" });
            DropIndex("dbo.SectionTranslation", new[] { "SectionID" });
            DropIndex("dbo.Section", new[] { "DeletedUserID" });
            DropIndex("dbo.Section", new[] { "UpdatedUserID" });
            DropIndex("dbo.Section", new[] { "CreatedUserID" });
            DropIndex("dbo.Section", new[] { "Alias" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CategoryTranslation", new[] { "LanguageID" });
            DropIndex("dbo.CategoryTranslation", new[] { "CategoryID" });
            DropIndex("dbo.Category", new[] { "DeletedUserID" });
            DropIndex("dbo.Category", new[] { "UpdatedUserID" });
            DropIndex("dbo.Category", new[] { "CreatedUserID" });
            DropIndex("dbo.Category", new[] { "Alias" });
            DropIndex("dbo.Category", new[] { "SectionID" });
            DropIndex("dbo.Language", new[] { "TwoLetterISOLanguageName" });
            DropIndex("dbo.ArticleTranslation", new[] { "LanguageID" });
            DropIndex("dbo.ArticleTranslation", new[] { "ArticleID" });
            DropIndex("dbo.Article", new[] { "DeletedUserID" });
            DropIndex("dbo.Article", new[] { "UpdatedUserID" });
            DropIndex("dbo.Article", new[] { "CreatedUserID" });
            DropIndex("dbo.Article", new[] { "Alias" });
            DropIndex("dbo.Article", new[] { "CategoryID" });
            DropIndex("dbo.Article", new[] { "SectionID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SectionTranslation");
            DropTable("dbo.Section");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CategoryTranslation");
            DropTable("dbo.Category");
            DropTable("dbo.Language");
            DropTable("dbo.ArticleTranslation");
            DropTable("dbo.Article");
        }
    }
}
