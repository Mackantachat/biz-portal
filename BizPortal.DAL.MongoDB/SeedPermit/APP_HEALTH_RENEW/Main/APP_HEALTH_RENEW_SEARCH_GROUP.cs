
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HEALTH_RENEW_SEARCH_GROUP
{
    public partial class APP_HEALTH_RENEW_SEARCH_GROUP
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HEALTH_RENEW_SEARCH_SECTION_GROUP").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HEALTH_RENEW_SEARCH_SECTION_GROUP",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                       // AppSystemNameTextConst.APP_HEALTH_RENEW,
                    },
                    Ordering = 0,
                    ResourceName = "PermitResource.RESOURCE_APP_HEALTH_RENEW"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HEALTH_RENEW_SEARCH_SECTION.Init();
        }
    }
}
