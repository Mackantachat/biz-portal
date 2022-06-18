namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleTagMapping",
                c => new
                    {
                        TagID = c.Int(nullable: false),
                        ArticleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagID, t.ArticleID })
                .ForeignKey("dbo.Article", t => t.ArticleID)
                .ForeignKey("dbo.Tag", t => t.TagID)
                .Index(t => t.TagID)
                .Index(t => t.ArticleID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 450),
                        Count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleTagMapping", "TagID", "dbo.Tag");
            DropForeignKey("dbo.Tag", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tag", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tag", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleTagMapping", "ArticleID", "dbo.Article");
            DropIndex("dbo.Tag", new[] { "DeletedUserID" });
            DropIndex("dbo.Tag", new[] { "UpdatedUserID" });
            DropIndex("dbo.Tag", new[] { "CreatedUserID" });
            DropIndex("dbo.ArticleTagMapping", new[] { "ArticleID" });
            DropIndex("dbo.ArticleTagMapping", new[] { "TagID" });
            DropTable("dbo.Tag");
            DropTable("dbo.ArticleTagMapping");
        }
    }
}
