
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE
{
    public partial class APP_ACCOUNTING_SERVICE
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ACCOUNTING_SERVICE").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ACCOUNTING_SERVICE",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
					Ordering = 4301,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION.Init();
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_MANAGER_SECTION.Init();
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_ACCOUTANT_SECTION.Init();
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_AUDITOR_SECTION.Init();
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE.Init();
            APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION_CONFIRM.Init();
        }
    }
}
