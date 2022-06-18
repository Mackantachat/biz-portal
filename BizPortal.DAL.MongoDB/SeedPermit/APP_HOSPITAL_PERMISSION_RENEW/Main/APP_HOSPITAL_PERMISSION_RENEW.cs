
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PERMISSION_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    },
                    Ordering = 4701,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW"
                });
            }

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_BUSINESS_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_RENEW
                    },
                    Ordering = 4701,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW"
                });
            }

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_OPERATION_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_OPERATION_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW
                    },
                    Ordering = 4701,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION.Init();
            APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION.Init();
            APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION.Init();
            APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION.Init();
            APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION.Init();
        }
    }
}
