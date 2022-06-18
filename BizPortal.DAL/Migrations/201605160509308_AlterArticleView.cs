namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterArticleView : DbMigration
    {
        public override void Up()
        {
            Sql(@"
ALTER VIEW [dbo].[ArticleView]
AS
SELECT        Article.ArticleID, Article.Unlisted, Article.SectionID, Article.CategoryID, Article.StartPublishing, Article.FinishPublishing, Article.Reads, Article.Published, Article.Ordering, Article.CreatedUserID, Article.CreatedDate, 
                         Article.UpdatedUserID, Article.UpdatedDate, Article.ArticleSysName, Article.ThumbnailID, ArticleTranslation.ArticleName, ArticleTranslation.ArticleBody, ArticleTranslation.ArticleIntroText, 
                         Language.TwoLetterISOLanguageName, AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName, Section.SectionSysName, SectionTranslation.SectionName, 
                         Category.CategorySysName, CategoryTranslation.CategoryName, Article.PreviousArticleID, Article.NextArticleID, MappingJoin.TagName
FROM            Article INNER JOIN
                         ArticleTranslation ON Article.ArticleID = ArticleTranslation.ArticleID INNER JOIN
                         Language ON ArticleTranslation.LanguageID = Language.LanguageID INNER JOIN
                         AspNetUsers ON Article.CreatedUserID = AspNetUsers.Id LEFT OUTER JOIN
                         Category ON Article.CategoryID = Category.CategoryID LEFT OUTER JOIN
                         CategoryTranslation ON Language.LanguageID = CategoryTranslation.LanguageID AND Category.CategoryID = CategoryTranslation.CategoryID LEFT OUTER JOIN
                         AspNetUsers AS AspNetUsers_1 ON Article.UpdatedUserID = AspNetUsers_1.Id LEFT OUTER JOIN
                         Section ON Article.SectionID = Section.SectionID LEFT OUTER JOIN
                         SectionTranslation ON Language.LanguageID = SectionTranslation.LanguageID AND Section.SectionID = SectionTranslation.SectionID LEFT OUTER JOIN
                             (SELECT        Mapping.ArticleID, Mapping.LanguageID, LEFT(Mapping.TagName, Len(Mapping.TagName) - 1) AS TagName
                               FROM            (SELECT DISTINCT AM1.ArticleID, AM1.LanguageID,
                                                                                       (SELECT        T2.TagName + ',' AS [text()]
                                                                                         FROM            Tag AS T2 INNER JOIN
                                                                                                                   ArticleTagMapping AS AM2 ON T2.TagID = AM2.TagID
                                                                                         WHERE        AM1.ArticleID = AM2.ArticleID AND AM1.LanguageID = AM2.LanguageID FOR XML PATH('')) AS TagName
                                                         FROM            ArticleTagMapping AS AM1 INNER JOIN
                                                                                   Article AS A1 ON AM1.ArticleID = A1.ArticleID INNER JOIN
                                                                                   Tag AS T1 ON AM1.TagID = T1.TagID
                                                         WHERE        (T1.IsDeleted = 0) AND (A1.IsDeleted = 0)) AS Mapping) AS MappingJoin ON Article.ArticleID = MappingJoin.ArticleID AND ArticleTranslation.LanguageID = MappingJoin.LanguageID
WHERE        (Article.IsDeleted = 0)
");
        }
        
        public override void Down()
        {
            Sql(@"
ALTER VIEW [dbo].[ArticleView]
AS
SELECT        Article.ArticleID, Article.Unlisted, Article.SectionID, Article.CategoryID, Article.StartPublishing, Article.FinishPublishing, Article.Reads, Article.Published, Article.Ordering, Article.CreatedUserID, Article.CreatedDate, 
                         Article.UpdatedUserID, Article.UpdatedDate, Article.ArticleSysName, Article.ThumbnailID, ArticleTranslation.ArticleName, ArticleTranslation.ArticleBody, ArticleTranslation.ArticleIntroText, 
                         Language.TwoLetterISOLanguageName, AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName, Section.SectionSysName, SectionTranslation.SectionName, 
                         Category.CategorySysName, CategoryTranslation.CategoryName, Article.PreviousArticleID, Article.NextArticleID, MappingJoin.TagName
FROM            Article INNER JOIN
                         ArticleTranslation ON Article.ArticleID = ArticleTranslation.ArticleID INNER JOIN
                         Language ON ArticleTranslation.LanguageID = Language.LanguageID INNER JOIN
                         AspNetUsers ON Article.CreatedUserID = AspNetUsers.Id LEFT OUTER JOIN
                         Category ON Article.CategoryID = Category.CategoryID LEFT OUTER JOIN
                         CategoryTranslation ON Language.LanguageID = CategoryTranslation.LanguageID AND Category.CategoryID = CategoryTranslation.CategoryID LEFT OUTER JOIN
                         AspNetUsers AS AspNetUsers_1 ON Article.UpdatedUserID = AspNetUsers_1.Id LEFT OUTER JOIN
                         Section ON Article.SectionID = Section.SectionID LEFT OUTER JOIN
                         SectionTranslation ON Language.LanguageID = SectionTranslation.LanguageID AND Section.SectionID = SectionTranslation.SectionID LEFT OUTER JOIN
						 (SELECT Mapping.ArticleID, LEFT(Mapping.TagName,Len(Mapping.TagName)-1) AS TagName 
							FROM (SELECT        DISTINCT AM1.ArticleID, (
												SELECT T2.TagName + ',' AS [text()]
												FROM Tag AS T2 INNER JOIN
													ArticleTagMapping AS AM2 ON T2.TagID = AM2.TagID
												WHERE AM1.ArticleID = AM2.ArticleID
												FOR XML PATH ('')
											) AS TagName
							FROM            ArticleTagMapping AS AM1 INNER JOIN
													 Article AS A1 ON AM1.ArticleID = A1.ArticleID INNER JOIN
													 Tag AS T1 ON AM1.TagID = T1.TagID
							WHERE        (T1.IsDeleted = 0) AND (A1.IsDeleted = 0)) AS Mapping) AS MappingJoin ON Article.ArticleID = MappingJoin.ArticleID
WHERE        (Article.IsDeleted = 0)
");
        }
    }
}
