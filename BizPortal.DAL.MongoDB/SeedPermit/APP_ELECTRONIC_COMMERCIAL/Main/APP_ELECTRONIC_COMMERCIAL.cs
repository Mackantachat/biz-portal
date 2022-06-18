
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ELECTRONIC_COMMERCIAL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 3701,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INVESTMENT_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INFO_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_AGENT_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_OTHER_NON_ELECTRONIC_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION.Init();
            APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION.Init();
        }
    }
}
