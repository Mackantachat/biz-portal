
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ENERGY_PRODUCTION").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
					Ordering = 3801,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_INFO_SECTION.Init();
            APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_MACHINE_SECTION.Init();
            //APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION.Init();
            APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_ELECTRIC_SECTION.Init();
            APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_CONTACT_SECTION.Init();
            APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM.Init();
        }
    }
}
