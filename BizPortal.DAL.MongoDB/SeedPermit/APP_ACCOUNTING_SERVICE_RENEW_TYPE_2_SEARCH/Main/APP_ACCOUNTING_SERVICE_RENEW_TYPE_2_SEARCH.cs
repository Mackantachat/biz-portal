
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    },
					Ordering = 0,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION.Init();
        }
    }
}
