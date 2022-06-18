namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJuristicRequestLogView : DbMigration
    {
        public override void Up()
        {


            Sql(@"CREATE VIEW [dbo].[JuristicRequestLogView]
AS
SELECT DISTINCT 
                         dbo.JuristicApplicationStatusRequestLog.JuristicApplicationStatusRequestLogID, dbo.JuristicApplicationStatusRequestLog.JuristicApplicationStatusRequestID, dbo.JuristicApplicationStatusRequestLog.Remark, 
                         dbo.JuristicApplicationStatusRequestLog.ApplicationStatusID, dbo.JuristicApplicationStatusRequestLog.ApplicationStatusOther, dbo.JuristicApplicationStatusRequestLog.IsDeleted, 
                         dbo.JuristicApplicationStatusRequestLog.CreatedUserID, dbo.JuristicApplicationStatusRequestLog.CreatedDate, dbo.JuristicApplicationStatusRequestLog.UpdatedUserID, 
                         dbo.JuristicApplicationStatusRequestLog.UpdatedDate, dbo.JuristicApplicationStatusRequestLog.DeletedUserID, dbo.JuristicApplicationStatusRequestLog.DeletedDate, 
                         dbo.ApplicationStatusTranslation.ApplicationStatusName, dbo.Language.TwoLetterISOLanguageName
FROM            dbo.JuristicApplicationStatusRequestLog INNER JOIN
                         dbo.ApplicationStatus ON dbo.JuristicApplicationStatusRequestLog.ApplicationStatusID = dbo.ApplicationStatus.ApplicationStatusID INNER JOIN
                         dbo.ApplicationStatusTranslation ON dbo.ApplicationStatus.ApplicationStatusID = dbo.ApplicationStatusTranslation.ApplicationStatusID INNER JOIN
                         dbo.Language ON dbo.ApplicationStatusTranslation.LanguageID = dbo.Language.LanguageID

");
        }
        
        public override void Down()
        {
            Sql(@"DROP VIEW [dbo].[JuristicRequestLogView]");
        }
    }
}
