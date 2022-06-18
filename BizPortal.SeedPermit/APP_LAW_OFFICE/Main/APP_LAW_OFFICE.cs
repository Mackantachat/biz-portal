
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE
{
    public partial class APP_LAW_OFFICE
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_LAW_OFFICE").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_LAW_OFFICE",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE,
                    },
					Ordering = 3601,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_LAW_OFFICE_APP_LAW_OFFICE_INFO_SECTION.Init();
            APP_LAW_OFFICE_APP_LAW_OFFICE_LAWYER_SECTION.Init();
            APP_LAW_OFFICE_APP_LAW_OFFICE_INFO_SECTION_2.Init();
        }
    }
}
