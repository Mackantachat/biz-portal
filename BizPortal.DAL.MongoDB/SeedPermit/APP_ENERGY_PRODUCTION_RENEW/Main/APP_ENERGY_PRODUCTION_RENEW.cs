
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ENERGY_PRODUCTION_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ENERGY_PRODUCTION_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                    },
					Ordering = 3821,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION.Init();
            APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_MACHINE_SECTION.Init();
            //APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION.Init();
            APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION.Init();
            APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_CONFIRM.Init();
        }
    }
}
