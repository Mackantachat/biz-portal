namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[CategoryView]
AS
SELECT        Category.CategoryID, Category.SectionID, Category.Alias, Category.Published, Category.Ordering, Category.ThumbnailID, Category.CreatedUserID, Category.CreatedDate, Category.UpdatedUserID, 
                         Category.UpdatedDate, Category.CategorySysName, CategoryTranslation.CategoryName, CategoryTranslation.Description, Language.TwoLetterISOLanguageName, AspNetUsers.UserName AS CreatedUserName, 
                         AspNetUsers_1.UserName AS UpdatedUserName, Section.Alias AS SectionAlias, Section.SectionSysName, SectionTranslation.SectionName
FROM            Category INNER JOIN
                         CategoryTranslation ON Category.CategoryID = CategoryTranslation.CategoryID INNER JOIN
                         Language ON CategoryTranslation.LanguageID = Language.LanguageID INNER JOIN
                         AspNetUsers ON Category.CreatedUserID = AspNetUsers.Id LEFT OUTER JOIN
                         Section ON Category.SectionID = Section.SectionID LEFT OUTER JOIN
                         SectionTranslation ON Section.SectionID = SectionTranslation.SectionID AND CategoryTranslation.LanguageID = SectionTranslation.LanguageID LEFT OUTER JOIN
                         AspNetUsers AS AspNetUsers_1 ON Category.UpdatedUserID = AspNetUsers_1.Id
WHERE        (Category.IsDeleted = 0)");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW [dbo].[CategoryView]");
        }
    }
}
