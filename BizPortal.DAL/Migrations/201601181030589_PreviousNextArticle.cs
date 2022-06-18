namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreviousNextArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "PreviousArticleID", c => c.Int());
            AddColumn("dbo.Article", "NextArticleID", c => c.Int());
            CreateIndex("dbo.Article", "PreviousArticleID");
            CreateIndex("dbo.Article", "NextArticleID");
            AddForeignKey("dbo.Article", "NextArticleID", "dbo.Article", "ArticleID");
            AddForeignKey("dbo.Article", "PreviousArticleID", "dbo.Article", "ArticleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "PreviousArticleID", "dbo.Article");
            DropForeignKey("dbo.Article", "NextArticleID", "dbo.Article");
            DropIndex("dbo.Article", new[] { "NextArticleID" });
            DropIndex("dbo.Article", new[] { "PreviousArticleID" });
            DropColumn("dbo.Article", "NextArticleID");
            DropColumn("dbo.Article", "PreviousArticleID");
        }
    }
}
