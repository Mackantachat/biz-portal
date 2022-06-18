
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TOURISM_BUSINESS_RENEW
{
    public partial class APP_TOURISM_BUSINESS_RENEW
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TOURISM_BUSINESS_RENEW").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TOURISM_BUSINESS_RENEW",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TOURISM_BUSINESS_RENEW,
                    },
					Ordering = 4602,
					ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS_RENEW"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
        }
    }
}
