
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_SECURITIES_BUSINESS").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_SECURITIES_BUSINESS",
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS,
					Ordering = 4,
					ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_INFO_SECTION.Init();
            APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY.Init();
            APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER.Init();
            APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER.Init();
            APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_JURISTIC_MAJOR_SHAREHOLDER.Init();
        }
    }
}
