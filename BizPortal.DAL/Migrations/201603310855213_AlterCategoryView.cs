namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCategoryView : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[CategoryView]
AS
SELECT        dbo.Category.CategoryID, dbo.Category.SectionID, dbo.Category.Published, dbo.Category.Ordering, dbo.Category.ThumbnailID, dbo.Category.CreatedUserID, dbo.Category.CreatedDate, 
                         dbo.Category.UpdatedUserID, dbo.Category.UpdatedDate, dbo.Category.CategorySysName, dbo.CategoryTranslation.CategoryName, dbo.CategoryTranslation.Description, 
                         dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName, dbo.Section.SectionSysName, 
                         dbo.SectionTranslation.SectionName, dbo.Category.Unlisted
FROM            dbo.Category INNER JOIN
                         dbo.CategoryTranslation ON dbo.Category.CategoryID = dbo.CategoryTranslation.CategoryID INNER JOIN
                         dbo.Language ON dbo.CategoryTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.Category.CreatedUserID = dbo.AspNetUsers.Id LEFT OUTER JOIN
                         dbo.Section ON dbo.Category.SectionID = dbo.Section.SectionID LEFT OUTER JOIN
                         dbo.SectionTranslation ON dbo.Section.SectionID = dbo.SectionTranslation.SectionID AND dbo.CategoryTranslation.LanguageID = dbo.SectionTranslation.LanguageID LEFT OUTER JOIN
                         dbo.AspNetUsers AS AspNetUsers_1 ON dbo.Category.UpdatedUserID = AspNetUsers_1.Id
WHERE        (dbo.Category.IsDeleted = 0)
");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[CategoryView]
AS
SELECT        dbo.Category.CategoryID, dbo.Category.SectionID, dbo.Category.Published, dbo.Category.Ordering, dbo.Category.ThumbnailID, dbo.Category.CreatedUserID, dbo.Category.CreatedDate, 
                         dbo.Category.UpdatedUserID, dbo.Category.UpdatedDate, dbo.Category.CategorySysName, dbo.CategoryTranslation.CategoryName, dbo.CategoryTranslation.Description, 
                         dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName, dbo.Section.SectionSysName, 
                         dbo.SectionTranslation.SectionName
FROM            dbo.Category INNER JOIN
                         dbo.CategoryTranslation ON dbo.Category.CategoryID = dbo.CategoryTranslation.CategoryID INNER JOIN
                         dbo.Language ON dbo.CategoryTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.Category.CreatedUserID = dbo.AspNetUsers.Id LEFT OUTER JOIN
                         dbo.Section ON dbo.Category.SectionID = dbo.Section.SectionID LEFT OUTER JOIN
                         dbo.SectionTranslation ON dbo.Section.SectionID = dbo.SectionTranslation.SectionID AND dbo.CategoryTranslation.LanguageID = dbo.SectionTranslation.LanguageID LEFT OUTER JOIN
                         dbo.AspNetUsers AS AspNetUsers_1 ON dbo.Category.UpdatedUserID = AspNetUsers_1.Id
WHERE        (dbo.Category.IsDeleted = 0)
");
        }
    }
}
