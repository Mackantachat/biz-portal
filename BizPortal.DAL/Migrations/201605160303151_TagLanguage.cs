namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleTagMapping", "LanguageID", c => c.Int(nullable: true));
            Sql("UPDATE dbo.ArticleTagMapping SET LanguageID = (SELECT LanguageID FROM [Language] WHERE (TwoLetterISOLanguageName = 'th'))");

            AlterColumn("dbo.ArticleTagMapping", "LanguageID", c => c.Int(nullable: false));
            CreateIndex("dbo.ArticleTagMapping", "LanguageID");
            AddForeignKey("dbo.ArticleTagMapping", "LanguageID", "dbo.Language", "LanguageID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleTagMapping", "LanguageID", "dbo.Language");
            DropIndex("dbo.ArticleTagMapping", new[] { "LanguageID" });
            DropColumn("dbo.ArticleTagMapping", "LanguageID");
        }
    }
}
