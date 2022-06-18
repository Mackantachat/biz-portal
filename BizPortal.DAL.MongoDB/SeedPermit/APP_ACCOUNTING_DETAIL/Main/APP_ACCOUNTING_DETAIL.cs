
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_DETAIL
{
    public partial class APP_ACCOUNTING_DETAIL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ACCOUNTING_DETAIL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ACCOUNTING_DETAIL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
					Ordering = 4302,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_DETAIL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_DETAIL_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_CALCULATE_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_PROVIDING_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_POLICY_SECTION.Init();
            APP_ACCOUNTING_DETAIL_APP_ACCOUNTING_DETAIL_DETAIL_SECTION_CONFIRM.Init();
        }
    }
}
