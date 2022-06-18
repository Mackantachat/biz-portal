using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.Seeds
{
    class _20190514_Phase3PermitSeed
    {
        private static void createApplication(ApplicationDbContext context, Application application, string translateName, ApplicationUser user)
        {
            application.SingleFormEnabled = true;
            application.CreatedUserID = user.Id;
            application.CreatedDate = DateTime.Now;
            application.UpdatedUserID = user.Id;
            application.UpdatedDate = DateTime.Now;

            context.Applications.Add(application);
            context.SaveChanges(false);

            Application newApp = context.Applications.Where(x => x.ApplicationSysName == application.ApplicationSysName).FirstOrDefault();
            if (newApp != null)
            {
                int appID = newApp.ApplicationID;
                var AppTranslate = new List<ApplicationTranslation>()
                {
                   new ApplicationTranslation
                   {
                        ApplicationID = appID,
                        ApplicationName = translateName,
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 1,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                    new ApplicationTranslation
                    {
                        ApplicationID = appID,
                        ApplicationName = translateName + " (en)",
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 2,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                };
                context.ApplicationTranslations.AddRange(AppTranslate);
                context.SaveChanges(false);
            }
        }

        private class ApplicationSysName
        {
            public const string APP_35 = "APP_HOSPITAL";
        }

        private class CostType
        {
            public const string Fixed = "Fixed";
            public const string Range = "Range";
            public const string StartAt = "StartAt";
        }

        const string APP_HOOK_NEW_PERMIT = "BizPortal.AppsHook.NEW_PERMIT";
        const string REMARK_BKK = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
        const string REMARK_BKK_AND_2_PROVINCE = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานคร ฉะเชิงเทราและราชบุรี เท่านั้น</span>";

        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser creater)
        {
            #region APP 35 HOSPITAL

            Application APP_35 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_35).FirstOrDefault();

            if (APP_35 == null)
            {
                APP_35 = new Application();
                APP_35.ApplicationSysName = ApplicationSysName.APP_35;
                APP_35.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_35.OrgCode = "19007000";
                APP_35.LogoFileID = 328;
                APP_35.HandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_35.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_35.OperatingDays = 81;
                APP_35.OperatingDayType = CostType.Fixed;
                APP_35.OperatingDays2 = null;
                APP_35.OperatingCost = 0;
                APP_35.OperatingCostType = CostType.Fixed;
                APP_35.OperatingCost2 = null;
                APP_35.CitizenOperatingDays = 81;
                APP_35.CitizenOperatingDayType = CostType.Fixed;
                APP_35.CitizenOperatingDays2 = null;
                APP_35.CitizenOperatingCost = 0;
                APP_35.CitizenOperatingCostType = CostType.Fixed;
                APP_35.CitizenOperatingCost2 = null;
                APP_35.ShowRemark = true;
                APP_35.Remark = REMARK_BKK;
                APP_35.CitizenShowRemark = true;
                APP_35.CitizenRemark = REMARK_BKK;

                string TranslateName = "ขอใบอนุมัติแผนงานการจัดตั้งสถานพยาบาลประเภทที่รับผู้ป่วยไว้ค้างคืน";

                createApplication(context, APP_35, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */

            #endregion
        }
    }
}
