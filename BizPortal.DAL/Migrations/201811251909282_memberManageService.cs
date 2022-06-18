namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberManageService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberManageService",
                c => new
                    {
                        MemberManageServiceID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberManageServiceID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.ApplicationID)
                .Index(t => t.UserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberManageService", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberManageService", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberManageService", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberManageService", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberManageService", "ApplicationID", "dbo.Application");
            DropIndex("dbo.MemberManageService", new[] { "DeletedUserID" });
            DropIndex("dbo.MemberManageService", new[] { "UpdatedUserID" });
            DropIndex("dbo.MemberManageService", new[] { "CreatedUserID" });
            DropIndex("dbo.MemberManageService", new[] { "UserID" });
            DropIndex("dbo.MemberManageService", new[] { "ApplicationID" });
            DropTable("dbo.MemberManageService");
        }
    }
}
