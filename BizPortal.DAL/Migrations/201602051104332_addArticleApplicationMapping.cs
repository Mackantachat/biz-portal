namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addArticleApplicationMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleApplicationMapping",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false),
                        ArticleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationID, t.ArticleID })
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.Article", t => t.ArticleID)
                .Index(t => t.ApplicationID)
                .Index(t => t.ArticleID);
            
            AddColumn("dbo.Application", "UrlCallBack", c => c.String());
            AddColumn("dbo.Application", "ParamCallBack", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleApplicationMapping", "ArticleID", "dbo.Article");
            DropForeignKey("dbo.ArticleApplicationMapping", "ApplicationID", "dbo.Application");
            DropIndex("dbo.ArticleApplicationMapping", new[] { "ArticleID" });
            DropIndex("dbo.ArticleApplicationMapping", new[] { "ApplicationID" });
            DropColumn("dbo.Application", "ParamCallBack");
            DropColumn("dbo.Application", "UrlCallBack");
            DropTable("dbo.ArticleApplicationMapping");
        }
    }
}
