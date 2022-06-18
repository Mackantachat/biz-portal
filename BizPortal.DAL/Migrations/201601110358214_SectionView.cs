namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SectionView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[SectionView]
AS
SELECT        dbo.Section.SectionID, dbo.Section.Alias, dbo.Section.Published, dbo.Section.Ordering, dbo.Section.ThumbnailID, dbo.Section.CreatedUserID, dbo.Section.CreatedDate, 
                         dbo.Section.UpdatedUserID, dbo.Section.UpdatedDate, dbo.Section.SectionSysName, dbo.SectionTranslation.SectionName, dbo.SectionTranslation.Description, 
                         dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.UserName AS CreatedUserName, AspNetUsers_1.UserName AS UpdatedUserName
FROM            dbo.Section INNER JOIN
                         dbo.SectionTranslation ON dbo.Section.SectionID = dbo.SectionTranslation.SectionID INNER JOIN
                         dbo.Language ON dbo.SectionTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.Section.CreatedUserID = dbo.AspNetUsers.Id LEFT OUTER JOIN
                         dbo.AspNetUsers AS AspNetUsers_1 ON dbo.Section.UpdatedUserID = AspNetUsers_1.Id
WHERE        (dbo.Section.IsDeleted = 0)");
        }
        
        public override void Down()
        {
            Sql(@"DROP VIEW [dbo].[SectionView]");
        }
    }
}
