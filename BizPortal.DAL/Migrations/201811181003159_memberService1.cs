namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberService1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberServiceArea",
                c => new
                    {
                        MemberServiceAreaID = c.Int(nullable: false, identity: true),
                        MemberServiceID = c.Int(nullable: false),
                        ProvinceID = c.Int(nullable: false),
                        DistrictID = c.Int(nullable: false),
                        SectionID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberServiceAreaID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.MemberService", t => t.MemberServiceID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.MemberServiceID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.MemberService",
                c => new
                    {
                        MemberServiceID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberServiceID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.ApplicationID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberServiceArea", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberService", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberServiceArea", "MemberServiceID", "dbo.MemberService");
            DropForeignKey("dbo.MemberService", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberService", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberService", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.MemberServiceArea", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberServiceArea", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.MemberService", new[] { "DeletedUserID" });
            DropIndex("dbo.MemberService", new[] { "UpdatedUserID" });
            DropIndex("dbo.MemberService", new[] { "CreatedUserID" });
            DropIndex("dbo.MemberService", new[] { "ApplicationID" });
            DropIndex("dbo.MemberServiceArea", new[] { "DeletedUserID" });
            DropIndex("dbo.MemberServiceArea", new[] { "UpdatedUserID" });
            DropIndex("dbo.MemberServiceArea", new[] { "CreatedUserID" });
            DropIndex("dbo.MemberServiceArea", new[] { "MemberServiceID" });
            DropTable("dbo.MemberService");
            DropTable("dbo.MemberServiceArea");
        }
    }
}
