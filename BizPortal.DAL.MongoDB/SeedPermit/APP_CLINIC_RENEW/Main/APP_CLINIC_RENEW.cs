
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_RENEW,
                    },
					Ordering = 4221,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                });

                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_BUSINESS_RENEW_SECTION_GROUP",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});

                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});
            }

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_BUSINESS_RENEW_SECTION_GROUP").Count() == 0)
            {
                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_RENEW",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});

                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_BUSINESS_RENEW_SECTION_GROUP",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW,
                    },
                    Ordering = 4221,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                });

                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});
            }

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP").Count() == 0)
            {
                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_RENEW",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});

                //items.Add(new FormSectionGroup()
                //{
                //    SectionGroup = "APP_CLINIC_BUSINESS_RENEW_SECTION_GROUP",
                //    ShowOnSpecificApps = true,
                //    AppSystemNames = new string[] {
                //        AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW,
                //    },
                //    Ordering = 4221,
                //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                //});

                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                    },
                    Ordering = 4221,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_CLINIC_RENEW_APP_CLINIC_RENEW_INFO_SECTION.Init();
            APP_CLINIC_RENEW_APP_CLINIC_RENEW_RENEW_SECTION.Init();
            APP_CLINIC_RENEW_APP_CLINIC_RENEW_WOKING_STATUS_SECTION.Init();
            //APP_CLINIC_RENEW_APP_CLINIC_RENEW_DAY_TIME_SECTION.Init();
            APP_CLINIC_RENEW_APP_CLINIC_RENEW_CONFIRM_SECTION.Init();
        }
    }
}
