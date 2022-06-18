namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterJuristicRequestViewsAddConsumerKey : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[JuristicRequestView]
AS
SELECT DISTINCT 
                         dbo.JuristicApplicationStatusRequest.JuristicApplicationStatusRequestID, dbo.JuristicApplicationStatusRequest.ApplicationID, dbo.JuristicApplicationStatusRequest.JuristicID, 
                         dbo.JuristicApplicationStatusRequest.Remark, dbo.JuristicApplicationStatusRequest.ApplicationStatusID, dbo.JuristicApplicationStatusRequest.ApplicationStatusOther, 
                         dbo.JuristicApplicationStatusRequest.IsDeleted, dbo.JuristicApplicationStatusRequest.CreatedUserID, dbo.JuristicApplicationStatusRequest.CreatedDate, dbo.JuristicApplicationStatusRequest.UpdatedUserID, 
                         dbo.JuristicApplicationStatusRequest.UpdatedDate, dbo.JuristicApplicationStatusRequest.DeletedUserID, dbo.JuristicApplicationStatusRequest.DeletedDate, dbo.ApplicationStatus.ApplicationSysStatusName, 
                         dbo.ApplicationStatusTranslation.ApplicationStatusName, dbo.Application.ApplicationSysName, dbo.Application.OrgCode, dbo.ApplicationTranslation.ApplicationName, dbo.ApplicationTranslation.ApplicationDetail, 
                         dbo.OrganizationTranslation.OrgName, dbo.OrganizationTranslation.Abbreviation, dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.Email, dbo.AspNetUsers.UserName, dbo.AspNetUsers.FullName, 
                         dbo.AspNetUsers.UserType, dbo.AspNetUsers.CitizenID, dbo.AspNetUsers.PassportID, dbo.Application.ConsumerKey
FROM            dbo.ApplicationStatus INNER JOIN
                         dbo.ApplicationStatusTranslation ON dbo.ApplicationStatus.ApplicationStatusID = dbo.ApplicationStatusTranslation.ApplicationStatusID INNER JOIN
                         dbo.JuristicApplicationStatusRequest ON dbo.ApplicationStatus.ApplicationStatusID = dbo.JuristicApplicationStatusRequest.ApplicationStatusID INNER JOIN
                         dbo.Application ON dbo.JuristicApplicationStatusRequest.ApplicationID = dbo.Application.ApplicationID INNER JOIN
                         dbo.ApplicationTranslation ON dbo.Application.ApplicationID = dbo.ApplicationTranslation.ApplicationID INNER JOIN
                         dbo.Organization ON dbo.Application.OrgCode = dbo.Organization.OrgCode INNER JOIN
                         dbo.OrganizationTranslation ON dbo.Organization.OrgCode = dbo.OrganizationTranslation.OrgCode INNER JOIN
                         dbo.Language ON dbo.ApplicationStatusTranslation.LanguageID = dbo.Language.LanguageID AND dbo.ApplicationTranslation.LanguageID = dbo.Language.LanguageID AND 
                         dbo.OrganizationTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.JuristicApplicationStatusRequest.CreatedUserID = dbo.AspNetUsers.Id

");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[JuristicRequestView]
AS
SELECT DISTINCT 
                         dbo.JuristicApplicationStatusRequest.JuristicApplicationStatusRequestID, dbo.JuristicApplicationStatusRequest.ApplicationID, dbo.JuristicApplicationStatusRequest.JuristicID, 
                         dbo.JuristicApplicationStatusRequest.Remark, dbo.JuristicApplicationStatusRequest.ApplicationStatusID, dbo.JuristicApplicationStatusRequest.ApplicationStatusOther, 
                         dbo.JuristicApplicationStatusRequest.IsDeleted, dbo.JuristicApplicationStatusRequest.CreatedUserID, dbo.JuristicApplicationStatusRequest.CreatedDate, dbo.JuristicApplicationStatusRequest.UpdatedUserID, 
                         dbo.JuristicApplicationStatusRequest.UpdatedDate, dbo.JuristicApplicationStatusRequest.DeletedUserID, dbo.JuristicApplicationStatusRequest.DeletedDate, dbo.ApplicationStatus.ApplicationSysStatusName, 
                         dbo.ApplicationStatusTranslation.ApplicationStatusName, dbo.Application.ApplicationSysName, dbo.Application.OrgCode, dbo.ApplicationTranslation.ApplicationName, dbo.ApplicationTranslation.ApplicationDetail, 
                         dbo.OrganizationTranslation.OrgName, dbo.OrganizationTranslation.Abbreviation, dbo.Language.TwoLetterISOLanguageName, dbo.AspNetUsers.Email, dbo.AspNetUsers.UserName, dbo.AspNetUsers.FullName, 
                         dbo.AspNetUsers.UserType, dbo.AspNetUsers.CitizenID, dbo.AspNetUsers.PassportID
FROM            dbo.ApplicationStatus INNER JOIN
                         dbo.ApplicationStatusTranslation ON dbo.ApplicationStatus.ApplicationStatusID = dbo.ApplicationStatusTranslation.ApplicationStatusID INNER JOIN
                         dbo.JuristicApplicationStatusRequest ON dbo.ApplicationStatus.ApplicationStatusID = dbo.JuristicApplicationStatusRequest.ApplicationStatusID INNER JOIN
                         dbo.Application ON dbo.JuristicApplicationStatusRequest.ApplicationID = dbo.Application.ApplicationID INNER JOIN
                         dbo.ApplicationTranslation ON dbo.Application.ApplicationID = dbo.ApplicationTranslation.ApplicationID INNER JOIN
                         dbo.Organization ON dbo.Application.OrgCode = dbo.Organization.OrgCode INNER JOIN
                         dbo.OrganizationTranslation ON dbo.Organization.OrgCode = dbo.OrganizationTranslation.OrgCode INNER JOIN
                         dbo.Language ON dbo.ApplicationStatusTranslation.LanguageID = dbo.Language.LanguageID AND dbo.ApplicationTranslation.LanguageID = dbo.Language.LanguageID AND 
                         dbo.OrganizationTranslation.LanguageID = dbo.Language.LanguageID INNER JOIN
                         dbo.AspNetUsers ON dbo.JuristicApplicationStatusRequest.CreatedUserID = dbo.AspNetUsers.Id

");
        }
    }
}
