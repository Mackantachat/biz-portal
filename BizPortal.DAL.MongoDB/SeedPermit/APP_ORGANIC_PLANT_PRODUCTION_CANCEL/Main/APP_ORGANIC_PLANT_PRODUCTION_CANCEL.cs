
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_CANCEL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_CANCEL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_CANCEL,
                    },
					Ordering = 4941,
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION.Init();
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION.Init();
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_INFO_SECTION_1.Init();
        }
    }
}
