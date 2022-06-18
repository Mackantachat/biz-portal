namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAlias1 : DbMigration
    {
        public override void Up()
        {
            Sql(@"DROP VIEW [dbo].[SectionView]");
            Sql(@"DROP VIEW [dbo].[CategoryView]");
            Sql(@"DROP VIEW [dbo].[ArticleView]");

            Sql(@"CREATE VIEW [dbo].[SectionView]
AS
SELECT        dbo.Section.SectionID, dbo.Section.Published, dbo.Section.Ordering, dbo.Section.ThumbnailID, dbo.Section.CreatedUserID, dbo.Section.CreatedDate, 
                         dbo.Section.UpdatedUserID, dbo.Section.UpdatedDate, dbo.Section.SectionSysName, dbo.SectionTranslation.SectionName, dbo.SectionTranslation.Description, 
                         dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName
FROM            dbo.Section INNER JOIN
                         dbo.SectionTranslation ON dbo.Section.SectionID = dbo.SectionTranslation.SectionID INNER JOIN
                         dbo.Language ON dbo.SectionTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.Section.CreatedUserID = dbo.AspNetUsers.Id LEFT OUTER JOIN
                         dbo.AspNetUsers AS AspNetUsers_1 ON dbo.Section.UpdatedUserID = AspNetUsers_1.Id
WHERE        (dbo.Section.IsDeleted = 0)");

            Sql(@"CREATE VIEW [dbo].[CategoryView]
AS
SELECT        Category.CategoryID, Category.SectionID, Category.Published, Category.Ordering, Category.ThumbnailID, Category.CreatedUserID, Category.CreatedDate, Category.UpdatedUserID, 
                         Category.UpdatedDate, Category.CategorySysName, CategoryTranslation.CategoryName, CategoryTranslation.Description, Language.TwoLetterISOLanguageName, AspNetUsers.UserName AS CreatedUserName, 
                         AspNetUsers_1.UserName AS UpdatedUserName, Section.SectionSysName, SectionTranslation.SectionName
FROM            Category INNER JOIN
                         CategoryTranslation ON Category.CategoryID = CategoryTranslation.CategoryID INNER JOIN
                         Language ON CategoryTranslation.LanguageID = Language.LanguageID INNER JOIN
                         AspNetUsers ON Category.CreatedUserID = AspNetUsers.Id LEFT OUTER JOIN
                         Section ON Category.SectionID = Section.SectionID LEFT OUTER JOIN
                         SectionTranslation ON Section.SectionID = SectionTranslation.SectionID AND CategoryTranslation.LanguageID = SectionTranslation.LanguageID LEFT OUTER JOIN
                         AspNetUsers AS AspNetUsers_1 ON Category.UpdatedUserID = AspNetUsers_1.Id
WHERE        (Category.IsDeleted = 0)");

            Sql(@"CREATE VIEW [dbo].[ArticleView]
AS
SELECT        Article.ArticleID, Article.Unlisted, Article.SectionID, Article.CategoryID, Article.StartPublishing, Article.FinishPublishing, Article.Reads, Article.Published, Article.Ordering, 
                         Article.CreatedUserID, Article.CreatedDate, Article.UpdatedUserID, Article.UpdatedDate, Article.ArticleSysName, Article.ThumbnailID, ArticleTranslation.ArticleName, ArticleTranslation.ArticleBody, 
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
            Sql(@"DROP VIEW [dbo].[SectionView]");
            Sql(@"DROP VIEW [dbo].[CategoryView]");
            Sql(@"DROP VIEW [dbo].[ArticleView]");
        }
    }
}
