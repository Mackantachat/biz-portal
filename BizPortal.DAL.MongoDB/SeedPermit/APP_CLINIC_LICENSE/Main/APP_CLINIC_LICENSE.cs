
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_LICENSE").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_LICENSE",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 4203,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_INFO_SECTION.Init();
            APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION.Init();
            APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION_EN.Init();
            APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_INFO_SECTION_2.Init();
        }
    }
}
