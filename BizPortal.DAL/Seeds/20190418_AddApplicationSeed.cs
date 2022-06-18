using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace BizPortal.DAL.Seeds
{
    public class _20190418_AddApplicationSeed
    {
        public static void Seed(ApplicationDbContext context, ApplicationUser adminUser)
        {
            try
            {
                List<Application> appList = new List<Application>()
                {
                    //new Application()
                    //{
                    //    ApplicationSysName = "ขออนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร (ข.1)",
                    //    OrgCode = "13007000",
                    //    AppsHookClassName = "BizPortal.AppsHook.DPTBuildingG1AppHook",
                    //    SingleFormEnabled = true,
                    //    MultipleRequestSupport = true,
                    //    CreatedUserID = adminUser.Id,
                    //    CreatedDate = DateTime.Now,
                    //    ConsumerKey = new Guid("a581577e-9f38-4b59-8f73-69441b9dcefe"),
                    //    FileOwner = "e22c4b17-11b2-4b76-b1a5-7bd28d05ac0f",
                    //    OperatingCost = 20,
                    //    OperatingCostType = "StartAt",
                    //    OperatingDays = 45,
                    //    OperatingDayType = "StartAt",
                    //    CitizenOperatingCost = 20,
                    //    CitizenOperatingCostType = "StartAt",
                    //    CitizenOperatingDays = 45,
                    //    CitizenOperatingDayType = "StartAt",
                    //    Remark = "ค่าประเมินแปรผันตามขนาดพื้นที่",
                    //    CitizenRemark = "ค่าประเมินแปรผันตามขนาดพื้นที่",
                    //    LogoFileID = 2938
                    //},

                    new Application()
                    {
                        ApplicationSysName = "ขอใบรับรองการก่อสร้างอาคาร ดัดแปลงอาคาร หรือเคลื่อนย้ายอาคาร (ข.6)",
                        OrgCode = "13007000",
                        AppsHookClassName = "BizPortal.AppsHook.DPTBuildingR6AppHook",
                        SingleFormEnabled = true,
                        MultipleRequestSupport = true,
                        CreatedUserID = adminUser.Id,
                        CreatedDate = DateTime.Now,
                        ConsumerKey = new Guid("a581577e-9f38-4b59-8f73-69441b9dcefe"),
                        FileOwner = "e22c4b17-11b2-4b76-b1a5-7bd28d05ac0f",
                        OperatingCost = 10,
                        OperatingCostType = "StartAt",
                        OperatingDays = 30,
                        OperatingDayType = "StartAt",
                        CitizenOperatingCost = 10,
                        CitizenOperatingCostType = "StartAt",
                        CitizenOperatingDays = 30,
                        CitizenOperatingDayType = "StartAt",
                        Remark = "ค่าประเมินแปรผันตามขนาดพื้นที่",
                        CitizenRemark = "ค่าประเมินแปรผันตามขนาดพื้นที่",
                        LogoFileID = null
                    }
                };

                context.Applications.AddOrUpdate(o => o.ApplicationSysName, appList.ToArray());
                context.SaveChanges(false);

                List<ApplicationTranslation> tranList = new List<ApplicationTranslation>();

                foreach (var app in appList)
                {
                    var appDb = context.Applications.Where(o => o.ApplicationSysName == app.ApplicationSysName).SingleOrDefault();
                    if (appDb != null)
                    {
                        tranList.Add(new ApplicationTranslation()
                        {
                            ApplicationID = appDb.ApplicationID,
                            ApplicationName = app.ApplicationSysName,
                            ApplicationDetail = "<p><br></p>",
                            LanguageID = 1,
                            ApprovedMailMessage = "<p><br></p>",
                            RejectedMailMessage = "<p><br></p>",
                            SubmitMailMessage = "<p><br></p>",
                        });
                        tranList.Add(new ApplicationTranslation()
                        {
                            ApplicationID = appDb.ApplicationID,
                            ApplicationName = app.ApplicationSysName + " (en)",
                            ApplicationDetail = "<p><br></p>",
                            LanguageID = 2,
                            ApprovedMailMessage = "<p><br></p>",
                            RejectedMailMessage = "<p><br></p>",
                            SubmitMailMessage = "<p><br></p>",
                        });
                    }
                }
                context.ApplicationTranslations.AddOrUpdate(a => new { a.ApplicationID, a.LanguageID }, tranList.ToArray());
                context.SaveChanges(false);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var e in ex.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", e.Entry.Entity.GetType().Name, e.Entry.State);
                    foreach (var ev in e.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"", ev.PropertyName, e.Entry.CurrentValues.GetValue<object>(ev.PropertyName), ev.ErrorMessage);
                    }
                }
                throw;
            }

        }
    }
}
