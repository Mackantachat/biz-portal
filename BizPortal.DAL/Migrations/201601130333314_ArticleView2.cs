namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ArticleView2 : DbMigration
    {

        public override void Up()
        {
            Sql(@"DROP VIEW [dbo].[ArticleView]");
            Sql(@"CREATE VIEW [dbo].[ArticleView]
AS
SELECT        Article.ArticleID, Article.Unlisted, Article.SectionID, Article.CategoryID, Article.StartPublishing, Article.FinishPublishing, Article.Reads, Article.Published, Article.Ordering, 
                         Article.CreatedUserID, Article.CreatedDate, Article.UpdatedUserID, Article.UpdatedDate, Article.ArticleSysName, Article.ThumbnailID, ArticleTranslation.ArticleName, ArticleTranslation.ArticleBody, ArticleTranslation.ArticleIntroText, 
                         Language.TwoLetterISOLanguageName, AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName, Section.SectionSysName, 
                         SectionTranslation.SectionName, Category.CategorySysName, CategoryTranslation.CategoryName
FROM            Article INNER JOIN
                         ArticleTranslation ON Article.ArticleID = ArticleTranslation.ArticleID INNER JOIN
                         Language ON ArticleTranslation.LanguageID = Language.LanguageID INNER JOIN
                         AspNetUsers ON Article.CreatedUserID = AspNetUsers.Id LEFT OUTER JOIN
                         Category ON Article.CategoryID = Category.CategoryID LEFT OUTER JOIN
                         CategoryTranslation ON Language.LanguageID = CategoryTranslation.LanguageID AND Category.CategoryID = CategoryTranslation.CategoryID LEFT OUTER JOIN
                         AspNetUsers AS AspNetUsers_1 ON Article.UpdatedUserID = AspNetUsers_1.Id LEFT OUTER JOIN
                         Section ON Article.SectionID = Section.SectionID LEFT OUTER JOIN
                         SectionTranslation ON Language.LanguageID = SectionTranslation.LanguageID AND Section.SectionID = SectionTranslation.SectionID
WHERE        (Article.IsDeleted = 0)");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW [dbo].[ArticleView]");
        }
    }
}
