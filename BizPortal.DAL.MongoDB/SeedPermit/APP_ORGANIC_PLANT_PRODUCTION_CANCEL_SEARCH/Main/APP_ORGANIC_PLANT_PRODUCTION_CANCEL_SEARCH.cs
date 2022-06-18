
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_CANCEL,
                    },
					Ordering = 0,
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION.Init();
        }
    }
}
