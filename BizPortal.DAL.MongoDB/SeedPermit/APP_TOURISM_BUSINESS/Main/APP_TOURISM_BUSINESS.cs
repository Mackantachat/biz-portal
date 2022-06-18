
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TOURISM_BUSINESS
{
    public partial class APP_TOURISM_BUSINESS
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TOURISM_BUSINESS").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TOURISM_BUSINESS",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TOURISM_BUSINESS,
                    },
					Ordering = 4601,
					ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_TOURISM_BUSINESS_APP_TOURISM_BUSINESS_INFO_SECTION.Init();
        }
    }
}
